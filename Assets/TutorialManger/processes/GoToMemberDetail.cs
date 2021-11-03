using mainMenu;

// Tutorial 1 
public class GoToMemberDetail : TutorialProcess
{
    public GoToMemberDetail()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.HigtLightRect(TutorialHelper.target.MemberEditButton.transform);
    }
    
    public override void ProcessEnd()
    {
        PopupLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.MonsterList;
    }
}