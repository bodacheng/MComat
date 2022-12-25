using Unity.Collections;
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
    [SerializeField] PlayfabSetting playFabSetting;
    [SerializeField] FightGlobalSetting fightGlobalSetting;
    [SerializeField] KeywordSetting keywordSetting;
    [SerializeField] DefaultIconSetting defaultIconSetting;

    public void Initialise()
    {
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        
        Debug.Log("files loads...");
        fightGlobalSetting.Initialise();
        playFabSetting.Initialise();
        keywordSetting.Initialise();
        Translate.LoadLanguageCodes();
        defaultIconSetting.Initialise();
        SkillConfigTable.LoadAllSkillConfigs();
        PowerEstimateTable.LoadByResource();
        Units.LoadByResource();
        Units.RefreshDic();
    }
    
    public void EnterFrontScene()
    {
        var stage = FightInfo.RandomSkillTestStage(TeamMode.Rotation);
        stage.EventType = FightEventType.Screensaver;
        FightLoad.Go(stage);
    }
}
