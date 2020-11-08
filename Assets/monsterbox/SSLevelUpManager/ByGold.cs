using UnityEngine;
using dataAccess;
using Api.Dto.Model;

// 智慧果实消耗
public partial class SSLevelUpManager : MonoBehaviour
{
    int CurrentGoldExaust;

    #region 调整目标等级 直接放在按钮上。
    public void PlusTargetLevel()
    {
        SkillStoneOfPlayerInfoModel StoneInfoModel = MySkillStonesReader.Get(stoneOfPlayerId);
        if (StoneInfoModel == null)
            return;
        
        LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo(CurrentAddExp() + StoneInfoModel.EXP);
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
        SkillStoneOfPlayerInfoModel StoneInfoModel = MySkillStonesReader.Get(stoneOfPlayerId);
        if (StoneInfoModel == null)
        {
            RefreshSkillLevelUpModule();
            return;
        }
        
        LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo(CurrentAddExp() + StoneInfoModel.EXP);
        
        if (current.currentLevel == 1)
        {
            CurrentGoldExaust = 0;
            RefreshSkillLevelUpModule();
            return;
        }
        
        if (current.expRemain > 0)
        {
            if (CurrentGoldExaust >= StoneExpManager.ExpToGold(current.expRemain))
            {
                CurrentGoldExaust -= StoneExpManager.ExpToGold(current.expRemain);
                Debug.Log(CurrentGoldExaust);
            }
            else
                CurrentGoldExaust = 0;
        }else{
            if (CurrentGoldExaust >= StoneExpManager.ExpToGold(LevelExpConfig.GetLevelExp(current.currentLevel)))
            {
                CurrentGoldExaust -= StoneExpManager.ExpToGold(LevelExpConfig.GetLevelExp(current.currentLevel));
            }else{
                CurrentGoldExaust = 0; 
            }
        }
        RefreshSkillLevelUpModule();
    }
    #endregion
}
