using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;

public class MemberDetail_edit : MainSceneProcess
{
    public IEnumerator enterProcess()
    {
        this._LoadingCanvas.DarkOff(0.5f);
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(true);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(true);
        yield return SkillEditorButtonBehaviour(this._MemberDetail.focusingCharacterDataInfo);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        //this._CameraManager.Assign_LerpToCertainPlaceCamera(this._MemberDetail.MemDetailWatchPos.position, this._MemberDetail.MemDetailWatchPos.rotation);
        
        // 表现系
        CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(this._MemberDetail.focusingCharacterDataInfo.monsterId);
        SkillStonesBox.Instance._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.NormalTab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX1Tab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX2Tab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX3Tab.gameObject,5f),
            _CharacterResourceInfo._zokusei);
        this._LoadingCanvas.LightUp();
        yield break;
    }
    
    public MemberDetail_edit(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.MemberDetail_edit;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
         SkillStonesBox.Instance._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
    }

    Vector3 screenPos = new Vector3(0.23f, 0.37f, 20f);
    public override void LocalUpdate()
    {
        if (!_MemberDetail._SkillsPrintOut.IfShowingSkill)
        {
            _modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
    
    // 技能编辑画面的进入按钮按下时候的处理。有这样几个逻辑上极其极其重要的点
    // 1. 每次进入一次技能编辑画面，代表技能石头盒子进入了一个针对特定type角色的锁定状态，从而应该只生成一次相应type的石头
    // 2. 除非切换画面，否则石头应该不会再重新生成，进一步说，这次生成石头所进行的石头本地id发配环节(numinbox)也只能进行一次
    // 3. 除非切换画面，生成的石头应该是数量守恒的，如果消耗就消耗，绝不能出现逻辑错误导致的复制情况
    IEnumerator SkillEditorButtonBehaviour(GetMonsterOfPlayerDetailModel _AccountCharacterInfo)
    {
        if (_AccountCharacterInfo == null || _AccountCharacterInfo.monsterId == null)
        {
            Debug.Log("到达了没道理到达的地方");
            yield break;
        }
        yield return TheNineSlot.Instance.ReadANineAndTwo(_AccountCharacterInfo);
        CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(_AccountCharacterInfo.monsterId);
        SkillStonesBox.Instance.SetFocusingType(_CharacterResourceInfo.type);
        yield return SkillStonesBox.Instance.ArrangeSkillStonesToBox();
        yield return SkillStonesBox.Instance.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            mainProcessRunner.triggerMainProcess(TheNineSlot.Instance.UpdateEditingNineAndTwoBaseOnSlots(_AccountCharacterInfo));
            _MemberDetail.presentationProcessRunner.triggerMainProcess(_MemberDetail.SkillEditConfirmAnimation());
        }

        void SkillUpdateValidation()
        {
            _preparingScene._LoadingCanvas.arrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        }
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
    }
}
