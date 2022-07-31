using mainMenu;
using UnityEngine;

// Tutorial 2
public class OpenSkillEdit : TutorialProcess
{
    bool waitCompleted;
    UnitListPage MemberDetailProcess;
    public OpenSkillEdit()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        waitCompleted = false;
        MemberDetailProcess = (UnitListPage)ProcessesRunner.Main.GetProcess(MainSceneStep.UnitList);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.UnitSkillEdit;
    }
    
    public override void LocalUpdate()
    {        
        if (!waitCompleted)
        {
            if (MemberDetailProcess.GetLoaded())
            {
                PopupLayer.HighLightRect(PreScene.target.T, TutorialHelper.target.SkillEditButton.GetComponent<RectTransform>());
                waitCompleted = true;
            }
        }
    }
}