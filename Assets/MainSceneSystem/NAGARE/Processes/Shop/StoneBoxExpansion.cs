using mainMenu;

public class StoneBoxExpansion : MainSceneProcess
{
    public StoneBoxExpansion()
    {
        Step = MainSceneStep.BoxExpansion;
        EelementsInherit(PreScene.target);
    }

    public override void ProcessEnter()
    {
        BoxExpandHelper.target.ExpansionT.gameObject.SetActive(true);
        BoxExpandHelper.target.ArrangeButtonsFeature();
    }
    
    public override void ProcessEnd()
    {
        BoxExpandHelper.target.ExpansionT.gameObject.SetActive(false);    
    }
            
    public override void LocalUpdate()
    {    
    }
}
