using System;
using System.Threading;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using ModelView;

public partial class SkillEditLayer : UILayer
{
    public DedicatedCameraConnector connector;
    
    [Header("九宫格")]
    public TheNineSlot nineSlot;
    
    [Header("技能石盒")]
    public SkillStonesBox stonesBox;
    
    [Header("技能信息")]
    [SerializeField] SkillStoneDetail skillStoneDetail;
    
    [Header("技能展示器模式切换角色按钮")]
    [SerializeField] Button unitSwitcher;
    
    [Header("Tutorial")]
    [SerializeField] ClickNextTutorial clickNextTutorial1, clickNextTutorial2;
    
    public async UniTask Setup(Action<SkillEditLayer> toDo = null)
    {
        stonesBox.GenerateCells(9);
        gameObject.SetActive(false);
        nineSlot.PrintSkillInfo = skillStoneDetail.RefreshInfo;
        nineSlot.plsTryNormalSkill = stonesBox.TriggerNormalTab;
        nineSlot.StartUp((x) =>
            {
                connector.SkillShowRunWithPrepare(x).Forget();
            }
        );
        
        // 表现系
        var unitConfig = Units.GetUnitConfig(PreScene.target.Focusing.r_id);
        stonesBox.AddFeatureToCells(StoneCellFeature);
        stonesBox.IniExTabs();
        
        var cts = new CancellationTokenSource();
        ReturnLayer.AddUniTaskCancel(cts);
        await stonesBox._tabEffects.SwitchElement
            (unitConfig.element, 
                ()=> stonesBox.IniExTabsEffects(PreScene.target.postProcessCamera),
                cts.Token);
        stonesBox.FilterFeatureRefresh(true);
        skillStoneDetail.Clear();
        SkillEditButtonFeature(PreScene.target.Focusing);
        toDo?.Invoke(this);
        gameObject.SetActive(true);
    }
    
    public override void OnDestroy()
    {
        stonesBox._tabEffects.CloseShowingTagEffects();
    }

    void SkillEditButtonFeature(UnitInfo _unitInfo)
    {
        if (_unitInfo == null || _unitInfo.r_id == null)
        {
            Debug.Log("到达了没道理到达的地方");
            return;
        }
        nineSlot.ReadANineAndTwo(_unitInfo);
        var unitInfo = Units.GetUnitConfig(_unitInfo.r_id);
        stonesBox.FocusingType = unitInfo.TYPE;
        stonesBox.RestFilter();
        stonesBox.FilterFeatureRefresh(false);
        void SkillEditConfirm()
        {
            nineSlot.UpdateStonesBaseOnSlots(_unitInfo);
        }
        void SkillSetUpdate()
        {
            var valid = nineSlot.CheckEditBasedOnCurrent();
            if (valid != SkillSet.SkillEditError.Perfect)
            {
                // 比如想给角色卸载全部技能的时候，虽然全部卸载后不能再战斗但是需要更新。
                PopupLayer.ArrangeConfirmWindow(SkillEditConfirm, Translate.Get("NotLegalButStillUpdate"));
            }
            else
            {
                SkillEditConfirm();
            }
        }
        
        nineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillSetUpdate);
        nineSlot.ResetButton.onClick.AddListener(nineSlot.ResetNineSlot);
        nineSlot.removeAllBtn.onClick.AddListener(nineSlot.ClearSkillEquip);
        nineSlot.randomBtn.onClick.AddListener(FinishRemains);
    }
    
    void ForceClearAll()
    {
        foreach (var _slot in nineSlot.allSlot)
        {
            _slot._cell.RemoveToTemp();
        }
    }
    
    void StoneCellFeature(StoneCell _Cell)
    {
        void buttonFeature()
        {
            var _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                skillStoneDetail.RefreshInfo(_stone.instanceId);
            }else{
                skillStoneDetail.Clear();
            }
            StoneCell.SelectedRender(_Cell, SkillStonesBox._Selected);
        }
        
        void doubleClick()
        {
            if (nineSlot.GetFocusingStoneSlot() != null)
            {
                StoneCell.Install(_Cell, nineSlot.GetFocusingStoneSlot()._cell);
            }
        }
        
        // 前往技能石升级画面
        void PressGoToLevelUpPage()
        {
            var _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                if (FightGlobalSetting._skillStoneHasExp)
                    PreScene.target.trySwitchToStep(MainSceneStep.SkillStoneList, _stone.instanceId, true);
            }
        }
        
        _Cell.btn.SetListener(buttonFeature);

        _Cell.btn.ActivateHold = true;
        _Cell.btn.ActivateDoubleClick = true;
        
        _Cell.btn.onHold.AddListener(PressGoToLevelUpPage);
        _Cell.btn.onDoubleClick.AddListener(doubleClick);
        _Cell.SetOnDropAction(StoneCell.Install);
    }
    
    public void SkillEditConfirmAnimation()
    {
        var personalEffectsPath = FightGlobalSetting.EffectPathDefine(Element.Null);
        EffectsManager.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, connector.FocusingC.WholeT.position, Quaternion.identity, null).Forget();
    }
    
    public void OpenTutorial1()
    {
        clickNextTutorial1.Open();
    }
    
    public void OpenTutorial2()
    {
        clickNextTutorial2.Open();
    }
}
