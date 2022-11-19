using DummyLayerSystem;
using mainMenu;

public class ShopTop : MSceneProcess
{
    private ShopTopLayer shopTopLayer;
    public ShopTop()
    {
        Step = MainSceneStep.ShopTop;
    }
    
    public override void ProcessEnter()
    {
        shopTopLayer = UILayerLoader.Load<ShopTopLayer>();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<ShopTopLayer>();
    }
    
    public override void LocalUpdate()
    {
    
    }
}