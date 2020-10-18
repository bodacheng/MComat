using System.Collections;
using mainMenu;
using Api.Dto.Model;

// 邮箱top
public class MailDetailProcess : MainSceneProcess
{
    public static string targetMailID;

    public MailDetailProcess()
    {
        Step = MainSceneStep.MailDetail;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        GetMailOfPlayerModel mail = MailManager.target.Get(targetMailID);
        MailManager.target.Read(mail);
        MailManager.target.detailPartT.gameObject.SetActive(true);
        yield break;
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        //PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        MailManager.target.detailPartT.gameObject.SetActive(false);
    }
}
