using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;

public class SelfFightFront : MainSceneProcess
{
    public IEnumerator enterProcess()
    {
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(false);
        this._SelfFightManager.clear();
        yield return this._SelfFightManager.INITeamPosButtons();
        this._MonsterBox.MonsterBoxContainer.gameObject.SetActive(true);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return (this._MonsterBox.myMonsterBox());
        this._SelfFightManager.selfFightUI.gameObject.SetActive(true);
    }
    
    public SelfFightFront(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.SelfFightFront;
        this._preparingScene = _preparingScene;
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
        this._SelfFightManager.selfFightUI.gameObject.SetActive(false);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
    }
}
