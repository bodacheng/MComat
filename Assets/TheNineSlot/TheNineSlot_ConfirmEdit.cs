using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        // 这个函数应该能够被用于Tutorial模式下亚当的技能编辑。
        public IEnumerator UpdateMyStonesBaseOnSlots(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            for (int i = 0; i < allSlot.Count; i++)
            {
                if (allSlot[i]._DragAndDropCell.GetItem() != null)
                {
                    if (allSlot[i].OnSlotStoneID != allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId) 
                    {
                        yield return RemoveStone(allSlot[i].OnSlotStoneID);
                        // 下面是将九宫格slot上放着的技能石正式装备到目标角色身上。
                        SkillStoneOfPlayerInfoModel new_skillStoneInfo = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId);
                        if (new_skillStoneInfo != null)
                        {
                            new_skillStoneInfo.inUsingMonsterOfPlayerId = accountCharacterInfo.monsterOfPlayerId;
                            new_skillStoneInfo.inUsingSkillSlot = allSlot[i].number.ToString();
                            yield return MySkillStonesReader.Instance.UpdateMySkillStone(new_skillStoneInfo.skillStoneOfPlayerId);
                        }
                    }
                }else{
                    yield return RemoveStone(allSlot[i].OnSlotStoneID);
                }
            }
            yield return ReadANineAndTwo(accountCharacterInfo);
            SeletedRender(null);
            yield break;
        }
        
        IEnumerator RemoveStone(string stoneID)
        {
            // 将原先九宫格对应位置的技能石卸载。即将其inUsingMonsterOfPlayerId变为null。
            SkillStoneOfPlayerInfoModel formerStoneInfo = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(stoneID);
            if (formerStoneInfo != null)
            {
                if (!GetUsingStonesId().Contains(formerStoneInfo.skillStoneOfPlayerId)) //代表原来那个位置上有个技能石，但现在它在技能背包，这轮技能编辑它是要被卸载到背包里去。
                {
                    Debug.Log("技能石头："+ formerStoneInfo.skillStoneOfPlayerId + "被卸下");
                    formerStoneInfo.inUsingMonsterOfPlayerId = null;
                    formerStoneInfo.inUsingSkillSlot = null;
                    yield return MySkillStonesReader.Instance.UpdateMySkillStone(formerStoneInfo.skillStoneOfPlayerId);
                }else{
                    // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                }
            }
        }
    }
}