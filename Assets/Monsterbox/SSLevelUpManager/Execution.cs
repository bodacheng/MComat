using System;
using dataAccess;
using UnityEngine;

// 执行
public partial class SSLevelUpManager : MonoBehaviour
{
    void LevelUpStone(string InstanceId, Action<string> refreshStoneData)
    {
        var form = new SkillStoneLevelUpForm();
        
        var item1 = cell1.GetItem();
        var item2 = cell2.GetItem();
        var item3 = cell3.GetItem();
        var item4 = cell4.GetItem();
        
        form.targetStoneID = InstanceId;
        form.M1Stone = item1 != null ? item1.instanceId : null;
        form.M2Stone = item2 != null ? item2.instanceId : null;
        form.M3Stone = item3 != null ? item3.instanceId : null;
        form.M4Stone = item4 != null ? item4.instanceId : null;
        
        CloudScript.UpdateStone(form, 
            (targetInstanceId,x) =>
            {
                refreshStoneData.Invoke(targetInstanceId);
                foreach (var instanceId in x)
                {
                    Stones.RemoveStoneLocal(instanceId);
                }
            }
        );
    }
    
    // 技能升级确认。
    public void ConfirmSkillStoneLevelUp(Action<string> refresh)
    {
        var target = Stones.Get(targetInstanceID);
        if (target == null)
        {
            Debug.Log("逻辑顺序错误？");
            return;
        }
        LevelUpStone(target.InstanceId, refresh);
    }
}