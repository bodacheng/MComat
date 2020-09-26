using System.Collections;
using mainMenu;
using DG.Tweening;

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
        yield break;
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 3f, 0.1f);
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
    }
}
