using mainMenu;

// Tutorial 3  1
public class SkillEditA1Try : MainSceneProcess
{
    bool missionCompleted;
    MemberDetail_edit memberDetail_Edit;
    public SkillEditA1Try()
    {
        Step = MainSceneStep.SkillEditTry_A1Selected;
        nextProcessStep = MainSceneStep.SkillEditTry_A1Filled;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        missionCompleted = false;
        memberDetail_Edit = (MemberDetail_edit)ProcessesRunner.Main.GetProcess(MainSceneStep.MemberDetail_edit);
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.focusingSlot == TheNineSlot.target.A1DragAndDropCell._SkillStoneSlot;
    }
    
    public override void LocalUpdate()
    {
        if (!missionCompleted)
        {
            if (memberDetail_Edit.loadFinished)
            {
                LoadingCanvas.target.HigtLightRect(TheNineSlot.target.A1DragAndDropCell.transform);
                missionCompleted = true;
            }
        }
    }
}
