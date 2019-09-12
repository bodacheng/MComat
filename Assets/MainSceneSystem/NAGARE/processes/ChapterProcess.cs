using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class ChapterProcess : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        this._LoadingCanvas.DarkOff(1f);
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(false);
        this.T.gameObject.SetActive(true);
        this._LoadingCanvas.LightUp();
        yield break;
    }
    
    public ChapterProcess(preparingScene _preparingScene,RectTransform T)
    {
        this.thisProcessStep = MainSceneStep.Chapter;
        this._preparingScene = _preparingScene;
        this.T = T;
        EelementsInherit(_preparingScene);
    }

    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this.T.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
    }
}
