#if UNITY_EDITOR

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
}
#endif