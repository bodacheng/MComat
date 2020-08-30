#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class StagesManagerGUI : Editor {
    
    int selectedmonsterindex;
    string focusingMemberPosID;
    
    void Members()
    {        
        EditorGUILayout.LabelField(" 关卡敌人信息  ", Title);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("back", (focusingMemberPosID != 0.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberPosID = 0.ToString();
            focusingCharInfo = _stagesManager.EditoringFight.EnemySets.Get(0, 0);
        }
        if (GUILayout.Button("left",(focusingMemberPosID != 1.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberPosID = 1.ToString();
            focusingCharInfo = _stagesManager.EditoringFight.EnemySets.Get(0, 1);
        }
        if (GUILayout.Button("front", (focusingMemberPosID != 2.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberPosID = 2.ToString();
            focusingCharInfo = _stagesManager.EditoringFight.EnemySets.Get(0, 2);
        }
        if (GUILayout.Button("right",(focusingMemberPosID != 3.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedmonsterindex = 0;
            focusingMemberPosID = 3.ToString();
            focusingCharInfo = _stagesManager.EditoringFight.EnemySets.Get(0, 3);
        }
        GUILayout.EndHorizontal();
    }
}
#endif