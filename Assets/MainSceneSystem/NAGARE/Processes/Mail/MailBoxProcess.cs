using DummyLayerSystem;
using mainMenu;

// 迎合PlayFab的机制我们是把邮件作为“item”去看待

// 邮箱top
public class MailBoxProcess : MSceneProcess
{
    MailBox mailBox;
    public MailBoxProcess()
    {
        Step = MainSceneStep.MailBox;
    }
    
    public override void ProcessEnter()
    {
        mailBox = UILayerLoader.Load<MailBox>();
        mailBox.Setup();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<MailBox>();
    }
}
