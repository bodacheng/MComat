#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class StageEditor {
    
    int selectedUnitIndex;
    string focusingPosID;
    
    void Members(FightMembers target)
    {
        EditorGUILayout.LabelField(" Enemies infos ", Title);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("left", (focusingPosID != 1.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedUnitIndex = 0;
            focusingPosID = 1.ToString();
            _focusingUnitInfo = target.EnemySets.Get(0, 1);
            _targetSlot = 0;
        }
        if (GUILayout.Button("mid", (focusingPosID != 0.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedUnitIndex = 0;
            focusingPosID = 0.ToString();
            _focusingUnitInfo = target.EnemySets.Get(0, 0);
            _targetSlot = 0;
        }
        if (GUILayout.Button("right", (focusingPosID != 2.ToString()) ? ButtonStyle : ButtonStyle_selected))
        {
            selectedUnitIndex = 0;
            focusingPosID = 2.ToString();
            _focusingUnitInfo = target.EnemySets.Get(0, 2);
            _targetSlot = 0;
        }
        GUILayout.EndHorizontal();
    }
}
#endif