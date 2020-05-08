using mainMenu;

// Tutorial 3
public class SkillEditA3Try : MainSceneProcess
{
    public SkillEditA3Try()
    {
        Step = MainSceneStep.SkillEditTry_A3Selected;
        nextProcessStep = MainSceneStep.SkillEditTry_A3Filled;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(TheNineSlot.target.A3DragAndDropCell.transform);
    }
        
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.focusingSlot == TheNineSlot.target.A3DragAndDropCell._SkillStoneSlot;
    }
}
