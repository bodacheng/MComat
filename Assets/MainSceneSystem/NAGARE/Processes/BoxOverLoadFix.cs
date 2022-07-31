using mainMenu;

public class BoxOverLoadFix : MSceneProcess
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
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        BoxOverLoadFixLayer.Close();
    }
}