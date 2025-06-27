// EnableGpuInstancingForFx_Unity6_Fallback.cs
// 放到 Assets/Editor/ 下
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class EnableGpuInstancingForFx_Unity6_Fallback
{
    // ☆ 可按需修改：只处理这些组，留空 = 全部组
    private static readonly string[] GroupNameFilters = { "Effects" };
    // ☆ 可按需修改：只处理带这些标签的 Addressables 条目，留空 = 不筛标签
    private static readonly string[] LabelFilters     = { /* "weapon" */ };

    [MenuItem("Tools/Addressables/批量开启 GPU Instancing (Unity6-Fallback)")]
    private static void Run()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("找不到 AddressableAssetSettings，请先初始化 Addressables。");
            return;
        }

        // 先筛组
        var groups = settings.groups
            .Where(g => !g.ReadOnly && !g.IsDefaultGroup())
            .Where(g => GroupNameFilters.Length == 0 || GroupNameFilters.Any(f => g.Name.Contains(f)));

        int processed = 0, updated = 0;

        AssetDatabase.StartAssetEditing();
        try
        {
            foreach (var group in groups)
            {
                // 用来去重
                var prefabPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // 遍历条目，把文件夹里的 Prefab + 直接的 Prefab 都捞出来
                foreach (var entry in group.entries)
                {
                    var path = entry.AssetPath;
                    if (string.IsNullOrEmpty(path))
                        continue;

                    // 1) 如果这是个文件夹
                    if (AssetDatabase.IsValidFolder(path))
                    {
                        // 在该文件夹下查找所有 Prefab
                        var guids = AssetDatabase.FindAssets("t:Prefab", new[]{ path });
                        foreach (var guid in guids)
                        {
                            var p = AssetDatabase.GUIDToAssetPath(guid);
                            if (string.IsNullOrEmpty(p)) continue;
                            // （可选）按 Label 过滤：如果你需要只对带标签的 Prefab 生效：
                            if (LabelFilters.Length > 0)
                            {
                                var assetEntry = settings.FindAssetEntry(AssetDatabase.AssetPathToGUID(p));
                                if (assetEntry == null || !LabelFilters.Any(lb => assetEntry.labels.Contains(lb)))
                                    continue;
                            }
                            prefabPaths.Add(p);
                        }
                    }
                    // 2) 如果这是个 Prefab 文件
                    else if (path.EndsWith(".prefab", StringComparison.OrdinalIgnoreCase))
                    {
                        // （同上，可以加 Label 过滤）
                        prefabPaths.Add(path);
                    }
                }

                // 真正处理每个 Prefab
                foreach (var prefabPath in prefabPaths)
                {
                    var root = PrefabUtility.LoadPrefabContents(prefabPath);
                    bool anyChange = false;

                    foreach (var rdr in root.GetComponentsInChildren<Renderer>(true))
                    {
                        foreach (var mat in rdr.sharedMaterials)
                        {
                            if (mat == null) continue;
                            processed++;

                            if (!mat.enableInstancing)
                            {
                                Undo.RecordObject(mat, "Enable Instancing");
                                mat.enableInstancing = true;
                                EditorUtility.SetDirty(mat);

                                // 确认是不是生效了
                                if (mat.enableInstancing)
                                {
                                    updated++;
                                    anyChange = true;
                                }
                            }
                        }
                    }

                    if (anyChange)
                        PrefabUtility.SaveAsPrefabAsset(root, prefabPath);

                    PrefabUtility.UnloadPrefabContents(root);
                }
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        Debug.Log($"【完成】共扫描 {processed} 个材质，成功开启 {updated} 个 GPU Instancing。");
    }
}
