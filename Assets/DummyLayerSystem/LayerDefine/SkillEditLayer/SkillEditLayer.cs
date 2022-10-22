using System;
using DummyLayerSystem;
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
    
    public static async UniTask<SkillEditLayer> Open(Action<SkillEditLayer> toDo = null)
    {
        var l = UILayerLoader.Get<SkillEditLayer>();
        SkillEditLayer returnValue;
        if (l != null)
        {
            returnValue = l;
            returnValue.StonesBox.GenerateCells(9);
            returnValue.gameObject.SetActive(true);
            return returnValue;
        }
        l = UILayerLoader.Load(PreScene.target.T,"SkillEditLayer") as SkillEditLayer;
        returnValue = (SkillEditLayer)l;
        returnValue.StonesBox.GenerateCells(9);
        returnValue.gameObject.SetActive(false);
        returnValue.NineSlot.PrintSkillInfo = returnValue._skillStoneDetail.RefreshInfo;
        returnValue.NineSlot.StartUp((x) =>
            {
                returnValue._connector.SkillShowRunWithPrepare(x).Forget();
            }
        );
        
        // 表现系
        var unitConfig = Units.GetUnitConfig(PreScene.target._focusing.r_id);
        returnValue.StonesBox.AddFeatureToCells(returnValue.StoneCellFeature);
        returnValue.StonesBox.IniExTabs();
        await returnValue.StonesBox._tabEffects.SwitchZokusei(unitConfig.element, ()=> returnValue.StonesBox.IniExTabsEffects(PreScene.target.FxCamera));
        returnValue.StonesBox.FilterFeatureRefresh(true);
        returnValue._skillStoneDetail.Clear();
        returnValue.SkillEditButtonFeature(PreScene.target._focusing);
        toDo?.Invoke(returnValue);
        returnValue.gameObject.SetActive(true);
        return returnValue;
    }
    
    public static void Close()
    {
        var layer = UILayerLoader.Get<SkillEditLayer>();
        if (layer != null)
        {
            layer.StonesBox._tabEffects.CloseShowingZokuseiTagEffects();
        }
        UILayerLoader.Remove("SkillEditLayer");
    }
    
    void SkillEditButtonFeature(UnitInfo _UnitInfo)
    {
        if (_UnitInfo == null || _UnitInfo.r_id == null)
        {
            Debug.Log("到达了没道理到达的地方");
            return;
        }
        NineSlot.ReadANineAndTwo(_UnitInfo);
        var unitInfo = Units.GetUnitConfig(_UnitInfo.r_id);
        StonesBox.FocusingType = unitInfo.TYPE;
        StonesBox.RestFilter();
        StonesBox.FilterFeatureRefresh(false);
        void SkillEditConfirm()
        {
            NineSlot.UpdateStonesBaseOnSlots(_UnitInfo);
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
            PopupLayer.ArrangeConfirmWindow(PreScene.target.T, SkillEditConfirm, warn);
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
        
        _Cell.btn.AddListener(buttonFeature);
        _Cell.btn.AddHoldEvent(PressGoToLevelUpPage);
        _Cell.btn.AddDoubleClickEvent(doubleClick);
        _Cell.SetOnDropAction(StoneCell.Install);
    }
    
    public void SkillEditConfirmAnimation()
    {
        var personalEffectsPath = FightGlobalSetting.EffectPathDefine(Element.Null);
        EffectsManager.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, _connector.FocusingC.WholeT.position, Quaternion.identity, null).Forget();
    }
}
