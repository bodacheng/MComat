using mainMenu;

public class ArenaPage : MainSceneProcess
{
    void EnterProcess()
    {
        PageTo.Go(MainSceneStep.Arena);
        ArenaManager.target.RefreshOpponent();
    }
    
    public ArenaPage()
    {
        Step = MainSceneStep.Arena;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        EnterProcess();
    }
    
    public override void ProcessEnd()
    {
    }
}