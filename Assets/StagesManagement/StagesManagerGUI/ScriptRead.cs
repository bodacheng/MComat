#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public partial class StagesManager : EditorWindow {

    void LoadScript()
    {
        GUILayout.BeginHorizontal();
        FightScript = EditorGUILayout.ObjectField("战斗脚本读取", FightScript, typeof(TextAsset), true) as TextAsset;
        if (GUILayout.Button("read", ButtonStyle))
        {
            if (FightScript != null)
            {
                target = FightMembers.LoadEnemies_Json(FightScript);
                foreach (var _one in target.EnemySets._SerializableSets)
                {
                    UnitConfig unitConfig = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(_one.value.r_id));
                    if (unitConfig == null)
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