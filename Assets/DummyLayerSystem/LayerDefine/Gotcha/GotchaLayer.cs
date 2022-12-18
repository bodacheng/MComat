using System;
using mainMenu;
using dataAccess;
using UnityEngine.UI;
using System.Collections.Generic;
using DummyLayerSystem;
using UnityEngine;

public class GotchaLayer : UILayer
{
    [SerializeField] private Button Gotcha1;
    [SerializeField] private Button Gotcha9;
    [SerializeField] private Button GetAllSKBtn;
    [SerializeField] private Button GetAllMBtn;
    [SerializeField] private Button Remove25StonesBtn;

    [SerializeField] private Button openDropTableInfo;

    private static Action extraSuccessAction;

    public static void SetExtraSuccessAction(Action _extraSuccessAction)
    {
        extraSuccessAction = _extraSuccessAction;
    }
    
    public void Setup()
    {
        Gotcha1.onClick.AddListener(OneTime);
        Gotcha9.onClick.AddListener(NineTimes);
        
        GetAllSKBtn.gameObject.SetActive(true);
        GetAllMBtn.gameObject.SetActive(true);
        Remove25StonesBtn.gameObject.SetActive(true);

        GetAllSKBtn.onClick.AddListener(GetAllSK);
        GetAllMBtn.onClick.AddListener(GetAllM);
        Remove25StonesBtn.onClick.AddListener(Remove25Stones);
        
        openDropTableInfo.onClick.AddListener(DropTableInfo);
    }

    void DropTableInfo()
    {
        PreScene.target.trySwitchToStep(MainSceneStep.DropTableInfo,"GotchaX9", true);
    }
    
    static void OneTime()
    {
    }
    
    /// <summary>
    /// 缺少消费关联处理
    /// </summary>
    static void NineTimes()
    {
        UILayerLoader.Remove<GotchaLayer>();// 点击按钮瞬间关闭layer。
        CloudScript.GotchaX9(temp);
        void temp(List<StoneOfPlayerInfo> stones)
        {
            GotchaResult.Result = stones;
            PreScene.target.trySwitchToStep(MainSceneStep.GotchaResult, true);
            extraSuccessAction?.Invoke();
        }
    }
    
    static void GetAllSK()
    {
        CloudScript.GrantStonesTest();
    }

    static void GetAllM()
    {
        CloudScript.GrantMonsterTest();
    }

    static void Remove25Stones()
    {
        CloudScript.Remove25Stones();
    }

    public void TutorialMode()
    {
        Gotcha1.gameObject.SetActive(false);
    }
}