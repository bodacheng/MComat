using System.Collections;
using mainMenu;

public class GachaAnim : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        yield return GachaManager.target.Gacha();
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        EffectsManager.IniEffectsPool("gachastar", "defaultmagic", 3);
        yield return GachaRender.target.TenGotchaAnimProcess(GachaManager.target.GetResult());
        PreScene.target.trySwitchToStep(MainSceneStep.GotchaResult,false);
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
    }
    
    public override void LocalUpdate()
    {
    }
}
