using System.Collections;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using dataAccess;

public class MemberDetail_edit : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        SkillStonesBox.target = PreScene.Instance._SkillStonesBox_NineSlot;
        SSLevelUpManager.target.SetFocusingSSD(SkillStonesBox.target._skillStoneDetail);
        LoadingCanvas.target.DarkOff(0.5f);
        PreScene.Instance.MainMenuCanvas.gameObject.SetActive(false);
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        SkillStonesBox.target.GenerateCells(AccountSet.Instance._PlayerAccountInfo.Stoneboxsize,2);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        SkillStonesBox.target.BoxWholeT.gameObject.SetActive(true);
        yield return SkillEditButtonFeature(_MemberDetail.focusingCharDataInfo);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(this._MemberDetail.focusingCharDataInfo.monsterId);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.NormalTab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX1Tab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX2Tab.gameObject,5f),
            SkillStonesBox.target.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.target.fxCamera,SkillStonesBox.target.EX3Tab.gameObject,5f),
            _CharConfig._zokusei);
        
        LoadingCanvas.target.LightUp();
        yield break;
    }
    
    public MemberDetail_edit()
    {
        thisProcessStep = MainSceneStep.MemberDetail_edit;
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
        PreScene.Instance.MainMenuCanvas.gameObject.SetActive(true);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
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
    IEnumerator SkillEditButtonFeature(GetMonsterOfPlayerDetailModel _AccountCharacterInfo)
    {
        if (_AccountCharacterInfo == null || _AccountCharacterInfo.monsterId == null)
        {
            Debug.Log("到达了没道理到达的地方");
            yield break;
        }
        yield return TheNineSlot.Instance.ReadANineAndTwo(_AccountCharacterInfo);
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharConfig(_AccountCharacterInfo.monsterId);
        SkillStonesBox.target.SetFocusingType(_CharacterResourceInfo.type);
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            mainProcessRunner.Run(TheNineSlot.Instance.UpdateMyStonesBaseOnSlots(_AccountCharacterInfo));
            _MemberDetail.presentationProcessRunner.Run(_MemberDetail.SkillEditConfirmAnimation());
        }
        
        void SkillUpdateValidation()
        {
            LoadingCanvas.target.ArrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        }
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
    }
}
