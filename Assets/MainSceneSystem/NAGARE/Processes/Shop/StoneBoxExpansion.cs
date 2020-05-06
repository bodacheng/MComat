using mainMenu;

public class StoneBoxExpansion : MainSceneProcess
{
    public StoneBoxExpansion()
    {
        thisProcessStep = MainSceneStep.BoxExpansion;
        EelementsInherit(PreScene.target);
    }

    public override void ProcessEnter()
    {
        BoxManageHelper.target.ExpansionT.gameObject.SetActive(true);
        BoxManageHelper.target.ArrangeButtonsFeature();
    }
    
    public override void ProcessEnd()
    {
        BoxManageHelper.target.ExpansionT.gameObject.SetActive(false);    
    }
            
    public override void LocalUpdate()
    {    
    }
}
