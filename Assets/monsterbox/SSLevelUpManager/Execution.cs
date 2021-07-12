using dataAccess;
using UnityEngine;
using Api.Dto.Form;
using Api.Dto.Model;

// 执行
public partial class SSLevelUpManager : MonoBehaviour
{
    public void LevelUpStone(string InstanceId)
    {
        SkillStoneLevelUpForm skillStoneLevelUpForm = new SkillStoneLevelUpForm();
        
        SKStoneItem item1 = cell1.GetItem();
        SKStoneItem item2 = cell2.GetItem();
        SKStoneItem item3 = cell3.GetItem();
        SKStoneItem item4 = cell4.GetItem();
        SKStoneItem item5 = cell5.GetItem();
        
        skillStoneLevelUpForm.targetStoneID = InstanceId;
        
        skillStoneLevelUpForm.M1Stone = item1 != null ? item1.instanceId : null;
        skillStoneLevelUpForm.M2Stone = item2 != null ? item2.instanceId : null;
        skillStoneLevelUpForm.M3Stone = item3 != null ? item3.instanceId : null;
        skillStoneLevelUpForm.M4Stone = item4 != null ? item4.instanceId : null;
        skillStoneLevelUpForm.M5Stone = item5 != null ? item5.instanceId : null;
        
        skillStoneLevelUpForm.UseGold = CurrentGoldExaust.ToString();
    }
    
    // 技能升级确认。
    public void ConfirmSkillStoneLevelUp()
    {
        StoneOfPlayerInfo StoneInfoModel = Stones.Get(stoneOfPlayerId);
        if (StoneInfoModel == null)
            return;
        LevelUpStone(StoneInfoModel.InstanceId);
    }
}