using mainMenu;

public class BoxOverLoadFix : MainSceneProcess
{
    private BoxOverLoadFixLayer boxOverLoadFixLayer;
    public BoxOverLoadFix()
    {
        Step = MainSceneStep.BoxOverLoadHelper;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        boxOverLoadFixLayer = BoxOverLoadFixLayer.Open();
    }
    
    public override void ProcessEnd()
    {
        BoxOverLoadFixLayer.Close();
    }
}