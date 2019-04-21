using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonsGamen : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        this._LoadingCanvas.DarkOff();
        this._SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
        //BigMenu_Fight.OnclickBeheviour();
        this.T.gameObject.SetActive(true);
        this._LoadingCanvas.LightUp();
        yield break;
    }
    
    public SeasonsGamen(preparingScene _preparingScene,RectTransform T)
    {
        this.step = MainSceneStep.SeasonsGamen;
        this._preparingScene = _preparingScene;
        this.T = T;
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

