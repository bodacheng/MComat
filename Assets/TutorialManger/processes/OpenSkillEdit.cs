using mainMenu;
using UnityEngine;
// Tutorial 2
public class OpenSkillEdit : TutorialProcess
{
    bool waitCompleted;
    MonsterListPage MemberDetailProcess;
    public OpenSkillEdit()
    {
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        waitCompleted = false;
        MemberDetailProcess = (MonsterListPage)ProcessesRunner.Main.GetProcess(MainSceneStep.MonsterList);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return ProcessesRunner.Main.currentProcess.Step == MainSceneStep.UnitSkillEdit;
    }
    
    public override void LocalUpdate()
    {        
        if (!waitCompleted)
        {
            if (MemberDetailProcess.loadFinished)
            {
                PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
                popupLayer.HighLightRect(TutorialHelper.target.SkillEditButton.GetComponent<RectTransform>());
                waitCompleted = true;
            }
        }
    }
}