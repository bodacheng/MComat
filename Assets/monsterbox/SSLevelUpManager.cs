using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;
using dataAccess;
using System.Collections;
using mainMenu;

public class SSLevelUpManager : MonoBehaviour
{    
    [Space(7)]
    [Header("升级按钮系列")]
    public RectTransform levelUpPageRect;
    public Button plusLevel; // 这个按钮的有效与否应该是取决于有没有足够的经验值币来满足升级请求。
    public Button minusLevel;
    public Button confirmLevelUp;
    public Text TargetLevel;

    public static SSLevelUpManager target;

    SkillStoneDetail focusingSSD;
    public void SetFocusingSSD(SkillStoneDetail fSSD)
    {
        focusingSSD = fSSD;
    }

    void Awake()
    {
        target = this;
    }

    #region 技能石升级窗口的开启与关闭,都是直接放在按钮上。
    public void OpenLevelUpPage()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        IniStartLevel();
        RefreshSkillLevelUpModule();
        levelUpPageRect.gameObject.SetActive(true);
        //LoadingCanvas.target.HigtLightRect(levelUpPageRect);// 这个到底有没有必要那待定吧。。。
    }
    public void CloseLevelUpPage()
    {
        levelUpPageRect.gameObject.SetActive(false);
        LoadingCanvas.target.ClearHigtLight();
    }
    #endregion
    
    #region 打开技能升级画面时候数值的初始化 （起始等级一类的）。
    void IniStartLevel()
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
    }
    #endregion
    
    bool IfCanLevelUp(int tartgetlevel, SkillStoneOfPlayerInfoModel currentStone)
    {
        int current_level = currentStone.GetLevel();
        Debug.Log("Current Level:" + current_level + ", Targetlevel : " + tartgetlevel);
        return AccountSet._AccInfo.Coin > (tartgetlevel - current_level);
    }

    // 技能升级确认。放在按钮上就可以
    public void ConfirmSkillStoneLevelUp()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        StartCoroutine(SkillStoneLevelUp(focusingSSD.GetSTTarget().skillStoneOfPlayerId));
    }

    // 实际将技能石提升等级的执行函数
    IEnumerator SkillStoneLevelUp(string PlayerSkillStoneID)
    {
        IEnumerator up = MySkillStonesReader.Update_Level(PlayerSkillStoneID, selectedTargetLevel.ToString(), ApiLanguage.EnUs);
        yield return up;
        RefreshSkillLevelUpModule();
    }
}