using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfFightFront : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        this._CameraManager.Assign_Camera(Camera_Mode_Num.LockCamera);
        this._CameraManager.current_Camera_Mode.targets = new List<Transform>() { _MemberDetail.MemDetailWatchPos };
        this._LoadingCanvas.DarkOff();
        this._SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
        this._SelfFightManager.clear();
        this._SelfFightManager.INITeamPosButtons();
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return (this._MonsterBox.myMonsterBox());
        this.T.gameObject.SetActive(true);
        this._LoadingCanvas.LightUp();
    }
    
    public SelfFightFront(preparingScene _preparingScene,RectTransform T)
    {
        this.step = MainSceneStep.SelfFightFront;
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
