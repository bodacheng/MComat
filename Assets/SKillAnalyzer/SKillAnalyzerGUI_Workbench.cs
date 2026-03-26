#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public partial class SKillAnalyzerGUI
{
    private static readonly string[] WorkbenchTabLabels =
    {
        "攻击帧编辑",
        "技能制作与添加",
        "旧工具"
    };

    private SkillCreationPanel _skillCreationPanel;
    private int _selectedWorkbenchTab;

    public static SKillAnalyzerGUI OpenWorkbench(int initialTab = 0)
    {
        var window = GetWindow<SKillAnalyzerGUI>();
        window.titleContent = new GUIContent("技能工作台");
        window.minSize = new Vector2(840f, 760f);
        window._selectedWorkbenchTab = Mathf.Clamp(initialTab, 0, WorkbenchTabLabels.Length - 1);
        window.Show();
        window.Focus();
        return window;
    }

    private void EnsureSkillCreationPanel()
    {
        if (_skillCreationPanel != null)
        {
            return;
        }

        _skillCreationPanel = new SkillCreationPanel();
        _skillCreationPanel.Initialize();

        if (_editingClip != null)
        {
            _skillCreationPanel.SyncFromClip(_editingClip, GetSuggestedSkillTypeFromCurrentClip());
        }
    }

    private void DisposeSkillCreationPanel()
    {
        _skillCreationPanel = null;
    }

    private void DrawWorkbenchToolbar()
    {
        _selectedWorkbenchTab = GUILayout.Toolbar(_selectedWorkbenchTab, WorkbenchTabLabels);
    }

    private void DrawSkillCreationWorkbench()
    {
        EnsureSkillCreationPanel();

        EditorGUILayout.LabelField("技能制作与添加", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("当前窗口已整合攻击帧编辑、动画预览和技能条目创建。拖入任意 AnimationClip 后，可直接复用该 Clip 自动填写技能表单。", MessageType.Info);
        DrawSkillCreationBridge();
        EditorGUILayout.Space(6f);
        _skillCreationPanel.DrawGUI();
    }

    private void DrawSkillCreationBridge()
    {
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUI.BeginChangeCheck();
            var clip = EditorGUILayout.ObjectField("当前 Clip", _editingClip, typeof(AnimationClip), false) as AnimationClip;
            if (EditorGUI.EndChangeCheck())
            {
                SetEditingClip(clip);
            }

            if (_editingClip == null)
            {
                EditorGUILayout.HelpBox("先拖入一个 AnimationClip，工作台会自动把 REAL_NAME、类型和动画模板同步到下方创建表单。", MessageType.None);
                return;
            }

            var suggestedType = GetSuggestedSkillTypeFromCurrentClip();
            EditorGUILayout.LabelField("推断类型", string.IsNullOrEmpty(suggestedType) ? "<未推断>" : suggestedType);

            var clipPath = AssetDatabase.GetAssetPath(_editingClip);
            if (!string.IsNullOrEmpty(clipPath))
            {
                EditorGUILayout.LabelField("Clip 路径", clipPath);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("重新同步当前 Clip"))
                {
                    SyncCurrentClipToSkillCreation();
                }

                if (GUILayout.Button("返回攻击帧编辑"))
                {
                    _selectedWorkbenchTab = 0;
                }
            }
        }
    }

    private void ShowSkillCreationTab()
    {
        _selectedWorkbenchTab = 1;
    }

    private void SyncCurrentClipToSkillCreation()
    {
        if (_editingClip == null)
        {
            return;
        }

        EnsureSkillCreationPanel();
        _skillCreationPanel.SyncFromClip(_editingClip, GetSuggestedSkillTypeFromCurrentClip());
    }

    private void OnEditingClipChanged()
    {
        if (_editingClip == null)
        {
            return;
        }

        SyncCurrentClipToSkillCreation();
    }

    private string GetSuggestedSkillTypeFromCurrentClip()
    {
        var inferredType = InferSkillTypeFromClip(_editingClip);
        if (!string.IsNullOrEmpty(inferredType))
        {
            return inferredType;
        }

        return _previewType;
    }

    private static string InferSkillTypeFromClip(AnimationClip clip)
    {
        if (clip == null)
        {
            return string.Empty;
        }

        var assetPath = AssetDatabase.GetAssetPath(clip);
        if (string.IsNullOrEmpty(assetPath))
        {
            return string.Empty;
        }

        var normalized = assetPath.Replace("\\", "/");
        var parts = normalized.Split('/');
        for (var i = 0; i < parts.Length - 1; i++)
        {
            if (parts[i] == "Animations" && i + 1 < parts.Length)
            {
                return parts[i + 1];
            }
        }

        return string.Empty;
    }
}

#endif
