using mainMenu;

public class ShopTop : MainSceneProcess
{
    public ShopTop()
    {
        thisProcessStep = MainSceneStep.ShopTop;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        ShopManager.target.ShopTop.gameObject.SetActive(true);
    }
    
    public override void ProcessEnd()
    {
        ShopManager.target.ShopTop.gameObject.SetActive(false);
    }
    
    public override void LocalUpdate()
    {
    
    }
}