using UnityEngine;
using dataAccess;
using Api.Dto.Model;

// 技能石消耗
public partial class SSLevelUpManager : MonoBehaviour
{
    #region 素材的添加与移除
    public void AddMaterial(StoneCell skillboxcell)
    {
        StoneOfPlayerInfo steppingstone = Stones.Get(targetInstanceId);
        Debug.Log("为"+ steppingstone);
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            Debug.Log("slot"+ MaterialSlots[i]);
            MaterialSlots[i].UpdateMyItem();
            SKStoneItem Material = skillboxcell.GetItem();
            if (MaterialSlots[i].GetItem() == null && Material != null)
            {
                Debug.Log(Material.instanceId + ":"+ steppingstone.InstanceId);
                if (Material.instanceId != steppingstone.InstanceId)
                {
                    Debug.Log("dsds");
                    StoneCell.Install(skillboxcell, MaterialSlots[i]);
                    break;
                }
            }
        }
    }
    #endregion

    int CalCurrentExpFromMaterialStone()
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