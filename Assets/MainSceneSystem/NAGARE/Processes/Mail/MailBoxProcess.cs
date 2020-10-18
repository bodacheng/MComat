using System.Collections;
using mainMenu;

// 邮箱top
public class MailBoxProcess : MainSceneProcess
{
    public MailBoxProcess()
    {
        Step = MainSceneStep.MailBox;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        yield return MailManager.target.RequestMails(Setting.Language);
        MailManager.target.MailCanvas.gameObject.SetActive(true);
        MailManager.target.BoxPartT.gameObject.SetActive(true);
        yield break;
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        MailManager.target.BoxPartT.gameObject.SetActive(false);
    }
}
