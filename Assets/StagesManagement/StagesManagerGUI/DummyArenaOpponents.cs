#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class StagesManager : EditorWindow
{
    void GenerateArenaDummies()
    {
        if (GUILayout.Button("自动生成100个假想敌文件", ButtonStyle_save))
        {
            int i;
            for (i = 0; i < 100; i++)
            {
                target = FightMembers.RandomFight();
                target.SaveFightAsJson(pathAndNameForLocalSave + "/"+ i, target.EnemySets);
            }
        }
    }
}
#endif