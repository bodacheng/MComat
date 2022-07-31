using mainMenu;
using UnityEngine;
// Tutorial 1 
public class GoToMemberDetail : TutorialProcess
{
    public GoToMemberDetail()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        PopupLayer.HighLightRect(PreScene.target.T, TutorialHelper.target.MemberEditButton.GetComponent<RectTransform>());
    }
    
    public override void ProcessEnd()
    {
        PopupLayer.Close();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.UnitList;
    }
}