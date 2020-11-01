using System.Collections;
using mainMenu;

public class GachaAnim : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        EffectsManager.INIEffectsPool("gachastar", "defaultmagic", 3);
        GachaRender.target.Skip.gameObject.SetActive(true);
        yield return GachaRender.target.GotchaAnimProcess(GachaManager.target.GetResult());
        PreScene.target.trySwitchToStep(MainSceneStep.GotchaResult, false);
    }
    
    public GachaAnim()
    {
        Step = MainSceneStep.GotchaAnim;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        GachaRender.target.Reset();
        GachaRender.target.Skip.gameObject.SetActive(false);
    }
    
    public override void LocalUpdate()
    {
    }
}
