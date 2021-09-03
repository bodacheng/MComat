using mainMenu;
using dataAccess;

// 阅读邮件，获取邮件附带的礼物应该都属于远程更新
// 礼物其实可以靠代码来代表，服务器记录代码和礼物内容的对应关系
// 从一个礼物性邮件里获取礼物的过程靠一个已读flag和这个邮件代码就能控制好。
// 一个邮件设置只能获取一种报酬就可以
// 任何报酬的赋予都是服务端的工作，而反应在客户端上应该是一种根据远程结果进行刷新的机制

// 邮件详情
public class MailDetailProcess : MainSceneProcess
{
    public static string targetMailID;
    
    public MailDetailProcess()
    {
        Step = MainSceneStep.MailDetail;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        MailOfPlayerModel mail = MailManager.target.Get(targetMailID);
        MailManager.target.Read(mail);
        MailManager.target.detailPartT.gameObject.SetActive(true);
    }
    
    public override void ProcessEnd()
    {
        MailManager.target.detailPartT.gameObject.SetActive(false);
    }
}
