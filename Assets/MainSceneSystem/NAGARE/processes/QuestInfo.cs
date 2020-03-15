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
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        this.T.gameObject.SetActive(true);
        PreScene.Instance.MainMenuBottonsT.gameObject.SetActive(false);
        //_QuestPreparePage.QuestName.text = _QuestPreparePage._Stage.battleNameENG;
        yield break;
    }
    
    public QuestInfo(PreScene _preparingScene,RectTransform T)
    {
        this.thisProcessStep = MainSceneStep.QuestInfo;
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
