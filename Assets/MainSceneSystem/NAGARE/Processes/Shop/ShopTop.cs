using mainMenu;

public class ShopTop : MainSceneProcess
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
    }
    
    public override void ProcessEnd()
    {
        ShopTopLayer.Close();
    }
    
    public override void LocalUpdate()
    {
    
    }
}