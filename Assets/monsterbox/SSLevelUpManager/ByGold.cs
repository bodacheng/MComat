using UnityEngine;
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
        
        LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo(CurrentAddExp() + focusingSSD.GetSTTarget().EXP);
        // +号代表直接把技能石升到下一级所需要的经验全数补充上，不够的话就把当前所有剩余的金币加上
        if (StoneExpManager.GoldToExp(AccountSet._AccInfo.coinCount) >= current.expToNextLevel)
        {
            CurrentGoldExaust += StoneExpManager.ExpToGold(current.expToNextLevel);
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
            
        LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo(CurrentAddExp() + focusingSSD.GetSTTarget().EXP);
        
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
        }else{// 即便为0
            if (CurrentGoldExaust >= StoneExpManager.ExpToGold(LevelExpConfig.GetLevelExp(current.currentLevel - 1)))
            {
                CurrentGoldExaust -= StoneExpManager.ExpToGold(LevelExpConfig.GetLevelExp(current.currentLevel - 1));
            }else{
                CurrentGoldExaust = 0; 
            }
        }
        RefreshSkillLevelUpModule();
    }
    #endregion


}
