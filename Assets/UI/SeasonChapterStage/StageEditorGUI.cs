using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(StageInspector))]
public class StageEditorGUI : Editor
{
    StageInspector stage;
    public override void OnInspectorGUI()
    {
        stage = (StageInspector)target;
        stage.loadOneLocalFightByScript();
        DrawDefaultInspector();

    }
}
#endif