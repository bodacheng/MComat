#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public partial class StagesManagerGUI : Editor {

    void LoadScript()
    {
        GUILayout.BeginHorizontal();
        _stagesManager.FightScript = EditorGUILayout.ObjectField("战斗脚本读取", _stagesManager.FightScript, typeof(TextAsset), true) as TextAsset;
        if (GUILayout.Button("read", ButtonStyle))
        {
            if (_stagesManager.FightScript != null)
            {
                _stagesManager.EditoringFight = FightMembers.LoadEnemies_Json(_stagesManager.FightScript);
                foreach (var _one in _stagesManager.EditoringFight.EnemySets._SerializableSets)
                {
                    CharConfig _CharConfig = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(_one.value.r_id));
                    if (_CharConfig == null)
                    {
                        Debug.Log("检测到存档错误：ResourceID");
                        continue;
                    }
                    _one.value.set.SortNineAndTwo();
                }
            }
        }
        GUILayout.EndHorizontal();
    }
}
#endif