using System.Collections;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using dataAccess;
using System.Collections.Generic;

public class MemberDetail_edit : MainSceneProcess
{
    public static bool loadFinished;
    public IEnumerator EnterProcess()
    {
        loadFinished = false;
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            yield break;
        }
        TheNineSlot.target.NineSlotT.gameObject.SetActive(true);
        SkillStonesBox.target = PreScene.target._SkillStonesBox_NineSlot;
        SSLevelUpManager.target.SetFocusingSSD(SkillStonesBox.target._skillStoneDetail);
        PreScene.target.MainMenuCanvas.gameObject.SetActive(false);
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        SkillStonesBox.target.GenerateCells(AccountSet._AccInfo.Stoneboxsize, 2);
        yield return SkillEditButtonFeature(MemberDetail.target._focusing);
        SkillStonesBox.target._skillStoneDetail.Clear();
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(MemberDetail.target._focusing.monsterId);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX3Tab.GetComponent<RectTransform>(),5f), 
            _CharConfig._zokusei
        );
        loadFinished = true;
    }
    
    // 技能浏览器程序模式专用
    public static IEnumerator SkillShowSpEnterProcess()
    {
        loadFinished = false;
        TheNineSlot.target.A1DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.A2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.A3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.B1DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.B2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.B3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.C1DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.C2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.C3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target._HP.gameObject.SetActive(false);
        for (int i = 0; i < TheNineSlot.target.remainCharges.Count; i++)
        {
            TheNineSlot.target.remainCharges[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < TheNineSlot.target.burdenCharges.Count; i++)
        {
            TheNineSlot.target.burdenCharges[i].gameObject.SetActive(false);
        }
        TheNineSlot.target.ResetButton.gameObject.SetActive(false);
        TheNineSlot.target.ConfirmSkillChangeButton.gameObject.SetActive(false);
        SSLevelUpManager.target.plusLevel.gameObject.SetActive(false);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(true);
        
        SkillStonesBox.target = PreScene.target._SkillStonesBox_NineSlot;        
        PreScene.target.MainMenuCanvas.gameObject.SetActive(false);
        SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        SkillStonesBox.target.GenerateCells(AccountSet._AccInfo.Stoneboxsize, 3);
        yield return SkillEditButtonFeature_SP(MemberDetail.target._focusing);
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(MemberDetail.target._focusing.monsterId);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, SkillStonesBox.target.fxCamera, SkillStonesBox.target.EX3Tab.GetComponent<RectTransform>(),5f), 
            _CharConfig._zokusei
        );
        loadFinished = true;
    }
    
    public MemberDetail_edit()
    {
        Step = MainSceneStep.MemberDetail_edit;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter()
    {
        if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
        {
            mainProcessRunner.Run(SkillShowSpEnterProcess());
        }else{
            mainProcessRunner.Run(EnterProcess());
        }
    }
    
    public override void ProcessEnd()
    {
        PreScene.target.MainMenuCanvas.gameObject.SetActive(true);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
    
    // 技能编辑画面的进入按钮按下时候的处理。有这样几个逻辑上极其极其重要的点
    // 1. 每次进入一次技能编辑画面，代表技能石头盒子进入了一个针对特定type角色的锁定状态，从而应该只生成一次相应type的石头
    // 2. 除非切换画面，否则石头应该不会再重新生成，进一步说，这次生成石头所进行的石头本地id发配环节(numinbox)也只能进行一次
    // 3. 除非切换画面，生成的石头应该是数量守恒的，如果消耗就消耗，绝不能出现逻辑错误导致的复制情况
    IEnumerator SkillEditButtonFeature(GetMonsterOfPlayerDetailModel _AccCharInfo)
    {
        if (_AccCharInfo == null || _AccCharInfo.monsterId == null)
        {
            Debug.Log("到达了没道理到达的地方");
            yield break;
        }
        yield return TheNineSlot.target.ReadANineAndTwo(_AccCharInfo);
        CharConfig _CharInfo = MonstersConfigTable.GetCharConfig(_AccCharInfo.monsterId);
        SkillStonesBox.target.SetFocusingType(_CharInfo.TYPE);
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            mainProcessRunner.Run(TheNineSlot.target.UpdateMyStonesBaseOnSlots(_AccCharInfo));
        }
        void SkillUpdateValidation()
        {
            // 第一列技能必须有普通技能
            TheNineSlot.SkillEditError valR = TheNineSlot.target.CheckEditBasedOnCurrent();
            if (valR != TheNineSlot.SkillEditError.Perfect)
            {
                TheNineSlot.target.ValiationWarn(valR, MemberDetail.target._focusing.monsterOfPlayerId);
                return;
            }
            LoadingCanvas.target.ArrangeConfirmWindow(SkillEditConfirm, "确实要进行技能更新？");
        }
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
        TheNineSlot.target.ResetButton.onClick.RemoveAllListeners();
        TheNineSlot.target.ResetButton.onClick.AddListener(TheNineSlot.target.ResetNineSlot);
    }
    
    // 技能浏览器版本
    static IEnumerator SkillEditButtonFeature_SP(GetMonsterOfPlayerDetailModel _AccCharInfo)
    {
        if (_AccCharInfo == null || _AccCharInfo.monsterId == null)
        {
            Debug.Log("到达了没道理到达的地方");
            yield break;
        }
        CharConfig _CharInfo = MonstersConfigTable.GetCharConfig(_AccCharInfo.monsterId);
        SkillStonesBox.target.SetFocusingType(_CharInfo.TYPE);
        yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(false);
    }
}
