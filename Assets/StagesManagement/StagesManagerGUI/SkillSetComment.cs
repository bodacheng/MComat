#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class StagesManagerGUI : Editor {

    NineAndTwo.SkillEditError se;
    
    void SkillSetComent()
    {
        // 技能组评价
        GUILayout.BeginHorizontal();
        se = NineAndTwo.CheckEdit(
            focusingCharInfo.set.GetA1Config()?.RECORD_ID,focusingCharInfo.set.GetA2Config()?.RECORD_ID, focusingCharInfo.set.GetA3Config()?.RECORD_ID,
            focusingCharInfo.set.GetB1Config()?.RECORD_ID,focusingCharInfo.set.GetB2Config()?.RECORD_ID, focusingCharInfo.set.GetB3Config()?.RECORD_ID,
            focusingCharInfo.set.GetC1Config()?.RECORD_ID,focusingCharInfo.set.GetC2Config()?.RECORD_ID, focusingCharInfo.set.GetC3Config()?.RECORD_ID);
            
        switch (se)
        {
            case NineAndTwo.SkillEditError.Perfect:
                Title.normal.textColor = Color.green;
                EditorGUILayout.LabelField("合法", Title);
            break;
            case NineAndTwo.SkillEditError.NoNormalStart:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("首发技能无普攻", Title);
            break;
            case NineAndTwo.SkillEditError.RepeatedSkill:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("技能重复", Title);
            break;
            case NineAndTwo.SkillEditError.UnBalanced:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("必杀普攻不平衡", Title);
            break;
        }
        Title.normal.textColor = Color.black;
        GUILayout.EndHorizontal();
    }
}
#endif