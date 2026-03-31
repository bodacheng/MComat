#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class SkillAIDistanceTuningEditor
{
    const string MenuPath = "MCombat/Skill AI/Apply Tuned Distances To skill_ai_attrs";
    const string DefaultSkillAiCsvPath = "Assets/ExternalAssets/Data/Config/skill_ai_attrs.csv";
    const string CsvNewLine = "\r\n";

    [MenuItem(MenuPath, priority = 5)]
    static void ApplyTunedDistancesToCsv()
    {
        var csvPath = FindSkillAiCsvPath();
        if (string.IsNullOrEmpty(csvPath) || !File.Exists(csvPath))
        {
            EditorUtility.DisplayDialog("Apply Tuned Distances", "未找到 skill_ai_attrs.csv。", "OK");
            return;
        }

        var statePath = SkillAIDistanceAutoTuner.TuneStateFilePath;
        if (!File.Exists(statePath))
        {
            EditorUtility.DisplayDialog("Apply Tuned Distances", $"未找到调参状态文件：\n{statePath}", "OK");
            return;
        }

        var overrideMap = LoadOverrides(statePath);
        if (overrideMap.Count == 0)
        {
            EditorUtility.DisplayDialog("Apply Tuned Distances", "调参状态文件存在，但没有可应用的距离覆盖。", "OK");
            return;
        }

        var grid = CsvParser2.Parse(File.ReadAllText(csvPath));
        if (grid.Length == 0)
        {
            EditorUtility.DisplayDialog("Apply Tuned Distances", "skill_ai_attrs.csv 内容为空。", "OK");
            return;
        }

        var updatedCount = 0;
        for (var i = 1; i < grid.Length; i++)
        {
            var row = grid[i];
            if (row.Length < 3)
                continue;

            var recordId = row[0]?.Trim();
            if (string.IsNullOrEmpty(recordId))
                continue;

            if (!overrideMap.TryGetValue(recordId, out var tuned))
                continue;

            var oldMin = row[1];
            var oldMax = row[2];
            if (string.Equals(oldMin, tuned.Min, StringComparison.Ordinal) &&
                string.Equals(oldMax, tuned.Max, StringComparison.Ordinal))
            {
                continue;
            }

            row[1] = tuned.Min;
            row[2] = tuned.Max;
            updatedCount++;
        }

        if (updatedCount == 0)
        {
            EditorUtility.DisplayDialog("Apply Tuned Distances", "没有需要写回 skill_ai_attrs.csv 的新距离。", "OK");
            return;
        }

        File.WriteAllText(csvPath, SerializeGrid(grid), new UTF8Encoding(false));
        AssetDatabase.ImportAsset(csvPath, ImportAssetOptions.ForceSynchronousImport);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        var aiCsv = AssetDatabase.LoadAssetAtPath<TextAsset>(csvPath);
        if (aiCsv != null)
        {
            SkillAIAttrs.Load(aiCsv);
            if (SkillConfigTable.rowList.Count > 0)
            {
                SkillConfigTable.RefreshSkillConfigDicForReference();
            }
        }

        EditorUtility.DisplayDialog(
            "Apply Tuned Distances",
            $"已把 {updatedCount} 条自动调参后的距离写回：\n{csvPath}\n\n状态来源：\n{statePath}",
            "OK");
    }

    [MenuItem(MenuPath, true)]
    static bool ValidateApplyTunedDistancesToCsv()
    {
        return File.Exists(SkillAIDistanceAutoTuner.TuneStateFilePath);
    }

    static string FindSkillAiCsvPath()
    {
        if (File.Exists(DefaultSkillAiCsvPath))
            return DefaultSkillAiCsvPath;

        var fileStem = "skill_ai_attrs";
        var commonSettingGuids = AssetDatabase.FindAssets("t:CommonSetting");
        if (commonSettingGuids.Length > 0)
        {
            var commonSettingPath = AssetDatabase.GUIDToAssetPath(commonSettingGuids[0]);
            var commonSetting = AssetDatabase.LoadAssetAtPath<CommonSetting>(commonSettingPath);
            if (commonSetting != null)
            {
                var serialized = new SerializedObject(commonSetting);
                var skillAiFileProp = serialized.FindProperty("skillAIFile");
                if (skillAiFileProp != null && !string.IsNullOrWhiteSpace(skillAiFileProp.stringValue))
                {
                    fileStem = skillAiFileProp.stringValue.Trim();
                }
            }
        }

        var guids = AssetDatabase.FindAssets($"{fileStem} t:TextAsset");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.EndsWith($"{fileStem}.csv", StringComparison.OrdinalIgnoreCase))
                return path;
        }

        return null;
    }

    static Dictionary<string, TuneOverride> LoadOverrides(string statePath)
    {
        var overrides = new Dictionary<string, TuneOverride>();
        var grid = CsvParser2.Parse(File.ReadAllText(statePath));
        for (var i = 1; i < grid.Length; i++)
        {
            var row = grid[i];
            if (row.Length < 3)
                continue;

            var recordId = row[0]?.Trim();
            var min = row[1]?.Trim();
            var max = row[2]?.Trim();
            if (string.IsNullOrEmpty(recordId) || string.IsNullOrEmpty(min) || string.IsNullOrEmpty(max))
                continue;

            if (!float.TryParse(min, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                continue;
            if (!float.TryParse(max, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                continue;

            overrides[recordId] = new TuneOverride
            {
                Min = min,
                Max = max
            };
        }
        return overrides;
    }

    static string SerializeGrid(string[][] grid)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < grid.Length; i++)
        {
            if (i > 0)
                builder.Append(CsvNewLine);
            builder.Append(SerializeRow(grid[i]));
        }
        builder.Append(CsvNewLine);
        return builder.ToString();
    }

    static string SerializeRow(IReadOnlyList<string> row)
    {
        if (row == null || row.Count == 0)
            return string.Empty;

        var builder = new StringBuilder();
        for (var i = 0; i < row.Count; i++)
        {
            if (i > 0)
                builder.Append(',');
            builder.Append(ToCsvField(row[i]));
        }
        return builder.ToString();
    }

    static string ToCsvField(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0)
        {
            var escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }

        return value;
    }

    sealed class TuneOverride
    {
        public string Min;
        public string Max;
    }
}
#endif
