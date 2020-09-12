using UnityEngine;
using dataAccess;

// 技能石消耗
public partial class SSLevelUpManager : MonoBehaviour
{    
    #region 素材的添加与移除
    public void AddMaterial(StoneCell skillboxcell)
    {
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            MaterialSlots[i].UpdateMyItem();
            SKStoneItem Material = skillboxcell.GetItem();
            if (MaterialSlots[i].GetItem() == null && Material != null)
            {
                if (Material.SkillStoneOfPlayerId != focusingSSD.GetSTTarget().skillStoneOfPlayerId)
                {
                    StoneCell.Install(skillboxcell, MaterialSlots[i]);
                    break;
                }
            }
        }
    }
    
    void ReturnAllMaterialsToBox()
    {
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            MaterialSlots[i].UpdateMyItem();
            if (MaterialSlots[i].GetItem() != null)
            {
                MaterialSlots[i].ReturnStoneToBox();
            }
        }
    }
    #endregion
    
    private int CalCurrentExpFromMaterialStone()
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
        
        int point1 = item1 != null ? MySkillStonesReader.ConvertSKStoneToExp(item1.SkillStoneOfPlayerId) : 0;
        int point2 = item2 != null ? MySkillStonesReader.ConvertSKStoneToExp(item2.SkillStoneOfPlayerId) : 0;
        int point3 = item3 != null ? MySkillStonesReader.ConvertSKStoneToExp(item3.SkillStoneOfPlayerId) : 0;
        int point4 = item4 != null ? MySkillStonesReader.ConvertSKStoneToExp(item4.SkillStoneOfPlayerId) : 0;
        int point5 = item5 != null ? MySkillStonesReader.ConvertSKStoneToExp(item5.SkillStoneOfPlayerId) : 0;
        
        int fullAmount = GoldToExp(point1) + GoldToExp(point2) + GoldToExp(point3) + GoldToExp(point4) + GoldToExp(point5);
        return fullAmount;
    }
    
    #region 智慧果实与经验值转换关系 可能改变位置
    public int GoldToExp(int gold)
    {
        return (gold) / 10 * 1;
    }
    
    public int ExpToGold(int Exp)
    {
        return Exp * 10;
    }
    #endregion
}