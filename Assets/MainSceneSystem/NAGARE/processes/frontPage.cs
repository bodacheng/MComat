using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class frontPage : MainSceneProcess
{
    public frontPage(preparingScene _preparingScene)
    {
        thisProcessStep = MainSceneStep.frontPage;
        this._preparingScene = _preparingScene;
        EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public IEnumerator enterProcess()
    {
        _preparingScene.MainMenuCanvas.gameObject.SetActive(true);
        preparingScene.Instance.MainMenuBottonsT.gameObject.SetActive(true);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        _preparingScene.FightModeChooseT.gameObject.SetActive(true);

        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        _CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        _MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);

        yield return TeamSet.Instance.LoadTeamSet(TeamSetGameMode.story);
        
        if (TeamSet.Instance.storyModeTeamSet != null)
        {
            string focusLocalid = TeamSet.Instance.storyModeTeamSet.GetPositionMonsterOfPlayerId(0);
            if (focusLocalid != null)
            {
                 yield return _MemberDetail.SetMemberDetailSystemFocusingCharacter(focusLocalid);//确立focusing角色
                yield return _modelShower.ShowThisCharacterModel(focusLocalid);
            }
        }
        yield break;
    
    }
        
    public override void ProcessEnter()
    {
        this.mainProcessRunner.TriggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._preparingScene.FightModeChooseT.gameObject.SetActive(false);
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void LocalUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.IfShowingSkill)
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
