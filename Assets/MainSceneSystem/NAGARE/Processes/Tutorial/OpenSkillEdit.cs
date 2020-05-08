using mainMenu;
using UnityEngine;

public class OpenSkillEdit : MainSceneProcess
{
    bool missionCompleted;
    MemberDetailProcess MemberDetailProcess;
    public OpenSkillEdit()
    {
        Step = MainSceneStep.OpenSkillEdit;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        missionCompleted = false;
        MemberDetailProcess = (MemberDetailProcess)ProcessesRunner.Main.GetProcess(MainSceneStep.MemberDetail);
    }
    
    public override void LocalUpdate()
    {        
        if (!missionCompleted)
        {
            Debug.Log("here");
            if (MemberDetailProcess.loadFinished)
            {
                LoadingCanvas.target.HigtLightRect(TutorialHelper.target.SkillEditButton.transform);
                missionCompleted = true;
            }
        }
    }
}
