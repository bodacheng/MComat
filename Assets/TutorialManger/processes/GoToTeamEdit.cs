using mainMenu;

// Tutorial 1 
public class GoToTeamEdit : TutorialProcess
{
    public GoToTeamEdit()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(PreScene.target.TeamEditor.StartToTeamEditButton.transform);
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.TeamEditFront;
    }
    
    public override void LocalUpdate()
    {
    }
}