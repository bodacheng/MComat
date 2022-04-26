using UnityEngine;
using dataAccess;
using System.Collections.Generic;

// 技能石消耗
public partial class SSLevelUpManager : MonoBehaviour
{
    List<StoneCell> MaterialSlots;
    
    public void AddMaterialFromCell(StoneCell boxCell)
    {
        var target = Stones.Get(targetStoneID);
        foreach (var slot in MaterialSlots)
        {
            var Material = boxCell.GetItem();
            if (slot.GetItem() == null && Material != null &&
                Material._SkillConfig.RECORD_ID == target.skillId) // 只能以同技能石为材料
            {
                if (Material.instanceId != target.InstanceId)
                {
                    StoneCell.Install(boxCell, slot);
                }
            }
        }
    }
    
    // 自动添加技能卡
    public void AutoAddMaterials(string skillId)
    {
        var stones = Stones.GetMyStonesBySkillID(skillId);
        for (var i = 0; i < Mathf.Min(stones.Count, MaterialSlots.Count); i++)
        {
            var alreadyThere = MaterialSlots[i].GetItem();
            if (alreadyThere == null)
            {
                if (targetStoneID != stones[i])
                {
                    var itemModel = Stones.GetRenderModel(stones[i]);
                    MaterialSlots[i].AddItem(itemModel);
                }
            }
        }
        RefreshSkillLevelUpModule();
    }
}