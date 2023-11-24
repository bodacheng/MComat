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
    [SerializeField] PlayFabSetting playFabSetting;
    [SerializeField] CommonSetting commonSetting;
    [SerializeField] DefaultIconSetting defaultIconSetting;
    [SerializeField] DebugLogManager inGameDebugConsole;
    
    public static bool ConfigInitialised = false;

    void Awake()
    {
        if (Application.isEditor)
        {
            Initialise().Forget();
        }
    }

    public List<string> DownLoadLabels => commonSetting.DownLoadLabels;

    public async UniTask Initialise()
    {
        ConfigInitialised = false;
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        AddressablesLogic.ReleaseAsyncOperationHandles();
        commonSetting.Initialise();
        inGameDebugConsole.gameObject.SetActive(CommonSetting.DevMode);
        playFabSetting.Initialise();
        defaultIconSetting.Initialise();
        await UniTask.WhenAll(
            new List<UniTask>()
            {
                SkillConfigTable.LoadAllSkillConfigs(),
                PowerEstimateTable.LoadFile(),
                Units.LoadUnitConfigs(),
                Translate.LoadLanguageCodes(),
                ShortStory.LoadLanguageCodes(),
                GBShortStory.LoadLanguageCodes(),
                UnitPassiveTable.Load(),
                FightGlobalSetting.LoadFightParams()
            }
        );
        //MobileAds.Initialize(initStatus => { Debug.Log(initStatus);});
        ConfigInitialised = true;
    }
    
    public void EnterFrontScene()
    {
        var stage = FightInfo.ScreenSaverStage(TeamMode.Rotation);
        stage.EventType = FightEventType.Screensaver;
        FightLoad.Go(stage);
    }
}
