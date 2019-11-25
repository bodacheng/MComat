using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class ShowOneSeasonChapters : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        this._LoadingCanvas.DarkOff(1f);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        this.T.gameObject.SetActive(true);
        this._LoadingCanvas.LightUp();
        yield break;
    }
    
    public ShowOneSeasonChapters(preparingScene _preparingScene,RectTransform T)
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
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
         this.T.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}

