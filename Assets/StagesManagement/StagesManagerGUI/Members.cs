#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class StagesManagerGUI : EditorWindow {
    
    int selectedUnitIndex;
    string focusingPosID;
    
    void Members()
    {
        EditorGUILayout.LabelField(" 关卡敌人信息  ", Title);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("left", (focusingPosID != 1.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedUnitIndex = 0;
            focusingPosID = 1.ToString();
            focusingUnitInfo = _stagesManager.target.EnemySets.Get(0, 1);
            targetSlot = 0;
        }
        if (GUILayout.Button("mid", (focusingPosID != 0.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedUnitIndex = 0;
            focusingPosID = 0.ToString();
            focusingUnitInfo = _stagesManager.target.EnemySets.Get(0, 0);
            targetSlot = 0;
        }
        if (GUILayout.Button("right", (focusingPosID != 2.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedUnitIndex = 0;
            focusingPosID = 2.ToString();
            focusingUnitInfo = _stagesManager.target.EnemySets.Get(0, 2);
            targetSlot = 0;
        }
        GUILayout.EndHorizontal();
    }
}
#endif