#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Skill;

public partial class StagesManagerGUI : Editor {
    
    readonly int[] exoptions = { 0, 1, 2, 3 };
    readonly string[] exoptions_display = {"normal","ex1","ex2","ex3"};
    
    void SkillInfo(SkillConfig defaultSkillConfig)
    {
        EditorGUILayout.LabelField("技能详细信息");
        targetSC.STATE_TYPE = (BehaviorType)EditorGUILayout.EnumPopup("Attack Type",(targetSC.STATE_TYPE == BehaviorType.NONE && defaultSkillConfig != null && defaultSkillConfig.STATE_TYPE != BehaviorType.NONE) ? defaultSkillConfig.STATE_TYPE : targetSC.STATE_TYPE);                                                    
        targetSC.ATTACK_WEIGHT = EditorGUILayout.FloatField("AT", (defaultSkillConfig != null) ? defaultSkillConfig.ATTACK_WEIGHT : targetSC.ATTACK_WEIGHT);
        targetSC.SP_LEVEL = EditorGUILayout.IntPopup("SPLevel",(targetSC.SP_LEVEL == -1 && defaultSkillConfig != null) ? defaultSkillConfig.SP_LEVEL : targetSC.SP_LEVEL, exoptions_display,exoptions);
        GUI.backgroundColor = new Color(1f, 0.7f, 0.5f);
        GUILayout.Space(2f);
        EditorGUILayout.LabelField("AI模式技能触发范围");
        defaultSkillConfig.AI_MIN_DIS = EditorGUILayout.FloatField("min_dis",defaultSkillConfig.AI_MIN_DIS);
        defaultSkillConfig.AI_MAX_DIS = EditorGUILayout.FloatField("min_dis",defaultSkillConfig.AI_MAX_DIS);
        GUI.backgroundColor = Color.white;
    }
}
#endif