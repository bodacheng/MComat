using UnityEngine;
using Api.Dto.Model;
using dataAccess;
// 智慧果实消耗
public partial class SSLevelUpManager : MonoBehaviour
{
    #region 调整目标等级 直接放在按钮上。
    public void PlusTargetLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        
        LevelCal levelCal = new LevelCal();
            levelCal.INI();
        LevelCal.Current current = levelCal.GetCurrentLevel((int)focusingSSD.GetSTTarget().EXP + CurrentExpAmount);
        if (GoldToExp(AccountSet._AccInfo.Coin) >= current.expToNextLevel)
        {
            CurrentGoldExaust += ExpToGold(current.expToNextLevel);
            CurrentExpAmount += current.expToNextLevel;
        }else{
            CurrentGoldExaust += AccountSet._AccInfo.Coin;
            CurrentExpAmount += GoldToExp(AccountSet._AccInfo.Coin);
        }
        RefreshSkillLevelUpModule();
        // 消耗coin的显示？
    }
    public void MinusTargetLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;

        LevelCal levelCal = new LevelCal();
            levelCal.INI();
        LevelCal.Current current = levelCal.GetCurrentLevel((int)focusingSSD.GetSTTarget().EXP + CurrentExpAmount);
        CurrentExpAmount -= GoldToExp(CurrentGoldExaust);
        CurrentGoldExaust = 0;
        
        RefreshSkillLevelUpModule();
        // 消耗coin的显示？
    }
    #endregion
}
