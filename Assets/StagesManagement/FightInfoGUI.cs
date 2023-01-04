#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FightInfo))]
public class FightInfoGUI : Editor
{
    private StageEditor _stageEditor;
    private bool initialized = false;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector ();
        var fightInfo = (FightInfo)target;
        if (!initialized)
        {
            Units.LoadUnitConfigs();
            SkillConfigTable.LoadAllSkillConfigs();
            fightInfo.Open();
            _stageEditor = new StageEditor();
            initialized = true;
        }
        _stageEditor.OnGUIView(fightInfo.FightMembers);
        
        if (GUILayout.Button("Save"))
        {
            fightInfo.SaveDicToData();
            EditorUtility.SetDirty(fightInfo);
            AssetDatabase.SaveAssets();
        }
    }
    
    public static Sprite GetSprite(string name)
    {
        var searchRootAssetFolder = Application.dataPath;
        var pfGuiPaths = Directory.GetFiles(searchRootAssetFolder, name, SearchOption.AllDirectories);
        foreach (var eachPath in pfGuiPaths)
        {
            var loadPath = eachPath.Substring(eachPath.LastIndexOf("Assets"));
            var sprite =(Sprite)AssetDatabase.LoadAssetAtPath(loadPath, typeof(Sprite));
            return sprite;
        }
        return null;
    }
}
#endif