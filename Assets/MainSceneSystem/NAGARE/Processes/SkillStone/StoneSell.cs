using mainMenu;

public class StoneSell : MSceneProcess
{
    void EnterProcess()
    {
        //StonesPage.EnterProcess();  １１。８　临时逻辑
        //StoneDeleteManger.target.EnterDeleteMode();
        SetLoaded(true);
    }
    
    public StoneSell()
    {
        Step = MainSceneStep.SkillStones_Sell;
        Inherit(PreScene.target);
    }

    private StoneListLayer StoneListLayer;
    public override void ProcessEnter()
    {
        StoneListLayer = StoneListLayer.Open();
        EnterProcess();
    }
    
    public override void ProcessEnd()
    {
        //StoneDeleteManger.target.ExitDeleteMode();
        StoneListLayer.box._tabEffects.CloseShowingZokuseiTagEffects();
    }
}