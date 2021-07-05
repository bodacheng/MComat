using mainMenu;

public class ArenaProcess : MainSceneProcess
{
    public void EnterProcess()
    {
        ArenaManager.target.RefreshOpponent();
    }
    
    public ArenaProcess()
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
        ArenaManager.target.ArenaCanvas.gameObject.SetActive(false);
    }
}