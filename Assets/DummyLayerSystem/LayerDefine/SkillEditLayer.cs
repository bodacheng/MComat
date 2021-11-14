using mainMenu;
using UnityEngine;
using UnityEngine.UI;

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
        returnValue.NineSlot.StartUp();
        returnValue.StonesBox.GenerateCells();
        returnValue.StonesBox._SkillStoneBoxTabEffectsManager.StartUp();
        returnValue.StonesBox._skillStoneDetail.Clear();
        SkillStonesBox.target = returnValue.StonesBox;
        returnValue.unitSwitcher.gameObject.SetActive(FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow);
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
    public void SkillEditButtonFeature(UnitInfo _AccCharInfo)
    {
        if (_AccCharInfo == null || _AccCharInfo.r_id == null)
        {
            Debug.Log("到达了没道理到达的地方");
            return;
        }
        NineSlot.ReadANineAndTwo(_AccCharInfo);
        CharConfig _CharInfo = MonstersConfigTable.GetCharConfig(_AccCharInfo.r_id);
        StonesBox.SetFocusingType(_CharInfo.TYPE);
        StonesBox.RestFilter();
        StonesBox.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            NineSlot.UpdateStonesBaseOnSlots(_AccCharInfo);
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
        NineSlot.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        NineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
        NineSlot.ResetButton.onClick.RemoveAllListeners();
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
            ScreenPositionCal.Cal(1, StonesBox.fxCamera, StonesBox.NormalTab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, StonesBox.fxCamera, StonesBox.EX1Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, StonesBox.fxCamera, StonesBox.EX2Tab.GetComponent<RectTransform>(),5f),
            ScreenPositionCal.Cal(1, StonesBox.fxCamera, StonesBox.EX3Tab.GetComponent<RectTransform>(),5f), 
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
}
