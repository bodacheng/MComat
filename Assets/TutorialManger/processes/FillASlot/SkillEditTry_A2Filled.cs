using mainMenu;
using System.Collections.Generic;
using UnityEngine;

public class SkillEditTry_A2Filled : TutorialProcess
{
    public SkillEditTry_A2Filled()
    {
        Step = TutorialStep.SkillEditTry_A2Filled;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(new List<Transform> {SkillStonesBox.CellsDictionary[1].transform , TheNineSlot.target.A2DragAndDropCell.transform});
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.A2DragAndDropCell.GetItem() != null;
    }
}
