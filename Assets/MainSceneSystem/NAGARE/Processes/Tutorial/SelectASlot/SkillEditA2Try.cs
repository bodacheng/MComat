using mainMenu;

// Tutorial 3
public class SkillEditA2Try : TutorialProcess
{
    public SkillEditA2Try()
    {
        Step = TutorialStep.SkillEditTry_A2Selected;
        nextProcessStep = TutorialStep.SkillEditTry_A2Filled;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(TheNineSlot.target.A2DragAndDropCell.transform);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.focusingSlot == TheNineSlot.target.A2DragAndDropCell._SkillStoneSlot;
    }
    
    public override void ProcessEnd()
    {
    }
}
