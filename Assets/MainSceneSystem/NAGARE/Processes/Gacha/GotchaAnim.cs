using System.Collections;
using mainMenu;

public class GotchaAnim : MainSceneProcess
{
    private GotchaResultLayer gotchaResultLayer;
    public IEnumerator EnterProcess()
    {
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        PreScene.target.GotchaCamera.gameObject.SetActive(true);
        //EffectsManager.INIEffectsPool("gachastar", "defaultmagic", 3);
        yield return gotchaResultLayer.GotchaAnimProcess(GachaResult.Result);
        PreScene.target.trySwitchToStep(MainSceneStep.GotchaResult, false);
    }
    
    public GotchaAnim()
    {
        Step = MainSceneStep.GotchaAnim;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        gotchaResultLayer = GotchaResultLayer.Open();
        gotchaResultLayer.NineForShow.transform.gameObject.SetActive(false);
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.target.GotchaCamera.gameObject.SetActive(false);
        gotchaResultLayer.NineForShow.transform.gameObject.SetActive(true);
        gotchaResultLayer.Reset();
    }
}
