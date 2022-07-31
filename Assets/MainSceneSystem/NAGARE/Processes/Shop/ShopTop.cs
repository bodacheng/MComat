using mainMenu;

public class ShopTop : MSceneProcess
{
    private ShopTopLayer shopTopLayer;
    public ShopTop()
    {
        Step = MainSceneStep.ShopTop;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        shopTopLayer = ShopTopLayer.Open();
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        ShopTopLayer.Close();
    }
    
    public override void LocalUpdate()
    {
    
    }
}