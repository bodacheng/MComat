using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IngameDebugConsole;
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
            s.Initialise().Forget();
        }
    }
}
#endif

[ExecuteInEditMode]
public class Starter : MonoBehaviour
{
    [SerializeField] PlayfabSetting playFabSetting;
    [SerializeField] FightGlobalSetting fightGlobalSetting;
    [SerializeField] CommonSetting commonSetting;
    [SerializeField] DefaultIconSetting defaultIconSetting;
    [SerializeField] DebugLogManager inGameDebugConsole;
    [SerializeField] List<string> downLoadLabels;
    public List<string> DownLoadLabels => downLoadLabels;

    public static bool ConfigInitialised = false;

    void Awake()
    {
        if (Application.isEditor)
        {
            Initialise().Forget();
        }
    }

    public async UniTask Initialise()
    {
        ConfigInitialised = false;
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        AddressablesLogic.ReleaseAsyncOperationHandles();
        commonSetting.Initialise();

        inGameDebugConsole.gameObject.SetActive(CommonSetting.DevMode);
        
        fightGlobalSetting.Initialise();
        playFabSetting.Initialise();
        defaultIconSetting.Initialise();
        await UniTask.WhenAll(
            new List<UniTask>()
            {
                SkillConfigTable.LoadAllSkillConfigs(),
                PowerEstimateTable.LoadFile(),
                Units.LoadUnitConfigs(),
                Translate.LoadLanguageCodes(),
                UnitPassiveTable.Load()
            }
        );
        //MobileAds.Initialize(initStatus => { Debug.Log(initStatus);});
        Debug.Log("Config Files Loaded With No Errors");
        ConfigInitialised = true;
    }
    
    public void EnterFrontScene()
    {
        var stage = FightInfo.RandomSkillTestStage(TeamMode.Rotation);
        stage.EventType = FightEventType.Screensaver;
        FightLoad.Go(stage);
    }
}
