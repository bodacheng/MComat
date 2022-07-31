using mainMenu;

// 迎合PlayFab的机制我们是把邮件作为“item”去看待

// 邮箱top
public class MailBoxProcess : MSceneProcess
{
    MailBox mailBox;
    public MailBoxProcess()
    {
        Step = MainSceneStep.MailBox;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        mailBox = MailBox.Open();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        MailBox.Close();
    }
}
