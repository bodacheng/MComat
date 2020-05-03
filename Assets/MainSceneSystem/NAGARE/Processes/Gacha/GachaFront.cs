using System.Collections;
using mainMenu;

public class GachaFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        yield return _modelShower.ShowModel(null);
        PreScene.Instance.MainMenuCanvas.gameObject.SetActive(true);
        GachaManager.target.GotchaCanvas.gameObject.SetActive(true);
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        CameraManager._camera.gameObject.SetActive(false);
        GachaRender.target.Camera.gameObject.SetActive(true);
        BackGroundPS.target.Off();
        GachaManager.target.GotchaFrontT.gameObject.SetActive(true);
        GachaManager.target.GotchaResultT.gameObject.SetActive(false);
        yield break;
    }
    
    public GachaFront()
    {
        thisProcessStep = MainSceneStep.GotchaFront;
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
        GachaManager.target.GotchaCanvas.gameObject.SetActive(false);
        CameraManager._camera.gameObject.SetActive(true);
        GachaRender.target.Camera.gameObject.SetActive(false);
    }
    
    public override void LocalUpdate()
    {
    }
}
