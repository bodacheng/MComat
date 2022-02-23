using DummyLayerSystem;
using mainMenu;

public class ShopTopLayer : UILayer
{
    public static ShopTopLayer Open()
    {
        return UILayerLoader.Load(PreScene.target.T,"ShopTopLayer") as ShopTopLayer;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("ShopTopLayer");
    }
}
