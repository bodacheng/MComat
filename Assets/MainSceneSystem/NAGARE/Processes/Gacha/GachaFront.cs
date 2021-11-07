using System.Collections;
using mainMenu;
using System.Collections.Generic;

public class GachaFront : MainSceneProcess
{
    private GotchaLayer gotchaLayer;
    
    public IEnumerator EnterProcess()
    {
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        CameraManager._camera.gameObject.SetActive(false);
        PreScene.target.GotchaCamera.gameObject.SetActive(true);
        
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            yield break;
        }
        
        BackGroundPS.target.Off();
        PageTo.Go(MainSceneStep.GotchaFront);
        gotchaLayer = GotchaLayer.Open();
    }
    
    public GachaFront()
    {
        Step = MainSceneStep.GotchaFront;
        EelementsInherit(PreScene.target);
    }
     
    public override void ProcessEnter()
    {
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        CameraManager._camera.gameObject.SetActive(true);
        PreScene.target.GotchaCamera.gameObject.SetActive(false);
        GotchaLayer.Close();
    }
}
