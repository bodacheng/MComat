using System.Collections;
using mainMenu;
using System.Collections.Generic;

public class GachaFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        yield return ModelShower.target.ShowMyModel(null);
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        CameraManager._camera.gameObject.SetActive(false);
        GachaRender.target.Camera.gameObject.SetActive(true);
        
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            yield break;
        }
        
        BackGroundPS.target.Off();
        PreScene.target.MainMenuCanvas.gameObject.SetActive(true);
        GachaManager.target.GotchaCanvas.gameObject.SetActive(true);
        GachaManager.target.GotchaFrontT.gameObject.SetActive(true);
        GachaManager.target.GotchaResultT.gameObject.SetActive(false);
        yield break;
    }
    
    public GachaFront()
    {
        Step = MainSceneStep.GotchaFront;
        EelementsInherit(PreScene.target);
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
}
