using mainMenu;

// Tutorial 1 
public class ClickTeamEditSlotOne : TutorialProcess
{
    public ClickTeamEditSlotOne()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(PreScene.target.TeamEditor.team1front.transform);
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return PreScene.target.TeamEditor.focusingPosNum == 0;
    }
}