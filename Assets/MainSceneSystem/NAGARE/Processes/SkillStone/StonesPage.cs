using mainMenu;

public class StonesPage : MSceneProcess
{
    public StonesPage()
    {
        Step = MainSceneStep.SkillStoneList;
        Inherit(PreScene.target);
    }
    
    private StoneListLayer stoneListLayer;
    
    public override void ProcessEnter()
    {
        ProcessEnter<Any>(null);
        SetLoaded(true);
    }
    
    public override void ProcessEnter<T>(T t)
    {
        if (t != null)
            EnterProcess(t);
        else
            EnterProcess();
        SetLoaded(true);
    }

    //EnterProcess()内绝不能出现triggerMainProcess
    void EnterProcess()
    {
        var CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        
        CommonEnterProcess();
    }
    
    //EnterProcess()内绝不能出现triggerMainProcess
    void EnterProcess<T>(T t)
    {
        var CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        
        CommonEnterProcess();
        stoneListLayer.levelManager.OpenLevelUpPage(t as string);
    }
    
    void CommonEnterProcess()
    {
        stoneListLayer = StoneListLayer.Open();
    }
    
    public override void ProcessEnd()
    {
        StoneListLayer.Close();
    }
}