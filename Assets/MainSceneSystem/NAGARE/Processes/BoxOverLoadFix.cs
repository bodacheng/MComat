using DummyLayerSystem;
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
        layer = UILayerLoader.Load<BoxOverLoadFixLayer>();
        layer.INI();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<BoxOverLoadFixLayer>();
    }
}