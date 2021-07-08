using mainMenu;

// Tutorial 3  1
public class SkillEditA1Try : TutorialProcess
{
    bool waitCompleted;
    MonsterEditPage memberDetail_Edit;
    public SkillEditA1Try()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        waitCompleted = false;
        memberDetail_Edit = (MonsterEditPage)ProcessesRunner.Main.GetProcess(MainSceneStep.UnitSkillEdit);
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
        if (!waitCompleted)
        {
            if (MonsterEditPage.loadFinished)
            {
                LoadingCanvas.target.HigtLightRect(TheNineSlot.target.A1DragAndDropCell.transform);
                waitCompleted = true;
            }
        }
    }
}