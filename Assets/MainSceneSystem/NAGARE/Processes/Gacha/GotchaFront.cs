using mainMenu;
using System.Collections.Generic;

public class GotchaFront : MainSceneProcess
{
    private GotchaLayer layer;
    
    public GotchaFront()
    {
        Step = MainSceneStep.GotchaFront;
        EelementsInherit(PreScene.target);
    }
     
    public override void ProcessEnter()
    {
        _CameraManager.Assign_Camera(C_Mode.NULL, null);
        //CameraManager._camera.gameObject.SetActive(false);
        //PreScene.target.GotchaCamera.gameObject.SetActive(true);
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        BackGroundPS.target.Off();
        layer = GotchaLayer.Open();
    }
    
    public override void ProcessEnd()
    {
        CameraManager._camera.gameObject.SetActive(true);
        //PreScene.target.GotchaCamera.gameObject.SetActive(false);
        GotchaLayer.Close();
    }
}
