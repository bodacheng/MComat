using UnityEngine;
using dataAccess;
using System.Collections.Generic;

// 技能石消耗
public partial class SSLevelUpManager : MonoBehaviour
{
    List<StoneCell> MaterialSlots;
    
    public void AddMaterialFromCell(StoneCell boxCell)
    {
        StoneOfPlayerInfo target = Stones.Get(targetStoneID);
        foreach (var t in MaterialSlots)
        {
            SKStoneItem Material = boxCell.GetItem();
            if (t.GetItem() == null && Material != null &&
                Material._SkillConfig.RECORD_ID == target.skillId) // 只能以同技能石为材料
            {
                if (Material.instanceId != target.InstanceId)
                {
                    StoneCell.Install(boxCell, t);
                    break;
                }
            }
        }
    }
    
    public void AutoAddMaterials(string skillId)
    {
        List<string> stones = Stones.GetMyStonesBySkillID(skillId);
        for (var i = 0; i < Mathf.Min(stones.Count, MaterialSlots.Count); i++)
        {
            SKStoneItem alreadyThere = MaterialSlots[i].GetItem();
            if (alreadyThere == null)
            {
                if (targetStoneID != stones[i])
                {
                    SKStoneItem itemModel = Stones.GetRenderModel(stones[i]);
                    MaterialSlots[i].AddItem(itemModel);
                }
            }
        }
        RefreshSkillLevelUpModule();
    }
    
    // 具体怎么换算再说吧。
    int CalCurrentExpFromMaterials()
    {
        cell1.UpdateMyItem();
        cell2.UpdateMyItem();
        cell3.UpdateMyItem();
        cell4.UpdateMyItem();
        cell5.UpdateMyItem();
        
        SKStoneItem item1 = cell1.GetItem();
        SKStoneItem item2 = cell2.GetItem();
        SKStoneItem item3 = cell3.GetItem();
        SKStoneItem item4 = cell4.GetItem();
        SKStoneItem item5 = cell5.GetItem();
        
        int point1 = item1 != null ? StoneExpManager.ConvertSKStoneToExp(item1.instanceId) : 0;
        int point2 = item2 != null ? StoneExpManager.ConvertSKStoneToExp(item2.instanceId) : 0;
        int point3 = item3 != null ? StoneExpManager.ConvertSKStoneToExp(item3.instanceId) : 0;
        int point4 = item4 != null ? StoneExpManager.ConvertSKStoneToExp(item4.instanceId) : 0;
        int point5 = item5 != null ? StoneExpManager.ConvertSKStoneToExp(item5.instanceId) : 0;
        
        int fullAmount = StoneExpManager.GoldToExp(point1) + StoneExpManager.GoldToExp(point2) + StoneExpManager.GoldToExp(point3) + StoneExpManager.GoldToExp(point4) + StoneExpManager.GoldToExp(point5);
        return fullAmount;
    }
}