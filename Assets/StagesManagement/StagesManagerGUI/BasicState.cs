#if UNITY_EDITOR
using UnityEditor;

public partial class StagesManager : EditorWindow {

    void BasicStates(UnitInfo CharInfo)
    {
        if (CharInfo == null || CharInfo.r_id == null)
            return;
        EditorGUILayout.LabelField(" 角色基础进程  ", Title);
        EditorGUILayout.EnumPopup("Move Type", CharInfo.set.GetM());
        EditorGUILayout.Toggle("有防御技能", CharInfo.set.GetD());
        EditorGUILayout.EnumPopup("Rush Type", CharInfo.set.GetR());
    }
}
#endif