#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Skill;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Provides an editor utility for adding a new skill across the project's CSV-driven tables.
/// </summary>
public class SkillCreationTool : EditorWindow
{
    private const string SkillConfigCsvPath = "Assets/ExternalAssets/Data/Config/mst_skill.csv";
    private const string SkillAiCsvPath = "Assets/ExternalAssets/Data/Config/skill_ai_attrs.csv";
    private const string SkillNameCsvPath = "Assets/ExternalAssets/Data/Config/skill_name.csv";
    private const string SkillStaticCsvPath = "Assets/ExternalAssets/Data/Config/SkillStaticAnalysis.csv";
    private const string SkillIconFolder = "Assets/ExternalAssets/Textures/Icons/Skill";
    private const string SkillAnimationRoot = "Assets/ExternalAssets/Animations";
    private const string CsvNewLine = "\r\n";

    private static readonly BehaviorType[] BehaviorTypeOptions =
    {
        BehaviorType.GR,
        BehaviorType.GI,
        BehaviorType.GM,
        BehaviorType.GMB,
        BehaviorType.CT,
        BehaviorType.RB,
        BehaviorType.NONE
    };

    private static readonly string[] BehaviorTypeLabels = BehaviorTypeOptions.Select(t => t.ToString()).ToArray();
    private readonly Vector2 _minWindowSize = new Vector2(460f, 720f);

    private bool _autoId = true;
    private string _recordId = string.Empty;
    private int _maxRecordId;

    private readonly HashSet<string> _existingIds = new HashSet<string>();
    private readonly List<string> _types = new List<string>();
    private int _selectedTypeIndex;
    private string _type = "human";

    private string _realName = string.Empty;
    private BehaviorType _behaviorType = BehaviorType.GR;
    private int _spLevel;
    private float _attackWeight = 1f;
    private float _hpWeight = 1f;
    private string _eventCode = string.Empty;

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

    private Vector2 _scroll;
    private MessageType _messageType = MessageType.Info;
    private string _message = string.Empty;
    private bool _iconExists;
    private string _iconAssetPath = string.Empty;
    private bool _animExists;
    private string _animAssetPath = string.Empty;
    private string _animSearchNote = string.Empty;

    [MenuItem("Tools/Skill Creation Tool")]
    private static void Open()
    {
        var window = GetWindow<SkillCreationTool>("Skill Creator");
        window.minSize = window._minWindowSize;
    }

    private void OnEnable()
    {
        minSize = _minWindowSize;
        ReloadSourceData();
    }

    private void OnGUI()
    {
        using (new EditorGUILayout.VerticalScope())
        {
            EditorGUILayout.Space();
            if (!string.IsNullOrEmpty(_message))
            {
                EditorGUILayout.HelpBox(_message, _messageType);
            }

            if (GUILayout.Button("重新读取配置文件"))
            {
                ReloadSourceData();
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

            EvaluateResourceAvailability();
            DrawResourceValidationSection();
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("该工具会向 mst_skill.csv、skill_ai_attrs.csv、skill_name.csv 和 SkillStaticAnalysis.csv 追加新条目。添加后请按需在版本库中提交更改。", MessageType.Info);

            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_recordId));
            if (GUILayout.Button("生成技能条目"))
            {
                CreateSkillEntry();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();
        }
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
            EditorGUILayout.LabelField("下一可用 ID", _recordId);
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

    private void CreateSkillEntry()
    {
        EvaluateResourceAvailability();
        var candidateId = _autoId ? (_maxRecordId + 1).ToString(CultureInfo.InvariantCulture) : _recordId.Trim();
        var validationMessage = ValidateInputs(candidateId);
        if (!string.IsNullOrEmpty(validationMessage))
        {
            _message = validationMessage;
            _messageType = MessageType.Error;
            return;
        }

        var typeToUse = _type.Trim();
        var realNameToUse = _realName.Trim();
        var attackTypeCode = MapBehaviorTypeToCode(_behaviorType);
        var attackWeightStr = FormatFloat(_attackWeight);
        var hpWeightStr = FormatFloat(_hpWeight);
        var eventCodeStr = _eventCode?.Trim() ?? string.Empty;

        var aiMinStr = FormatFloat(Mathf.Max(0f, _aiMin));
        var aiMaxStr = FormatFloat(Mathf.Max(float.Parse(aiMinStr, CultureInfo.InvariantCulture), _aiMax));
        var aiHeightStr = Mathf.Clamp(_aiHeight, -1, 2).ToString(CultureInfo.InvariantCulture);

        var enName = string.IsNullOrWhiteSpace(_enName) ? realNameToUse : _enName.Trim();
        var jpName = string.IsNullOrWhiteSpace(_jpName) ? enName : _jpName.Trim();
        var cnName = string.IsNullOrWhiteSpace(_cnName) ? enName : _cnName.Trim();

        var enIntro = _enIntro ?? string.Empty;
        var jpIntro = _jpIntro ?? string.Empty;
        var cnIntro = _cnIntro ?? string.Empty;

        var estimatedDamageStr = FormatFloat(Mathf.Max(_estimatedDamage, 0f));
        var attackCountStr = Mathf.Max(_attackCount, 1).ToString(CultureInfo.InvariantCulture);
        var estimatedHpStr = FormatFloat(Mathf.Max(_estimatedHp, 0f));

        try
        {
            AppendCsvRow(SkillConfigCsvPath, SerializeRow(new[]
            {
                candidateId,
                realNameToUse,
                typeToUse,
                _spLevel.ToString(CultureInfo.InvariantCulture),
                attackWeightStr,
                hpWeightStr,
                attackTypeCode,
                eventCodeStr
            }));

            AppendCsvRow(SkillAiCsvPath, SerializeRow(new[]
            {
                candidateId,
                aiMinStr,
                aiMaxStr,
                aiHeightStr
            }));

            AppendCsvRow(SkillNameCsvPath, SerializeRow(new[]
            {
                candidateId,
                enName,
                jpName,
                cnName,
                enIntro,
                jpIntro,
                cnIntro
            }));

            AppendCsvRow(SkillStaticCsvPath, SerializeRow(new[]
            {
                candidateId,
                realNameToUse,
                _spLevel.ToString(CultureInfo.InvariantCulture),
                estimatedDamageStr,
                attackCountStr,
                estimatedHpStr
            }));

            AssetDatabase.Refresh();
            _messageType = MessageType.Info;
            _message = $"成功为技能 {candidateId} 写入配置。";

            ReloadSourceData();
            ClearForm();
        }
        catch (Exception ex)
        {
            Debug.LogError($"SkillCreationTool 写入失败: {ex}");
            _messageType = MessageType.Error;
            _message = $"写入 CSV 时失败: {ex.Message}";
        }
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
            issues.Add("Record ID 必须是一个正整数。");
        }
        else if (_existingIds.Contains(candidateId))
        {
            issues.Add($"Record ID {candidateId} 已存在于 mst_skill.csv 中。");
        }

        if (CsvContainsId(SkillAiCsvPath, candidateId))
        {
            issues.Add($"Record ID {candidateId} 已存在于 skill_ai_attrs.csv 中。");
        }
        if (CsvContainsId(SkillNameCsvPath, candidateId))
        {
            issues.Add($"Record ID {candidateId} 已存在于 skill_name.csv 中。");
        }
        if (CsvContainsId(SkillStaticCsvPath, candidateId))
        {
            issues.Add($"Record ID {candidateId} 已存在于 SkillStaticAnalysis.csv 中。");
        }

        if (string.IsNullOrWhiteSpace(_realName))
        {
            issues.Add("REAL_NAME（动画名）不能为空。");
        }

        if (string.IsNullOrWhiteSpace(_type))
        {
            if (_types.Count > 0)
            {
                _type = _types[Mathf.Clamp(_selectedTypeIndex, 0, _types.Count - 1)];
            }
            else
            {
                issues.Add("USEABLE_MONSTER_TYPE 不能为空。");
            }
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

        if (!string.IsNullOrEmpty(candidateId) && !_iconExists)
        {
            issues.Add($"未检测到技能图标（期望 {SkillIconFolder}/{candidateId}.png）。");
        }
        if (!string.IsNullOrEmpty(_realName) && !_animExists)
        {
            issues.Add($"未检测到 `{_realName}` 对应的动画资源。");
        }

        if (issues.Count > 0)
        {
            return string.Join("\n", issues);
        }

        if (_autoId)
        {
            _recordId = candidateId;
        }

        return string.Empty;
    }

    private void ReloadSourceData()
    {
        var previousType = _type;
        _existingIds.Clear();
        _types.Clear();
        _maxRecordId = 0;

        if (!File.Exists(SkillConfigCsvPath))
        {
            _message = $"未找到 {SkillConfigCsvPath}，请检查项目资源。";
            _messageType = MessageType.Error;
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

            if (_autoId || string.IsNullOrEmpty(_recordId))
            {
                _recordId = (_maxRecordId + 1).ToString(CultureInfo.InvariantCulture);
            }

            _messageType = MessageType.Info;
            _message = $"已读取 {_existingIds.Count} 条技能记录，下一可用 ID 为 {_recordId}。";
        }
        catch (Exception ex)
        {
            Debug.LogError($"SkillCreationTool 读取 {SkillConfigCsvPath} 失败: {ex}");
            _messageType = MessageType.Error;
            _message = $"读取技能配置失败: {ex.Message}";
        }
    }

    private void ClearForm()
    {
        _realName = string.Empty;
        _eventCode = string.Empty;
        _enName = string.Empty;
        _jpName = string.Empty;
        _cnName = string.Empty;
        _enIntro = string.Empty;
        _jpIntro = string.Empty;
        _cnIntro = string.Empty;

        _aiMin = 0.2f;
        _aiMax = 5f;
        _aiHeight = 0;

        _attackWeight = 1f;
        _hpWeight = 1f;

        _estimatedDamage = 1f;
        _attackCount = 1;
        _estimatedHp = 3f;
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
        if (File.Exists(path))
        {
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
                writer.Write(CsvNewLine);
            }
            writer.Write(row);
            writer.Write(CsvNewLine);
        }
    }

    private void EvaluateResourceAvailability()
    {
        _iconExists = false;
        _iconAssetPath = string.Empty;
        _animExists = false;
        _animAssetPath = string.Empty;
        _animSearchNote = string.Empty;

        var recordIdForCheck = (_recordId ?? string.Empty).Trim();
        if (!string.IsNullOrEmpty(recordIdForCheck))
        {
            _iconExists = TryFindSkillIconAsset(recordIdForCheck, out _iconAssetPath);
        }

        var realNameForCheck = (_realName ?? string.Empty).Trim();
        if (!string.IsNullOrEmpty(realNameForCheck))
        {
            var typeForCheck = (_type ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(typeForCheck) && _types.Count > 0)
            {
                typeForCheck = _types[Mathf.Clamp(_selectedTypeIndex, 0, _types.Count - 1)];
            }
            _animExists = TryFindSkillAnimationAsset(realNameForCheck, typeForCheck, out _animAssetPath, out _animSearchNote);
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
        }
    }

    private void DrawIconStatus()
    {
        if (string.IsNullOrEmpty(_recordId))
        {
            EditorGUILayout.HelpBox("Record ID 未填写，无法检查图标资源。", MessageType.Info);
            return;
        }

        if (_iconExists)
        {
            EditorGUILayout.HelpBox($"已找到技能图标：{_iconAssetPath}", MessageType.Info);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("图标资源路径", _iconAssetPath);
                if (GUILayout.Button("定位", GUILayout.Width(70f)))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(_iconAssetPath);
                    if (asset != null)
                    {
                        Selection.activeObject = asset;
                        EditorGUIUtility.PingObject(asset);
                    }
                }
            }
        }
        else
        {
            EditorGUILayout.HelpBox($"未在 {SkillIconFolder} 下找到名称为 {_recordId} 的技能图标，请添加 `{_recordId}.png`（或同名纹理）并标记到 SkillIcon Addressable 组。", MessageType.Warning);
        }
    }

    private void DrawAnimationStatus()
    {
        if (string.IsNullOrEmpty(_realName))
        {
            EditorGUILayout.HelpBox("REAL_NAME 未填写，无法检查技能动画。", MessageType.Info);
            return;
        }

        if (_animExists)
        {
            EditorGUILayout.HelpBox($"已找到技能动画：{_animAssetPath}", MessageType.Info);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("动画资源路径", _animAssetPath);
                if (GUILayout.Button("定位", GUILayout.Width(70f)))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(_animAssetPath);
                    if (asset != null)
                    {
                        Selection.activeObject = asset;
                        EditorGUIUtility.PingObject(asset);
                    }
                }
            }

            if (!string.IsNullOrEmpty(_animSearchNote))
            {
                EditorGUILayout.HelpBox(_animSearchNote, MessageType.None);
            }
        }
        else
        {
            var targetType = string.IsNullOrEmpty(_type) && _types.Count > 0
                ? _types[Mathf.Clamp(_selectedTypeIndex, 0, _types.Count - 1)]
                : _type;

            var expectedPath = string.IsNullOrEmpty(targetType)
                ? "<未知类型目录>"
                : $"{SkillAnimationRoot}/{targetType}/skill/{_realName}.anim";

            var note = string.IsNullOrEmpty(_animSearchNote)
                ? $"未找到 `{_realName}` 对应的动画资源。请在 {expectedPath} 创建动画，并确保已加入 Addressables 的 skill_anim 标签。"
                : _animSearchNote;

            EditorGUILayout.HelpBox(note, MessageType.Warning);
        }
    }

    private static bool TryFindSkillIconAsset(string recordId, out string assetPath)
    {
        assetPath = string.Empty;
        if (string.IsNullOrEmpty(recordId))
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
        if (string.IsNullOrEmpty(realName))
        {
            return false;
        }

        if (!string.IsNullOrEmpty(type))
        {
            var expectedPath = $"{SkillAnimationRoot}/{type}/skill/{realName}.anim";
            var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(expectedPath);
            if (clip != null)
            {
                assetPath = expectedPath;
                return true;
            }
            note = $"未在默认路径 {expectedPath} 找到动画。";
        }

        var guids = AssetDatabase.FindAssets($"{realName} t:AnimationClip");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (Path.GetFileNameWithoutExtension(path).Equals(realName, StringComparison.OrdinalIgnoreCase))
            {
                assetPath = path;
                if (!string.IsNullOrEmpty(type))
                {
                    note = $"在其他路径找到匹配动画：{assetPath}";
                }
                return true;
            }
        }

        if (string.IsNullOrEmpty(note))
        {
            note = "未找到匹配的动画资源。";
        }

        return false;
    }

    private static bool CsvContainsId(string path, string id)
    {
        if (!File.Exists(path))
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
            Debug.LogError($"SkillCreationTool 读取 {path} 校验 ID 时发生异常: {ex}");
        }

        return false;
    }
}

#endif
