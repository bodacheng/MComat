using mainMenu;

// Tutorial
public class ALineConfirm : MainSceneProcess
{
    public ALineConfirm()
    {
        Step = MainSceneStep.ALineConfirm;
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(TheNineSlot.target.ConfirmSkillChangeButton.transform);
    }
    
    public override void ProcessEnd()
    {
    }
}
