#if UNITY_EDITOR

using UnityEditor;

public static class SkillCreationTool
{
    [MenuItem("Tools/Skill Creation Tool")]
    private static void Open()
    {
        SKillAnalyzerGUI.OpenWorkbench(1);
    }
}

#endif
