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
        shopTopLayer.Initialize();
        var upperInfoBar = UILayerLoader.Load<UpperInfoBar>();
        upperInfoBar.Setup(null,
            null, 
            null,
            null);
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<UpperInfoBar>();
        UILayerLoader.Remove<ShopTopLayer>();
    }
    
    public override void LocalUpdate()
    {
    
    }
}