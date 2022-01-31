using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public static class MyEditorWindow
{
    [MenuItem("MCombat/StageManager", priority = 1)]
    static void StageManager()
    {
        StagesManagerGUI window = (StagesManagerGUI)EditorWindow.GetWindow(typeof(StagesManagerGUI));
        window.titleContent = new GUIContent("关卡管理器");
        window.Show();
    }
}
