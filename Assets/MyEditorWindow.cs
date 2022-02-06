using System;
using System.IO;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public static class MyEditorWindow
{
    [MenuItem("MCombat/StageManager", priority = 1)]
    static void StageManager()
    {
        StagesManager window = (StagesManager)EditorWindow.GetWindow(typeof(StagesManager));
        window.titleContent = new GUIContent("关卡管理器");
        window.Show();
    }
}
#endif
