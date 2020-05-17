using mainMenu;
using System.Collections.Generic;
using UnityEngine;

public class SkillEditTry_A1Filled : TutorialProcess
{
    public SkillEditTry_A1Filled()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        LoadingCanvas.target.HigtLightRect(new List<Transform> {SkillStonesBox.CellsDictionary[0].transform , TheNineSlot.target.A1DragAndDropCell.transform});
    }
    
    public override void ProcessEnd()
    {
    }
    
    public override bool CanEnterOtherProcess()
    {
        return TheNineSlot.target.A1DragAndDropCell.GetItem() != null;
    }
}