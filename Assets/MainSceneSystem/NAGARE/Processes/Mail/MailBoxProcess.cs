using UnityEngine;
using mainMenu;

// 迎合PlayFab的机制我们是把邮件作为“item”去看待

// 邮箱top
public class MailBoxProcess : MainSceneProcess
{
    MailBox mailBox;
    public MailBoxProcess()
    {
        Step = MainSceneStep.MailBox;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        mailBox = UILayerLoader.Load(PreScene.target.T, "MailBox") as MailBox;
        mailBox.GenerateMailModels();
        mailBox.AddButtonFeatures();
    }
    
    public override void ProcessEnd()
    {
        if (mailBox != null)
            GameObject.Destroy(mailBox.gameObject);
    }
}
