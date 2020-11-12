#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Skill;

public partial class StagesManagerGUI : Editor {
    
    readonly int[] exoptions = { 0, 1, 2, 3 };
    readonly string[] exoptions_display = {"normal","ex1","ex2","ex3"};
    
    void SkillInfo(SkillConfig defaultSkillConfig)
    {
        if (defaultSkillConfig == null)
            return;
        EditorGUILayout.LabelField("技能详细信息");
        GUI.backgroundColor = new Color(1f, 0.7f, 0.5f);
        defaultSkillConfig.REAL_NAME = EditorGUILayout.TextField("Name",defaultSkillConfig.REAL_NAME);
        targetSC.STATE_TYPE = (BehaviorType)EditorGUILayout.EnumPopup("Attack Type", defaultSkillConfig.STATE_TYPE);                                                    
        targetSC.ATTACK_WEIGHT = EditorGUILayout.FloatField("AT", defaultSkillConfig.ATTACK_WEIGHT);
        targetSC.SP_LEVEL = EditorGUILayout.IntPopup("SPLevel", defaultSkillConfig.SP_LEVEL, exoptions_display, exoptions);        
        EditorGUILayout.LabelField("AI模式技能触发范围");
        defaultSkillConfig.AIAttrs.AI_MIN_DIS = EditorGUILayout.FloatField("min_dis",defaultSkillConfig.AIAttrs.AI_MIN_DIS);
        defaultSkillConfig.AIAttrs.AI_MAX_DIS = EditorGUILayout.FloatField("max_dis",defaultSkillConfig.AIAttrs.AI_MAX_DIS);
        defaultSkillConfig.AIAttrs.height = EditorGUILayout.IntField("height",defaultSkillConfig.AIAttrs.height);
        GUI.backgroundColor = Color.white;
    }
}
#endif