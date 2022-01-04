using dataAccess;
using UnityEngine;
using Api.Dto.Form;

// 执行
public partial class SSLevelUpManager : MonoBehaviour
{
    void LevelUpStone(string InstanceId)
    {
        SkillStoneLevelUpForm form = new SkillStoneLevelUpForm();
        
        SKStoneItem item1 = cell1.GetItem();
        SKStoneItem item2 = cell2.GetItem();
        SKStoneItem item3 = cell3.GetItem();
        SKStoneItem item4 = cell4.GetItem();
        SKStoneItem item5 = cell5.GetItem();
        
        form.targetStoneID = InstanceId;
        
        form.M1Stone = item1 != null ? item1.instanceId : null;
        form.M2Stone = item2 != null ? item2.instanceId : null;
        form.M3Stone = item3 != null ? item3.instanceId : null;
        form.M4Stone = item4 != null ? item4.instanceId : null;
        form.M5Stone = item5 != null ? item5.instanceId : null;
    }
    
    // 技能升级确认。
    public void ConfirmSkillStoneLevelUp()
    {
        StoneOfPlayerInfo target = Stones.Get(targetStoneID);
        if (target == null)
            return;
        LevelUpStone(target.InstanceId);
    }
}