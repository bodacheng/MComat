#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;

public class StageEditorWindow : EditorWindow
{
    private StageEditor _stageEditor;
    private FightInfo _target;
    string _pathAndNameForLocalSave = "Assets/ExternalAssets/ArcadeStages";
    string _fileName;
    bool _initialized;
    void OnGUI()
    {
        if (!_initialized)
        {
            Units.LoadUnitConfigs();
            SkillConfigTable.LoadAllSkillConfigs();
            
            _target = new FightInfo();
            _target.FightMembers = new FightMembers();
            _stageEditor = new StageEditor();
            _initialized = true;
        }

        if (_target == null || _target.FightMembers == null || _stageEditor == null)
        {
            _initialized = false;
            return;
        }
        
        _stageEditor.OnGUIView(_target.FightMembers);
        _pathAndNameForLocalSave = EditorGUILayout.TextField("local Path For Saving", _pathAndNameForLocalSave);
        _fileName = EditorGUILayout.TextField("file", _fileName);
        
        if (GUILayout.Button("Save"))
        {
            FightInfo.CreateFightInfoAsset(_target.FightMembers, _pathAndNameForLocalSave, _fileName);
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
                var fight = FightMembers.RandomFight();
                fight.SetEnemyLevel(Int32.Parse(row.LEVEL));
                FightInfo.CreateFightInfoAsset(fight, "Assets/Resources/ArenaDummies", row.NICK_NAME);
            }
        }
    }
}
#endif