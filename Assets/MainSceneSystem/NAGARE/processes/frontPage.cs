using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;

public class frontPage : MainSceneProcess
{
    public frontPage(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.frontPage;
        this._preparingScene = _preparingScene;
        EelementsInherit(_preparingScene);
    }

    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public IEnumerator enterProcess()
    {
        this._preparingScene.MainMenuCanvas.gameObject.SetActive(true);
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(false);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
        this._preparingScene.FightModeChooseT.gameObject.SetActive(true);

        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);

        yield return TeamSet.Instance.loadTeamSet(TeamSetGameMode.story);
        
        if (TeamSet.Instance.storyModeTeamSet != null)
        {
            string focusLocalid = TeamSet.Instance.storyModeTeamSet.getPositionMonsterOfPlayerId(PosNum.back);
            if (focusLocalid != null)
            {
                 yield return _MemberDetail.SetMemberDetailSystemFocusingCharacter(focusLocalid);//确立focusing角色
                yield return _modelShower.showThisCharacterModel(focusLocalid);
            }
        }
        yield break;
    
    }
        
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._preparingScene.FightModeChooseT.gameObject.SetActive(false);
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void localUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.ifShowingSkill())
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
