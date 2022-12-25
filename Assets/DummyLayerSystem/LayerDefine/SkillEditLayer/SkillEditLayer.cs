using System;
using System.Threading;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using ModelView;

public partial class SkillEditLayer : UILayer
{
    public DedicatedCameraConnector _connector;
    
    [Header("九宫格")]
    public TheNineSlot NineSlot;
    
    [Header("技能石盒")]
    public SkillStonesBox StonesBox;
    
    [Header("技能信息")]
    [SerializeField] SkillStoneDetail _skillStoneDetail;
    
    [Header("技能展示器模式切换角色按钮")]
    [SerializeField] Button unitSwitcher;
    
    [Header("Tutorial")]
    [SerializeField] ClickNextTutorial clickNextTutorial1, clickNextTutorial2;
    
    public async UniTask Setup(Action<SkillEditLayer> toDo = null)
    {
        StonesBox.GenerateCells(9);
        gameObject.SetActive(false);
        NineSlot.PrintSkillInfo = _skillStoneDetail.RefreshInfo;
        NineSlot.StartUp((x) =>
            {
                _connector.SkillShowRunWithPrepare(x).Forget();
            }
        );
        
        // 表现系
        var unitConfig = Units.GetUnitConfig(PreScene.target.Focusing.r_id);
        StonesBox.AddFeatureToCells(StoneCellFeature);
        StonesBox.IniExTabs();
        
        var cts = new CancellationTokenSource();
        ReturnLayer.AddUniTaskCancel(cts);
        await StonesBox._tabEffects.SwitchElement
            (unitConfig.element, 
                ()=> StonesBox.IniExTabsEffects(PreScene.target.mainC),
                cts.Token);
        StonesBox.FilterFeatureRefresh(true);
        _skillStoneDetail.Clear();
        SkillEditButtonFeature(PreScene.target.Focusing);
        toDo?.Invoke(this);
        gameObject.SetActive(true);
    }
    
    public override void OnDestroy()
    {
        StonesBox._tabEffects.CloseShowingTagEffects();
    }

    void SkillEditButtonFeature(UnitInfo _unitInfo)
    {
        if (_unitInfo == null || _unitInfo.r_id == null)
        {
            Debug.Log("到达了没道理到达的地方");
            return;
        }
        NineSlot.ReadANineAndTwo(_unitInfo);
        var unitInfo = Units.GetUnitConfig(_unitInfo.r_id);
        StonesBox.FocusingType = unitInfo.TYPE;
        StonesBox.RestFilter();
        StonesBox.FilterFeatureRefresh(false);
        void SkillEditConfirm()
        {
            NineSlot.UpdateStonesBaseOnSlots(_unitInfo);
        }
        void SkillUpdateValidation()
        {
            if (NineSlot.CheckEditBasedOnCurrent() != SkillSet.SkillEditError.Perfect)
            {
                // 可以更新但不能上场
                //return;
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
            PopupLayer.ArrangeConfirmWindow(SkillEditConfirm, warn);
        }
        
        NineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
        NineSlot.ResetButton.onClick.AddListener(NineSlot.ResetNineSlot);
        NineSlot.removeAllBtn.onClick.AddListener(NineSlot.ClearSkillEquip);
        NineSlot.randomBtn.onClick.AddListener(FinishRemains);
    }
    
    void ForceClearAll()
    {
        foreach (SkillStoneSlot _slot in NineSlot.allSlot)
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
                _skillStoneDetail.RefreshInfo(_stone.instanceId);
            }else{
                _skillStoneDetail.Clear();
            }
            StoneCell.SelectedRender(_Cell, SkillStonesBox._Selected);
        }
        
        void doubleClick()
        {
            if (NineSlot.GetFocusingStoneSlot() != null)
            {
                StoneCell.Install(_Cell, NineSlot.GetFocusingStoneSlot()._cell);
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
        EffectsManager.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, _connector.FocusingC.WholeT.position, Quaternion.identity, null).Forget();
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
