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
                LocalFight one = _stagesManager.LoadOneLocalFight_Json(_stagesManager.FightScript);
                if (one != null)
                {
                    _stagesManager.EditoringFight = one;
                    foreach (MultiDictionary<int,int,CharDataInfo>.SerializableSets _one in _stagesManager.EditoringFight.EnemySets._SerializableSets)
                    {
                        foreach (MultiDictionary<int,int,CharDataInfo>.SerializableSet set in _one.value)
                        {
                            if (set._Value != null)
                            {
                                CharConfig _CharacterResourceInfo = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(set._Value.ResourceID));
                                if (_CharacterResourceInfo == null)
                                {
                                    Debug.Log("检测到存档错误：ResourceID");
                                    continue;
                                }
                                set._Value._NineAndTwo.SortNineAndTwo();
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("读取本地信息失败");
                }
            }
        }
        GUILayout.EndHorizontal();
    }
}
#endif