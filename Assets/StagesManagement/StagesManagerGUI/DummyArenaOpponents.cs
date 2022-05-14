#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

public partial class StagesManager : EditorWindow
{
    readonly ArenaDummiesTable table = new ArenaDummiesTable();
    void GenerateArenaDummies()
    {
        table.Load();
        if (GUILayout.Button("根据ArenaDummiesTable生成假想敌文件", ButtonStyle_save))
        {
            foreach (var row in table.GetRowList())
            {
                var target = FightMembers.RandomFight();
                target.SetEnemyLevel(Int32.Parse(row.LEVEL));
                target.SaveFightAsJson(target.EnemySets, pathAndNameForLocalSave + "/"+ row.NICK_NAME + ".json");
            }
        }
    }
}
#endif