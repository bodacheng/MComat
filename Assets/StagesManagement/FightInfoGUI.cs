#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(FightInfo))]
public class FightInfoGUI : Editor
{
    private StageEditor _stageEditor;
    private bool _initialized = false;
    private static readonly Dictionary<string, Sprite> SpriteCache = new Dictionary<string, Sprite>(StringComparer.OrdinalIgnoreCase);
    
    public override void OnInspectorGUI()
    {
        if (!Starter.ConfigInitialised)
        {
            EditorGUILayout.LabelField("Loading config");
        }
        
        DrawDefaultInspector();
        var fightInfo = (FightInfo)target;
        if (!_initialized)
        {
            fightInfo.OpenAndSetEnemyDataOnPlace();
            _stageEditor = new StageEditor();
            _initialized = true;
        }
        
        if (fightInfo.FightMode == FightMode.Evolve)
        {
            if (GUILayout.Button("进化模式随机全部队员"))
            {
                fightInfo.FightMembers = new FightMembers();
                fightInfo.UnitsData = new List<UnitInfo>();
                fightInfo.AutoFillEvolution(fightInfo.FightMembers, "human");
                SaveProcess();
                return;
            }
        }
        
        fightInfo.SetUnitLevelByRefLevel();
        _stageEditor.OnGUIView(
            fightInfo.FightMembers, 
            fightInfo.FightMode == FightMode.Group ? fightInfo.GetTeam2GroupSet : null, ()=>
            {
                if (GUILayout.Button("Save"))
                {
                    SaveProcess();
                }
            }
        );

        void SaveProcess()
        {
            fightInfo.SaveDicToData();
            EditorUtility.SetDirty(fightInfo);
            AssetDatabase.SaveAssets();
        }
    }
    
    public static Sprite GetSprite(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        
        if (SpriteCache.TryGetValue(name, out var cachedSprite))
        {
            return cachedSprite;
        }
        
        Sprite sprite = null;
        if (name.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
        {
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>(name);
        }
        
        if (sprite == null)
        {
            var searchName = Path.GetFileNameWithoutExtension(name);
            if (string.IsNullOrEmpty(searchName))
            {
                searchName = name;
            }
            
            var guids = AssetDatabase.FindAssets($"{searchName} t:Sprite");
            foreach (var guid in guids)
            {
                var loadPath = AssetDatabase.GUIDToAssetPath(guid);
                var loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(loadPath);
                if (loadedSprite == null)
                {
                    continue;
                }
                
                if (!SpriteCache.ContainsKey(loadedSprite.name))
                {
                    SpriteCache[loadedSprite.name] = loadedSprite;
                }
                
                sprite = loadedSprite;
                break;
            }
        }
        
        if (sprite == null)
        {
            var searchRootAssetFolder = Application.dataPath;
            var fileName = Path.GetFileName(name);
            if (!string.IsNullOrEmpty(fileName))
            {
                var pfGuiPaths = Directory.GetFiles(searchRootAssetFolder, fileName, SearchOption.AllDirectories);
                foreach (var eachPath in pfGuiPaths)
                {
                    var loadPath = eachPath.Substring(eachPath.LastIndexOf("Assets", StringComparison.Ordinal));
                    var loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(loadPath);
                    if (loadedSprite == null)
                    {
                        continue;
                    }
                    
                    if (!SpriteCache.ContainsKey(loadedSprite.name))
                    {
                        SpriteCache[loadedSprite.name] = loadedSprite;
                    }
                    
                    sprite = loadedSprite;
                    break;
                }
            }
        }
        
        SpriteCache[name] = sprite;
        return sprite;
    }
}
#endif
