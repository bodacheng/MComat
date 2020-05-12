using mainMenu;

// Tutorial 1 
public class GoToStageOne : TutorialProcess
{
    public GoToStageOne()
    {
        Step = TutorialStep.GoToStage1;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(TutorialHelper.target.MemberEditButton.transform);
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.MemberDetail;
    }
}