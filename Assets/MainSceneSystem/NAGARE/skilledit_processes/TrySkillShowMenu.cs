using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using mainMenu;
using dataAccess;

// 先试着把石头添加到一个格子上。
public class TrySkillShowMenu : MainSceneProcess
{
    public TrySkillShowMenu(ProcessesRunner processesRunner)
    {
        //this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub4;
        this.subProcessesRunner = processesRunner;
        this.EelementsInherit(PreScene.Instance);
    }
    
    public IEnumerator EnterProcess()
    {
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(true);
        CharacterDataInfo characterDataInfo = RemoteAccess.GetCharacterDataInfo(this._MemberDetail.focusingCharacterDataInfo);
        this._MemberDetail._SkillsPrintOut.SkillsPrintGamenRefresh(characterDataInfo);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(false);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(false);
        this._MemberDetail.MemberSkillshowT.gameObject.SetActive(true);
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        yield break;     
    }
    
    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.TriggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        this._MemberDetail.MemberSkillshowT.gameObject.SetActive(false);
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void LocalUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.IfShowingSkill)
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }}
