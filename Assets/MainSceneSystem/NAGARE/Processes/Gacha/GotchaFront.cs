using mainMenu;

public class GotchaFront : MSceneProcess
{
    private GotchaLayer layer;
    
    public GotchaFront()
    {
        Step = MainSceneStep.GotchaFront;
        Inherit(PreScene.target);
    }
     
    public override void ProcessEnter()
    {
        StarsFall.target.gameObject.SetActive(true);
        var CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        BackGroundPS.target.Off();
        layer = GotchaLayer.Open();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        GotchaLayer.Close();
        StarsFall.target.gameObject.SetActive(false);
    }
}
