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
    
    public ShowOneSeasonChapters(PreScene _preparingScene,RectTransform T)
    {
        this.thisProcessStep = MainSceneStep.ChaptersOfOneSeason;
        this._preparingScene = _preparingScene;
        this.T = T;
        this.EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        Debug.Log("ChaptersOfOneSeason " + this._preparingScene._ReturnButtonManager.returnMissionList.Count);
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

