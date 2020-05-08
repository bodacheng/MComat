using mainMenu;

public class SkillEditTry_A3Filled : MainSceneProcess
{
    public SkillEditTry_A3Filled()
    {
        Step = MainSceneStep.SkillEditTry_A3Filled;
        nextProcessStep = MainSceneStep.ALineConfirm;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(SkillStonesBox.CellsDictionary[2].transform);
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.A3DragAndDropCell.GetItem() != null;
    }
}
