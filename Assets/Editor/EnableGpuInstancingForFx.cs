// EnableGpuInstancingForFx_Unity6_WithPSR.cs
// 放到 Assets/Editor/ 下
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class EnableGpuInstancingForFx_Unity6_WithPSR
{
    private static readonly string[] GroupNameFilters = { "Effects" }; // 只处理这些组，留空=全部
    private static readonly string[] LabelFilters     = { /* "weapon" */ }; // 只处理带这些标签的条目，留空=全部

    [MenuItem("Tools/Addressables/批量开启 GPU Instancing (Unity6 + PSR)")]
    private static void Run()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings == null)
        {
            Debug.LogError("Addressables 未初始化，请先启用 Addressables。");
            return;
        }

        var groups = settings.groups
            .Where(g => !g.ReadOnly && !g.IsDefaultGroup())
            .Where(g => GroupNameFilters.Length == 0 || GroupNameFilters.Any(f => g.Name.Contains(f)));

        int processedMat = 0, updatedMat = 0;
        int processedPSR = 0, updatedPSR = 0;

        AssetDatabase.StartAssetEditing();
        try
        {
            foreach (var group in groups)
            {
                // 收集所有 Prefab 路径（包含文件夹内递归）
                var prefabPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var entry in group.entries)
                {
                    var path = entry.AssetPath;
                    if (string.IsNullOrEmpty(path)) continue;

                    if (AssetDatabase.IsValidFolder(path))
                    {
                        // 递归找出该文件夹下的所有 Prefab
                        foreach (var guid in AssetDatabase.FindAssets("t:Prefab", new[]{ path }))
                        {
                            var p = AssetDatabase.GUIDToAssetPath(guid);
                            if (string.IsNullOrEmpty(p)) continue;
                            if (LabelFilters.Length > 0)
                            {
                                var e = settings.FindAssetEntry(AssetDatabase.AssetPathToGUID(p));
                                if (e == null || !LabelFilters.Any(lb => e.labels.Contains(lb))) continue;
                            }
                            prefabPaths.Add(p);
                        }
                    }
                    else if (path.EndsWith(".prefab", StringComparison.OrdinalIgnoreCase))
                    {
                        if (LabelFilters.Length > 0)
                        {
                            var e = settings.FindAssetEntry(AssetDatabase.AssetPathToGUID(path));
                            if (e == null || !LabelFilters.Any(lb => e.labels.Contains(lb))) continue;
                        }
                        prefabPaths.Add(path);
                    }
                }

                // 处理每个 Prefab
                foreach (var prefabPath in prefabPaths)
                {
                    var root = PrefabUtility.LoadPrefabContents(prefabPath);
                    bool anyChange = false;

                    // 遍历所有 Renderer（包含 ParticleSystemRenderer）
                    foreach (var rdr in root.GetComponentsInChildren<Renderer>(true))
                    {
                        // —— 1) 材质层面的 GPU Instancing —— 
                        foreach (var mat in rdr.sharedMaterials)
                        {
                            if (mat == null) continue;
                            processedMat++;
                            if (!mat.enableInstancing)
                            {
                                Undo.RecordObject(mat, "Enable GPU Instancing on Material");
                                mat.enableInstancing = true;
                                EditorUtility.SetDirty(mat);
                                if (mat.enableInstancing)
                                {
                                    updatedMat++;
                                    anyChange = true;
                                }
                            }
                        }

                        // —— 2) ParticleSystemRenderer 专属的 GPU Instancing —— 
                        if (rdr is ParticleSystemRenderer psr)
                        {
                            processedPSR++;
                            // 只有 Mesh 模式下才有这个选项
                            if (psr.renderMode == ParticleSystemRenderMode.Mesh && !psr.enableGPUInstancing)
                            {
                                Undo.RecordObject(psr, "Enable GPU Instancing on ParticleSystemRenderer");
                                psr.enableGPUInstancing = true;
                                EditorUtility.SetDirty(psr);
                                updatedPSR++;
                                anyChange = true;
                                Debug.Log("记录 GPU Instancing on ParticleSystemRenderer："+ prefabPath);
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

        Debug.Log($"完成：材质检查 {processedMat} 张，开启 {updatedMat} 张；ParticleSystemRenderer 检查 {processedPSR} 个，开启 {updatedPSR} 个。");
    }
}
