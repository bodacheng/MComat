#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Skill;

public partial class StagesManagerGUI : Editor {

    void BasicStates(CharDataInfo CharInfo)
    {
        if (CharInfo == null || CharInfo.r_id == null)
            return;
        EditorGUILayout.LabelField(" 角色基础进程  ", Title);
        CharInfo.set.moveType = (MoveType)EditorGUILayout.EnumPopup("Move Type", CharInfo.set.moveType);
        CharInfo.set.canDefend = EditorGUILayout.Toggle("有防御技能", CharInfo.set.canDefend);
        CharInfo.set.rushType = (RushType)EditorGUILayout.EnumPopup("Rush Type", CharInfo.set.rushType);
    }
}
#endif