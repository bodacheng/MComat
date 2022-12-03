#if UNITY_EDITOR
using UnityEditor;

public partial class StageEditor {

    void BasicStates(UnitInfo unitInfo)
    {
        if (unitInfo == null || unitInfo.r_id == null)
            return;
        EditorGUILayout.LabelField(" 角色基础进程  ", Title);
        EditorGUILayout.EnumPopup("Move Type", unitInfo.set.GetM());
        EditorGUILayout.Toggle("有防御技能", unitInfo.set.GetD());
        EditorGUILayout.EnumPopup("Rush Type", unitInfo.set.GetR());
    }
}
#endif