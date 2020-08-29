#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using mainMenu;

public partial class StagesManagerGUI : Editor {

    TheNineSlot.SkillEditError se;

    void SkillSetComent()
    {
        // 技能组评价
        GUILayout.BeginHorizontal();
        se = TheNineSlot.CheckEdit(
            focusingCharInfo._NineAndTwo.GetA1Config()?.RECORD_ID,focusingCharInfo._NineAndTwo.GetA2Config()?.RECORD_ID,focusingCharInfo._NineAndTwo.GetA3Config()?.RECORD_ID,
            focusingCharInfo._NineAndTwo.GetB1Config()?.RECORD_ID,focusingCharInfo._NineAndTwo.GetB2Config()?.RECORD_ID,focusingCharInfo._NineAndTwo.GetB3Config()?.RECORD_ID,
            focusingCharInfo._NineAndTwo.GetC1Config()?.RECORD_ID,focusingCharInfo._NineAndTwo.GetC2Config()?.RECORD_ID,focusingCharInfo._NineAndTwo.GetC3Config()?.RECORD_ID
        );
        switch (se)
        {
            case TheNineSlot.SkillEditError.Perfect:
                Title.normal.textColor = Color.green;
                EditorGUILayout.LabelField("合法", Title);
            break;
            case TheNineSlot.SkillEditError.NoNormalStart:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("首发技能无普攻", Title);
            break;
            case TheNineSlot.SkillEditError.RepeatedSkill:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("技能重复", Title);
            break;
            case TheNineSlot.SkillEditError.UnBalanced:
                Title.normal.textColor = Color.red;
                EditorGUILayout.LabelField("必杀普攻不平衡", Title);
            break;
        }
        Title.normal.textColor = Color.black;
        GUILayout.EndHorizontal();
    }
}
#endif