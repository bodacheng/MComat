using mainMenu;

// Tutorial 1 
public class ClickTeamEditSlotOne : TutorialProcess
{
    public override void ProcessEnter()
    {
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        //popupLayer.HigtLightRect(PreScene.target.TeamEditor.team1front.transform);
    }
    
    public override void ProcessEnd()
    {
        PopupLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return false; //PreScene.target.TeamEditor.focusingPosNum == 0;
    }
}