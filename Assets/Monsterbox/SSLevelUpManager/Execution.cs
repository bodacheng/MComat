using System;
using System.Collections.Generic;
using dataAccess;
using UnityEngine;

// 执行
public partial class SSLevelUpManager : MonoBehaviour
{
    void LevelUpStone(string InstanceId, Action<string> refreshStoneData)
    {
        var materialInstanceIds = new List<string>();
        
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
        
        void AddInstanceIdToList(SKStoneItem item)
        {
            if (item != null)
                materialInstanceIds.Add(item.instanceId);
        }
        
        AddInstanceIdToList(item1);
        AddInstanceIdToList(item2);
        AddInstanceIdToList(item3);
        AddInstanceIdToList(item4);
        
        // 以下是远程那边计算技能石升到等级的逻辑：
        var materialLevels = new List<int>();
        var addLevel = 0; // 增加的等级
        void Temp(string instanceID)
        {
            var ssInfo = Stones.Get(instanceID);
            if (ssInfo.Born == "true")
            {
                Debug.Log("操作终止。被动技能正在被用作材料："+ssInfo.InstanceId);
                return;
            }
            
            materialLevels.Add(ssInfo.Level);
            addLevel += (ssInfo.Level - 1);
            if (materialLevels.Count == 4)
                addLevel += 1;
        }
        
        foreach (var instanceId in materialInstanceIds)
        {
            Temp(instanceId);
        }
        
        if (materialLevels.Count != 4)
        {
            Debug.Log("逻辑错误，material count :"+ materialLevels.Count);
            return;
        }

        form.addLevel = addLevel.ToString();
        
        CloudScript.UpdateStone(
            form,
            (targetInstanceId,x) =>
            {
                refreshStoneData.Invoke(targetInstanceId);
                foreach (var instanceId in x)
                {
                    Stones.RemoveStoneLocal(instanceId);
                }
                CloseLevelUpPage();
                _stoneListLayer.TargetStoneID = targetInstanceId;
            }
        );
    }
    
    // 技能升级确认。
    public void ConfirmSkillStoneLevelUp(Action<string> refresh)
    {
        var target = Stones.Get(_stoneListLayer.TargetStoneID);
        if (target == null)
        {
            Debug.Log("逻辑顺序错误？");
            return;
        }

        if (target.Born == "true")
        {
            Debug.Log("原生技能石不需升级");
            return;
        }
        LevelUpStone(target.InstanceId, refresh);
    }
}