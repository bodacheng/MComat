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
        StarsFall.target.gameObject.SetActive(true);
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
        GotchaLayer.Close();
        StarsFall.target.gameObject.SetActive(false);
    }
}
