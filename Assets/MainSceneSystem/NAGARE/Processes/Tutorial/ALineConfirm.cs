using mainMenu;

// Tutorial
public class ALineConfirm : MainSceneProcess
{
    public ALineConfirm()
    {
        Step = MainSceneStep.ALineConfirm;
        nextProcessStep = MainSceneStep.TutorialReturn;
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(TheNineSlot.target.ConfirmSkillChangeButton.transform);
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return 
        (
            MainSceneLogger.Logs[MainSceneLogger.Logs.Count - 1].step == MainSceneStep.MemberDetail_edit 
            &&
            MainSceneLogger.Logs[MainSceneLogger.Logs.Count - 1].description == "success"
        );
    }
}