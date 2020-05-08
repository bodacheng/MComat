using mainMenu;

public class SkillEditTry_A1Filled : MainSceneProcess
{
    public SkillEditTry_A1Filled()
    {
        Step = MainSceneStep.SkillEditTry_A1Filled;
        nextProcessStep = MainSceneStep.SkillEditTry_A2Selected;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(SkillStonesBox.CellsDictionary[0].transform);
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.A1DragAndDropCell.GetItem() != null;
    }
}
