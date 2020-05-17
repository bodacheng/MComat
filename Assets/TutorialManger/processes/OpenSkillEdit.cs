using mainMenu;

// Tutorial 2
public class OpenSkillEdit : TutorialProcess
{
    bool waitCompleted;
    MemberDetailProcess MemberDetailProcess;
    public OpenSkillEdit()
    {
        Step = TutorialStep.OpenSkillEdit;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        waitCompleted = false;
        MemberDetailProcess = (MemberDetailProcess)ProcessesRunner.Main.GetProcess(MainSceneStep.MemberDetail);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.MemberDetail_edit;
    }
    
    public override void LocalUpdate()
    {        
        if (!waitCompleted)
        {
            if (MemberDetailProcess.loadFinished)
            {
                LoadingCanvas.target.HigtLightRect(TutorialHelper.target.SkillEditButton.transform);
                waitCompleted = true;
            }
        }
    }
}