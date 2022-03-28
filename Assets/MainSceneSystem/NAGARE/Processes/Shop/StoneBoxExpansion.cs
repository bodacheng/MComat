using mainMenu;

public class StoneBoxExpansion : MainSceneProcess
{
    private BoxExpandHelperLayer boxExpandHelperLayer;
    
    public StoneBoxExpansion()
    {
        Step = MainSceneStep.BoxExpansion;
        Inherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        boxExpandHelperLayer = BoxExpandHelperLayer.Open();
        boxExpandHelperLayer.ArrangeButtonsFeature();
    }
    
    public override void ProcessEnd()
    {
        BoxExpandHelperLayer.Close();
    }
    
    public override void LocalUpdate()
    {    
    }
}
