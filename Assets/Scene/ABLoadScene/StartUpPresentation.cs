using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StartUpPresentation : MonoBehaviour
{    
    [Space(7)]
    [Header("ResourceLordSceneStarter")]
    public ResourceLordSceneUtil _Util;
    
    [Space(7)]
    [Header("开发模式启动画面")]
    public RectTransform DevT;

    [Space(7)]
    [Header("开发公司商标")]
    public Image logo;
    
    SingleAssignmentDisposable Watershed;
    
    // step1 商标显示
    // step2 可skip的小动画
    // step3 标题
    
    void Start()
    {
        _Util.DProcessFinished = false;

        StartCoroutine(_Util.ResourcePrepareProcess());
        StartCoroutine(PresentationProcess());
    }
            
    public IEnumerator PresentationProcess()
    {
        // step1 ：商标显示
        LoadingCanvas.target.LightUp();
        logo.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        logo.gameObject.SetActive(false);
        
        // step2:主洁面
        Watershed = new SingleAssignmentDisposable
        {
            Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
                    {
                        _Util.DeleteLocalSaveDate();
                        _Util.StartNewLocalTestMode();
                        Watershed.Dispose();
                        return;
                    }
                    
                    if (_Util.DProcessFinished)
                    {
                        switch (_Util.ProjectPlayerInfoRefMode)
                        {
                            case PlayerInfoRefMode.toBeSelect:
                                DevT.gameObject.SetActive(true);
                                break;
                            case PlayerInfoRefMode.formalVersion:
                                break;
                            case PlayerInfoRefMode.localTestSaveData:
                                _Util.BeginLocalTestMode();
                                break;
                            case PlayerInfoRefMode.remoteTestPlayer:
                                _Util.BeginRemoteTestMode();
                                break;
                        }
                        Watershed.Dispose();
                    }
                }
            )
        };
    }
}
