using System.Collections;
using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("材料技能石参数")]
    public SkillStoneDetail _MSkillStoneDetail;
    
    [Space(7)]
    [Header("尝试升级的技能石的选中标记框")]
    public GameObject _Selected;
    
    [Space(7)]
    [Header("融合技能槽")]
    public StoneCell cell1;
    public StoneCell cell2;
    public StoneCell cell3;
    public StoneCell cell4;
    public StoneCell cell5;
    List<StoneCell> MaterialSlots;

    SkillStoneDetail focusingSSD;
    public static SSLevelUpManager target;

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
        if (button != null)
        {
            void buttonFeature()
            {
                SKStoneItem _stone = cell.GetItem();
                if (_stone != null && _stone._SkillConfig != null)
                {
                    // 如果点击的不是升级对象技能石
                    if (_stone.SkillStoneOfPlayerId != focusingSSD.GetSTTarget().skillStoneOfPlayerId)
                    {
                        _MSkillStoneDetail.RefreshSkillDetail(_stone.SkillStoneOfPlayerId);
                    }
                }
                else{
                    _MSkillStoneDetail.Clear();
                }
            }
            button.onClick.AddListener(buttonFeature);
            button.onClick.AddListener(delegate { StoneCell.SeletedRender(cell, SkillStonesBox._Selected); });
        }
    }
    
    // 设置目前链接的技能石细节显示模块
    public void SetFocusingSSD(SkillStoneDetail fSSD)
    {
        focusingSSD = fSSD;
    }
    
    public SkillStoneDetail GetFocusingSSD()
    {
        return focusingSSD;
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
        if (focusingSSD.GetSTTarget() == null)
            return;
        SKStoneItem targetStone = MySkillStonesReader.GetRenderModel(focusingSSD.GetSTTarget().skillStoneOfPlayerId);
        StoneCell targetStoneOnCell = targetStone.GetCell();
        SKStoneItem.SeletedRender(targetStone, _Selected);
        SkillStonesBox.target.CellsFeatureLoad(AccountSet._AccInfo.Stoneboxsize, 0);
        levelUpPageRect.gameObject.SetActive(true);
        RefreshSkillLevelUpModule();
        StoneDeleteManger.target.EnterDeleteModeButton.gameObject.SetActive(false);
    }
    public void CloseLevelUpPage()
    {
        _Selected.SetActive(false);
        ReturnAllMaterialsToBox();
        SkillStonesBox.target.CellsFeatureLoad(AccountSet._AccInfo.Stoneboxsize, 1);
        levelUpPageRect.gameObject.SetActive(false);
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
        expValue.value = 0;
        plusLevel.gameObject.SetActive(false);
        minusLevel.gameObject.SetActive(false);
        confirmLevelUp.gameObject.SetActive(false);
    }
    
    #region 技能石升级画面更新。每调整一次目标等级画面都要随之更新
    public void RefreshSkillLevelUpModule()
    {
        if (focusingSSD == null || focusingSSD.GetSTTarget() == null)
        {
            Clear();
            return;
        }
        
        #region 各数值文本刷新
        LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo(CurrentAddExp() + focusingSSD.GetSTTarget().EXP);
        StoneTargetLevel.text = "Level:" + current.currentLevel.ToString() + "/100";
        expValue.value = (float)current.expRemain / (current.expRemain + current.expToNextLevel);
        StoneTargetLevel.color = CurrentAddExp() > 0 ? new Color(0, 1, 1) : new Color(1, 1, 1);
        CurrentExpToNextLevel.text = "(" + current.expRemain + "/" + (current.expRemain + current.expToNextLevel).ToString() + ")";
        CurrentExpToNextLevel.color = CurrentGoldExaust > 0 ? new Color(0, 1, 1) : new Color(1, 1, 1);
        CurrentGoldExaustText.text = "消耗金币："+ CurrentGoldExaust;
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
    
    // 技能升级确认。
    public void ConfirmSkillStoneLevelUp()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        PreScene.target.mainProcessRunner.Run(LevelUpStone(focusingSSD.GetSTTarget().skillStoneOfPlayerId));
    }
    
    // 分析当前选定的技能石
    public IEnumerator LevelUpStone(string PlayerSkillStoneID)
    {
        SKStoneItem item1 = cell1.GetItem();
        SKStoneItem item2 = cell2.GetItem();
        SKStoneItem item3 = cell3.GetItem();
        SKStoneItem item4 = cell4.GetItem();
        SKStoneItem item5 = cell5.GetItem();
        
        yield return MySkillStonesReader.RemoveStone(item1.SkillStoneOfPlayerId);
        yield return MySkillStonesReader.RemoveStone(item2.SkillStoneOfPlayerId);
        yield return MySkillStonesReader.RemoveStone(item3.SkillStoneOfPlayerId);
        yield return MySkillStonesReader.RemoveStone(item4.SkillStoneOfPlayerId);
        yield return MySkillStonesReader.RemoveStone(item5.SkillStoneOfPlayerId);
        
        yield return SkillStoneLevelUp(PlayerSkillStoneID);
    }
    
    // 实际将技能石提升等级的执行函数
    IEnumerator SkillStoneLevelUp(string PlayerSkillStoneID)
    {
        IEnumerator up = MySkillStonesReader.Update_Level(ApiLanguage.EnUs);
        yield return up;
        RefreshSkillLevelUpModule();
    }
}