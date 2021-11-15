using System;
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
    public Button unitSwitcher;
    
    [Space(10)] 
    [Header("FX Camera")] 
    public Camera fxCamera;
    
    public static SkillEditLayer Open()
    {
        UILayer l = UILayerLoader.Get("SkillEditLayer");
        SkillEditLayer returnValue;
        if (l != null)
        {
            returnValue = l as SkillEditLayer;
            SkillStonesBox.target = returnValue.StonesBox;
            return returnValue;
        }
        
        l = UILayerLoader.Load(PreScene.target.T,"SkillEditLayer") as SkillEditLayer;
        returnValue = l as SkillEditLayer;
        SkillStonesBox.target = returnValue.StonesBox;
        returnValue.NineSlot.StartUp();
        returnValue.StonesBox.GenerateCells();
        returnValue.StonesBox._SkillStoneBoxTabEffectsManager.StartUp();
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(PreScene.target._focusing.r_id);
        returnValue.StonesBox.CellsFeatureLoad(2);
        returnValue.StonesBox.IniExTabs(returnValue.fxCamera);
        returnValue.StonesBox.EXTabsFeatureRefresh(true);
        returnValue.StonesBox._skillStoneDetail.Clear();
        returnValue.unitSwitcher.gameObject.SetActive(FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow);
        returnValue.SkillEditButtonFeature(PreScene.target._focusing);
        
        returnValue.StonesBox._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            returnValue.StonesBox.NormalTab.transform,
            returnValue.StonesBox.EX1Tab.transform,
            returnValue.StonesBox.EX2Tab.transform,
            returnValue.StonesBox.EX3Tab.transform, 
            _CharConfig._zokusei
        );
        
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("SkillEditLayer");
    }
    
    // 技能编辑画面的进入按钮按下时候的处理。有这样几个逻辑上极其极其重要的点
    // 1. 每次进入一次技能编辑画面，代表技能石头盒子进入了一个针对特定type角色的锁定状态，从而应该只生成一次相应type的石头
    // 2. 除非切换画面，否则石头应该不会再重新生成，进一步说，这次生成石头所进行的石头本地id发配环节(numinbox)也只能进行一次
    // 3. 除非切换画面，生成的石头应该是数量守恒的，如果消耗就消耗，绝不能出现逻辑错误导致的复制情况
    void SkillEditButtonFeature(UnitInfo _UnitInfo)
    {
        if (_UnitInfo == null || _UnitInfo.r_id == null)
        {
            Debug.Log("到达了没道理到达的地方");
            return;
        }
        NineSlot.ReadANineAndTwo(_UnitInfo);
        CharConfig _CharInfo = MonstersConfigTable.GetCharConfig(_UnitInfo.r_id);
        Debug.Log(_CharInfo.TYPE + ":"+ _CharInfo.REAL_NAME);
        StonesBox.SetFocusingType(_CharInfo.TYPE);
        StonesBox.RestFilter();
        StonesBox.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            NineSlot.UpdateStonesBaseOnSlots(_UnitInfo);
        }
        void SkillUpdateValidation()
        {
            // 第一列技能必须有普通技能
            SkillSet.SkillEditError valR = NineSlot.CheckEditBasedOnCurrent();
            if (valR != SkillSet.SkillEditError.Perfect)
            {
                NineSlot.ValiationWarn(valR, PreScene.target._focusing.id);
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
    }
    
    // 技能浏览器程序模式专用
    public void SkillShowSpEnterProcess()
    {
        StonesBox.CellsFeatureLoad(3);
        SkillEditButtonFeature_SP(PreScene.target._focusing);
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(PreScene.target._focusing.r_id);
        StonesBox._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, fxCamera, StonesBox.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, fxCamera, StonesBox.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, fxCamera, StonesBox.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, fxCamera, StonesBox.EX3Tab.GetComponent<RectTransform>(),5f), 
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
    
    public void Random()
    {
        //RandomAll();
        FinishRemains();
    }
    
    void FinishRemains()
    {
        UnitInfo info = PreScene.target._focusing;
        CharConfig charConfig = MonstersConfigTable.GetCharConfig(info.r_id);
        SkillSet now = NineSlot.GetCurrentNineAndTwo();
        SkillSet targetSkillSet = SkillSet.FixSkillSet(charConfig.TYPE, now, 1, true);

        if (targetSkillSet == null)
        {
            NineSlot.ValiationWarn(SkillSet.SkillEditError.UnableToFinish, PreScene.target._focusing.id);
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
            SkillStonesBox.target.RestFilter();
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
        SkillStonesBox.target.RestFilter();
    }
    
    void ForceClearAll()
    {
        foreach (SkillStoneSlot _slot in NineSlot.allSlot)
        {
            _slot._DragAndDropCell.RemoveToTemp();
        }
    }
    
    void AddRandomStoneToSlot(string monsterOfPlayerId, int targetSlot, string skillid)
    {
        if (NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.GetItem() != null)
        {
            return;
        }
        
        StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(monsterOfPlayerId);
        List<string> Options = Stones.GetMyStonesBySkillID(skillid);
        if (originSkillInfo != null && skillid == originSkillInfo.skillId)
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

        SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillid);
        SkillStonesBox.target._SkillStoneBoxTabEffectsManager.SkillButtonExplosion(skillConfig.SP_LEVEL,
            ScreenPositionCal.Cal(1, fxCamera, NineSlot.allSlot[targetSlot - 1]._DragAndDropCell.GetComponent<RectTransform>(), 3),
            SkillStonesBox.target._SkillStoneBoxTabEffectsManager.transform);
    }
}
