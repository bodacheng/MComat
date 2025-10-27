#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

[CanEditMultipleObjects]
[CustomEditor(typeof(FightInfo))]
public class FightInfoGUI : Editor
{
    private StageEditor _stageEditor;
    private bool _initialized = false;
    private static readonly Dictionary<string, Sprite> SpriteCache = new Dictionary<string, Sprite>(StringComparer.OrdinalIgnoreCase);
    private const string BattleGroundIdPropertyName = "battleGroundID";
    private static readonly GUIContent BattleGroundLabel = new GUIContent("Battle Ground");

    private struct BattleGroundOption
    {
        public int Id;
        public string DisplayName;
        public string Address;
        public string AssetPath;
    }

    private static readonly List<BattleGroundOption> BattleGroundOptions = new List<BattleGroundOption>();

    public override void OnInspectorGUI()
    {
        if (!Starter.ConfigInitialised)
        {
            EditorGUILayout.LabelField("Loading config");
        }
        
        serializedObject.Update();

        DrawBattleGroundDropdown();
        EditorGUILayout.Space();
        DrawPropertiesExcluding(serializedObject, BattleGroundIdPropertyName);

        serializedObject.ApplyModifiedProperties();

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

    private void DrawBattleGroundDropdown()
    {
        var battleGroundProperty = serializedObject.FindProperty(BattleGroundIdPropertyName);
        if (battleGroundProperty == null)
        {
            return;
        }

        var options = GetBattleGroundOptions();
        if (options.Count == 0)
        {
            EditorGUILayout.PropertyField(battleGroundProperty);
            return;
        }

        var displayedOptions = new List<GUIContent>(options.Count);
        var optionValues = new List<int>(options.Count);
        for (var index = 0; index < options.Count; index++)
        {
            var option = options[index];
            var tooltip = string.IsNullOrEmpty(option.AssetPath)
                ? option.Address
                : $"{option.Address}\n{option.AssetPath}";
            displayedOptions.Add(new GUIContent(option.DisplayName, tooltip));
            optionValues.Add(option.Id);
        }

        var previousValue = battleGroundProperty.intValue;
        if (optionValues.All(value => value != previousValue))
        {
            displayedOptions.Add(new GUIContent($"(Unused) {previousValue}"));
            optionValues.Add(previousValue);
        }

        var newValue = EditorGUILayout.IntPopup(
            BattleGroundLabel,
            previousValue,
            displayedOptions.ToArray(),
            optionValues.ToArray());
        if (newValue != previousValue)
        {
            battleGroundProperty.intValue = newValue;
        }
    }

    private static List<BattleGroundOption> GetBattleGroundOptions()
    {
        BattleGroundOptions.Clear();

        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if (settings != null)
        {
            foreach (var group in settings.groups)
            {
                if (group == null)
                {
                    continue;
                }

                foreach (var entry in group.entries)
                {
                    if (entry == null || entry.IsFolder)
                    {
                        continue;
                    }

                    if (!entry.labels.Contains("battle_ground"))
                    {
                        continue;
                    }

                    if (!TryExtractBattleGroundId(entry.address, out var id))
                    {
                        continue;
                    }

                    var displayName = GetDisplayName(entry.AssetPath, entry.address);
                    BattleGroundOptions.Add(new BattleGroundOption
                    {
                        Id = id,
                        DisplayName = displayName,
                        Address = entry.address,
                        AssetPath = entry.AssetPath
                    });
                }
            }
        }

        if (BattleGroundOptions.Count == 0)
        {
            var guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/ExternalAssets/BattleGround" });
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var displayName = Path.GetFileNameWithoutExtension(assetPath);
                BattleGroundOptions.Add(new BattleGroundOption
                {
                    Id = BattleGroundOptions.Count,
                    DisplayName = displayName,
                    Address = displayName,
                    AssetPath = assetPath
                });
            }
        }

        BattleGroundOptions.Sort((left, right) => left.Id.CompareTo(right.Id));
        return BattleGroundOptions;
    }

    private static bool TryExtractBattleGroundId(string address, out int id)
    {
        id = default;
        if (string.IsNullOrEmpty(address))
        {
            return false;
        }

        var separatorIndex = address.LastIndexOf("/", StringComparison.Ordinal);
        var idCandidate = separatorIndex >= 0 ? address.Substring(separatorIndex + 1) : address;
        return int.TryParse(idCandidate, out id);
    }

    private static string GetDisplayName(string assetPath, string address)
    {
        if (!string.IsNullOrEmpty(assetPath))
        {
            var fileName = Path.GetFileNameWithoutExtension(assetPath);
            if (!string.IsNullOrEmpty(fileName))
            {
                return fileName;
            }
        }

        var separatorIndex = address.LastIndexOf("/", StringComparison.Ordinal);
        if (separatorIndex >= 0 && separatorIndex < address.Length - 1)
        {
            return address.Substring(separatorIndex + 1);
        }

        return address;
    }
}
#endif
