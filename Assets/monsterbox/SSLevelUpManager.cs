using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;
using dataAccess;
using System.Collections;
using mainMenu;
using System.Collections.Generic;

public class SSLevelUpManager : MonoBehaviour
{
    [Space(7)]
    [Header("升级按钮系列")]
    public RectTransform levelUpPageRect;
    public Button LevelUp;
    public Button plusLevel;
    public Button minusLevel;
    public Button confirmLevelUp;
    public Text TargetLevel;
    
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
    
    public static SSLevelUpManager target;
    
    SkillStoneDetail focusingSSD;
    public void SetFocusingSSD(SkillStoneDetail fSSD)
    {
        focusingSSD = fSSD;
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
    }
    
    // 添加强化素材用技能石
    public void AddMaterial(StoneCell skillboxcell)
    {
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            MaterialSlots[i].UpdateMyItem();
            if (MaterialSlots[i].GetItem() == null)
            {
                StoneCell.Install(skillboxcell, MaterialSlots[i]);
                break;
            }
        }
    }
    
    void ReturnAllMaterialsToBox()
    {
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            MaterialSlots[i].UpdateMyItem();
            if (MaterialSlots[i].GetItem() != null)
            {
                MaterialSlots[i].ReturnStoneToBox();
            }
        }
    }
    
    #region 技能石升级窗口的开启与关闭,都是直接放在按钮上。
    public void OpenLevelUpPage()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        SKStoneItem targetStone = MySkillStonesReader.GetRenderModel(focusingSSD.GetSTTarget().skillStoneOfPlayerId);
        StoneCell targetStoneOnCell = targetStone.GetCell();
        StoneCell.SeletedRender(targetStoneOnCell, _Selected);
        
        INIStartLevel();
        SkillStonesBox.target.CellsFeatureLoad(AccountSet._AccInfo.Stoneboxsize, 0);
        RefreshSkillLevelUpModule();
        levelUpPageRect.gameObject.SetActive(true);
        LevelUp.gameObject.SetActive(false);
        StoneDeleteManger.target.EnterDeleteModeButton.gameObject.SetActive(false);
        //LoadingCanvas.target.HigtLightRect(levelUpPageRect);// 这个到底有没有必要那待定吧。。。
    }
    public void CloseLevelUpPage()
    {
        ReturnAllMaterialsToBox();
        StoneCell.SeletedRender(null, _Selected);
        SkillStonesBox.target.CellsFeatureLoad(AccountSet._AccInfo.Stoneboxsize, 1);
        levelUpPageRect.gameObject.SetActive(false);
        LevelUp.gameObject.SetActive(true);
        StoneDeleteManger.target.EnterDeleteModeButton.gameObject.SetActive(true);
        //LoadingCanvas.target.ClearHigtLight();
    }
    #endregion
    
    #region 打开技能升级画面时候数值的初始化 （起始等级一类的）。
    void INIStartLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        selectedTargetLevel = focusingSSD.GetSTTarget().GetLevel();
    }
    #endregion
    
    #region 调整目标等级 直接放在按钮上。
    int selectedTargetLevel;
    public void PlusTargetLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        selectedTargetLevel += 1;
        RefreshSkillLevelUpModule();
        // 消耗coin的显示？
    }
    public void MinusTargetLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        selectedTargetLevel -= 1;
        RefreshSkillLevelUpModule();
        // 消耗coin的显示？
    }
    #endregion

    #region 技能石升级画面更新。每调整一次目标等级画面都要随之更新
    int currentlevel;
    void RefreshSkillLevelUpModule()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        currentlevel = focusingSSD.GetSTTarget().GetLevel();
        if (IfCanLevelUp(selectedTargetLevel, focusingSSD.GetSTTarget()))
        {
            plusLevel.gameObject.SetActive(true);
        }
        else
        {
            plusLevel.gameObject.SetActive(false);
        }
        if (selectedTargetLevel > currentlevel)
        {
            minusLevel.gameObject.SetActive(true);
        }
        else
        {
            minusLevel.gameObject.SetActive(false);
        }
        TargetLevel.text = "Level " + currentlevel + "->" + selectedTargetLevel.ToString();
        
        void LevelUp()
        {
            LoadingCanvas.target.ArrangeConfirmWindow(ConfirmSkillStoneLevelUp, "确实要升级技能石？");
        }
        confirmLevelUp.onClick.RemoveAllListeners();
        confirmLevelUp.onClick.AddListener(LevelUp);
    }
    #endregion
    
    bool IfCanLevelUp(int tartgetlevel, SkillStoneOfPlayerInfoModel currentStone)
    {
        int current_level = currentStone.GetLevel();
        Debug.Log("Current Level:" + current_level + ", Targetlevel : " + tartgetlevel);
        return AccountSet._AccInfo.Coin > (tartgetlevel - current_level);
    }

    // 技能升级确认。
    public void ConfirmSkillStoneLevelUp()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        PreScene.target.mainProcessRunner.Run(SkillStoneLevelUp(focusingSSD.GetSTTarget().skillStoneOfPlayerId));
    }
    
    // 分析当前选定的技能石
    public IEnumerator levelUp()
    {
        SKStoneItem item1 = cell1.GetItem();
        SKStoneItem item2 = cell2.GetItem();
        SKStoneItem item3 = cell3.GetItem();
        SKStoneItem item4 = cell4.GetItem();
        SKStoneItem item5 = cell5.GetItem();
        
        float point1 = MySkillStonesReader.ConvertSKStoneToWisdomFruit(item1.SkillStoneOfPlayerId);
        float point2 = MySkillStonesReader.ConvertSKStoneToWisdomFruit(item2.SkillStoneOfPlayerId);
        float point3 = MySkillStonesReader.ConvertSKStoneToWisdomFruit(item3.SkillStoneOfPlayerId);
        float point4 = MySkillStonesReader.ConvertSKStoneToWisdomFruit(item4.SkillStoneOfPlayerId);
        float point5 = MySkillStonesReader.ConvertSKStoneToWisdomFruit(item5.SkillStoneOfPlayerId);
        
        yield return MySkillStonesReader.RemoveStone(item1.SkillStoneOfPlayerId);
        yield return MySkillStonesReader.RemoveStone(item2.SkillStoneOfPlayerId);
        yield return MySkillStonesReader.RemoveStone(item3.SkillStoneOfPlayerId);
        yield return MySkillStonesReader.RemoveStone(item4.SkillStoneOfPlayerId);
        yield return MySkillStonesReader.RemoveStone(item5.SkillStoneOfPlayerId);
    }
    
    // 实际将技能石提升等级的执行函数
    IEnumerator SkillStoneLevelUp(string PlayerSkillStoneID)
    {
        IEnumerator up = MySkillStonesReader.Update_Level(PlayerSkillStoneID, selectedTargetLevel.ToString(), ApiLanguage.EnUs);
        yield return up;
        Debug.Log("here"+PlayerSkillStoneID);
        RefreshSkillLevelUpModule();
    }
}