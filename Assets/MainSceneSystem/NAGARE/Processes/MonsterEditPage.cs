using System.Collections;
using UnityEngine;
using mainMenu;
using System.Collections.Generic;
using UniRx;

public class MonsterEditPage : MainSceneProcess
{
    private SkillEditLayer skillEditLayer;
    public static bool loadFinished;

    ReactiveProperty<int> itemsLoadFinished = new ReactiveProperty<int>(0);
    void ItemsLoadFinished(int value)
    {
        itemsLoadFinished.Value = value;
    }

    public IEnumerator EnterProcess()
    {
        loadFinished = false;
        
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            yield break;
        }
        skillEditLayer.NineSlot.NineSlotT.gameObject.SetActive(true);
        skillEditLayer.StonesBox.CellsFeatureLoad(2);
        SkillEditButtonFeature(PreScene.target._focusing);
        skillEditLayer.StonesBox._skillStoneDetail.Clear();

        // 没这行的话从技能石升级画面返回的话角色模型加载不出来
        //yield return UnitOptionLayer.target.CharModelRender(UnitInfo.GetCharDataInfo(PreScene.target._focusing));
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(PreScene.target._focusing.r_id);
        skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.NormalTab.GetComponent<RectTransform>(), 10f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX1Tab.GetComponent<RectTransform>(), 10f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX2Tab.GetComponent<RectTransform>(), 10f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX3Tab.GetComponent<RectTransform>(), 10f), 
            _CharConfig._zokusei
        );
        loadFinished = true;
    }
    
    public MonsterEditPage()
    {
        Step = MainSceneStep.UnitSkillEdit;
        EelementsInherit(PreScene.target);
    }
    
    //private StoneListSideLayer StoneListSideLayer;
    public override void ProcessEnter()
    {
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        
        //StoneListSideLayer = StoneListSideLayer.Open();
        skillEditLayer = SkillEditLayer.Open();
        
        missionWatcher = new MissionWatcher(
            new List<ReactiveProperty<int>>() {
                itemsLoadFinished
            },
            () => {
                if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
                {
                    SkillShowSpEnterProcess();
                }
                else
                {
                    mainProcessRunner.RunAsQueued(EnterProcess());
                }
            },
            () => { Debug.Log("failed"); }
        );
    }
    
    public override void ProcessEnd()
    {
        SkillEditLayer.Close();
        ItemsLoadFinished(0);
        missionWatcher.DisposeAll();
        skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!SkillShowSupporter.IfShowingSkill)
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
    void SkillEditButtonFeature(UnitInfo _AccCharInfo)
    {
        if (_AccCharInfo == null || _AccCharInfo.r_id == null)
        {
            Debug.Log("到达了没道理到达的地方");
            return;
        }
        skillEditLayer.NineSlot.ReadANineAndTwo(_AccCharInfo);
        CharConfig _CharInfo = MonstersConfigTable.GetCharConfig(_AccCharInfo.r_id);
        skillEditLayer.StonesBox.SetFocusingType(_CharInfo.TYPE);
        skillEditLayer.StonesBox.RestFilter();
        skillEditLayer.StonesBox.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            skillEditLayer.NineSlot.UpdateStonesBaseOnSlots(_AccCharInfo);
        }
        void SkillUpdateValidation()
        {
            // 第一列技能必须有普通技能
            SkillSet.SkillEditError valR = skillEditLayer.NineSlot.CheckEditBasedOnCurrent();
            if (valR != SkillSet.SkillEditError.Perfect)
            {
                skillEditLayer.NineSlot.ValiationWarn(valR, PreScene.target._focusing.id);
                return;
            }

            string warn;
            switch (AppSetting.Language)
            {
                case ApiLanguage.JaJp:
                    warn = "選択したスキルストーンでユニットの技を更新しますか？";
                break;
                default:
                    warn = "确实要进行技能更新？";
                break;
            }
            PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
            popupLayer.ArrangeConfirmWindow(SkillEditConfirm, warn);
        }
        skillEditLayer.NineSlot.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        skillEditLayer.NineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
        skillEditLayer.NineSlot.ResetButton.onClick.RemoveAllListeners();
        skillEditLayer.NineSlot.ResetButton.onClick.AddListener(skillEditLayer.NineSlot.ResetNineSlot);
    }
    
    // 技能浏览器程序模式专用
    void SkillShowSpEnterProcess()
    {
        loadFinished = false;
        
        skillEditLayer.NineSlot.NineSlotT.gameObject.SetActive(false);
        skillEditLayer.StonesBox.CellsFeatureLoad(3);
        SkillEditButtonFeature_SP(PreScene.target._focusing);
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(PreScene.target._focusing.r_id);
        skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX3Tab.GetComponent<RectTransform>(),5f), 
            _CharConfig._zokusei
        );
        loadFinished = true;
    }
    
    // 技能浏览器版本
    void SkillEditButtonFeature_SP(UnitInfo _AccCharInfo)
    {
        if (_AccCharInfo == null || _AccCharInfo.r_id == null)
        {
            Debug.Log("到达了没道理到达的地方");
            return;
        }
        CharConfig _CharInfo = MonstersConfigTable.GetCharConfig(_AccCharInfo.r_id);
        skillEditLayer.StonesBox.SetFocusingType(_CharInfo.TYPE);
        skillEditLayer.StonesBox.RestFilter();
        skillEditLayer.StonesBox.EXTabsFeatureRefresh(false);
    }
}
