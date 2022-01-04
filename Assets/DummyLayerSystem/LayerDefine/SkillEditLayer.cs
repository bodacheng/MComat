using TouchScript.Gestures;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using System.Collections.Generic;
using System.Linq;
using Skill;

public class SkillEditLayer : UILayer
{
    [Space(10)]
    [Header("九宫格")]
    public TheNineSlot NineSlot;
    
    [Space(10)]
    [Header("技能石盒")]
    public SkillStonesBox StonesBox;
    
    [Space(10)]
    [Header("技能展示器模式切换角色按钮")]
    [SerializeField] Button unitSwitcher;
    
    [Space(10)]
    [Header("SkillStoneDetail")]
    [SerializeField] SkillStoneDetail _skillStoneDetail;
    
    public static SkillEditLayer Get()
    {
        UILayer l = UILayerLoader.Get("SkillEditLayer");
        if (l != null)
        {
            var returnValue = l as SkillEditLayer;
            return returnValue;
        }
        return null;
    }
    
    public static SkillEditLayer Open()
    {
        UILayer l = UILayerLoader.Get("SkillEditLayer");
        SkillEditLayer returnValue;
        if (l != null)
        {
            returnValue = l as SkillEditLayer;
            return returnValue;
        }
        
        l = UILayerLoader.Load(PreScene.target.T,"SkillEditLayer") as SkillEditLayer;
        returnValue = l as SkillEditLayer;
        returnValue.NineSlot.PrintSkillInfo = returnValue._skillStoneDetail.RefreshInfo;
        returnValue.NineSlot.StartUp();
        returnValue.StonesBox.GenerateCells();
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(PreScene.target._focusing.r_id);
        if (FightGlobalSetting._programMode != FightGlobalSetting.ProgramMode.skillShow)
            returnValue.StonesBox.AddFeatureToCells(returnValue.StoneCellFeature);
        else
        {
            returnValue.StonesBox.AddFeatureToCells(returnValue.CellFeature_SkillShowMode);
        }
        
        returnValue.StonesBox._SkillStoneBoxTabEffectsManager.SwitchZokusei
        (
            _CharConfig._zokusei
        );
        returnValue.StonesBox.IniExTabs(PreScene.target.FxCamera);
        returnValue.StonesBox.EXTabsFeatureRefresh(true);
        returnValue._skillStoneDetail.Clear();
        returnValue.unitSwitcher.gameObject.SetActive(FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow);
        returnValue.SkillEditButtonFeature(PreScene.target._focusing);
        
        return returnValue;
    }
    
    public static void Close()
    {
        var layer = UILayerLoader.Get("SkillEditLayer");
        if (layer != null)
        {
            SkillEditLayer se = (SkillEditLayer)layer;
            se.StonesBox._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
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
        CharConfig _CharInfo = MonstersConfigTable.GetCharConfig(_UnitInfo.r_id);
        StonesBox.SetFocusingType(_CharInfo.TYPE);
        StonesBox.RestFilter();
        StonesBox.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            NineSlot.UpdateStonesBaseOnSlots(_UnitInfo);
        }
        void SkillUpdateValidation()
        {
            if (NineSlot.CheckEditBasedOnCurrent() != SkillSet.SkillEditError.Perfect)
            {
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
        NineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
        NineSlot.ResetButton.onClick.AddListener(NineSlot.ResetNineSlot);
        NineSlot.removeAllBtn.onClick.AddListener(NineSlot.ClearSkillEquip);
        NineSlot.randomBtn.onClick.AddListener(FinishRemains);
    }
    
    // 技能浏览器程序模式专用
    public void SkillShowSpEnterProcess()
    {
        SkillEditButtonFeature_SP(PreScene.target._focusing);
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(PreScene.target._focusing.r_id);
        StonesBox._SkillStoneBoxTabEffectsManager.SwitchZokusei
        (
            _CharConfig._zokusei
        );
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
        StonesBox.SetFocusingType(_CharInfo.TYPE);
        StonesBox.RestFilter();
        StonesBox.EXTabsFeatureRefresh(false);
    }
    
    void FinishRemains()
    {
        UnitInfo info = PreScene.target._focusing;
        CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.r_id);
        SkillSet now = NineSlot.GetCurrentNineAndTwo();
        SkillSet targetSkillSet = SkillSet.FixSkillSet(charConfig.TYPE, now, 1, true);

        if (targetSkillSet == null)
        {
            NineSlot.ValidationWarn(SkillSet.SkillEditError.UnableToFinish, PreScene.target._focusing.id);
        }
        else
        {
            // 如果角色有原生技能，则已经存在于targetSkillSet当中
            AddRandomStoneToSlot(info.id, 1, targetSkillSet.a1);
            AddRandomStoneToSlot(info.id, 2, targetSkillSet.a2);
            AddRandomStoneToSlot(info.id, 3, targetSkillSet.a3);
            AddRandomStoneToSlot(info.id, 4, targetSkillSet.b1);
            AddRandomStoneToSlot(info.id, 5, targetSkillSet.b2);
            AddRandomStoneToSlot(info.id, 6, targetSkillSet.b3);
            AddRandomStoneToSlot(info.id, 7, targetSkillSet.c1);
            AddRandomStoneToSlot(info.id, 8, targetSkillSet.c2);
            AddRandomStoneToSlot(info.id, 9, targetSkillSet.c3);
            NineSlot.NineSlotsStatusRefresh();
            StonesBox.RestFilter();
        }
    }
    
    void RandomAll()
    {
        UnitInfo info = PreScene.target._focusing;
        CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.r_id);
        StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(info.id);
        // 这一步仅仅是根据账户拥有技能石的情况来确定了可行的技能组，也就是说根据手上的石头这个技能组能拼出来，但没提供具体的石头，所以防重复工作在实际装备技能石的时候（AddRandomStoneToSlot）也要做
        SkillSet targetSkillSet = SkillSet.RandomSkillSet(charConfig.TYPE, originSkillInfo?.skillId, 1, true);

        ForceClearAll();
        // 如果角色有原生技能，则已经存在于targetSkillSet当中
        AddRandomStoneToSlot(info.id, 1, targetSkillSet.a1);
        AddRandomStoneToSlot(info.id, 2, targetSkillSet.a2);
        AddRandomStoneToSlot(info.id, 3, targetSkillSet.a3);
        AddRandomStoneToSlot(info.id, 4, targetSkillSet.b1);
        AddRandomStoneToSlot(info.id, 5, targetSkillSet.b2);
        AddRandomStoneToSlot(info.id, 6, targetSkillSet.b3);
        AddRandomStoneToSlot(info.id, 7, targetSkillSet.c1);
        AddRandomStoneToSlot(info.id, 8, targetSkillSet.c2);
        AddRandomStoneToSlot(info.id, 9, targetSkillSet.c3);
        NineSlot.NineSlotsStatusRefresh();
        StonesBox.RestFilter();
    }
    
    void ForceClearAll()
    {
        foreach (SkillStoneSlot _slot in NineSlot.allSlot)
        {
            _slot._DragAndDropCell.RemoveToTemp();
        }
    }
    
    void AddRandomStoneToSlot(string monsterOfPlayerId, int targetSlot, string skillID)
    {
        if (NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.GetItem() != null)
        {
            return;
        }
        
        StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(monsterOfPlayerId);
        List<string> Options = Stones.GetMyStonesBySkillID(skillID);
        if (originSkillInfo != null && skillID == originSkillInfo.skillId)
        {
            NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.AddItem(Stones.GetRenderModel(originSkillInfo.InstanceId));
        }else{
            Options.OrderByDescending(x => Stones.Get(x).EXP);
            string targetStoneId = null;
            for (int i = 0; i < Options.Count; i++)
            {
                StoneOfPlayerInfo stoneInfo = Stones.Get(Options[i]);
                if (MyMonsters.Get(stoneInfo.inUsingMonsterOfPlayerId) == null)
                {
                    targetStoneId = Options[i];
                    break;
                }
            }
            NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.AddItem(Stones.GetRenderModel(targetStoneId));
        }

        SkillConfig skillConfig = SkillConfigTable.GetSkillConfig(skillID);
        StonesBox._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(skillConfig.SP_LEVEL,
            PosCal.GetWorldPos(PreScene.target.FxCamera, 
                NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.GetComponent<RectTransform>(), 
                3),
            StonesBox._SkillStoneBoxTabEffectsManager.transform);
    }
    
    void StoneCellFeature(StoneCell _Cell)
    {
        void buttonFeature(object sender, System.EventArgs e)
        {
            SKStoneItem _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                _skillStoneDetail.RefreshInfo(_stone.instanceId);
            }else{
                _skillStoneDetail.Clear();
            }
            StoneCell.SelectedRender(_Cell, SkillStonesBox._Selected);
        }
        
        void doubleClick(object sender, System.EventArgs e)
        {
            if (NineSlot.GetFocusingStoneSlot() != null)
            {
                StoneCell.Install(_Cell, NineSlot.GetFocusingStoneSlot()._DragAndDropCell);
            }
        }
        
        // 前往技能石升级画面
        void PressGoToLevelUpPage(object sender, GestureStateChangeEventArgs e)
        {
            SKStoneItem _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                if (FightGlobalSetting._skillStoneHasExp)
                    PreScene.target.trySwitchToStep(MainSceneStep.SkillStoneList, _stone.instanceId, true);
            }
        }
        
        _Cell.pGesture.Pressed += buttonFeature;
        _Cell.lpGesture.StateChanged += PressGoToLevelUpPage;
        _Cell.tGesture.Tapped += doubleClick;
        
        _Cell.SetOnDropAction(StoneCell.Install);
    }
    
    // 技能浏览器模式
    void CellFeature_SkillShowMode(StoneCell _Cell)
    {
        void buttonFeature(object sender, System.EventArgs e)
        {
            SKStoneItem _stone = _Cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                _skillStoneDetail.RefreshInfo(_stone.instanceId);
                 PreScene.target.mainProcessRunner.RunAsQueued(SkillShowSupporter.SkillShowRunWithPrepare(_stone._SkillConfig.REAL_NAME));
            }else{
                _skillStoneDetail.Clear();
            }
            StoneCell.SelectedRender(_Cell, SkillStonesBox._Selected);
        }
        _Cell.pGesture.Pressed += buttonFeature;
        _Cell.SetOnDropAction(StoneCell.Install);
    }
}
