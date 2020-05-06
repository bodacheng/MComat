using mainMenu;

public class BoxOverLoadFix : MainSceneProcess
{
    public BoxOverLoadFix()
    {
        thisProcessStep = MainSceneStep.BoxOverLoadHelper;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        BoxOverLoadFixManager.target.T.gameObject.SetActive(true);
        BoxOverLoadFixManager.target.ArrangeButtonsFeature();
    }
    
    public override void ProcessEnd()
    {
        BoxOverLoadFixManager.target.T.gameObject.SetActive(false);
    }
    
    public override void LocalUpdate()
    {
    }
}
