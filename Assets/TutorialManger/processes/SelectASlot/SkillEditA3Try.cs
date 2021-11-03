using mainMenu;

// Tutorial 3
public class SkillEditA3Try : TutorialProcess
{
    public SkillEditA3Try()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.HigtLightRect(TheNineSlot.target.A3DragAndDropCell.transform);
    }
        
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.focusingSlot == TheNineSlot.target.A3DragAndDropCell._SkillStoneSlot;
    }
}
