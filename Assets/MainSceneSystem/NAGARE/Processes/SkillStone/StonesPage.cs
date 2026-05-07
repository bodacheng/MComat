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
        ProgressLayer.Loading(string.Empty);
        await Stones.RenderAll();
        ProgressLayer.Close();
        layer = UILayerLoader.Load<StoneListLayer>();
        await layer.Setup();
        ReturnLayer.MoveFront();
        StoneLevelUpProccessor.CalUpdateAllForms();
        layer.levelManager.LevelUpAllStonesBtn.interactable = StoneLevelUpProccessor.HasStoneToBeUpdate();
        layer.levelManager.LevelUpAllStonesBtnAnimator.SetBool("on", StoneLevelUpProccessor.HasStoneToBeUpdate());
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<StoneUpdatesConfirm>();
        UILayerLoader.Remove<StoneListLayer>();
    }
}
