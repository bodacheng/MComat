#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;

public class StageManager : EditorWindow
{
    private FightMemberManager _fightMemberManager;
    private FightInfo target;
    string pathAndNameForLocalSave = "Assets/ExternalAssets/ArcadeStages";
    private string fileName;
    bool Initialized;
    void OnGUI()
    {
        if (!Initialized)
        {
            Units.LoadMonstersConfig();
            SkillConfigTable.LoadAllSkillConfigs();
            
            target = new FightInfo();
            _fightMemberManager = new FightMemberManager();
            Initialized = true;
        }
        
        _fightMemberManager.OnGUIView(target.FightMembers);

        pathAndNameForLocalSave = EditorGUILayout.TextField("local Path For Saving", pathAndNameForLocalSave);
        fileName = EditorGUILayout.TextField("file", fileName);
        
        if (GUILayout.Button("Save"))
        {
            FightInfo.CreateFightInfoAsset(target.FightMembers, pathAndNameForLocalSave, fileName);
        }
        
        GenerateArenaDummies();
    }
    
    readonly ArenaDummiesTable table = new ();
    void GenerateArenaDummies()
    {
        table.Load();
        if (GUILayout.Button("根据ArenaDummiesTable生成假想敌文件"))
        {
            foreach (var row in table.GetRowList())
            {
                var target = FightMembers.RandomFight();
                target.SetEnemyLevel(Int32.Parse(row.LEVEL));
                FightInfo.CreateFightInfoAsset(target, pathAndNameForLocalSave, row.NICK_NAME);
            }
        }
    }
}
#endif