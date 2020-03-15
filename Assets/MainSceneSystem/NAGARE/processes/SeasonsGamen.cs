using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class SeasonsGamen : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        this.T.gameObject.SetActive(true);
        LoadingCanvas.target.LightUp();
        yield break;
    }
    
    public SeasonsGamen(preparingScene2 _preparingScene,RectTransform T)
    {
        this.thisProcessStep = MainSceneStep.SeasonsGamen;
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
        this.mainProcessRunner.TriggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this.T.gameObject.SetActive(false);
    }

    public override void LocalUpdate()
    {
    }
}

