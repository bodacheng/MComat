using UnityEngine;
using Api.Dto.Model;
using dataAccess;

// 智慧果实消耗
public partial class SSLevelUpManager : MonoBehaviour
{
    int CurrentGoldExaust;

    #region 调整目标等级 直接放在按钮上。
    public void PlusTargetLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
        
        LevelCal.Current current = LevelCal.Instance.GetCurrentInfo(CurrentAddExp() + focusingSSD.GetSTTarget().EXP);
        if (StoneExpManager.GoldToExp(AccountSet._AccInfo.coinCount) >= current.expToNextLevel)
        {
            CurrentGoldExaust += GetGoldNeedForNextLevel();
        }
        else
        {
            CurrentGoldExaust += AccountSet._AccInfo.coinCount;
        }
        RefreshSkillLevelUpModule();
    }

    public void MinusTargetLevel()
    {
        if (focusingSSD.GetSTTarget() == null)
            return;
            
        LevelCal.Current current = LevelCal.Instance.GetCurrentInfo(CurrentAddExp() + focusingSSD.GetSTTarget().EXP);
        
        if (current.currentLevel ==1)
        {
            return;
        }
        
        if (current.expRemain > 0)
        {
            if (CurrentGoldExaust >= StoneExpManager.ExpToGold(current.expRemain))
                CurrentGoldExaust -= StoneExpManager.ExpToGold(current.expRemain);
            else
                CurrentGoldExaust = 0;
        }else{
            if (CurrentGoldExaust >= StoneExpManager.ExpToGold(LevelCal.Instance.GetLevelExp(current.currentLevel - 1)))
            {
                CurrentGoldExaust -= StoneExpManager.ExpToGold(LevelCal.Instance.GetLevelExp(current.currentLevel - 1));
            }else{
                CurrentGoldExaust = 0; 
            }
        }
        RefreshSkillLevelUpModule();
    }
    #endregion


}
