using mainMenu;

// Tutorial 2
public class OpenSkillEdit : TutorialProcess
{
    bool waitCompleted;
    MonsterListPage MemberDetailProcess;
    public OpenSkillEdit()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        waitCompleted = false;
        MemberDetailProcess = (MonsterListPage)ProcessesRunner.Main.GetProcess(MainSceneStep.MonsterList);
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