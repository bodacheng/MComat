#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MCombat.Shared.Behaviour;
using Skill;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

/// <summary>
/// 技能创建表单，可嵌入任意 EditorWindow。
/// 1. 追加四张 CSV 配置表
/// 2. 创建/复制图标与动画资源
/// 3. 注册 Addressables 地址与标签
/// </summary>
public class SkillCreationPanel
{
    private const string SkillConfigCsvPath = "Assets/ExternalAssets/Data/Config/mst_skill.csv";
    private const string SkillAiCsvPath = "Assets/ExternalAssets/Data/Config/skill_ai_attrs.csv";
    private const string SkillNameCsvPath = "Assets/ExternalAssets/Data/Config/skill_name.csv";
    private const string SkillStaticCsvPath = "Assets/ExternalAssets/Data/Config/SkillStaticAnalysis.csv";

    private const string SkillIconFolder = "Assets/ExternalAssets/Textures/Icons/Skill";
    private const string SkillAnimationRoot = "Assets/ExternalAssets/Animations";
    private const string SkillIconGroupName = "SkillIcon";
    private const string SkillAnimationGroupName = "SkillAnim";
    private const string SkillAnimationLabel = AddressablesResourcePolicy.SkillAnimationLabel;
    private const string CsvNewLine = "\r\n";
    private const int PlaceholderIconSize = 128;

    private static readonly BehaviorType[] BehaviorTypeOptions = BehaviorTypeUtility.CreateSkillStateOptions(true);

    private static readonly string[] BehaviorTypeLabels = BehaviorTypeOptions.Select(x => x.ToString()).ToArray();
    private static readonly string[] RequiredCsvPaths =
    {
        SkillConfigCsvPath,
        SkillAiCsvPath,
        SkillNameCsvPath,
        SkillStaticCsvPath
    };

    private readonly HashSet<string> _existingIds = new HashSet<string>();
    private readonly List<string> _types = new List<string>();
    private bool _initialized;

    private bool _autoId = true;
    private string _recordId = string.Empty;
    private int _maxRecordId;

    private int _selectedTypeIndex;
    private string _type = "human";
    private string _realName = string.Empty;
    private BehaviorType _behaviorType = BehaviorType.GR;
    private int _spLevel;
    private float _attackWeight = 1f;
    private float _hpWeight = 3f;
    private string _eventCode = string.Empty;
    private bool _allowSharedRealName;

    private float _aiMin = 0.2f;
    private float _aiMax = 5f;
    private int _aiHeight;

    private string _enName = string.Empty;
    private string _jpName = string.Empty;
    private string _cnName = string.Empty;
    private string _enIntro = string.Empty;
    private string _jpIntro = string.Empty;
    private string _cnIntro = string.Empty;

    private float _estimatedDamage = 1f;
    private int _attackCount = 1;
    private float _estimatedHp = 3f;

    private bool _createIconAsset = true;
    private Texture2D _iconTemplate;
    private bool _createAnimationAsset = true;
    private AnimationClip _animationTemplate;
    private AnimationEvent[] _animationEvents;
    private bool _registerAddressables = true;
    private Color _placeholderIconColor = new Color32(78, 88, 110, 255);

    private bool _iconExists;
    private string _iconAssetPath = string.Empty;
    private bool _animationExists;
    private string _animationAssetPath = string.Empty;
    private string _animationSearchNote = string.Empty;

    private Vector2 _scroll;
    private MessageType _messageType = MessageType.Info;
    private string _message = string.Empty;

    private sealed class AddressableEntryBackup
    {
        public string Guid;
        public bool Existed;
        public string GroupName;
        public string Address;
        public List<string> Labels = new List<string>();
    }

    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        ReloadSourceData();
        _initialized = true;
    }

    public void DrawGUI()
    {
        Initialize();

        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.Space();
            if (!string.IsNullOrEmpty(_message))
            {
                EditorGUILayout.HelpBox(_message, _messageType);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("重新读取配置"))
                {
                    ReloadSourceData();
                }

                if (GUILayout.Button("重置表单"))
                {
                    ClearForm();
                    EvaluateResourceAvailability();
                }
            }

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            DrawIdSection();
            EditorGUILayout.Space();

            DrawSkillConfigSection();
            EditorGUILayout.Space();

            DrawAiSection();
            EditorGUILayout.Space();

            DrawDisplaySection();
            EditorGUILayout.Space();

            DrawStaticAnalysisSection();
            EditorGUILayout.Space();

            DrawResourceCreationSection();
            EditorGUILayout.Space();

            EvaluateResourceAvailability();
            DrawResourceValidationSection();
            EditorGUILayout.Space();

            DrawSummarySection();
            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(GetCandidateId()));
                if (GUILayout.Button("生成技能条目", GUILayout.Height(32f)))
                {
                    CreateSkillEntry();
                }
                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.EndScrollView();
        }
    }

    public void SyncFromClip(AnimationClip clip, string suggestedType, AnimationEvent[] draftEvents = null)
    {
        Initialize();
        if (clip == null)
        {
            return;
        }

        var clipName = (clip.name ?? string.Empty).Trim();
        if (!string.IsNullOrEmpty(clipName))
        {
            _realName = clipName;

            if (string.IsNullOrWhiteSpace(_enName))
            {
                _enName = clipName;
            }

            if (string.IsNullOrWhiteSpace(_jpName))
            {
                _jpName = clipName;
            }

            if (string.IsNullOrWhiteSpace(_cnName))
            {
                _cnName = clipName;
            }
        }

        var resolvedType = string.IsNullOrWhiteSpace(suggestedType)
            ? InferTypeFromClipPath(clip)
            : suggestedType.Trim();

        ApplyTypeSelection(resolvedType);
        _createAnimationAsset = true;
        _animationTemplate = clip;
        // The workbench may hold unsaved events for an imported FBX subclip.
        // Carry that draft into the newly created animation instead of silently discarding it.
        _animationEvents = draftEvents;
        if (draftEvents != null)
        {
            _attackCount = Math.Max(1, draftEvents.Count(SKillAnalyzer.IsAttackStartEvent));
            _estimatedDamage = _attackCount * _attackWeight;
        }
        EvaluateResourceAvailability();

        _messageType = MessageType.Info;
        _message = $"已根据 Clip `{clip.name}` 自动填充技能创建表单。"
            + (draftEvents != null ? "未保存的事件草稿会写入新动画；请使用新的 REAL_NAME。" : string.Empty);
    }

    private void DrawIdSection()
    {
        EditorGUILayout.LabelField("记录 ID", EditorStyles.boldLabel);
        _autoId = EditorGUILayout.ToggleLeft("自动生成递增 ID", _autoId);
        EditorGUI.BeginDisabledGroup(_autoId);
        _recordId = EditorGUILayout.TextField("Record ID", _recordId);
        EditorGUI.EndDisabledGroup();

        if (_autoId)
        {
            EditorGUILayout.LabelField("下一可用 ID", GetCandidateId());
        }
    }

    private void DrawSkillConfigSection()
    {
        EditorGUILayout.LabelField("技能基础配置（mst_skill.csv）", EditorStyles.boldLabel);

        _realName = EditorGUILayout.TextField("REAL_NAME（动画名）", _realName);

        if (_types.Count > 0)
        {
            EditorGUI.BeginChangeCheck();
            var newIndex = EditorGUILayout.Popup("已存在的类型", _selectedTypeIndex, _types.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                _selectedTypeIndex = Mathf.Clamp(newIndex, 0, _types.Count - 1);
                _type = _types[_selectedTypeIndex];
            }
        }

        _type = EditorGUILayout.TextField("USEABLE_MONSTER_TYPE", _type);

        var behaviorIndex = Array.IndexOf(BehaviorTypeOptions, _behaviorType);
        if (behaviorIndex < 0)
        {
            behaviorIndex = 0;
        }

        behaviorIndex = EditorGUILayout.Popup("技能行为类型", behaviorIndex, BehaviorTypeLabels);
        _behaviorType = BehaviorTypeOptions[behaviorIndex];

        _spLevel = EditorGUILayout.IntSlider("SP_LEVEL", _spLevel, 0, 3);
        _attackWeight = EditorGUILayout.FloatField("ATTACK_WEIGHT", _attackWeight);
        _hpWeight = EditorGUILayout.FloatField("HP_WEIGHT", _hpWeight);
        _eventCode = EditorGUILayout.TextField("EVENT_CODE（可选）", _eventCode);
        _allowSharedRealName = EditorGUILayout.ToggleLeft("允许同类型复用 REAL_NAME（高级用法）", _allowSharedRealName);
    }

    private void DrawAiSection()
    {
        EditorGUILayout.LabelField("AI 触发参数（skill_ai_attrs.csv）", EditorStyles.boldLabel);
        _aiMin = EditorGUILayout.FloatField("TRIGGER_DIS_MIN", _aiMin);
        _aiMax = EditorGUILayout.FloatField("TRIGGER_DIS_MAX", _aiMax);
        _aiHeight = EditorGUILayout.IntSlider("TRIGGER_HEIGHT (-1 ~ 2)", _aiHeight, -1, 2);
    }

    private void DrawDisplaySection()
    {
        EditorGUILayout.LabelField("多语言显示（skill_name.csv）", EditorStyles.boldLabel);
        _enName = EditorGUILayout.TextField("英文名", _enName);
        _jpName = EditorGUILayout.TextField("日文名", _jpName);
        _cnName = EditorGUILayout.TextField("中文名", _cnName);

        EditorGUILayout.LabelField("技能简介", EditorStyles.boldLabel);
        _enIntro = EditorGUILayout.TextArea(_enIntro, GUILayout.Height(48f));
        _jpIntro = EditorGUILayout.TextArea(_jpIntro, GUILayout.Height(48f));
        _cnIntro = EditorGUILayout.TextArea(_cnIntro, GUILayout.Height(48f));
    }

    private void DrawStaticAnalysisSection()
    {
        EditorGUILayout.LabelField("能力展示参考（SkillStaticAnalysis.csv）", EditorStyles.boldLabel);
        _estimatedDamage = EditorGUILayout.FloatField("估算攻击力", _estimatedDamage);
        _attackCount = EditorGUILayout.IntField("Attack Count", _attackCount);
        _estimatedHp = EditorGUILayout.FloatField("HP 权重", _estimatedHp);
    }

    private void DrawResourceCreationSection()
    {
        EditorGUILayout.LabelField("资源骨架生成", EditorStyles.boldLabel);

        using (new EditorGUILayout.VerticalScope("box"))
        {
            _createIconAsset = EditorGUILayout.ToggleLeft("自动生成/复制技能图标", _createIconAsset);
            EditorGUI.BeginDisabledGroup(!_createIconAsset);
            _iconTemplate = EditorGUILayout.ObjectField("图标模板（可选）", _iconTemplate, typeof(Texture2D), false) as Texture2D;
            _placeholderIconColor = EditorGUILayout.ColorField("占位图标颜色", _placeholderIconColor);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            _createAnimationAsset = EditorGUILayout.ToggleLeft("自动生成/复制技能动画", _createAnimationAsset);
            EditorGUI.BeginDisabledGroup(!_createAnimationAsset);
            EditorGUI.BeginChangeCheck();
            _animationTemplate = EditorGUILayout.ObjectField("动画模板（可选）", _animationTemplate, typeof(AnimationClip), false) as AnimationClip;
            if (EditorGUI.EndChangeCheck()) _animationEvents = null;
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            _registerAddressables = EditorGUILayout.ToggleLeft("自动注册 Addressables", _registerAddressables);
        }
    }

    private void DrawResourceValidationSection()
    {
        EditorGUILayout.LabelField("资源检查", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope("box"))
        {
            DrawIconStatus();
            EditorGUILayout.Space();
            DrawAnimationStatus();
            EditorGUILayout.Space();
            DrawAddressableStatus();
        }
    }

    private void DrawSummarySection()
    {
        var candidateId = GetCandidateId();
        var iconTargetPath = ResolveIconAssetPath(candidateId);
        var animationTargetPath = ResolveAnimationAssetPath();
        var animationAddress = BuildAnimationAddress((_type ?? string.Empty).Trim(), (_realName ?? string.Empty).Trim());

        EditorGUILayout.LabelField("生成摘要", EditorStyles.boldLabel);
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("技能 ID", string.IsNullOrEmpty(candidateId) ? "<未填写>" : candidateId);
            EditorGUILayout.LabelField("图标资源", string.IsNullOrEmpty(iconTargetPath) ? "<未确定>" : iconTargetPath);
            EditorGUILayout.LabelField("动画资源", string.IsNullOrEmpty(animationTargetPath) ? "<未确定>" : animationTargetPath);
            EditorGUILayout.LabelField("图标 Address", string.IsNullOrEmpty(candidateId) ? "<未确定>" : candidateId);
            EditorGUILayout.LabelField("动画 Address", string.IsNullOrEmpty(animationAddress) ? "<未确定>" : animationAddress);
        }
    }

    /// <summary>
    /// Creates a complete skill using the same transaction as the editor form.
    /// Call on Unity's main thread. Throws on validation or creation failure.
    /// </summary>
    public static SkillCreationResult Create(SkillCreationRequest request)
    {
        return FromRequest(request).CreateSkillTransaction();
    }

    /// <summary>Checks a request against the current project without writing any files.</summary>
    public static string ValidateRequest(SkillCreationRequest request)
    {
        try
        {
            var panel = FromRequest(request);
            panel.RefreshIdsForCreation();
            panel.EvaluateResourceAvailability();
            return panel.ValidateInputs(panel.GetCandidateId());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private static SkillCreationPanel FromRequest(SkillCreationRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var panel = new SkillCreationPanel
        {
            _autoId = request.autoId,
            _recordId = request.recordId,
            _type = request.type,
            _realName = request.realName,
            _behaviorType = request.behaviorType,
            _spLevel = request.spLevel,
            _attackWeight = request.attackWeight,
            _hpWeight = request.hpWeight,
            _eventCode = request.eventCode,
            _allowSharedRealName = request.allowSharedRealName,
            _aiMin = request.aiMin,
            _aiMax = request.aiMax,
            _aiHeight = request.aiHeight,
            _enName = request.enName,
            _jpName = request.jpName,
            _cnName = request.cnName,
            _enIntro = request.enIntro,
            _jpIntro = request.jpIntro,
            _cnIntro = request.cnIntro,
            _estimatedDamage = request.estimatedDamage,
            _attackCount = request.attackCount,
            _estimatedHp = request.estimatedHp,
            _createIconAsset = request.createIconAsset,
            _iconTemplate = request.iconTemplate,
            _createAnimationAsset = request.createAnimationAsset,
            _animationTemplate = request.animationTemplate,
            _animationEvents = request.animationEvents,
            _registerAddressables = request.registerAddressables,
            _placeholderIconColor = request.placeholderIconColor
        };
        return panel;
    }

    private void CreateSkillEntry()
    {
        try
        {
            var result = CreateSkillTransaction();
            ReloadSourceData();
            ClearForm();
            EvaluateResourceAvailability();
            _messageType = MessageType.Info;
            _message = $"已新增技能 {result.recordId}：CSV、资源骨架和 Addressables 已同步。";
        }
        catch (Exception ex)
        {
            Debug.LogError($"SkillCreationPanel 创建技能失败: {ex}");
            _messageType = MessageType.Error;
            _message = ex.Message;
        }
    }

    private SkillCreationResult CreateSkillTransaction()
    {
        // Re-read every table at commit time: the form may have been open while another
        // tool added a skill, and auxiliary tables may contain IDs absent from mst_skill.
        RefreshIdsForCreation();
        var candidateId = GetCandidateId();
        EvaluateResourceAvailability();
        var validationMessage = ValidateInputs(candidateId);
        if (!string.IsNullOrEmpty(validationMessage))
        {
            throw new ArgumentException(validationMessage);
        }

        var realName = (_realName ?? string.Empty).Trim();
        var type = (_type ?? string.Empty).Trim();
        var iconPath = ResolveIconAssetPath(candidateId);
        var animationPath = ResolveAnimationAssetPath();
        var csvBackups = BackupCsvFiles();
        var createdAssets = new List<string>();
        var addressableBackups = new List<AddressableEntryBackup>();
        var settings = _registerAddressables ? AddressableAssetSettingsDefaultObject.Settings : null;
        var originalLabels = settings != null ? settings.GetLabels().ToArray() : Array.Empty<string>();

        try
        {
            CreateOrReuseIcon(iconPath, createdAssets);
            CreateOrReuseAnimation(animationPath, createdAssets);
            if (_registerAddressables)
            {
                RegisterAssetToAddressables(iconPath, SkillIconGroupName, candidateId, new[] { AddressablesResourcePolicy.SkillIconLabel }, addressableBackups);
                RegisterAssetToAddressables(animationPath, SkillAnimationGroupName, BuildAnimationAddress(type, realName), new[] { SkillAnimationLabel }, addressableBackups);
            }

            AppendSkillRows(candidateId, realName, type);
            SaveAddressableChanges(addressableBackups);
            foreach (var path in createdAssets)
            {
                var asset = AssetDatabase.LoadMainAssetAtPath(path);
                if (asset != null)
                {
                    AssetDatabase.SaveAssetIfDirty(asset);
                }
            }
            AssetDatabase.Refresh();
            return new SkillCreationResult
            {
                recordId = candidateId,
                iconAssetPath = iconPath,
                animationAssetPath = animationPath,
                animationAddress = BuildAnimationAddress(type, realName)
            };
        }
        catch (Exception creationError)
        {
            // Attempt every part of rollback even if one part itself fails.
            var rollbackErrors = new List<Exception>();
            AttemptRollback(() => RestoreCsvFiles(csvBackups), rollbackErrors);
            AttemptRollback(() => RestoreAddressableEntries(addressableBackups, originalLabels), rollbackErrors);
            AttemptRollback(() => DeleteCreatedAssets(createdAssets), rollbackErrors);
            AttemptRollback(() => AssetDatabase.Refresh(), rollbackErrors);
            if (rollbackErrors.Count > 0)
            {
                rollbackErrors.Insert(0, creationError);
                throw new AggregateException("创建技能失败；部分回滚失败，请查看内部异常。", rollbackErrors);
            }
            throw new InvalidOperationException($"创建技能失败，已执行回滚：{creationError.Message}", creationError);
        }
    }

    private static void AttemptRollback(Action rollback, ICollection<Exception> errors)
    {
        try
        {
            rollback();
        }
        catch (Exception ex)
        {
            errors.Add(ex);
        }
    }

    private void RefreshIdsForCreation()
    {
        _existingIds.Clear();
        _maxRecordId = 0;
        var expectedColumns = new[] { 8, 4, 7, 6 };
        for (var table = 0; table < RequiredCsvPaths.Length; table++)
        {
            var path = RequiredCsvPaths[table];
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"缺少技能配置表：{path}", path);
            }
            var grid = CsvParser2.Parse(File.ReadAllText(path));
            if (grid.Length == 0 || grid[0].Length != expectedColumns[table]
                || !(string.Equals(grid[0][0].Trim(), "id", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(grid[0][0].Trim(), "RECORD_ID", StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidDataException($"技能配置表表头不匹配：{path}");
            }
            for (var i = 1; i < grid.Length; i++)
            {
                if (grid[i].Length == 0 || string.IsNullOrWhiteSpace(grid[i][0]))
                {
                    continue;
                }
                if (int.TryParse(grid[i][0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var id) && id > 0)
                {
                    _existingIds.Add(id.ToString(CultureInfo.InvariantCulture));
                    _maxRecordId = Math.Max(_maxRecordId, id);
                }
            }
        }
        if (_autoId && _maxRecordId == int.MaxValue)
        {
            throw new InvalidOperationException("技能 ID 已达到最大值，无法自动递增。");
        }
    }

    private void AppendSkillRows(string candidateId, string realName, string type)
    {
        var attackTypeCode = MapBehaviorTypeToCode(_behaviorType);
        var attackWeight = FormatFloat(_attackWeight);
        var hpWeight = FormatFloat(_hpWeight);
        var eventCode = (_eventCode ?? string.Empty).Trim();
        var aiMin = FormatFloat(Mathf.Max(0f, _aiMin));
        var aiMax = FormatFloat(Mathf.Max(float.Parse(aiMin, CultureInfo.InvariantCulture), _aiMax));
        var aiHeight = Mathf.Clamp(_aiHeight, -1, 2).ToString(CultureInfo.InvariantCulture);

        var enName = string.IsNullOrWhiteSpace(_enName) ? realName : _enName.Trim();
        var jpName = string.IsNullOrWhiteSpace(_jpName) ? enName : _jpName.Trim();
        var cnName = string.IsNullOrWhiteSpace(_cnName) ? enName : _cnName.Trim();

        AppendCsvRow(SkillConfigCsvPath, SerializeRow(new[]
        {
            candidateId,
            realName,
            type,
            _spLevel.ToString(CultureInfo.InvariantCulture),
            attackWeight,
            hpWeight,
            attackTypeCode,
            eventCode
        }));

        AppendCsvRow(SkillAiCsvPath, SerializeRow(new[]
        {
            candidateId,
            aiMin,
            aiMax,
            aiHeight
        }));

        AppendCsvRow(SkillNameCsvPath, SerializeRow(new[]
        {
            candidateId,
            enName,
            jpName,
            cnName,
            _enIntro ?? string.Empty,
            _jpIntro ?? string.Empty,
            _cnIntro ?? string.Empty
        }));

        AppendCsvRow(SkillStaticCsvPath, SerializeRow(new[]
        {
            candidateId,
            realName,
            _spLevel.ToString(CultureInfo.InvariantCulture),
            FormatFloat(Mathf.Max(0f, _estimatedDamage)),
            Mathf.Max(1, _attackCount).ToString(CultureInfo.InvariantCulture),
            FormatFloat(Mathf.Max(0f, _estimatedHp))
        }));
    }

    private string ValidateInputs(string candidateId)
    {
        var issues = new List<string>();

        if (string.IsNullOrWhiteSpace(candidateId))
        {
            issues.Add("Record ID 必须填写。");
        }
        else if (!int.TryParse(candidateId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedId) || parsedId <= 0)
        {
            issues.Add("Record ID 必须是正整数。");
        }
        else if (parsedId.ToString(CultureInfo.InvariantCulture) != candidateId)
        {
            issues.Add("Record ID 必须使用不含前导零或符号的十进制正整数。");
        }
        else if (_existingIds.Contains(candidateId))
        {
            issues.Add($"Record ID {candidateId} 已存在于技能配置表。");
        }

        if (CsvContainsId(SkillAiCsvPath, candidateId))
        {
            issues.Add($"Record ID {candidateId} 已存在于 skill_ai_attrs.csv。");
        }

        if (CsvContainsId(SkillNameCsvPath, candidateId))
        {
            issues.Add($"Record ID {candidateId} 已存在于 skill_name.csv。");
        }

        if (CsvContainsId(SkillStaticCsvPath, candidateId))
        {
            issues.Add($"Record ID {candidateId} 已存在于 SkillStaticAnalysis.csv。");
        }

        var realName = (_realName ?? string.Empty).Trim();
        var type = (_type ?? string.Empty).Trim();

        if (!IsSafeAssetName(realName))
        {
            issues.Add("REAL_NAME 必须是安全的单个文件名，不能包含路径分隔符、控制字符或保留字符。");
        }

        if (!IsSafeAssetName(type))
        {
            issues.Add("USEABLE_MONSTER_TYPE 必须是安全的单个目录名。");
        }
        if (!BehaviorTypeOptions.Contains(_behaviorType))
        {
            issues.Add("技能行为类型无效。");
        }
        if (_spLevel < 0 || _spLevel > 3)
        {
            issues.Add("SP_LEVEL 必须在 0 到 3 之间。");
        }
        if (_aiHeight < -1 || _aiHeight > 2)
        {
            issues.Add("TRIGGER_HEIGHT 必须在 -1 到 2 之间。");
        }
        if (new[] { _attackWeight, _hpWeight, _aiMin, _aiMax, _estimatedDamage, _estimatedHp,
            _placeholderIconColor.r, _placeholderIconColor.g, _placeholderIconColor.b, _placeholderIconColor.a }.Any(value => !IsFinite(value)))
        {
            issues.Add("数值参数必须是有限数，不能为 NaN 或 Infinity。");
        }
        if (_aiMin < 0f || _aiMax < 0f || _estimatedDamage < 0f || _estimatedHp < 0f)
        {
            issues.Add("AI 距离、估算攻击力及 HP 权重不能为负数。");
        }
        if (_animationEvents != null)
        {
            if (_animationExists || !_createAnimationAsset || _animationTemplate == null)
            {
                issues.Add("提供动画事件时必须从动画模板创建全新动画，不能修改或复用已有动画。");
            }
            else
            {
                foreach (var animationEvent in _animationEvents)
                {
                    if (animationEvent == null || !IsFinite(animationEvent.time) || !IsFinite(animationEvent.floatParameter)
                        || animationEvent.time < 0f || animationEvent.time > _animationTemplate.length
                        || string.IsNullOrWhiteSpace(animationEvent.functionName))
                    {
                        issues.Add("动画事件必须具有有效函数名、有限参数，且时间位于模板片段范围内。");
                        break;
                    }
                }
            }
        }

        if (!_allowSharedRealName && CsvContainsTypeAndRealName(SkillConfigCsvPath, type, realName))
        {
            issues.Add($"同类型下已存在 REAL_NAME `{realName}`。如需复用同一动画，请显式勾选“允许同类型复用 REAL_NAME”。");
        }

        if (_aiMin > _aiMax)
        {
            issues.Add("AI 最小触发距离不能大于最大值。");
        }

        if (_attackWeight <= 0f)
        {
            issues.Add("ATTACK_WEIGHT 必须大于 0。");
        }

        if (_hpWeight <= 0f)
        {
            issues.Add("HP_WEIGHT 必须大于 0。");
        }

        if (_attackCount <= 0)
        {
            issues.Add("Attack Count 必须至少为 1。");
        }

        if (!_iconExists && !_createIconAsset)
        {
            issues.Add("未检测到技能图标，且未开启自动生成图标。");
        }

        if (!_animationExists && !_createAnimationAsset)
        {
            issues.Add("未检测到技能动画，且未开启自动生成动画。");
        }

        if (_registerAddressables)
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                issues.Add("未找到 AddressableAssetSettings。");
            }
            else
            {
                if (settings.FindGroup(SkillIconGroupName) == null)
                {
                    issues.Add($"未找到 Addressables 分组 `{SkillIconGroupName}`。");
                }

                if (settings.FindGroup(SkillAnimationGroupName) == null)
                {
                    issues.Add($"未找到 Addressables 分组 `{SkillAnimationGroupName}`。");
                }
                ValidateAddressCollision(settings, ResolveIconAssetPath(candidateId), candidateId, issues);
                ValidateAddressCollision(settings, ResolveAnimationAssetPath(), BuildAnimationAddress(type, realName), issues);
            }
        }

        if (!_iconExists && AssetPathOccupied(ResolveIconAssetPath(candidateId)))
        {
            issues.Add("目标图标路径已被其他文件或资源占用。");
        }
        if (!_animationExists && AssetPathOccupied(ResolveAnimationAssetPath()))
        {
            issues.Add("目标动画路径已被其他文件或资源占用。");
        }

        if (issues.Count == 0 && _autoId)
        {
            _recordId = candidateId;
        }

        return issues.Count == 0 ? string.Empty : string.Join("\n", issues);
    }

    private static bool IsFinite(float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }

    private static bool IsSafeAssetName(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && value != "." && value != ".."
            && value.Length <= 120 && !value.EndsWith(".", StringComparison.Ordinal)
            && value.IndexOfAny(new[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' }) < 0
            && !value.Any(char.IsControl);
    }

    private static bool AssetPathOccupied(string path)
    {
        return !string.IsNullOrEmpty(path) && (File.Exists(path) || Directory.Exists(path)
            || File.Exists(path + ".meta") || !string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)));
    }

    private static void ValidateAddressCollision(AddressableAssetSettings settings, string assetPath, string address, ICollection<string> issues)
    {
        var guid = AssetDatabase.AssetPathToGUID(assetPath);
        foreach (var group in settings.groups)
        {
            if (group == null)
            {
                continue;
            }
            foreach (var entry in group.entries)
            {
                if (entry.address == address && entry.guid != guid)
                {
                    issues.Add($"Addressables 地址 `{address}` 已被资源 `{entry.AssetPath}` 占用。");
                }
            }
        }
        var existingEntry = string.IsNullOrEmpty(guid) ? null : settings.FindAssetEntry(guid);
        if (existingEntry != null && existingEntry.address != address)
        {
            issues.Add($"复用资源 `{assetPath}` 已使用另一个 Addressables 地址 `{existingEntry.address}`，不能改写。");
        }
    }

    private void ReloadSourceData()
    {
        var previousType = _type;
        _existingIds.Clear();
        _types.Clear();
        _maxRecordId = 0;

        if (!File.Exists(SkillConfigCsvPath))
        {
            _messageType = MessageType.Error;
            _message = $"未找到 {SkillConfigCsvPath}。";
            return;
        }

        try
        {
            var grid = CsvParser2.Parse(File.ReadAllText(SkillConfigCsvPath));
            for (var i = 1; i < grid.Length; i++)
            {
                var row = grid[i];
                if (row.Length == 0)
                {
                    continue;
                }

                var idCell = row[0]?.Trim();
                if (!string.IsNullOrEmpty(idCell))
                {
                    _existingIds.Add(idCell);
                    if (int.TryParse(idCell, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedId) && parsedId > _maxRecordId)
                    {
                        _maxRecordId = parsedId;
                    }
                }

                if (row.Length > 2)
                {
                    var typeCell = row[2]?.Trim();
                    if (!string.IsNullOrEmpty(typeCell) && !_types.Contains(typeCell))
                    {
                        _types.Add(typeCell);
                    }
                }
            }

            _types.Sort(StringComparer.Ordinal);
            _maxRecordId = Mathf.Max(
                _maxRecordId,
                GetMaxIdFromCsv(SkillAiCsvPath),
                GetMaxIdFromCsv(SkillNameCsvPath),
                GetMaxIdFromCsv(SkillStaticCsvPath));

            if (_types.Count > 0)
            {
                var matchIndex = _types.IndexOf(previousType);
                if (matchIndex < 0)
                {
                    matchIndex = 0;
                }

                _selectedTypeIndex = matchIndex;
                _type = _types[_selectedTypeIndex];
            }

            if (_autoId || string.IsNullOrWhiteSpace(_recordId))
            {
                _recordId = (_maxRecordId + 1).ToString(CultureInfo.InvariantCulture);
            }

            EvaluateResourceAvailability();
            _messageType = MessageType.Info;
            _message = $"已读取 {_existingIds.Count} 条技能记录，下一可用 ID 为 {_recordId}。";
        }
        catch (Exception ex)
        {
            Debug.LogError($"SkillCreationPanel 读取配置失败: {ex}");
            _messageType = MessageType.Error;
            _message = $"读取技能配置失败：{ex.Message}";
        }
    }

    private void ClearForm()
    {
        _realName = string.Empty;
        _behaviorType = BehaviorType.GR;
        _spLevel = 0;
        _attackWeight = 1f;
        _hpWeight = 3f;
        _eventCode = string.Empty;
        _allowSharedRealName = false;

        _aiMin = 0.2f;
        _aiMax = 5f;
        _aiHeight = 0;

        _enName = string.Empty;
        _jpName = string.Empty;
        _cnName = string.Empty;
        _enIntro = string.Empty;
        _jpIntro = string.Empty;
        _cnIntro = string.Empty;

        _estimatedDamage = 1f;
        _attackCount = 1;
        _estimatedHp = 3f;

        _createIconAsset = true;
        _iconTemplate = null;
        _createAnimationAsset = true;
        _animationTemplate = null;
        _animationEvents = null;
        _registerAddressables = true;
        _placeholderIconColor = new Color32(78, 88, 110, 255);

        if (_autoId)
        {
            _recordId = (_maxRecordId + 1).ToString(CultureInfo.InvariantCulture);
        }
    }

    private void ApplyTypeSelection(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            return;
        }

        _type = type.Trim();
        var matchIndex = _types.IndexOf(_type);
        if (matchIndex >= 0)
        {
            _selectedTypeIndex = matchIndex;
        }
    }

    private void EvaluateResourceAvailability()
    {
        _iconExists = false;
        _iconAssetPath = string.Empty;
        _animationExists = false;
        _animationAssetPath = string.Empty;
        _animationSearchNote = string.Empty;

        var candidateId = GetCandidateId();
        if (!string.IsNullOrEmpty(candidateId))
        {
            _iconExists = TryFindSkillIconAsset(candidateId, out _iconAssetPath);
        }

        var realName = (_realName ?? string.Empty).Trim();
        var type = (_type ?? string.Empty).Trim();
        if (!string.IsNullOrEmpty(realName))
        {
            _animationExists = TryFindSkillAnimationAsset(realName, type, out _animationAssetPath, out _animationSearchNote);
        }
    }

    private void DrawIconStatus()
    {
        var candidateId = GetCandidateId();
        if (string.IsNullOrEmpty(candidateId))
        {
            EditorGUILayout.HelpBox("Record ID 未填写，无法检查图标资源。", MessageType.Info);
            return;
        }

        if (_iconExists)
        {
            EditorGUILayout.HelpBox($"已找到技能图标：{_iconAssetPath}", MessageType.Info);
            DrawAssetLocator("图标资源路径", _iconAssetPath);
            return;
        }

        if (_createIconAsset)
        {
            EditorGUILayout.HelpBox($"未找到现有图标，将在 `{ResolveIconAssetPath(candidateId)}` 创建新图标。", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.HelpBox($"未在 {SkillIconFolder} 下找到名称为 {candidateId} 的技能图标。", MessageType.Warning);
        }
    }

    private void DrawAnimationStatus()
    {
        var realName = (_realName ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(realName))
        {
            EditorGUILayout.HelpBox("REAL_NAME 未填写，无法检查技能动画。", MessageType.Info);
            return;
        }

        if (_animationExists)
        {
            EditorGUILayout.HelpBox($"已找到技能动画：{_animationAssetPath}", MessageType.Info);
            DrawAssetLocator("动画资源路径", _animationAssetPath);
            if (!string.IsNullOrEmpty(_animationSearchNote))
            {
                EditorGUILayout.HelpBox(_animationSearchNote, MessageType.None);
            }
            return;
        }

        if (_createAnimationAsset)
        {
            EditorGUILayout.HelpBox($"未找到现有动画，将在 `{ResolveAnimationAssetPath()}` 创建新动画。", MessageType.Warning);
        }
        else
        {
            var note = string.IsNullOrEmpty(_animationSearchNote) ? "未找到匹配动画资源。" : _animationSearchNote;
            EditorGUILayout.HelpBox(note, MessageType.Warning);
        }
    }

    private void DrawAddressableStatus()
    {
        if (!_registerAddressables)
        {
            EditorGUILayout.HelpBox("未启用 Addressables 自动注册。", MessageType.Info);
            return;
        }

        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            EditorGUILayout.HelpBox("未找到 AddressableAssetSettings。", MessageType.Error);
            return;
        }

        var hasIconGroup = settings.FindGroup(SkillIconGroupName) != null;
        var hasAnimationGroup = settings.FindGroup(SkillAnimationGroupName) != null;

        if (hasIconGroup && hasAnimationGroup)
        {
            EditorGUILayout.HelpBox($"将自动注册到 `{SkillIconGroupName}` 与 `{SkillAnimationGroupName}`。", MessageType.Info);
        }
        else
        {
            var missingGroups = new List<string>();
            if (!hasIconGroup)
            {
                missingGroups.Add(SkillIconGroupName);
            }

            if (!hasAnimationGroup)
            {
                missingGroups.Add(SkillAnimationGroupName);
            }

            EditorGUILayout.HelpBox($"缺少 Addressables 分组：{string.Join(", ", missingGroups)}", MessageType.Error);
        }
    }

    private static void DrawAssetLocator(string label, string assetPath)
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField(label, assetPath);
            if (GUILayout.Button("定位", GUILayout.Width(70f)))
            {
                var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
                if (asset != null)
                {
                    Selection.activeObject = asset;
                    EditorGUIUtility.PingObject(asset);
                }
            }
        }
    }

    private void CreateOrReuseIcon(string targetPath, ICollection<string> createdAssets)
    {
        if (_iconExists && !string.IsNullOrEmpty(_iconAssetPath))
        {
            return;
        }

        if (string.IsNullOrEmpty(targetPath))
        {
            throw new InvalidOperationException("无法确定技能图标路径。");
        }

        if (AssetPathOccupied(targetPath))
        {
            throw new IOException($"图标目标路径已存在：{targetPath}");
        }
        EnsureAssetFolder(targetPath, createdAssets);
        // Track before writing: CopyAsset/import can leave partial files on failure.
        createdAssets.Add(targetPath);
        if (_iconTemplate != null)
        {
            var templatePath = AssetDatabase.GetAssetPath(_iconTemplate);
            if (string.IsNullOrEmpty(templatePath) || !(AssetImporter.GetAtPath(templatePath) is TextureImporter))
            {
                throw new InvalidOperationException("图标模板必须是已导入的图像文件。");
            }

            if (!AssetDatabase.CopyAsset(templatePath, targetPath))
            {
                throw new IOException($"复制图标模板失败：{templatePath} -> {targetPath}");
            }
        }
        else
        {
            CreatePlaceholderIcon(targetPath, _placeholderIconColor);
        }

        AssetDatabase.ImportAsset(targetPath, ImportAssetOptions.ForceSynchronousImport);
        ConfigureTextureAsSprite(targetPath);
    }

    private void CreateOrReuseAnimation(string targetPath, ICollection<string> createdAssets)
    {
        if (_animationExists && !string.IsNullOrEmpty(_animationAssetPath))
        {
            return;
        }

        if (string.IsNullOrEmpty(targetPath))
        {
            throw new InvalidOperationException("无法确定技能动画路径。");
        }

        if (AssetPathOccupied(targetPath))
        {
            throw new IOException($"动画目标路径已存在：{targetPath}");
        }
        EnsureAssetFolder(targetPath, createdAssets);
        createdAssets.Add(targetPath);
        AnimationClip clone = null;
        try
        {
            // FBX files can contain several clips. Clone the selected subasset, never
            // CopyAsset its containing model into an .anim file.
            clone = _animationTemplate != null
                ? UnityEngine.Object.Instantiate(_animationTemplate)
                : new AnimationClip();
            clone.name = (_realName ?? string.Empty).Trim();
            clone.hideFlags = HideFlags.None;
            if (_animationEvents != null)
            {
                AnimationUtility.SetAnimationEvents(clone, _animationEvents);
                var clipSettings = AnimationUtility.GetAnimationClipSettings(clone);
                clipSettings.loopTime = false;
                AnimationUtility.SetAnimationClipSettings(clone, clipSettings);
            }
            AssetDatabase.CreateAsset(clone, targetPath);
            AssetDatabase.ImportAsset(targetPath, ImportAssetOptions.ForceSynchronousImport);
            if (AssetDatabase.LoadAssetAtPath<AnimationClip>(targetPath) == null)
            {
                throw new IOException($"生成的动画无法加载：{targetPath}");
            }
        }
        finally
        {
            if (clone != null && !EditorUtility.IsPersistent(clone))
            {
                UnityEngine.Object.DestroyImmediate(clone);
            }
        }
    }

    private void RegisterAssetToAddressables(string assetPath, string groupName, string address, IEnumerable<string> labels, ICollection<AddressableEntryBackup> backups)
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            throw new InvalidOperationException("AddressableAssetSettings 不存在。");
        }

        var group = settings.FindGroup(groupName);
        if (group == null)
        {
            throw new InvalidOperationException($"Addressables 分组不存在：{groupName}");
        }

        var guid = AssetDatabase.AssetPathToGUID(assetPath);
        if (string.IsNullOrEmpty(guid))
        {
            throw new InvalidOperationException($"资源尚未被 Unity 导入：{assetPath}");
        }

        var collisionIssues = new List<string>();
        ValidateAddressCollision(settings, assetPath, address, collisionIssues);
        if (collisionIssues.Count > 0)
        {
            throw new InvalidOperationException(string.Join("\n", collisionIssues));
        }
        var oldGroup = settings.FindAssetEntry(guid)?.parentGroup;
        backups.Add(CreateAddressableBackup(settings, guid));

        var entry = settings.CreateOrMoveEntry(guid, group, false, false);
        if (entry == null)
        {
            throw new InvalidOperationException($"Addressables 条目创建失败：{assetPath}");
        }

        entry.SetAddress(address, false);
        foreach (var label in labels)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                continue;
            }

            settings.AddLabel(label, false);
            entry.SetLabel(label, true, true, false);
        }

        if (oldGroup != null)
        {
            EditorUtility.SetDirty(oldGroup);
        }
        EditorUtility.SetDirty(group);
        EditorUtility.SetDirty(settings);
    }

    private static AddressableEntryBackup CreateAddressableBackup(AddressableAssetSettings settings, string guid)
    {
        var entry = settings.FindAssetEntry(guid);
        if (entry == null)
        {
            return new AddressableEntryBackup
            {
                Guid = guid,
                Existed = false
            };
        }

        return new AddressableEntryBackup
        {
            Guid = guid,
            Existed = true,
            GroupName = entry.parentGroup != null ? entry.parentGroup.Name : string.Empty,
            Address = entry.address,
            Labels = entry.labels.ToList()
        };
    }

    private static void RestoreAddressableEntries(IReadOnlyCollection<AddressableEntryBackup> backups, IEnumerable<string> originalLabels)
    {
        if (backups.Count == 0)
        {
            return;
        }
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            throw new InvalidOperationException("回滚时 AddressableAssetSettings 不存在。");
        }
        var errors = new List<Exception>();
        foreach (var backup in backups.Reverse())
        {
            AttemptRollback(() =>
            {
                var currentGroup = settings.FindAssetEntry(backup.Guid)?.parentGroup;
                if (currentGroup != null)
                {
                    EditorUtility.SetDirty(currentGroup);
                }
                if (!backup.Existed)
                {
                    settings.RemoveAssetEntry(backup.Guid, false);
                    return;
                }
                var group = settings.FindGroup(backup.GroupName);
                if (group == null)
                {
                    throw new InvalidOperationException($"无法恢复 Addressables 分组：{backup.GroupName}");
                }
                var entry = settings.CreateOrMoveEntry(backup.Guid, group, false, false);
                if (entry == null)
                {
                    throw new InvalidOperationException($"无法恢复 Addressables 条目：{backup.Guid}");
                }
                entry.SetAddress(backup.Address, false);
                foreach (var label in entry.labels.ToList())
                {
                    entry.SetLabel(label, false, false, false);
                }
                foreach (var label in backup.Labels)
                {
                    entry.SetLabel(label, true, true, false);
                }
                EditorUtility.SetDirty(group);
            }, errors);
        }
        foreach (var label in settings.GetLabels().Except(originalLabels).ToArray())
        {
            AttemptRollback(() => settings.RemoveLabel(label, false), errors);
        }
        EditorUtility.SetDirty(settings);
        AttemptRollback(() => SaveAddressableChanges(backups), errors);
        if (errors.Count > 0)
        {
            throw new AggregateException("恢复 Addressables 失败。", errors);
        }
    }

    private static void SaveAddressableChanges(IEnumerable<AddressableEntryBackup> backups)
    {
        var entries = backups.ToArray();
        if (entries.Length == 0)
        {
            return;
        }
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            throw new InvalidOperationException("AddressableAssetSettings 不存在。");
        }
        var groupNames = entries.Select(x => x.GroupName)
            .Concat(new[] { SkillIconGroupName, SkillAnimationGroupName }).Distinct();
        foreach (var groupName in groupNames)
        {
            var group = settings.FindGroup(groupName);
            if (group != null)
            {
                AssetDatabase.SaveAssetIfDirty(group);
            }
        }
        AssetDatabase.SaveAssetIfDirty(settings);
    }

    private static Dictionary<string, byte[]> BackupCsvFiles()
    {
        var backups = new Dictionary<string, byte[]>();
        foreach (var path in RequiredCsvPaths)
        {
            backups[path] = File.Exists(path) ? File.ReadAllBytes(path) : null;
        }
        return backups;
    }

    private static void RestoreCsvFiles(IReadOnlyDictionary<string, byte[]> backups)
    {
        var errors = new List<Exception>();
        foreach (var pair in backups)
        {
            AttemptRollback(() =>
            {
                if (pair.Value == null)
                {
                    if (File.Exists(pair.Key))
                    {
                        File.Delete(pair.Key);
                    }
                }
                else
                {
                    File.WriteAllBytes(pair.Key, pair.Value);
                }
            }, errors);
        }
        if (errors.Count > 0)
        {
            throw new AggregateException("恢复 CSV 失败。", errors);
        }
    }

    private static void DeleteCreatedAssets(IEnumerable<string> createdAssets)
    {
        var errors = new List<Exception>();
        foreach (var assetPath in createdAssets.Reverse())
        {
            AttemptRollback(() =>
            {
                if (AssetDatabase.DeleteAsset(assetPath))
                {
                    return;
                }
                // A failed import may have left files unknown to AssetDatabase.
                if (File.Exists(assetPath))
                {
                    File.Delete(assetPath);
                }
                if (Directory.Exists(assetPath))
                {
                    Directory.Delete(assetPath, false);
                }
                if (File.Exists(assetPath + ".meta"))
                {
                    File.Delete(assetPath + ".meta");
                }
            }, errors);
        }
        if (errors.Count > 0)
        {
            throw new AggregateException("删除生成资源失败。", errors);
        }
    }

    private static void EnsureAssetFolder(string assetPath, ICollection<string> createdAssets)
    {
        var directoryPath = Path.GetDirectoryName(assetPath);
        if (string.IsNullOrEmpty(directoryPath))
        {
            return;
        }
        var normalized = directoryPath.Replace("\\", "/");
        if (AssetDatabase.IsValidFolder(normalized))
        {
            return;
        }
        var segments = normalized.Split('/');
        var current = segments[0];
        for (var i = 1; i < segments.Length; i++)
        {
            var next = $"{current}/{segments[i]}";
            if (!AssetDatabase.IsValidFolder(next))
            {
                if (AssetPathOccupied(next))
                {
                    throw new IOException($"目标目录路径已被占用：{next}");
                }
                createdAssets.Add(next);
                if (string.IsNullOrEmpty(AssetDatabase.CreateFolder(current, segments[i])))
                {
                    throw new IOException($"创建目录失败：{next}");
                }
            }
            current = next;
        }
    }

    private static void CreatePlaceholderIcon(string assetPath, Color color)
    {
        var texture = new Texture2D(PlaceholderIconSize, PlaceholderIconSize, TextureFormat.RGBA32, false);
        var pixels = Enumerable.Repeat(color, PlaceholderIconSize * PlaceholderIconSize).ToArray();
        texture.SetPixels(pixels);
        texture.Apply();

        var pngBytes = texture.EncodeToPNG();
        UnityEngine.Object.DestroyImmediate(texture);

        if (pngBytes == null || pngBytes.Length == 0)
        {
            throw new InvalidOperationException("占位图标 PNG 生成失败。");
        }

        File.WriteAllBytes(assetPath, pngBytes);
    }

    private static void ConfigureTextureAsSprite(string assetPath)
    {
        var importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (importer == null)
        {
            return;
        }

        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.alphaIsTransparency = true;
        importer.mipmapEnabled = false;
        importer.SaveAndReimport();
    }

    private string GetCandidateId()
    {
        return _autoId ? (_maxRecordId + 1).ToString(CultureInfo.InvariantCulture) : (_recordId ?? string.Empty).Trim();
    }

    private string ResolveIconAssetPath(string recordId)
    {
        if (_iconExists && !string.IsNullOrEmpty(_iconAssetPath))
        {
            return _iconAssetPath;
        }

        if (string.IsNullOrWhiteSpace(recordId))
        {
            return string.Empty;
        }

        var extension = ".png";
        if (_iconTemplate != null)
        {
            var templatePath = AssetDatabase.GetAssetPath(_iconTemplate);
            if (!string.IsNullOrEmpty(templatePath))
            {
                extension = Path.GetExtension(templatePath);
                if (string.IsNullOrEmpty(extension))
                {
                    extension = ".png";
                }
            }
        }

        return $"{SkillIconFolder}/{recordId}{extension}";
    }

    private string ResolveAnimationAssetPath()
    {
        if (_animationExists && !string.IsNullOrEmpty(_animationAssetPath))
        {
            return _animationAssetPath;
        }

        var type = (_type ?? string.Empty).Trim();
        var realName = (_realName ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(realName))
        {
            return string.Empty;
        }

        return $"{SkillAnimationRoot}/{type}/skill/{realName}.anim";
    }

    private static string BuildAnimationAddress(string type, string realName)
    {
        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(realName))
        {
            return string.Empty;
        }

        return $"{type}/skill/{realName}.anim";
    }

    private static string MapBehaviorTypeToCode(BehaviorType behaviorType)
    {
        switch (behaviorType)
        {
            case BehaviorType.GR:
            case BehaviorType.GI:
            case BehaviorType.GM:
            case BehaviorType.GMB:
            case BehaviorType.CT:
            case BehaviorType.RB:
                return behaviorType.ToString();
            default:
                return "NONE";
        }
    }

    private static string FormatFloat(float value)
    {
        return value.ToString(CultureInfo.InvariantCulture);
    }

    private static string SerializeRow(IEnumerable<string> fields)
    {
        var builder = new StringBuilder();
        var first = true;
        foreach (var rawField in fields)
        {
            if (!first)
            {
                builder.Append(',');
            }

            builder.Append(ToCsvField(rawField));
            first = false;
        }

        return builder.ToString();
    }

    private static string ToCsvField(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0)
        {
            var escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        return value;
    }

    private static void AppendCsvRow(string path, string row)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var needsLineBreak = false;
        var newLine = CsvNewLine;
        if (File.Exists(path))
        {
            var existingText = File.ReadAllText(path);
            var firstNewLine = existingText.IndexOf('\n');
            if (firstNewLine >= 0)
                newLine = firstNewLine > 0 && existingText[firstNewLine - 1] == '\r' ? "\r\n" : "\n";
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (stream.Length > 0)
                {
                    stream.Seek(-1, SeekOrigin.End);
                    needsLineBreak = stream.ReadByte() != '\n';
                }
            }
        }

        using (var writer = new StreamWriter(path, true, new UTF8Encoding(false)))
        {
            if (needsLineBreak)
            {
                writer.Write(newLine);
            }

            writer.Write(row);
            writer.Write(newLine);
        }
    }

    private static bool CsvContainsId(string path, string id)
    {
        if (string.IsNullOrWhiteSpace(id) || !File.Exists(path))
        {
            return false;
        }

        try
        {
            var grid = CsvParser2.Parse(File.ReadAllText(path));
            for (var i = 1; i < grid.Length; i++)
            {
                if (grid[i].Length == 0)
                {
                    continue;
                }

                var cell = grid[i][0]?.Trim();
                if (!string.IsNullOrEmpty(cell) && cell == id)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"SkillCreationPanel 校验 ID 失败: {path}\n{ex}");
        }

        return false;
    }

    private static int GetMaxIdFromCsv(string path)
    {
        if (!File.Exists(path))
        {
            return 0;
        }

        try
        {
            var maxId = 0;
            var grid = CsvParser2.Parse(File.ReadAllText(path));
            for (var i = 1; i < grid.Length; i++)
            {
                if (grid[i].Length == 0)
                {
                    continue;
                }

                if (int.TryParse(grid[i][0]?.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedId) && parsedId > maxId)
                {
                    maxId = parsedId;
                }
            }

            return maxId;
        }
        catch (Exception ex)
        {
            Debug.LogError($"SkillCreationPanel 读取最大 ID 失败: {path}\n{ex}");
            return 0;
        }
    }

    private static bool CsvContainsTypeAndRealName(string path, string type, string realName)
    {
        if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(realName) || !File.Exists(path))
        {
            return false;
        }

        try
        {
            var grid = CsvParser2.Parse(File.ReadAllText(path));
            for (var i = 1; i < grid.Length; i++)
            {
                var row = grid[i];
                if (row.Length < 3)
                {
                    continue;
                }

                if (string.Equals(row[1]?.Trim(), realName, StringComparison.Ordinal)
                    && string.Equals(row[2]?.Trim(), type, StringComparison.Ordinal))
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"SkillCreationPanel 校验 REAL_NAME 失败: {path}\n{ex}");
        }

        return false;
    }

    private static bool TryFindSkillIconAsset(string recordId, out string assetPath)
    {
        assetPath = string.Empty;
        if (string.IsNullOrWhiteSpace(recordId))
        {
            return false;
        }

        var guids = AssetDatabase.FindAssets($"{recordId} t:Texture2D", new[] { SkillIconFolder });
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (Path.GetFileNameWithoutExtension(path).Equals(recordId, StringComparison.OrdinalIgnoreCase))
            {
                assetPath = path;
                return true;
            }
        }

        return false;
    }

    private static bool TryFindSkillAnimationAsset(string realName, string type, out string assetPath, out string note)
    {
        assetPath = string.Empty;
        note = string.Empty;
        if (string.IsNullOrWhiteSpace(realName))
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(type))
        {
            var expectedPath = $"{SkillAnimationRoot}/{type}/skill/{realName}.anim";
            var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(expectedPath);
            if (clip != null)
            {
                assetPath = expectedPath;
                return true;
            }

            note = $"默认路径未命中：{expectedPath}";
        }

        if (string.IsNullOrEmpty(note))
        {
            note = "未找到匹配的动画资源。";
        }

        return false;
    }

    private static string InferTypeFromClipPath(AnimationClip clip)
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
        if (!normalized.StartsWith(SkillAnimationRoot + "/", StringComparison.Ordinal)) return string.Empty;
        var parts = normalized.Split('/');
        for (var i = 0; i < parts.Length - 1; i++)
        {
            if (string.Equals(parts[i], "Animations", StringComparison.Ordinal) && i + 1 < parts.Length)
            {
                return parts[i + 1];
            }
        }

        return string.Empty;
    }
}

#endif
