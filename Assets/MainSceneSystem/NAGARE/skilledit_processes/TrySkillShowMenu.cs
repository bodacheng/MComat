using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using mainMenu;
using dataAccess;

// 先试着把石头添加到一个格子上。
public class TrySkillShowMenu : MainSceneProcess
{
    public TrySkillShowMenu(preparingScene _preparingScene,ProcessesRunner processesRunner)
    {
        //this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub4;
        this.processesRunner = processesRunner;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }
    
    public IEnumerator enterProcess()
    {
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(true);
        CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(this._MemberDetail.focusingCharacterDataInfo);
        this._MemberDetail._SkillsPrintOut.SkillsPrintGamenRefresh(characterDataInfo);
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(false);
        this._SkillStonesBox.BoxWholeT.gameObject.SetActive(false);
        this._MemberDetail._TheNineSlot.NineSlotT.gameObject.SetActive(false);
        this._MemberDetail.MemberSkillshowT.gameObject.SetActive(true);
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        yield break;     
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
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        this._MemberDetail.MemberSkillshowT.gameObject.SetActive(false);
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void localUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.showingSkill)
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }}
