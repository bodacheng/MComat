using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Starter))]
public class StarterGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var s = (Starter)target;
        if (GUILayout.Button("Refresh"))
        {
            s.Initialise();
        }
    }
}
#endif

[ExecuteInEditMode]
public class Starter : MonoBehaviour
{
    [SerializeField] PlayfabSetting PlayfabSetting;
    [SerializeField] FightGlobalSetting FightGlobalSetting;
    [SerializeField] KeywordSetting keywordSetting;
    
    public void Initialise()
    {
        Debug.Log("files loads...");
        FightGlobalSetting.Initialise();
        PlayfabSetting.Initialise();
        keywordSetting.Initialise();
        SkillConfigTable.LoadAllSkillConfigs();
        PowerEstimateTable.LoadByResource();
        Units.LoadByResource();
        Units.RefreshDic();
    }
    
    public void EnterFrontScene()
    {
        Initialise();
        var stage = FightInfo.RandomSkillTestStage(TeamMode.rotation);
        stage.EventType = FightEventType.Screensaver;
        //stage.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
        FightLoad.Go(stage);
    }
}
