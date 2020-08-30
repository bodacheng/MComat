#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class StagesManagerGUI : Editor {
    
    int selectedmonsterindex;
    string focusingMemberPosID;
    
    void Members()
    {
        EditorGUILayout.LabelField(" 关卡基础数值信息  ", Title);
        _stagesManager.EditoringFight.Team1HpRate = EditorGUILayout.FloatField("队伍1血量比率", _stagesManager.EditoringFight.Team1HpRate);
        _stagesManager.EditoringFight.Team2HpRate = EditorGUILayout.FloatField("队伍2血量比率", _stagesManager.EditoringFight.Team2HpRate);
        _stagesManager.EditoringFight.team1CGMode = (LocalFight.CriticalGaugeMode)EditorGUILayout.EnumPopup("队伍1回气模式", _stagesManager.EditoringFight.team1CGMode);
        _stagesManager.EditoringFight.team2CGMode = (LocalFight.CriticalGaugeMode)EditorGUILayout.EnumPopup("队伍2回气模式", _stagesManager.EditoringFight.team2CGMode);
        
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