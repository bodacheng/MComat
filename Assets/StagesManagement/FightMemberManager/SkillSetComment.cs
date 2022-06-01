#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class FightMemberManager {

    SkillSet.SkillEditError se;
    
    void SkillSetComment()
    {
        // 技能组评价
        GUILayout.BeginHorizontal();
        se = SkillSet.CheckEdit(
            focusingUnitInfo.set.GetA1Config()?.RECORD_ID,focusingUnitInfo.set.GetA2Config()?.RECORD_ID, focusingUnitInfo.set.GetA3Config()?.RECORD_ID,
            focusingUnitInfo.set.GetB1Config()?.RECORD_ID,focusingUnitInfo.set.GetB2Config()?.RECORD_ID, focusingUnitInfo.set.GetB3Config()?.RECORD_ID,
            focusingUnitInfo.set.GetC1Config()?.RECORD_ID,focusingUnitInfo.set.GetC2Config()?.RECORD_ID, focusingUnitInfo.set.GetC3Config()?.RECORD_ID);
            
        switch (se)
        {
            case SkillSet.SkillEditError.Perfect:
                Title.normal.textColor = Color.green;
                EditorGUILayout.LabelField("合法", Title);
            break;
            case SkillSet.SkillEditError.NoNormalStart:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("首发技能无普攻", Title);
            break;
            case SkillSet.SkillEditError.RepeatedSkill:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("技能重复", Title);
            break;
            case SkillSet.SkillEditError.UnBalanced:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("必杀普攻不平衡", Title);
            break;
            case SkillSet.SkillEditError.NotFull:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("不满", Title);
                break;
        }
        Title.normal.textColor = Color.black;
        GUILayout.EndHorizontal();
    }
}
#endif