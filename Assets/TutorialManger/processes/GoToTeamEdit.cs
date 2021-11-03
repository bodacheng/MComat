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
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.HigtLightRect(PreScene.target.TeamEditor.StartToTeamEditButton.transform);
    }
    
    public override void ProcessEnd()
    {
        PopupLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.TeamEditFront;
    }
    
    public override void LocalUpdate()
    {
    }
}