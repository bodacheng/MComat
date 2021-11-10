using mainMenu;

// Tutorial 3
public class SkillEditA2Try : TutorialProcess
{
    public SkillEditA2Try()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        //popupLayer.HigtLightRect(TheNineSlot.target.A2DragAndDropCell.transform);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return false;
        //return TheNineSlot.target.focusingSlot == TheNineSlot.target.A2DragAndDropCell._SkillStoneSlot;
    }
    
    public override void ProcessEnd()
    {
    }
}
