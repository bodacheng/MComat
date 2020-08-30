#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Skill;

public partial class StagesManagerGUI : Editor {

    void BasicStates(CharDataInfo CharInfo)
    {
        if (CharInfo == null || CharInfo.ResourceID == null)
            return;
        EditorGUILayout.LabelField(" 角色基础进程  ", Title);
        CharInfo._NineAndTwo.moveType = (MoveType)EditorGUILayout.EnumPopup("Move Type", CharInfo._NineAndTwo.moveType);
        CharInfo._NineAndTwo.canDefend = EditorGUILayout.Toggle("有防御技能", CharInfo._NineAndTwo.canDefend);
        CharInfo._NineAndTwo.rushType = (RushType)EditorGUILayout.EnumPopup("Rush Type", CharInfo._NineAndTwo.rushType);
    }
}
#endif