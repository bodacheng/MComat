using System.Collections;
using mainMenu;

// 邮箱top
public class MailDetail : MainSceneProcess
{
    public MailDetail()
    {
        Step = MainSceneStep.MailDetail;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        MailManager.target.detailPartT.gameObject.SetActive(true);
        yield break;
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        MailManager.target.detailPartT.gameObject.SetActive(false);
    }
}
