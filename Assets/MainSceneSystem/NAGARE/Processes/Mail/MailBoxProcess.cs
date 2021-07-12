using System.Collections;
using mainMenu;

// 邮件的获取应该是客户端向服务端请求应该能收获的邮件，
// 而非直观上的服务端向客户端发送邮件。
// 而客户端向服务端请求这个邮件按理说不需要那么的频繁。
// 最起码不至于每次打开邮件箱都去请求一次，那么突然增加服务器负担
// 当然这就是目前我的猜想
// 如果这个设想成立，那么都哪些时间点需要请求邮件列表呢？
// 1. 程序刚启动
// 2. 距离上次请求邮件20分钟后，再次点击了邮件按钮
// 3. 关卡进度更新后

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
        MailManager.target.MailCanvas.gameObject.SetActive(true);
        MailManager.target.BoxPartT.gameObject.SetActive(true);
        yield break;
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        MailManager.target.BoxPartT.gameObject.SetActive(false);
    }
}
