using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontPage : MainSceneProcess
{
    RectTransform T;
    public frontPage(preparingScene _preparingScene,RectTransform T)
    {
        this.step = MainSceneStep.frontPage;
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
        this._preparingScene.setBattleEntryNum(4);
        this._preparingScene.triggerPresentationProcess(this._preparingScene.displayMy4V4Team(false, PosNum.none));
        this._SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
        this._CameraManager.Assign_Camera(Camera_Mode_Num.LockCamera, new List<Transform>()
        {
            this._preparingScene.TeamEditWatchPoint
        });
        this.T.gameObject.SetActive(true);
    }
    
    public override void ProcessEnd()
    {
        this.T.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
        this._preparingScene.showModelPositionAdjusting();
    }
}
