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

    [MenuItem("MCombat/MasterDataTool", priority = 2)]
    static void MasterDataTool()
    {
        LocalMasterDataToolGUI window = (LocalMasterDataToolGUI)EditorWindow.GetWindow(typeof(LocalMasterDataToolGUI));
        window.titleContent = new GUIContent("Master Data 出力工具");
        window.Show();
    }
}
#endif
