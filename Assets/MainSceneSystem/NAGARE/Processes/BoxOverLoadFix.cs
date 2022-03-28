using mainMenu;

public class BoxOverLoadFix : MainSceneProcess
{
    private BoxOverLoadFixLayer layer;
    public BoxOverLoadFix()
    {
        Step = MainSceneStep.BoxOverLoadHelper;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        layer = BoxOverLoadFixLayer.Open();
    }
    
    public override void ProcessEnd()
    {
        BoxOverLoadFixLayer.Close();
    }
}