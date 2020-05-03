using System.Collections;
using mainMenu;

public class GachaAnim : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        EffectAndHurtObjectLoading.Instance.IniEffectsPool("gachastar", "defaultmagic", 3);
        yield return GachaRender.target.TenGotchaAnimProcess(GachaManager.target.GetResult());
        PreScene.Instance.trySwitchToStep(MainSceneStep.GotchaResult,true);
    }
    
    public GachaAnim()
    {
        thisProcessStep = MainSceneStep.GotchaAnim;
        EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
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
