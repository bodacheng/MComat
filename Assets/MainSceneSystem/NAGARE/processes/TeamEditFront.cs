using System.Collections;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;

public class TeamEditFront : MainSceneProcess
{
    public IEnumerator enterProcess()
    {
        this._LoadingCanvas.DarkOff(0.5f);
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(true);
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        yield return this._preparingScene._MonsterBox.myMonsterBox();
        this._TeamEditManager.OpenButtons(true);
        this._LoadingCanvas.LightUp();
        yield break;
    }
    
    public TeamEditFront(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.TeamEditFront;
        this._preparingScene = _preparingScene;
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
        this._preparingScene._TeamEditManager.OpenButtons(false);
    }
    
    Vector3 screenPos = new Vector3(0.23f, 0.35f, 20f);
    public override void localUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.ifShowingSkill())
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
    
    public IEnumerator TeamEditMonsterDetailMonsterIconBehaviour()
    {
        GetMonsterOfPlayerDetailModel _AccountCharacterInfo = this._MemberDetail.focusingCharacterDataInfo;
        if (_AccountCharacterInfo == null)
        {
            Debug.Log("严重错误");yield break;
        }
        yield return this._TeamEditManager.monsterIConButton(_AccountCharacterInfo.monsterOfPlayerId,TeamEditManager.focusingPosNum);
    }
}
