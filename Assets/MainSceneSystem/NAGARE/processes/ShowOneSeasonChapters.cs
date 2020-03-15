using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class ShowOneSeasonChapters : MainSceneProcess
{
    RectTransform T;
    public IEnumerator EnterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        this.T.gameObject.SetActive(true);
        LoadingCanvas.target.LightUp();
        yield break;
    }
    
    public ShowOneSeasonChapters(RectTransform T)
    {
        this.thisProcessStep = MainSceneStep.ChaptersOfOneSeason;
        this.T = T;
        this.EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        Debug.Log("ChaptersOfOneSeason " + PreScene.Instance._ReturnButtonManager.returnMissionList.Count);
        this.mainProcessRunner.TriggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
         this.T.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}

