using mainMenu;

// Tutorial 2
public class OpenSkillEdit : TutorialProcess
{
    bool missionCompleted;
    MemberDetailProcess MemberDetailProcess;
    public OpenSkillEdit()
    {
        Step = TutorialStep.OpenSkillEdit;
        nextProcessStep = TutorialStep.SkillEditTry_A1Selected;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        missionCompleted = false;
        MemberDetailProcess = (MemberDetailProcess)ProcessesRunner.Main.GetProcess(MainSceneStep.MemberDetail);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.MemberDetail_edit;
    }
    
    public override void LocalUpdate()
    {        
        if (!missionCompleted)
        {
            if (MemberDetailProcess.loadFinished)
            {
                LoadingCanvas.target.HigtLightRect(TutorialHelper.target.SkillEditButton.transform);
                missionCompleted = true;
            }
        }
    }
}
