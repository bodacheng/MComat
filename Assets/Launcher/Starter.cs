using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    [SerializeField] CommonSetting commonSetting;
    [SerializeField] DefaultIconSetting defaultIconSetting;
    [SerializeField] List<string> downLoadLabels;
    public List<string> DownLoadLabels => downLoadLabels;

    void Awake()
    {
        if (Application.isEditor)
        {
            Initialise();
        }
    }

    public async void Initialise()
    {
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        AddressablesLogic.ReleaseAsyncOperationHandles();
        commonSetting.Initialise();
        fightGlobalSetting.Initialise();
        playFabSetting.Initialise();
        defaultIconSetting.Initialise();
        await UniTask.WhenAll(
            new List<UniTask>()
            {
                SkillConfigTable.LoadAllSkillConfigs(),
                PowerEstimateTable.LoadFile(),
                Units.LoadUnitConfigs(),
                Translate.LoadLanguageCodes()
            }
        );
        //MobileAds.Initialize(initStatus => { Debug.Log(initStatus);});
        Debug.Log("Config Files Loaded With No Errors");
    }
    
    public void EnterFrontScene()
    {
        var stage = FightInfo.RandomSkillTestStage(TeamMode.Rotation);
        stage.EventType = FightEventType.Screensaver;
        FightLoad.Go(stage);
    }
}
