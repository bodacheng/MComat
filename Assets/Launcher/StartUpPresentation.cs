using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StartUpPresentation : MonoBehaviour
{
    public GameObject T;
    
    [Space(7)]
    [Header("Starter")]
    public Starter Starter;

    [Space(7)]
    [Header("ResourceLordSceneStarter")]
    public ResourceDownLoad ResourceDownLoad;
    
    // step1 商标显示
    // step2 可skip的小动画
    // step3 标题
    private LogoLayer LogoLayer;
    
    void Start()
    {
        LogoLayer =  UILayerLoader.Load(T,"LogoLayer") as LogoLayer;
        LogoLayer.Nagare();
        StartCoroutine(ResourceDownLoad.ResourcePrepareProcess());
        PresentationProcess();
    }

    void PresentationProcess()
    {
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
                    
                    if (ResourceDownLoad.finished && LogoLayer.finished)
                    {
                        Starter.BeginNetMode();
                        Watershed.Dispose();
                    }
                }
            )
        };
    }
}
