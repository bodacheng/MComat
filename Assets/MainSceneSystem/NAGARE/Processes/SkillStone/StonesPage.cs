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
    }
    
    //EnterProcess()内绝不能出现triggerMainProcess
    async void EnterProcess()
    {
        await Stones.RenderAll();
        layer = UILayerLoader.Load<StoneListLayer>();
        layer.Setup();
        ReturnLayer.MoveFront();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<StoneListLayer>();
    }
}