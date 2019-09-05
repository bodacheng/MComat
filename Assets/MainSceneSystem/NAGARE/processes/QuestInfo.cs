using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using mainMenu;

public class QuestInfo : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(false);
        this.T.gameObject.SetActive(true);
        //_QuestPreparePage.QuestName.text = _QuestPreparePage._Stage.battleNameENG;
        yield break;
    }
    
    public QuestInfo(preparingScene _preparingScene,RectTransform T)
    {
        this.thisProcessStep = MainSceneStep.QuestInfo;
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
