using mainMenu;
using FightScene;

// Tutorial 1 
public class WaitProcess : TutorialProcess
{
    public delegate bool WaitOverDelegate();

    WaitOverDelegate waitForThis;
    
    public WaitProcess(WaitOverDelegate waitOverDelegate)
    {
        waitForThis = waitOverDelegate;
        EelementsInherit(NetFightScene.target);
    }
        
    public override void ProcessEnd()
    {
        RealTimeGameProcessManager.target.Messages.gameObject.SetActive(true);
        RealTimeGameProcessManager.target.Messages.text = "hello kitty";
    }
    
    public override bool CanEnterOtherProcess()
    {
        return waitForThis();
    }
}