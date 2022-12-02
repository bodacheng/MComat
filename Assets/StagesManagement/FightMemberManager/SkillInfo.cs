#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Skill;

public partial class StageEditor {
    
    readonly int[] exoptions = { 0, 1, 2, 3 };
    readonly string[] exoptions_display = {"normal","ex1","ex2","ex3"};
    
    void SkillInfo(SkillConfig SkillConfig)
    {
        if (SkillConfig == null)
            return;
        EditorGUILayout.LabelField("技能详细信息");
        GUI.backgroundColor = new Color(1f, 0.7f, 0.5f);
        SkillConfig.REAL_NAME = EditorGUILayout.TextField("Name",SkillConfig.REAL_NAME);
        SkillConfig.STATE_TYPE = (BehaviorType)EditorGUILayout.EnumPopup("Attack Type", SkillConfig.STATE_TYPE);                                                    
        SkillConfig.ATTACK_WEIGHT = EditorGUILayout.FloatField("AT", SkillConfig.ATTACK_WEIGHT);
        SkillConfig.SP_LEVEL = EditorGUILayout.IntPopup("SPLevel", SkillConfig.SP_LEVEL, exoptions_display, exoptions);        
        EditorGUILayout.LabelField("AI模式技能触发范围");
        SkillConfig.AIAttrs.AI_MIN_DIS = EditorGUILayout.FloatField("min_dis",SkillConfig.AIAttrs.AI_MIN_DIS);
        SkillConfig.AIAttrs.AI_MAX_DIS = EditorGUILayout.FloatField("max_dis",SkillConfig.AIAttrs.AI_MAX_DIS);
        SkillConfig.AIAttrs.height = EditorGUILayout.IntField("height",SkillConfig.AIAttrs.height);
        GUI.backgroundColor = Color.white;
    }
}
#endif