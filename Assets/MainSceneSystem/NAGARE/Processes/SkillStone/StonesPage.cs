using DummyLayerSystem;
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
    
    void CommonEnterProcess()
    {
        stoneListLayer = UILayerLoader.Load<StoneListLayer>();
        stoneListLayer.Setup();
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<StoneListLayer>();
    }
}