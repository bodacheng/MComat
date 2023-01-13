using dataAccess;
using DummyLayerSystem;
using mainMenu;

public class StonesPage : MSceneProcess
{
    public StonesPage()
    {
        Step = MainSceneStep.SkillStoneList;
    }
    
    private StoneListLayer layer;
    
    public override void ProcessEnter()
    {
        EnterProcess();
        SetLoaded(true);
    }
    
    //EnterProcess()内绝不能出现triggerMainProcess
    void EnterProcess()
    {
        CommonEnterProcess();
    }
    
    void CommonEnterProcess()
    {
        layer = UILayerLoader.Load<StoneListLayer>();
        layer.Setup();
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<StoneListLayer>();
    }
}