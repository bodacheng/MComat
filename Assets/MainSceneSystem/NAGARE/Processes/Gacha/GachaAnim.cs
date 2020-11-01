using System.Collections;
using mainMenu;

public class GachaAnim : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        //EffectsManager.INIEffectsPool("gachastar", "defaultmagic", 3);
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
    }
    
    public override void LocalUpdate()
    {
    }
}
