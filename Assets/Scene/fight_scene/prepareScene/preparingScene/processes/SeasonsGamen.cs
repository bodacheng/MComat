using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonsGamen : MainSceneProcess
{
    RectTransform T;
    ProjectStagesManger _ProjectStagesManger;
    public IEnumerator enterProcess()
    {
        this._LoadingCanvas.DarkOff();
        this._SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
        _ProjectStagesManger.showThisSeasonGamen(-1);
        this.T.gameObject.SetActive(true);
        this._LoadingCanvas.LightUp();
        yield break;
    }
    
    public SeasonsGamen(preparingScene _preparingScene,ProjectStagesManger _ProjectStagesManger,RectTransform T)
    {
        this.step = MainSceneStep.SeasonsGamen;
        this._preparingScene = _preparingScene;
        this.T = T;
        this._ProjectStagesManger = _ProjectStagesManger;
        this.EelementsInherit(_preparingScene);
    }

    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this._preparingScene.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this.T.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
    }
}

