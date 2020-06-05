using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartUpScenePresentation : MonoBehaviour
{
    [Space(7)]
    [Header("选择模式T")]
    public Button StartNewLocalMode,LocalMode,localDataDelete,NetMode;
    
    [Space(7)]
    [Header("ResourceLordSceneStarter")]
    public ResourceLordSceneStarter _resourceLordSceneStarter;

    [Space(7)]
    [Header("开发公司商标")]
    public Image logo;
    public Image bigPic;
    
    public bool pProcessFinished;
    
    // step1 商标显示
    // step2 可skip的小动画
    // step3 标题
    
    void Start()
    {
        StartCoroutine(_resourceLordSceneStarter.ResourcePrepareProcess());
        StartCoroutine(PresentationProcess());
    }
    
    void Update()
    {
        if (pProcessFinished && 
        _resourceLordSceneStarter.dProcessFinished 
        && !LocalMode.gameObject.activeSelf
        && FightGlobalSetting._programMode != FightGlobalSetting.ProgramMode.skillShow)
        {
            StartNewLocalMode.gameObject.SetActive(true);
            LocalMode.gameObject.SetActive(true);
            localDataDelete.gameObject.SetActive(true);
            NetMode.gameObject.SetActive(true);
        }
    }
    
    public IEnumerator PresentationProcess()
    {
        logo.gameObject.SetActive(false);
        bigPic.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LoadingCanvas.target.LightUp();
        pProcessFinished = true;
        
        if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
        {
            _resourceLordSceneStarter.DeleteLocalSaveDate();
            _resourceLordSceneStarter.StartNewLocalTestMode();
        }
        yield break;
    }
}
