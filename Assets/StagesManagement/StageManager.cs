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
            Units.LoadUnitConfigs();
            SkillConfigTable.LoadAllSkillConfigs();
            
            target = new FightInfo();
            target.FightMembers = new FightMembers();
            _fightMemberManager = new FightMemberManager();
            Initialized = true;
        }

        if (target == null || target.FightMembers == null || _fightMemberManager == null)
        {
            Initialized = false;
            return;
        }
        
        _fightMemberManager.OnGUIView(target.FightMembers);
        pathAndNameForLocalSave = EditorGUILayout.TextField("local Path For Saving", pathAndNameForLocalSave);
        fileName = EditorGUILayout.TextField("file", fileName);
        
        if (GUILayout.Button("Save"))
        {
            FightInfo.CreateFightInfoAsset(target.FightMembers, pathAndNameForLocalSave, fileName);
        }
        
        EditorGUILayout.Space(200);
        GenerateArenaDummies();
    }
    
    readonly ArenaDummiesTable table = new ();
    void GenerateArenaDummies()
    {
        table.Load();
        if (GUILayout.Button("根据ArenaDummiesTable生成假想敌文件（生成于Assets/Resources/ArenaDummies之下）"))
        {
            foreach (var row in table.GetRowList())
            {
                var target = FightMembers.RandomFight();
                target.SetEnemyLevel(Int32.Parse(row.LEVEL));
                FightInfo.CreateFightInfoAsset(target, "Assets/Resources/ArenaDummies", row.NICK_NAME);
            }
        }
    }
}
#endif