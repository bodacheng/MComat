using System;
using System.Collections.Generic;
using dataAccess;
using DummyLayerSystem;
using mainMenu;

public class GotchaFront : MSceneProcess
{
    private GotchaLayer layer;
    
    public GotchaFront()
    {
        Step = MainSceneStep.GotchaFront;
    }
     
    public override void ProcessEnter()
    {
        StarsFall.target.gameObject.SetActive(true);
        var CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        BackGroundPS.target.Off();
        layer = UILayerLoader.Load<GotchaLayer>();
        layer.Setup(NineTimes, GetAllSK, GetAllM, Remove25Stones, DropTableInfo);
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<GotchaLayer>();
        StarsFall.target.gameObject.SetActive(false);
    }
    
    void DropTableInfo()
    {
        PreScene.target.trySwitchToStep(MainSceneStep.DropTableInfo,"GotchaX9", true);
    }
    
    /// <summary>
    /// 缺少消费关联处理
    /// </summary>
    static void NineTimes(Action<List<StoneOfPlayerInfo>> success)
    {
        UILayerLoader.Remove<GotchaLayer>();// 点击按钮瞬间关闭layer。
        CloudScript.GotchaX9(success.Invoke);
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
}
