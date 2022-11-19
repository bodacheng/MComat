using DummyLayerSystem;
using mainMenu;

public class GotchaFront : MSceneProcess
{
    private GotchaLayer layer;
    
    public GotchaFront()
    {
        Step = MainSceneStep.GotchaFront;
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
        layer = UILayerLoader.Load<GotchaLayer>();
        layer.Setup();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<GotchaLayer>();
        StarsFall.target.gameObject.SetActive(false);
    }
}
