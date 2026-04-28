#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public partial class SKillAnalyzerGUI
{
    private enum SkillEventKind
    {
        AttackStart,
        AttackClear,
        MagicAttack,
        Cancel,
        Other
    }

    private const float PreviewHeight = 280f;
    private const float TimelineHeight = 78f;
    private const float EventListHeight = 230f;
    private const float AutoAnalysisMinActiveTime = 0.06f;
    private const float AutoAnalysisMaxActiveTime = 0.4f;
    private const int AutoAnalysisMaxWindowsPerLimb = 3;

    private static readonly string[] QuickMagicEventNames =
    {
        "MagicForward",
        "MagicForwardOnBody",
        "MagicToEnemy",
        "ReleasePreparedMagic",
        "ReleasePreparedMagicToAir",
        "Bullet_shoot_from_body_part",
        "Bullet_shoot_from_body_part_TD",
        "BlastAttack"
    };

    private static readonly string[] QuickMarkerLabels =
    {
        "右手",
        "左手",
        "右脚",
        "左脚"
    };

    private static readonly string[] QuickMarkerFunctionNames =
    {
        "SetRightHandMarkerManager",
        "SetLeftHandMarkerManager",
        "SetRightFootMarkerManager",
        "SetLeftFootMarkerManager"
    };

    private sealed class AutoAnalysisTrack
    {
        public string Label;
        public string FunctionName;
        public Transform Bone;
        public Transform Reference;
        public Vector3[] Positions;
        public float[] Reach;
        public float[] Speed;
    }

    private struct AutoAnalysisWindow
    {
        public string FunctionName;
        public int StartFrame;
        public int EndFrame;
        public float Score;
    }

    private AnimationClip _editingClip;
    private readonly List<AnimationEvent> _workingEvents = new List<AnimationEvent>();
    private bool _hasUnsavedEventChanges;

    private string _previewType = "human";
    private readonly List<string> _previewTypes = new List<string>();
    private readonly List<GameObject> _previewPrefabs = new List<GameObject>();
    private readonly List<string> _previewPrefabNames = new List<string>();
    private int _selectedPreviewPrefabIndex;

    private PreviewRenderUtility _previewUtility;
    private GameObject _previewInstance;
    private GameObject _currentPreviewPrefab;
    private Bounds _previewBounds;

    private float _previewTime;
    private bool _previewPlaying;
    private double _lastPreviewEditorTime;
    private float _previewYaw = 145f;
    private float _previewPitch = 12f;
    private float _previewDistance = 3f;
    private Vector3 _previewPivot = new Vector3(0f, 1f, 0f);

    private int _selectedEventIndex = -1;
    private bool _draggingTimelineMarker;
    private bool _scrubbingTimeline;
    private Vector2 _eventListScroll;

    private int _quickMagicEventIndex;
    private int _quickMarkerEventIndex;
    private bool _autoAddAttackClearAfterStart = true;
    private int _autoAddAttackClearOffsetFrames = 1;
    private bool _autoAnalysisReplaceExistingAttackEvents = true;
    private string _autoAnalysisStatus = string.Empty;
    private MessageType _autoAnalysisStatusType = MessageType.None;
    private void OnEnable()
    {
        minSize = new Vector2(840f, 760f);
        RefreshPreviewTypes();
        RefreshPreviewPrefabs();
        _lastPreviewEditorTime = EditorApplication.timeSinceStartup;
        EditorApplication.update += ClipEditorUpdate;
    }

    private void DisposeClipEditorState()
    {
        EditorApplication.update -= ClipEditorUpdate;
        DisposePreview();
        if (AnimationMode.InAnimationMode())
        {
            AnimationMode.StopAnimationMode();
        }
    }

    private void ClipEditorUpdate()
    {
        var now = EditorApplication.timeSinceStartup;
        if (_previewPlaying && _editingClip != null && _editingClip.length > 0f)
        {
            _previewTime += (float)(now - _lastPreviewEditorTime);
            while (_previewTime > _editingClip.length)
            {
                _previewTime -= _editingClip.length;
            }
            Repaint();
        }

        _lastPreviewEditorTime = now;
    }

    private void DrawClipEventEditorSection()
    {
        EditorGUILayout.LabelField("拖拽式攻击帧编辑器", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("把任意 AnimationClip 拖进这个窗口任意位置，即可基于现有角色预览动作、拖动时间轴事件点、编辑攻击帧并保存回动画；也可以直接把当前 Clip 同步到“技能制作与添加”页继续建技能。", MessageType.Info);

        DrawClipSelectionRow();
        DrawPreviewSourceRow();
        DrawPreviewToolbar();
        DrawPreviewSurface();

        if (_editingClip == null)
        {
            EditorGUILayout.HelpBox("请拖入一个 AnimationClip，或通过对象槽指定。", MessageType.None);
            return;
        }

        DrawTimeline();
        DrawTimelineLegend();
        DrawEventEditingPanel();
    }

    private void DrawClipSelectionRow()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUI.BeginChangeCheck();
            var clip = EditorGUILayout.ObjectField("Animation Clip", _editingClip, typeof(AnimationClip), false) as AnimationClip;
            if (EditorGUI.EndChangeCheck())
            {
                SetEditingClip(clip);
            }

            EditorGUI.BeginDisabledGroup(_editingClip == null);
            if (GUILayout.Button("同步到技能创建", GUILayout.Width(120f)))
            {
                SyncCurrentClipToSkillCreation();
                ShowSkillCreationTab();
            }
            EditorGUI.EndDisabledGroup();
        }
    }

    private void DrawPreviewSourceRow()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("预览角色", GUILayout.Width(56f));

            if (_previewTypes.Count == 0)
            {
                EditorGUILayout.HelpBox("未找到 `Assets/ExternalAssets/Prefabs/Units` 下的角色 prefab。", MessageType.Warning);
                return;
            }

            var typeIndex = Mathf.Max(0, _previewTypes.IndexOf(_previewType));
            EditorGUI.BeginChangeCheck();
            typeIndex = EditorGUILayout.Popup(typeIndex, _previewTypes.ToArray(), GUILayout.Width(140f));
            if (EditorGUI.EndChangeCheck())
            {
                _previewType = _previewTypes[typeIndex];
                RefreshPreviewPrefabs();
                RebuildPreviewInstance();
            }

            if (_previewPrefabNames.Count == 0)
            {
                EditorGUILayout.HelpBox($"类型 `{_previewType}` 下未找到 prefab。", MessageType.Warning);
                return;
            }

            EditorGUI.BeginChangeCheck();
            _selectedPreviewPrefabIndex = EditorGUILayout.Popup(_selectedPreviewPrefabIndex, _previewPrefabNames.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                RebuildPreviewInstance();
            }
        }
    }

    private void DrawPreviewToolbar()
    {
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            if (GUILayout.Button(_previewPlaying ? "暂停" : "播放", EditorStyles.toolbarButton, GUILayout.Width(56f)))
            {
                _previewPlaying = !_previewPlaying;
                _lastPreviewEditorTime = EditorApplication.timeSinceStartup;
            }

            EditorGUI.BeginDisabledGroup(_editingClip == null);
            if (GUILayout.Button("首帧", EditorStyles.toolbarButton, GUILayout.Width(48f)))
            {
                _previewTime = 0f;
                _previewPlaying = false;
            }

            if (GUILayout.Button("上一帧", EditorStyles.toolbarButton, GUILayout.Width(52f)))
            {
                StepPreviewFrame(-1);
            }

            if (GUILayout.Button("下一帧", EditorStyles.toolbarButton, GUILayout.Width(52f)))
            {
                StepPreviewFrame(1);
            }

            if (GUILayout.Button("重新读取事件", EditorStyles.toolbarButton, GUILayout.Width(88f)))
            {
                ReloadWorkingEvents();
            }

            if (GUILayout.Button("保存事件", EditorStyles.toolbarButton, GUILayout.Width(68f)))
            {
                SaveWorkingEvents();
            }

            if (GUILayout.Button("重置相机", EditorStyles.toolbarButton, GUILayout.Width(68f)))
            {
                FocusPreviewCamera();
            }
            EditorGUI.EndDisabledGroup();

            GUILayout.FlexibleSpace();

            if (_editingClip != null)
            {
                EditorGUILayout.LabelField(
                    $"Time {_previewTime:F3}s / Frame {TimeToFrame(_previewTime)} / Len {_editingClip.length:F3}s / FPS {_editingClip.frameRate:F1}",
                    EditorStyles.miniLabel,
                    GUILayout.Width(280f));
            }
        }
    }

    private void DrawPreviewSurface()
    {
        var rect = GUILayoutUtility.GetRect(10f, PreviewHeight, GUILayout.ExpandWidth(true));
        GUI.Box(rect, GUIContent.none);

        if (_editingClip == null)
        {
            EditorGUI.DropShadowLabel(rect, "Drop AnimationClip Here");
            return;
        }

        EnsurePreviewReady();
        if (_previewInstance == null || _previewUtility == null)
        {
            EditorGUI.DropShadowLabel(rect, "无法创建角色预览");
            return;
        }

        HandlePreviewInput(rect);
        SamplePreviewClip();
        RenderPreview(rect);
    }

    private void DrawTimeline()
    {
        var rect = GUILayoutUtility.GetRect(10f, TimelineHeight, GUILayout.ExpandWidth(true));
        GUI.Box(rect, GUIContent.none);

        if (_editingClip == null || _editingClip.length <= 0f)
        {
            EditorGUI.DropShadowLabel(rect, "时间轴不可用");
            return;
        }

        HandleTimelineInput(rect);
        DrawTimelineRuler(rect);
        DrawTimelinePlayhead(rect);
        DrawTimelineMarkers(rect);
    }

    private void DrawTimelineLegend()
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            DrawLegendItem("攻击开始", GetColorByKind(SkillEventKind.AttackStart));
            DrawLegendItem("攻击清理", GetColorByKind(SkillEventKind.AttackClear));
            DrawLegendItem("魔法攻击", GetColorByKind(SkillEventKind.MagicAttack));
            DrawLegendItem("技能迁移", GetColorByKind(SkillEventKind.Cancel));
            DrawLegendItem("其他", GetColorByKind(SkillEventKind.Other));
        }
    }

    private static void DrawLegendItem(string label, Color color)
    {
        var rect = EditorGUILayout.GetControlRect(false, 16f, GUILayout.Width(110f));
        var colorRect = new Rect(rect.x, rect.y + 3f, 12f, 12f);
        EditorGUI.DrawRect(colorRect, color);
        EditorGUI.LabelField(new Rect(rect.x + 18f, rect.y, rect.width - 18f, rect.height), label, EditorStyles.miniLabel);
    }

    private void DrawEventEditingPanel()
    {
        EditorGUILayout.Space(4f);
        var attackStartButtonLabel = _autoAddAttackClearAfterStart ? "添加全身攻击开始(+关闭)" : "添加全身攻击开始";
        var bodyPartStartButtonLabel = _autoAddAttackClearAfterStart ? "添加部位攻击开始(+关闭)" : "添加部位攻击开始";

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button(attackStartButtonLabel))
            {
                AddAttackStartEvent(CreateAllBodyAttackStartEvent(_previewTime));
            }

            if (GUILayout.Button("添加全身攻击清理"))
            {
                AddEvent(CreateAttackClearEvent(_previewTime));
            }

            if (GUILayout.Button("添加技能迁移标志"))
            {
                AddEvent(CreateCancelEvent(_previewTime));
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            _quickMagicEventIndex = EditorGUILayout.Popup("魔法攻击事件", _quickMagicEventIndex, QuickMagicEventNames);
            if (GUILayout.Button("按当前时间添加", GUILayout.Width(120f)))
            {
                AddEvent(CreateMagicAttackEvent(_previewTime, QuickMagicEventNames[Mathf.Clamp(_quickMagicEventIndex, 0, QuickMagicEventNames.Length - 1)]));
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            _quickMarkerEventIndex = EditorGUILayout.Popup("手脚攻击事件", _quickMarkerEventIndex, QuickMarkerLabels);
            if (GUILayout.Button(bodyPartStartButtonLabel, GUILayout.Width(150f)))
            {
                AddAttackStartEvent(CreateBodyPartAttackEvent(_previewTime, GetQuickMarkerFunctionName(), 1));
            }

            if (GUILayout.Button("添加部位攻击清理", GUILayout.Width(130f)))
            {
                AddEvent(CreateBodyPartAttackEvent(_previewTime, GetQuickMarkerFunctionName(), 0));
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            _autoAddAttackClearAfterStart = EditorGUILayout.ToggleLeft("攻击开始后自动补关闭帧", _autoAddAttackClearAfterStart, GUILayout.Width(170f));
            using (new EditorGUI.DisabledScope(!_autoAddAttackClearAfterStart))
            {
                _autoAddAttackClearOffsetFrames = EditorGUILayout.IntSlider("关闭帧偏移", _autoAddAttackClearOffsetFrames, 1, 6);
            }
        }

        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("自动分析（人形实验）", EditorStyles.boldLabel);
            _autoAnalysisReplaceExistingAttackEvents = EditorGUILayout.ToggleLeft("替换现有攻击开始/清理事件", _autoAnalysisReplaceExistingAttackEvents);
            if (GUILayout.Button("自动分析当前人形 Clip"))
            {
                AutoAnalyzeHumanoidAttackFrames();
            }

            if (!string.IsNullOrEmpty(_autoAnalysisStatus))
            {
                EditorGUILayout.HelpBox(_autoAnalysisStatus, _autoAnalysisStatusType);
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            using (new EditorGUILayout.VerticalScope(GUILayout.Width(Mathf.Min(position.width * 0.42f, 360f))))
            {
                EditorGUILayout.LabelField($"事件列表 ({_workingEvents.Count})", EditorStyles.boldLabel);
                _eventListScroll = EditorGUILayout.BeginScrollView(_eventListScroll, GUILayout.Height(EventListHeight));
                for (var i = 0; i < _workingEvents.Count; i++)
                {
                    DrawEventListRow(i, _workingEvents[i]);
                }
                EditorGUILayout.EndScrollView();
            }

            using (new EditorGUILayout.VerticalScope())
            {
                DrawSelectedEventEditor();
            }
        }
    }

    private void DrawEventListRow(int index, AnimationEvent animationEvent)
    {
        var kind = ClassifyEvent(animationEvent);
        var style = new GUIStyle(EditorStyles.miniButton)
        {
            alignment = TextAnchor.MiddleLeft
        };

        var previousColor = GUI.backgroundColor;
        if (index == _selectedEventIndex)
        {
            GUI.backgroundColor = new Color(0.35f, 0.55f, 0.9f, 0.85f);
        }
        else
        {
            var color = GetColorByKind(kind);
            GUI.backgroundColor = new Color(color.r, color.g, color.b, 0.65f);
        }

        var label = $"[{TimeToFrame(animationEvent.time),3}] {animationEvent.time:F3}s  {GetEventDisplayName(animationEvent)}";
        if (GUILayout.Button(label, style))
        {
            _selectedEventIndex = index;
            _previewTime = animationEvent.time;
            _previewPlaying = false;
        }

        GUI.backgroundColor = previousColor;
    }

    private void DrawSelectedEventEditor()
    {
        var selected = GetSelectedEvent();
        if (selected == null)
        {
            EditorGUILayout.HelpBox("点击时间轴上的事件点或左侧列表项来编辑事件。", MessageType.None);
            return;
        }

        var kind = ClassifyEvent(selected);
        EditorGUILayout.LabelField($"选中事件：{GetKindDisplayName(kind)}", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        var functionName = EditorGUILayout.TextField("Function", selected.functionName);
        var time = EditorGUILayout.Slider("Time", selected.time, 0f, _editingClip.length);
        var intParameter = EditorGUILayout.IntField("Int Param", selected.intParameter);
        var floatParameter = EditorGUILayout.FloatField("Float Param", selected.floatParameter);
        var stringParameter = EditorGUILayout.TextField("String Param", selected.stringParameter);
        var objectParameter = EditorGUILayout.ObjectField("Object Param", selected.objectReferenceParameter, typeof(UnityEngine.Object), false);
        if (EditorGUI.EndChangeCheck())
        {
            selected.functionName = functionName;
            selected.time = SnapToFrame(time);
            selected.intParameter = intParameter;
            selected.floatParameter = floatParameter;
            selected.stringParameter = stringParameter;
            selected.objectReferenceParameter = objectParameter;
            MarkEventsDirty();
        }

        EditorGUI.BeginChangeCheck();
        var frame = EditorGUILayout.IntField("Frame", TimeToFrame(selected.time));
        if (EditorGUI.EndChangeCheck())
        {
            selected.time = FrameToTime(frame);
            MarkEventsDirty();
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("设为全身攻击开始"))
            {
                selected.functionName = "SetAllBodyMarkerManagersIn";
                selected.intParameter = 0;
                MarkEventsDirty();
            }

            if (GUILayout.Button("设为攻击清理"))
            {
                selected.functionName = "ClearMarkerManagers";
                selected.intParameter = 0;
                MarkEventsDirty();
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            _quickMarkerEventIndex = EditorGUILayout.Popup("手脚攻击事件", _quickMarkerEventIndex, QuickMarkerLabels);
            if (GUILayout.Button("设为部位攻击开始"))
            {
                selected.functionName = GetQuickMarkerFunctionName();
                selected.intParameter = 1;
                MarkEventsDirty();
            }

            if (GUILayout.Button("设为部位攻击清理"))
            {
                selected.functionName = GetQuickMarkerFunctionName();
                selected.intParameter = 0;
                MarkEventsDirty();
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("设为技能迁移标志"))
            {
                selected.functionName = "turn_on_flag";
                selected.intParameter = 0;
                MarkEventsDirty();
            }

            if (GUILayout.Button("设为魔法攻击"))
            {
                selected.functionName = QuickMagicEventNames[Mathf.Clamp(_quickMagicEventIndex, 0, QuickMagicEventNames.Length - 1)];
                selected.intParameter = 0;
                MarkEventsDirty();
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("删除选中事件"))
            {
                _workingEvents.RemoveAt(_selectedEventIndex);
                _selectedEventIndex = Mathf.Clamp(_selectedEventIndex - 1, -1, _workingEvents.Count - 1);
                MarkEventsDirty();
            }

            if (GUILayout.Button("定位到当前时间"))
            {
                selected.time = SnapToFrame(_previewTime);
                MarkEventsDirty();
            }
        }

        if (_hasUnsavedEventChanges)
        {
            EditorGUILayout.HelpBox("当前事件修改尚未写回动画资源。点击上方“保存事件”提交。", MessageType.Warning);
        }
    }

    private void HandlePreviewInput(Rect rect)
    {
        var current = Event.current;
        if (!rect.Contains(current.mousePosition))
        {
            return;
        }

        switch (current.type)
        {
            case EventType.ScrollWheel:
                _previewDistance = Mathf.Clamp(_previewDistance + current.delta.y * 0.1f, 0.8f, 20f);
                current.Use();
                Repaint();
                break;
            case EventType.MouseDrag:
                if (current.button == 0)
                {
                    _previewYaw += current.delta.x * 0.45f;
                    _previewPitch = Mathf.Clamp(_previewPitch - current.delta.y * 0.3f, -45f, 70f);
                    current.Use();
                    Repaint();
                }
                break;
        }
    }

    private void HandleTimelineInput(Rect rect)
    {
        var current = Event.current;
        if (_editingClip == null || _editingClip.length <= 0f)
        {
            return;
        }

        if (current.type == EventType.MouseDown && rect.Contains(current.mousePosition))
        {
            var hitIndex = HitTestEventIndex(rect, current.mousePosition);
            if (hitIndex >= 0)
            {
                _selectedEventIndex = hitIndex;
                _draggingTimelineMarker = true;
                _previewPlaying = false;
                current.Use();
                return;
            }

            _scrubbingTimeline = true;
            _previewTime = PositionToTime(rect, current.mousePosition.x);
            _previewPlaying = false;
            current.Use();
            return;
        }

        if (current.type == EventType.MouseDrag)
        {
            if (_draggingTimelineMarker && _selectedEventIndex >= 0)
            {
                var selected = GetSelectedEvent();
                if (selected != null)
                {
                    selected.time = PositionToTime(rect, current.mousePosition.x);
                    _previewTime = selected.time;
                    MarkEventsDirty();
                    current.Use();
                }
                return;
            }

            if (_scrubbingTimeline)
            {
                _previewTime = PositionToTime(rect, current.mousePosition.x);
                current.Use();
                Repaint();
                return;
            }
        }

        if (current.type == EventType.MouseUp)
        {
            if (_draggingTimelineMarker || _scrubbingTimeline)
            {
                _draggingTimelineMarker = false;
                _scrubbingTimeline = false;
                current.Use();
            }
        }
    }

    private void DrawTimelineRuler(Rect rect)
    {
        var clipLength = _editingClip.length;
        var rulerRect = new Rect(rect.x + 8f, rect.y + 4f, rect.width - 16f, 18f);
        EditorGUI.DrawRect(new Rect(rect.x + 1f, rect.y + 1f, rect.width - 2f, rect.height - 2f), new Color(0.18f, 0.18f, 0.18f));

        var tickCount = Mathf.Clamp(Mathf.CeilToInt(clipLength * 4f), 4, 24);
        for (var i = 0; i <= tickCount; i++)
        {
            var normalized = i / (float)tickCount;
            var x = Mathf.Lerp(rulerRect.x, rulerRect.xMax, normalized);
            EditorGUI.DrawRect(new Rect(x, rect.y + 2f, 1f, rect.height - 4f), new Color(1f, 1f, 1f, i % 2 == 0 ? 0.12f : 0.06f));
            var tickTime = clipLength * normalized;
            GUI.Label(new Rect(x + 2f, rect.y + 2f, 48f, 16f), $"{tickTime:F2}s", EditorStyles.miniLabel);
        }
    }

    private void DrawTimelinePlayhead(Rect rect)
    {
        var x = TimeToPosition(rect, _previewTime);
        EditorGUI.DrawRect(new Rect(x - 1f, rect.y + 1f, 2f, rect.height - 2f), new Color(1f, 0.95f, 0.15f, 0.95f));
    }

    private void DrawTimelineMarkers(Rect rect)
    {
        for (var i = 0; i < _workingEvents.Count; i++)
        {
            var animationEvent = _workingEvents[i];
            var kind = ClassifyEvent(animationEvent);
            var color = GetColorByKind(kind);
            var x = TimeToPosition(rect, animationEvent.time);
            var y = rect.y + 28f;
            var markerRect = new Rect(x - 4f, y, 8f, rect.height - 34f);

            if (i == _selectedEventIndex)
            {
                EditorGUI.DrawRect(new Rect(markerRect.x - 2f, markerRect.y - 2f, markerRect.width + 4f, markerRect.height + 4f), new Color(1f, 1f, 1f, 0.8f));
            }

            EditorGUI.DrawRect(markerRect, color);
            GUI.Label(new Rect(x + 4f, rect.y + rect.height - 18f, 90f, 16f), ShortEventName(animationEvent), EditorStyles.miniLabel);
        }
    }

    private void RenderPreview(Rect rect)
    {
        var camera = _previewUtility.camera;
        UpdatePreviewCamera(camera);

        _previewUtility.BeginPreview(rect, GUIStyle.none);
        camera.Render();
        var previewTexture = _previewUtility.EndPreview();
        GUI.DrawTexture(rect, previewTexture, ScaleMode.StretchToFill, false);
    }

    private void UpdatePreviewCamera(Camera camera)
    {
        var rotation = Quaternion.Euler(_previewPitch, _previewYaw, 0f);
        var direction = rotation * Vector3.forward;
        camera.transform.position = _previewPivot - direction * _previewDistance;
        camera.transform.rotation = rotation;
        camera.nearClipPlane = 0.01f;
        camera.farClipPlane = Mathf.Max(50f, _previewDistance * 8f);
        camera.clearFlags = CameraClearFlags.Color;
        camera.backgroundColor = new Color(0.12f, 0.12f, 0.14f, 1f);
        camera.fieldOfView = 30f;

        if (_previewUtility.lights.Length > 0)
        {
            _previewUtility.lights[0].intensity = 1.15f;
            _previewUtility.lights[0].transform.rotation = Quaternion.Euler(38f, 132f, 0f);
        }

        if (_previewUtility.lights.Length > 1)
        {
            _previewUtility.lights[1].intensity = 1.0f;
            _previewUtility.lights[1].transform.rotation = Quaternion.Euler(340f, 218f, 177f);
        }

        _previewUtility.ambientColor = new Color(0.35f, 0.35f, 0.35f, 1f);
    }

    private void SamplePreviewClip()
    {
        if (_editingClip == null || _previewInstance == null)
        {
            return;
        }

        if (!AnimationMode.InAnimationMode())
        {
            AnimationMode.StartAnimationMode();
        }

        AnimationMode.BeginSampling();
        AnimationMode.SampleAnimationClip(_previewInstance, _editingClip, Mathf.Clamp(_previewTime, 0f, _editingClip.length));
        AnimationMode.EndSampling();
    }

    private void EnsurePreviewReady()
    {
        if (_previewUtility == null)
        {
            _previewUtility = new PreviewRenderUtility();
        }

        if (_previewPrefabs.Count == 0)
        {
            return;
        }

        var prefab = _previewPrefabs[Mathf.Clamp(_selectedPreviewPrefabIndex, 0, _previewPrefabs.Count - 1)];
        if (_previewInstance != null && _currentPreviewPrefab == prefab)
        {
            return;
        }

        RebuildPreviewInstance();
    }

    private void RebuildPreviewInstance()
    {
        DisposePreview();
        _previewUtility = new PreviewRenderUtility();

        if (_previewPrefabs.Count == 0)
        {
            return;
        }

        _currentPreviewPrefab = _previewPrefabs[Mathf.Clamp(_selectedPreviewPrefabIndex, 0, _previewPrefabs.Count - 1)];
        if (_currentPreviewPrefab == null)
        {
            return;
        }

        _previewInstance = UnityEngine.Object.Instantiate(_currentPreviewPrefab);
        _previewInstance.hideFlags = HideFlags.HideAndDontSave;

        foreach (var behaviour in _previewInstance.GetComponentsInChildren<Behaviour>(true))
        {
            behaviour.enabled = false;
        }

        foreach (var particle in _previewInstance.GetComponentsInChildren<ParticleSystem>(true))
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        _previewUtility.AddSingleGO(_previewInstance);
        CachePreviewBounds();
        FocusPreviewCamera();
    }

    private void CachePreviewBounds()
    {
        if (_previewInstance == null)
        {
            _previewBounds = new Bounds(Vector3.zero, Vector3.one);
            return;
        }

        var renderers = _previewInstance.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0)
        {
            _previewBounds = new Bounds(Vector3.zero, Vector3.one);
            return;
        }

        _previewBounds = renderers[0].bounds;
        for (var i = 1; i < renderers.Length; i++)
        {
            _previewBounds.Encapsulate(renderers[i].bounds);
        }
    }

    private void FocusPreviewCamera()
    {
        if (_previewBounds.size == Vector3.zero)
        {
            _previewPivot = new Vector3(0f, 1f, 0f);
            _previewDistance = 3f;
            return;
        }

        _previewPivot = _previewBounds.center;
        _previewPivot.y = Mathf.Lerp(_previewBounds.min.y, _previewBounds.max.y, 0.55f);
        _previewDistance = Mathf.Max(1.6f, _previewBounds.extents.magnitude * 2.2f);
    }

    private void DisposePreview()
    {
        if (AnimationMode.InAnimationMode())
        {
            AnimationMode.StopAnimationMode();
        }

        DisposePreviewInstance();
        if (_previewUtility != null)
        {
            _previewUtility.Cleanup();
            _previewUtility = null;
        }
    }

    private void DisposePreviewInstance()
    {
        if (_previewInstance != null)
        {
            UnityEngine.Object.DestroyImmediate(_previewInstance);
            _previewInstance = null;
        }
        _currentPreviewPrefab = null;
    }

    private void RefreshPreviewTypes()
    {
        _previewTypes.Clear();
        var rootPath = "Assets/ExternalAssets/Prefabs/Units";
        if (!AssetDatabase.IsValidFolder(rootPath))
        {
            return;
        }

        foreach (var directory in Directory.GetDirectories(rootPath))
        {
            var normalized = directory.Replace("\\", "/");
            _previewTypes.Add(Path.GetFileName(normalized));
        }

        _previewTypes.Sort(StringComparer.Ordinal);
        if (_previewTypes.Count > 0 && !_previewTypes.Contains(_previewType))
        {
            _previewType = _previewTypes[0];
        }
    }

    private void RefreshPreviewPrefabs()
    {
        _previewPrefabs.Clear();
        _previewPrefabNames.Clear();

        var folder = $"Assets/ExternalAssets/Prefabs/Units/{_previewType}";
        if (!AssetDatabase.IsValidFolder(folder))
        {
            return;
        }

        var guids = AssetDatabase.FindAssets("t:Prefab", new[] { folder });
        foreach (var guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (prefab == null)
            {
                continue;
            }

            _previewPrefabs.Add(prefab);
        }

        _previewPrefabs.Sort((a, b) => string.CompareOrdinal(a.name, b.name));
        foreach (var prefab in _previewPrefabs)
        {
            _previewPrefabNames.Add(prefab.name);
        }

        _selectedPreviewPrefabIndex = Mathf.Clamp(_selectedPreviewPrefabIndex, 0, Mathf.Max(0, _previewPrefabs.Count - 1));
    }

    private void HandleClipDragAndDrop(Event current)
    {
        if (current == null)
        {
            return;
        }

        var dropRect = new Rect(0f, 0f, position.width, position.height);
        if (!dropRect.Contains(current.mousePosition))
        {
            return;
        }

        if (current.type != EventType.DragUpdated && current.type != EventType.DragPerform)
        {
            return;
        }

        var clip = DragAndDrop.objectReferences.OfType<AnimationClip>().FirstOrDefault();
        if (clip == null)
        {
            return;
        }

        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
        if (current.type == EventType.DragPerform)
        {
            DragAndDrop.AcceptDrag();
            SetEditingClip(clip);
        }

        current.Use();
    }

    private void SetEditingClip(AnimationClip clip)
    {
        if (_editingClip == clip)
        {
            return;
        }

        _editingClip = clip;
        _previewTime = 0f;
        _previewPlaying = false;
        _selectedEventIndex = -1;
        _hasUnsavedEventChanges = false;

        AutoDetectPreviewTypeFromClip();
        RefreshPreviewPrefabs();
        RebuildPreviewInstance();
        ReloadWorkingEvents();
        OnEditingClipChanged();
    }

    private void AutoDetectPreviewTypeFromClip()
    {
        if (_editingClip == null)
        {
            return;
        }

        var assetPath = AssetDatabase.GetAssetPath(_editingClip);
        if (string.IsNullOrEmpty(assetPath))
        {
            return;
        }

        var normalized = assetPath.Replace("\\", "/");
        var parts = normalized.Split('/');
        for (var i = 0; i < parts.Length - 1; i++)
        {
            if (parts[i] == "Animations" && i + 1 < parts.Length)
            {
                var type = parts[i + 1];
                if (_previewTypes.Contains(type))
                {
                    _previewType = type;
                    return;
                }
            }
        }
    }

    private void ReloadWorkingEvents()
    {
        _workingEvents.Clear();
        _selectedEventIndex = -1;
        _hasUnsavedEventChanges = false;

        if (_editingClip == null)
        {
            return;
        }

        var events = AnimationUtility.GetAnimationEvents(_editingClip);
        _workingEvents.AddRange(events.OrderBy(x => x.time).ThenBy(x => x.functionName));
    }

    private void SaveWorkingEvents()
    {
        if (_editingClip == null)
        {
            return;
        }

        try
        {
            Undo.RegisterCompleteObjectUndo(_editingClip, "Edit skill animation events");
            AnimationUtility.SetAnimationEvents(_editingClip, _workingEvents.OrderBy(x => x.time).ThenBy(x => x.functionName).ToArray());
            EditorUtility.SetDirty(_editingClip);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(_editingClip));
            _hasUnsavedEventChanges = false;
            ReloadWorkingEvents();
        }
        catch (Exception ex)
        {
            Debug.LogError($"保存动画事件失败：{ex}");
        }
    }

    private void AutoAnalyzeHumanoidAttackFrames()
    {
        _autoAnalysisStatus = string.Empty;
        _autoAnalysisStatusType = MessageType.None;

        if (_editingClip == null)
        {
            _autoAnalysisStatusType = MessageType.Warning;
            _autoAnalysisStatus = "请先指定一个 AnimationClip。";
            return;
        }

        EnsurePreviewReady();
        if (_previewInstance == null)
        {
            _autoAnalysisStatusType = MessageType.Error;
            _autoAnalysisStatus = "当前没有可用的角色预览实例，无法进行人形判帧。";
            return;
        }

        if (!TryBuildAutoAnalysisTracks(out var tracks, out var error))
        {
            _autoAnalysisStatusType = MessageType.Error;
            _autoAnalysisStatus = error;
            return;
        }

        var windows = DetectHumanoidAttackWindows(tracks);
        if (windows.Count == 0)
        {
            _autoAnalysisStatusType = MessageType.Warning;
            _autoAnalysisStatus = "未从当前人形动作里识别出明显的手脚攻击窗口。你可以先切换到更匹配的角色预览，或手动补帧。";
            return;
        }

        if (_autoAnalysisReplaceExistingAttackEvents)
        {
            _workingEvents.RemoveAll(x =>
            {
                var kind = ClassifyEvent(x);
                return kind == SkillEventKind.AttackStart || kind == SkillEventKind.AttackClear;
            });
        }

        var pendingEvents = new List<AnimationEvent>();
        AnimationEvent lastAdded = null;
        foreach (var window in windows.OrderBy(x => x.StartFrame).ThenBy(x => x.FunctionName))
        {
            var startEvent = CreateBodyPartAttackEvent(FrameToTime(window.StartFrame), window.FunctionName, 1);
            var endEvent = CreateBodyPartAttackEvent(FrameToTime(window.EndFrame), window.FunctionName, 0);
            pendingEvents.Add(startEvent);
            pendingEvents.Add(endEvent);
            lastAdded = endEvent;
        }

        AddEvents(pendingEvents);

        if (lastAdded != null)
        {
            _previewTime = lastAdded.time;
        }

        if (lastAdded != null)
        {
            _selectedEventIndex = _workingEvents.IndexOf(lastAdded);
        }

        _autoAnalysisStatusType = MessageType.Info;
        _autoAnalysisStatus = $"已自动分析出 {windows.Count} 段人形攻击窗口：{DescribeAutoAnalysisWindows(windows)}。结果尚未保存，请点击上方“保存事件”。";
    }

    private bool TryBuildAutoAnalysisTracks(out List<AutoAnalysisTrack> tracks, out string error)
    {
        tracks = new List<AutoAnalysisTrack>();
        error = string.Empty;

        if (_editingClip == null)
        {
            error = "没有可分析的 AnimationClip。";
            return false;
        }

        var frameRate = Mathf.Max(1f, _editingClip.frameRate);
        var frameCount = Mathf.Max(2, Mathf.CeilToInt(_editingClip.length * frameRate) + 1);
        var animator = _previewInstance != null ? _previewInstance.GetComponentInChildren<Animator>(true) : null;
        var root = _previewInstance != null ? _previewInstance.transform : null;
        var hips = ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.Hips, "Hips", "Pelvis");

        AddAutoAnalysisTrack(
            tracks,
            frameCount,
            "右手",
            "SetRightHandMarkerManager",
            ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.RightHand, "RightHand", "Hand_R", "RHand"),
            ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.RightUpperArm, "RightUpperArm", "UpperArm_R", "RightArm", "RArm") ?? hips ?? root);

        AddAutoAnalysisTrack(
            tracks,
            frameCount,
            "左手",
            "SetLeftHandMarkerManager",
            ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.LeftHand, "LeftHand", "Hand_L", "LHand"),
            ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.LeftUpperArm, "LeftUpperArm", "UpperArm_L", "LeftArm", "LArm") ?? hips ?? root);

        AddAutoAnalysisTrack(
            tracks,
            frameCount,
            "右脚",
            "SetRightFootMarkerManager",
            ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.RightFoot, "RightFoot", "Foot_R", "RFoot"),
            ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.RightUpperLeg, "RightUpperLeg", "UpperLeg_R", "RightUpLeg", "RThigh") ?? hips ?? root);

        AddAutoAnalysisTrack(
            tracks,
            frameCount,
            "左脚",
            "SetLeftFootMarkerManager",
            ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.LeftFoot, "LeftFoot", "Foot_L", "LFoot"),
            ResolveHumanoidTransform(animator, _previewInstance, HumanBodyBones.LeftUpperLeg, "LeftUpperLeg", "UpperLeg_L", "LeftUpLeg", "LThigh") ?? hips ?? root);

        if (tracks.Count == 0)
        {
            error = "当前预览角色上找不到可用的人形手脚骨骼，无法自动判帧。";
            return false;
        }

        var sampledTimes = new float[frameCount];
        try
        {
            if (!AnimationMode.InAnimationMode())
            {
                AnimationMode.StartAnimationMode();
            }

            for (var frame = 0; frame < frameCount; frame++)
            {
                var time = Mathf.Clamp(frame / frameRate, 0f, _editingClip.length);
                sampledTimes[frame] = time;

                AnimationMode.BeginSampling();
                AnimationMode.SampleAnimationClip(_previewInstance, _editingClip, time);
                AnimationMode.EndSampling();

                for (var i = 0; i < tracks.Count; i++)
                {
                    var track = tracks[i];
                    if (track.Bone == null || track.Reference == null)
                    {
                        continue;
                    }

                    var localPosition = track.Reference.InverseTransformPoint(track.Bone.position);
                    track.Positions[frame] = localPosition;
                    track.Reach[frame] = localPosition.magnitude;
                    if (frame > 0)
                    {
                        var deltaTime = Mathf.Max(0.0001f, sampledTimes[frame] - sampledTimes[frame - 1]);
                        track.Speed[frame] = (localPosition - track.Positions[frame - 1]).magnitude / deltaTime;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            error = $"自动分析采样失败：{ex.Message}";
            return false;
        }
        finally
        {
            if (AnimationMode.InAnimationMode() && _previewInstance != null && _editingClip != null)
            {
                AnimationMode.BeginSampling();
                AnimationMode.SampleAnimationClip(_previewInstance, _editingClip, Mathf.Clamp(_previewTime, 0f, _editingClip.length));
                AnimationMode.EndSampling();
            }
        }

        return true;
    }

    private static void AddAutoAnalysisTrack(ICollection<AutoAnalysisTrack> tracks, int frameCount, string label, string functionName, Transform bone, Transform reference)
    {
        if (tracks == null || bone == null || reference == null)
        {
            return;
        }

        tracks.Add(new AutoAnalysisTrack
        {
            Label = label,
            FunctionName = functionName,
            Bone = bone,
            Reference = reference,
            Positions = new Vector3[frameCount],
            Reach = new float[frameCount],
            Speed = new float[frameCount]
        });
    }

    private List<AutoAnalysisWindow> DetectHumanoidAttackWindows(IEnumerable<AutoAnalysisTrack> tracks)
    {
        var results = new List<AutoAnalysisWindow>();
        if (tracks == null)
        {
            return results;
        }

        foreach (var track in tracks)
        {
            results.AddRange(DetectHumanoidAttackWindows(track));
        }

        return results.OrderBy(x => x.StartFrame).ThenBy(x => x.FunctionName).ToList();
    }

    private List<AutoAnalysisWindow> DetectHumanoidAttackWindows(AutoAnalysisTrack track)
    {
        var results = new List<AutoAnalysisWindow>();
        if (track == null || track.Reach == null || track.Speed == null || track.Reach.Length < 3)
        {
            return results;
        }

        var maxSpeed = track.Speed.Max();
        var minReach = track.Reach.Min();
        var maxReach = track.Reach.Max();
        var reachRange = maxReach - minReach;
        if (maxSpeed < 0.35f || reachRange < 0.05f)
        {
            return results;
        }

        var speedThreshold = Mathf.Max(0.35f, maxSpeed * 0.45f);
        var reachThreshold = minReach + reachRange * 0.6f;
        var minGapFrames = Mathf.Max(2, Mathf.RoundToInt(Mathf.Max(1f, _editingClip.frameRate) * 0.08f));

        for (var frame = 1; frame < track.Reach.Length - 1; frame++)
        {
            var speedNorm = Mathf.Clamp01(track.Speed[frame] / maxSpeed);
            var reachNorm = reachRange > 0f ? Mathf.Clamp01((track.Reach[frame] - minReach) / reachRange) : 0f;
            var score = speedNorm * 0.65f + reachNorm * 0.75f;
            if (track.Speed[frame] < speedThreshold || track.Reach[frame] < reachThreshold || score < 0.95f)
            {
                continue;
            }

            var prevSpeedNorm = Mathf.Clamp01(track.Speed[frame - 1] / maxSpeed);
            var prevReachNorm = reachRange > 0f ? Mathf.Clamp01((track.Reach[frame - 1] - minReach) / reachRange) : 0f;
            var nextSpeedNorm = Mathf.Clamp01(track.Speed[frame + 1] / maxSpeed);
            var nextReachNorm = reachRange > 0f ? Mathf.Clamp01((track.Reach[frame + 1] - minReach) / reachRange) : 0f;
            var prevScore = prevSpeedNorm * 0.65f + prevReachNorm * 0.75f;
            var nextScore = nextSpeedNorm * 0.65f + nextReachNorm * 0.75f;
            if (score < prevScore || score < nextScore)
            {
                continue;
            }

            var window = BuildAutoAnalysisWindow(track, frame, score);
            if (results.Count > 0 && window.StartFrame <= results[results.Count - 1].EndFrame + minGapFrames)
            {
                if (window.Score > results[results.Count - 1].Score)
                {
                    results[results.Count - 1] = window;
                }
            }
            else
            {
                results.Add(window);
            }

            if (results.Count >= AutoAnalysisMaxWindowsPerLimb)
            {
                break;
            }
        }

        return results;
    }

    private AutoAnalysisWindow BuildAutoAnalysisWindow(AutoAnalysisTrack track, int peakFrame, float score)
    {
        var frameRate = Mathf.Max(1f, _editingClip.frameRate);
        var minFrames = Mathf.Max(1, Mathf.RoundToInt(frameRate * AutoAnalysisMinActiveTime));
        var maxFrames = Mathf.Max(minFrames + 1, Mathf.RoundToInt(frameRate * AutoAnalysisMaxActiveTime));
        var peakReach = Mathf.Max(0.0001f, track.Reach[peakFrame]);
        var peakSpeed = Mathf.Max(0.0001f, track.Speed[peakFrame]);

        var startFrame = peakFrame;
        while (startFrame > 0 && peakFrame - startFrame < maxFrames)
        {
            var reachRatio = track.Reach[startFrame - 1] / peakReach;
            var speedRatio = track.Speed[startFrame - 1] / peakSpeed;
            if (reachRatio < 0.82f && speedRatio < 0.35f)
            {
                break;
            }

            startFrame--;
        }

        var endFrame = peakFrame;
        while (endFrame < track.Reach.Length - 1 && endFrame - peakFrame < maxFrames)
        {
            var reachRatio = track.Reach[endFrame + 1] / peakReach;
            var speedRatio = track.Speed[endFrame + 1] / peakSpeed;
            if (reachRatio < 0.78f && speedRatio < 0.25f)
            {
                break;
            }

            endFrame++;
        }

        if (endFrame - startFrame < minFrames)
        {
            var padding = minFrames - (endFrame - startFrame);
            startFrame = Mathf.Max(0, startFrame - padding / 2);
            endFrame = Mathf.Min(track.Reach.Length - 1, endFrame + padding - padding / 2);
        }

        return new AutoAnalysisWindow
        {
            FunctionName = track.FunctionName,
            StartFrame = startFrame,
            EndFrame = endFrame,
            Score = score
        };
    }

    private static Transform ResolveHumanoidTransform(Animator animator, GameObject root, HumanBodyBones humanBodyBone, params string[] nameHints)
    {
        if (animator != null && animator.avatar != null && animator.avatar.isHuman)
        {
            var bone = animator.GetBoneTransform(humanBodyBone);
            if (bone != null)
            {
                return bone;
            }
        }

        return FindTransformByHints(root, nameHints);
    }

    private static Transform FindTransformByHints(GameObject root, params string[] nameHints)
    {
        if (root == null || nameHints == null || nameHints.Length == 0)
        {
            return null;
        }

        var transforms = root.GetComponentsInChildren<Transform>(true);
        var normalizedHints = nameHints
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(NormalizeTransformName)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();

        if (normalizedHints.Length == 0)
        {
            return null;
        }

        foreach (var hint in normalizedHints)
        {
            var exact = transforms.FirstOrDefault(x => NormalizeTransformName(x.name) == hint);
            if (exact != null)
            {
                return exact;
            }
        }

        foreach (var hint in normalizedHints)
        {
            var partial = transforms.FirstOrDefault(x => NormalizeTransformName(x.name).Contains(hint));
            if (partial != null)
            {
                return partial;
            }
        }

        return null;
    }

    private static string NormalizeTransformName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        return new string(value.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());
    }

    private string DescribeAutoAnalysisWindows(IReadOnlyList<AutoAnalysisWindow> windows)
    {
        if (windows == null || windows.Count == 0)
        {
            return "无";
        }

        return string.Join("，", windows.Take(6).Select(x =>
        {
            var startTime = FrameToTime(x.StartFrame);
            var endTime = FrameToTime(x.EndFrame);
            return $"{GetBodyPartLabel(x.FunctionName)} {startTime:F2}s-{endTime:F2}s";
        }));
    }

    private void AddAttackStartEvent(AnimationEvent animationEvent)
    {
        if (animationEvent == null)
        {
            return;
        }

        var pendingEvents = new List<AnimationEvent> { animationEvent };
        AppendAutoAttackClearAfterStart(pendingEvents, animationEvent);
        AddEvents(pendingEvents);
        _selectedEventIndex = _workingEvents.IndexOf(animationEvent);
        _previewTime = animationEvent.time;
    }

    private void AddEvent(AnimationEvent animationEvent)
    {
        AddEvents(new[] { animationEvent });
    }

    private void AddEvents(IEnumerable<AnimationEvent> animationEvents)
    {
        if (animationEvents == null)
        {
            return;
        }

        AnimationEvent lastAdded = null;
        foreach (var animationEvent in animationEvents)
        {
            if (animationEvent == null || ContainsEquivalentEvent(_workingEvents, animationEvent))
            {
                continue;
            }

            _workingEvents.Add(animationEvent);
            lastAdded = animationEvent;
        }

        if (lastAdded == null)
        {
            return;
        }

        MarkEventsDirty();
        _selectedEventIndex = _workingEvents.IndexOf(lastAdded);
        _previewTime = lastAdded.time;
    }

    private void AppendAutoAttackClearAfterStart(ICollection<AnimationEvent> pendingEvents, AnimationEvent attackStartEvent)
    {
        if (!_autoAddAttackClearAfterStart || pendingEvents == null || attackStartEvent == null || !IsAttackStartEvent(attackStartEvent))
        {
            return;
        }

        var clearEvent = CreateAttackClearEventAfterAttackStart(attackStartEvent.time);
        if (ContainsEquivalentEvent(_workingEvents, clearEvent) || ContainsEquivalentEvent(pendingEvents, clearEvent))
        {
            return;
        }

        pendingEvents.Add(clearEvent);
    }

    private bool IsAttackStartEvent(AnimationEvent animationEvent)
    {
        if (animationEvent == null)
        {
            return false;
        }

        return SKillAnalyzer.IsNormalAttackStartEvent(animationEvent);
    }

    private AnimationEvent CreateAttackClearEventAfterAttackStart(float attackStartTime)
    {
        var clearFrame = TimeToFrame(attackStartTime) + Mathf.Max(1, _autoAddAttackClearOffsetFrames);
        return CreateAttackClearEvent(FrameToTime(clearFrame));
    }

    private static bool ContainsEquivalentEvent(IEnumerable<AnimationEvent> events, AnimationEvent candidate)
    {
        if (events == null || candidate == null)
        {
            return false;
        }

        foreach (var animationEvent in events)
        {
            if (animationEvent == null)
            {
                continue;
            }

            if (animationEvent.functionName == candidate.functionName
                && Mathf.Approximately(animationEvent.time, candidate.time)
                && animationEvent.intParameter == candidate.intParameter)
            {
                return true;
            }
        }

        return false;
    }

    private void MarkEventsDirty()
    {
        _hasUnsavedEventChanges = true;
        SortWorkingEvents();
        Repaint();
    }

    private void SortWorkingEvents()
    {
        var selected = GetSelectedEvent();
        _workingEvents.Sort((a, b) =>
        {
            var timeCompare = a.time.CompareTo(b.time);
            return timeCompare != 0 ? timeCompare : string.CompareOrdinal(a.functionName, b.functionName);
        });
        _selectedEventIndex = selected != null ? _workingEvents.IndexOf(selected) : -1;
    }

    private AnimationEvent GetSelectedEvent()
    {
        return _selectedEventIndex >= 0 && _selectedEventIndex < _workingEvents.Count ? _workingEvents[_selectedEventIndex] : null;
    }

    private SkillEventKind ClassifyEvent(AnimationEvent animationEvent)
    {
        if (animationEvent == null)
        {
            return SkillEventKind.Other;
        }

        if (animationEvent.functionName == "turn_on_flag")
        {
            return SkillEventKind.Cancel;
        }

        if (SKillAnalyzer.IsNormalAttackStartEvent(animationEvent))
        {
            return SkillEventKind.AttackStart;
        }

        if (SKillAnalyzer.IsNormalAttackClearEvent(animationEvent)
            || animationEvent.functionName == "ClearTargets"
            || animationEvent.functionName == "EnableMarkers")
        {
            return SkillEventKind.AttackClear;
        }

        if (SKillAnalyzer.IsEffectAttackStartEvent(animationEvent))
        {
            return SkillEventKind.MagicAttack;
        }

        return SkillEventKind.Other;
    }

    private static Color GetColorByKind(SkillEventKind kind)
    {
        switch (kind)
        {
            case SkillEventKind.AttackStart:
                return new Color(0.95f, 0.35f, 0.35f, 1f);
            case SkillEventKind.AttackClear:
                return new Color(0.35f, 0.85f, 0.45f, 1f);
            case SkillEventKind.MagicAttack:
                return new Color(0.25f, 0.8f, 0.95f, 1f);
            case SkillEventKind.Cancel:
                return new Color(1f, 0.88f, 0.2f, 1f);
            default:
                return new Color(0.75f, 0.75f, 0.75f, 1f);
        }
    }

    private static string GetKindDisplayName(SkillEventKind kind)
    {
        switch (kind)
        {
            case SkillEventKind.AttackStart:
                return "攻击开始";
            case SkillEventKind.AttackClear:
                return "攻击清理";
            case SkillEventKind.MagicAttack:
                return "魔法攻击";
            case SkillEventKind.Cancel:
                return "技能迁移";
            default:
                return "其他";
        }
    }

    private int HitTestEventIndex(Rect rect, Vector2 mousePosition)
    {
        for (var i = _workingEvents.Count - 1; i >= 0; i--)
        {
            var x = TimeToPosition(rect, _workingEvents[i].time);
            var markerRect = new Rect(x - 6f, rect.y + 24f, 12f, rect.height - 28f);
            if (markerRect.Contains(mousePosition))
            {
                return i;
            }
        }

        return -1;
    }

    private float PositionToTime(Rect rect, float x)
    {
        if (_editingClip == null || _editingClip.length <= 0f)
        {
            return 0f;
        }

        var normalized = Mathf.InverseLerp(rect.x + 8f, rect.xMax - 8f, x);
        return SnapToFrame(normalized * _editingClip.length);
    }

    private float TimeToPosition(Rect rect, float time)
    {
        if (_editingClip == null || _editingClip.length <= 0f)
        {
            return rect.x + 8f;
        }

        var normalized = Mathf.Clamp01(time / _editingClip.length);
        return Mathf.Lerp(rect.x + 8f, rect.xMax - 8f, normalized);
    }

    private float SnapToFrame(float time)
    {
        if (_editingClip == null || _editingClip.frameRate <= 0f)
        {
            return Mathf.Max(0f, time);
        }

        return Mathf.Clamp(Mathf.Round(time * _editingClip.frameRate) / _editingClip.frameRate, 0f, _editingClip.length);
    }

    private int TimeToFrame(float time)
    {
        if (_editingClip == null || _editingClip.frameRate <= 0f)
        {
            return 0;
        }

        return Mathf.RoundToInt(time * _editingClip.frameRate);
    }

    private float FrameToTime(int frame)
    {
        if (_editingClip == null || _editingClip.frameRate <= 0f)
        {
            return 0f;
        }

        return Mathf.Clamp(frame / _editingClip.frameRate, 0f, _editingClip.length);
    }

    private void StepPreviewFrame(int delta)
    {
        if (_editingClip == null || _editingClip.frameRate <= 0f)
        {
            return;
        }

        var currentFrame = TimeToFrame(_previewTime);
        _previewTime = FrameToTime(Mathf.Max(0, currentFrame + delta));
        _previewPlaying = false;
    }

    private string GetQuickMarkerFunctionName()
    {
        return QuickMarkerFunctionNames[Mathf.Clamp(_quickMarkerEventIndex, 0, QuickMarkerFunctionNames.Length - 1)];
    }

    private static string ShortEventName(AnimationEvent animationEvent)
    {
        var displayName = GetEventDisplayName(animationEvent);
        if (string.IsNullOrEmpty(displayName))
        {
            return string.Empty;
        }

        return displayName.Length <= 14 ? displayName : displayName.Substring(0, 14);
    }

    private static string GetEventDisplayName(AnimationEvent animationEvent)
    {
        if (animationEvent == null)
        {
            return string.Empty;
        }

        if (animationEvent.functionName == "SetAllBodyMarkerManagersIn")
        {
            return "全身攻击开始";
        }

        if (animationEvent.functionName == "ClearMarkerManagers")
        {
            return "全身攻击清理";
        }

        if (animationEvent.functionName == "turn_on_flag")
        {
            return "技能迁移标志";
        }

        var bodyPartLabel = GetBodyPartLabel(animationEvent.functionName);
        if (!string.IsNullOrEmpty(bodyPartLabel))
        {
            return animationEvent.intParameter == 0
                ? $"{bodyPartLabel}清理"
                : $"{bodyPartLabel}开始";
        }

        return animationEvent.functionName ?? string.Empty;
    }

    private static string GetBodyPartLabel(string functionName)
    {
        switch (functionName)
        {
            case "SetRightHandMarkerManager":
                return "右手";
            case "SetLeftHandMarkerManager":
                return "左手";
            case "SetRightFootMarkerManager":
                return "右脚";
            case "SetLeftFootMarkerManager":
                return "左脚";
            case "SetHeadMarkerManager":
                return "头部";
            case "SetTailMarkerManager":
                return "尾部";
            default:
                return string.Empty;
        }
    }

    private static AnimationEvent CreateAllBodyAttackStartEvent(float time)
    {
        return new AnimationEvent
        {
            functionName = "SetAllBodyMarkerManagersIn",
            time = time
        };
    }

    private static AnimationEvent CreateAttackClearEvent(float time)
    {
        return new AnimationEvent
        {
            functionName = "ClearMarkerManagers",
            time = time
        };
    }

    private static AnimationEvent CreateBodyPartAttackEvent(float time, string functionName, int intParameter)
    {
        return new AnimationEvent
        {
            functionName = functionName,
            intParameter = intParameter,
            time = time
        };
    }

    private static AnimationEvent CreateCancelEvent(float time)
    {
        return new AnimationEvent
        {
            functionName = "turn_on_flag",
            time = time
        };
    }

    private static AnimationEvent CreateMagicAttackEvent(float time, string functionName)
    {
        return new AnimationEvent
        {
            functionName = functionName,
            time = time
        };
    }
}

#endif
