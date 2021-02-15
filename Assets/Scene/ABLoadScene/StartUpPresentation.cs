using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StartUpPresentation : MonoBehaviour
{
    [Space(7)]
    [Header("runner")]
    public SingleThreadProcesser runner;

    [Space(7)]
    [Header("Starter")]
    public Starter Starter;

    [Space(7)]
    [Header("ResourceLordSceneStarter")]
    public ResourceDownLoad ResourceDownLoad;
    
    [Space(7)]
    [Header("开发模式启动画面")]
    public RectTransform DevT;

    [Space(7)]
    [Header("开发公司商标")]
    public Image logo;

    // step1 商标显示
    // step2 可skip的小动画
    // step3 标题

    void Awake()
    {
        SingleThreadProcesser.backup = runner;
    }

    void Start()
    {
        ResourceDownLoad.DProcessFinished = false;
        StartCoroutine(ResourceDownLoad.ResourcePrepareProcess());
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
        SingleAssignmentDisposable Watershed = null;
        Watershed = new SingleAssignmentDisposable
        {
            Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
                    {
                        Starter.ToSkillShowerMode();
                        Watershed.Dispose();
                        return;
                    }
                    
                    if (ResourceDownLoad.DProcessFinished)
                    {
                        switch (Starter.ProjectPlayerInfoRefMode)
                        {
                            case PlayerInfoRefMode.toBeSelect:
                                DevT.gameObject.SetActive(true);
                                break;
                            case PlayerInfoRefMode.formalVersion:
                                break;
                            case PlayerInfoRefMode.localTestSaveData:
                                Starter.BeginLocalTestMode();
                                break;
                            case PlayerInfoRefMode.remoteTestPlayer:
                                break;
                        }
                        Watershed.Dispose();
                    }
                }
            )
        };
    }
}
