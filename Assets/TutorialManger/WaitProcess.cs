using FightScene;

// Tutorial 1 
public class WaitProcess : TutorialProcess
{
    public delegate bool WaitOverDelegate();

    WaitOverDelegate waitForThis;
    
    public WaitProcess(WaitOverDelegate waitOverDelegate)
    {
        waitForThis = waitOverDelegate;
    }
        
    public override void ProcessEnd()
    {
        RTFightManager.target.Messages.gameObject.SetActive(true);
        RTFightManager.target.Messages.text = "hello kitty";
    }
    
    public override bool CanEnterOtherProcess()
    {
        return waitForThis();
    }
}