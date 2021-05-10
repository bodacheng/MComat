using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;
using DG.Tweening;
using System.Collections;

public partial class SSLevelUpManager : MonoBehaviour
{
    [Space(7)]
    [Header("按钮")]
    public RectTransform levelUpPageRect;
    public Button plusLevel;
    public Button minusLevel;
    public Button confirmLevelUp;
    
    [Space(7)]
    [Header("目前各种参数显示")]
    public Slider expValue;
    public Text StoneTargetLevel;
    public Text CurrentExpToNextLevel;
    public Text CurrentGoldExaustText;
    
    [Space(7)]
    [Header("升级对象技能石参数")]
    public SkillStoneDetail focusingSSD;
    
    [Space(7)]
    [Header("材料技能石参数")]
    public SkillStoneDetail _MSkillStoneDetail;
        
    [Space(7)]
    [Header("融合技能槽")]
    public StoneCell cell1;
    public StoneCell cell2;
    public StoneCell cell3;
    public StoneCell cell4;
    public StoneCell cell5;
    List<StoneCell> MaterialSlots;
    
    public static SSLevelUpManager target;

    int targetexp;
    public int TargetExp
    {
        get
        {
            return targetexp;
        }
        set
        {
            LevelExpConfig.Current before = LevelExpConfig.GetCurrentInfo(targetexp);
            LevelExpConfig.Current after = LevelExpConfig.GetCurrentInfo(value);
            DOTween.To(() => DataForShow, x => DataForShow = x, value, 0.6f);
            targetexp = value;
        }
    }
    
    int dataforshow;
    float DataForShow
    {
        get
        {
            return dataforshow;
        }
        set
        {
            LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo((int)value);
            expValue.value = (float)current.expRemain / (float)(current.expRemain + current.expToNextLevel);
            if (expValue.value >= 1)
                expValue.value = 0;
            StoneTargetLevel.text = "Level:" + current.currentLevel.ToString();
            StoneTargetLevel.color = CurrentAddExp() > 0 ? new Color(0, 1, 1) : new Color(1, 1, 1);
            CurrentExpToNextLevel.text = "( " + (expValue.value * 100).ToString() + "% )";
            CurrentExpToNextLevel.color = CurrentGoldExaust > 0 ? new Color(0, 1, 1) : new Color(1, 1, 1);
            CurrentGoldExaustText.text = "消耗金币："+ CurrentGoldExaust;
            dataforshow = (int)value;
        }
    }
    
    string stoneOfPlayerId;
    public void SetTargetStoneID(string value)
    {
        stoneOfPlayerId = value;
    }
    public string GetTargetStoneID()
    {
        return stoneOfPlayerId;
    }

    void Awake()
    {
        target = this;
        MaterialSlots = new List<StoneCell>
        {
            cell1,
            cell2,
            cell3,
            cell4,
            cell5
        };
        
        AddMSlotBehaviour(cell1);
        AddMSlotBehaviour(cell2);
        AddMSlotBehaviour(cell3);
        AddMSlotBehaviour(cell4);
        AddMSlotBehaviour(cell5);
    }
    
    // 材料槽
    public void AddMSlotBehaviour(StoneCell cell)
    {
        Button button = cell.GetComponent<Button>();
        if (button == null)
        {
            return;
        }
        
        void buttonFeature()
        {
            SKStoneItem _stone = cell.GetItem();
            if (_stone != null && _stone._SkillConfig != null)
            {
                // 如果点击的不是升级对象技能石
                if (_stone.SkillStoneOfPlayerId != stoneOfPlayerId)
                {
                    _MSkillStoneDetail.RefreshInfo(_stone.SkillStoneOfPlayerId);
                }
            }
            else{
                _MSkillStoneDetail.Clear();
            }
        }
        button.onClick.AddListener(buttonFeature);
        button.onClick.AddListener(delegate { StoneCell.SeletedRender(cell, SkillStonesBox._Selected); });
    }
    
    // 显示当前所有技能石消耗与金币消耗两方面合起来把对象技能石升到了多少经验
    public int CurrentAddExp()
    {
        return StoneExpManager.GoldToExp(CurrentGoldExaust) + CalCurrentExpFromMaterialStone();
    }
    
    #region 技能石升级窗口的开启与关闭
    // 长按技能石进入升级画面，也就是底下的函数。
    public void OpenLevelUpPage()
    {
        OpenLevelUpPage(stoneOfPlayerId);
    }
    
    public void OpenLevelUpPage(string skillstoneofplayer)
    {
        stoneOfPlayerId = skillstoneofplayer;
        
        focusingSSD.RefreshInfo(stoneOfPlayerId);
        SKStoneItem targetStone = MySkillStones.GetRenderModel(stoneOfPlayerId);
        List<string> GetSameSkillIdStones = MySkillStones.GetMyStonesBySkillID(targetStone._SkillConfig.RECORD_ID);
        List<string> extra = new List<string>();
        for (int i = 0; i < GetSameSkillIdStones.Count; i++)
        {
            if (GetSameSkillIdStones[i] != stoneOfPlayerId)
                extra.Add(GetSameSkillIdStones[i]);
        }
        SkillStonesBox.target.rares = new List<int> { 0, 1, 2 };
        SkillStonesBox.StoneFilterForm filterForm = SkillStonesBox.target.CurrentFilter();
        SkillStonesBox.target.PutSkillStonesToBox(filterForm, new List<string> {stoneOfPlayerId}, extra);
        SkillStonesBox.target.CellsFeatureLoad(0);
        levelUpPageRect.gameObject.SetActive(true);
        RefreshSkillLevelUpModule();
        StoneDeleteManger.target.EnterDeleteModeButton.gameObject.SetActive(false);
    }
    
    public void CloseLevelUpPage()
    {
        SkillStonesBox.target.rares = new List<int> { 0, 1, 2 ,3, 4, 5};
        SkillStonesBox.StoneFilterForm filterForm = SkillStonesBox.target.CurrentFilter();
        SkillStonesBox.target.PutSkillStonesToBox(filterForm);
        SKStoneItem targetStone = MySkillStones.GetRenderModel(stoneOfPlayerId);
        SKStoneItem.SeletedRender(targetStone, SkillStonesBox._Selected);
        focusingSSD.RefreshInfo(stoneOfPlayerId);
        ReturnAllMaterialsToBox();
        SkillStonesBox.target.CellsFeatureLoad(1);
        levelUpPageRect.gameObject.SetActive(false);
        CurrentGoldExaust = 0;
        RefreshSkillLevelUpModule();
        StoneDeleteManger.target.EnterDeleteModeButton.gameObject.SetActive(true);
    }
    #endregion
    
    // 清除显示
    public void Clear()
    {
        StoneTargetLevel.text = "";
        CurrentExpToNextLevel.text = "";
        CurrentGoldExaustText.text = "";
        if (expValue != null)
        {
            expValue.value = 0;
            expValue.gameObject.SetActive(false);
        }        
        plusLevel.gameObject.SetActive(false);
        minusLevel.gameObject.SetActive(false);
        confirmLevelUp.gameObject.SetActive(false);
    }
    
    #region 技能石升级画面更新。每调整一次目标等级画面都要随之更新
    public void RefreshSkillLevelUpModule()
    {
        _MSkillStoneDetail.Clear();
        if (focusingSSD == null || stoneOfPlayerId == null)
        {
            Clear();
            return;
        }
        
        StoneOfPlayerInfo StoneInfoModel = MySkillStones.Get(stoneOfPlayerId);
        
        #region 各数值文本刷新
        LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo(CurrentAddExp() + StoneInfoModel.EXP);
        TargetExp = CurrentAddExp() + StoneInfoModel.EXP;
        #endregion
        
        if (CurrentGoldExaust > 0)
        {
            minusLevel.gameObject.SetActive(true);
        }else{
            minusLevel.gameObject.SetActive(false);
        }
        if (AccountSet._AccInfo.coinCount == CurrentGoldExaust)
        {
            plusLevel.gameObject.SetActive(false);
        }else{
            plusLevel.gameObject.SetActive(true);
        }
        
        if (CurrentAddExp() > 0)
        {
            void LevelUp()
            {
                LoadingCanvas.target.ArrangeConfirmWindow(ConfirmSkillStoneLevelUp, "确实要升级技能石？");
            }
            confirmLevelUp.onClick.RemoveAllListeners();
            confirmLevelUp.onClick.AddListener(LevelUp);
            confirmLevelUp.gameObject.SetActive(true);
        }else{
            confirmLevelUp.gameObject.SetActive(false);
        }
    }
    #endregion
}