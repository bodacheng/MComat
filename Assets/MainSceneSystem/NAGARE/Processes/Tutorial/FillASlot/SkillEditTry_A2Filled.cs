using mainMenu;

public class SkillEditTry_A2Filled : MainSceneProcess
{
    public SkillEditTry_A2Filled()
    {
        Step = MainSceneStep.SkillEditTry_A2Filled;
        nextProcessStep = MainSceneStep.SkillEditTry_A3Selected;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(SkillStonesBox.CellsDictionary[1].transform);
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.A2DragAndDropCell.GetItem() != null;
    }
}
