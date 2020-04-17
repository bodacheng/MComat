using System.Collections;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;

public class TeamEditFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        PreScene.Instance._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.Instance._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        _CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        _CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        yield return MonsterBox.DisplayMonsterIcons();
        TeamEditManager.Instance._Canvas.gameObject.SetActive(true);
        yield break;
    }
    
    public TeamEditFront()
    {
        thisProcessStep = MainSceneStep.TeamEditFront;
        EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        TeamEditManager.Instance._Canvas.gameObject.SetActive(false);
    }
    
    Vector3 screenPos = new Vector3(0.23f, 0.35f, 20f);
    public override void LocalUpdate()
    {
        if (!_MemberDetail._SkillsPrintOut.IfShowingSkill)
        {
            _modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
    
    public IEnumerator TeamEditMonsterDetailMonsterIconBehaviour()
    {
        GetMonsterOfPlayerDetailModel _AccountCharacterInfo = this._MemberDetail.focusingCharDataInfo;
        if (_AccountCharacterInfo == null)
        {
            Debug.Log("严重错误");yield break;
        }
        yield return TeamEditManager.Instance.MonsterIConButton(_AccountCharacterInfo.monsterOfPlayerId,TeamEditManager.focusingPosNum);
    }
}
