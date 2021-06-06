using mainMenu;
using System.Collections.Generic;
using UnityEngine;

public class SkillEditTry_A3Filled : TutorialProcess
{
    public SkillEditTry_A3Filled()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        //LoadingCanvas.target.HigtLightRect(new List<Transform> {SkillStonesBox.CellsDic[2].transform , TheNineSlot.target.A3DragAndDropCell.transform});
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.A3DragAndDropCell.GetItem() != null;
    }
}
