#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FightInfo))]
public class FightInfoGUI : Editor
{
    private FightMemberManager _fightMemberManager;
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
            _fightMemberManager = new FightMemberManager();
            initialized = true;
        }
        _fightMemberManager.OnGUIView(fightInfo.FightMembers);
        
        if (GUILayout.Button("Save"))
        {
            fightInfo.SaveDicToData();
            EditorUtility.SetDirty(fightInfo);
        }
    }
}
#endif