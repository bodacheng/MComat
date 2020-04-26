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
            List<string> SkIDs = new List<string>();
            for (int i = 0; i < allSlot.Count; i++)
            {
                if (allSlot[i]._DragAndDropCell.GetItem() != null)
                {
                    SkIDs.Add(allSlot[i]._DragAndDropCell.GetItem()._SkillConfig.RECORD_ID);
                }else{
                    SkIDs.Add(null);
                }
            }
            int wholePoint = MySkillStonesReader.SkillBalancePoint(SkIDs[0],SkIDs[1],SkIDs[2],SkIDs[3],SkIDs[4],SkIDs[5],SkIDs[6],SkIDs[7],SkIDs[8]);
            if (wholePoint < 0)
            {
                Debug.Log("点数不平衡，停止技能更新");
                yield break;
            }
            
            for (int i = 0; i < allSlot.Count; i++)
            {
                if (allSlot[i]._DragAndDropCell.GetItem() != null)
                {
                    if (allSlot[i].OnSlotStoneID != allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId) 
                    {
                        SkillStoneOfPlayerInfoModel formerStoneInfo = MySkillStonesReader.Get(allSlot[i].OnSlotStoneID);
                        if (formerStoneInfo != null && formerStoneInfo.Inherent == "true")
                        {
                            Debug.Log("无法卸载原生技能.skillID: "+ formerStoneInfo.skillId + "  skillStoneOfPlayerId" + formerStoneInfo.skillStoneOfPlayerId);
                            yield break;
                        }
                        yield return RemoveStone(allSlot[i].OnSlotStoneID);
                        // 下面是将九宫格slot上放着的技能石正式装备到目标角色身上。
                        SkillStoneOfPlayerInfoModel new_skillStoneInfo = MySkillStonesReader.Get(allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId);
                        new_skillStoneInfo.inUsingMonsterOfPlayerId = accountCharacterInfo.monsterOfPlayerId;
                        new_skillStoneInfo.inUsingSkillSlot = allSlot[i].number.ToString();
                        yield return MySkillStonesReader.Update(new_skillStoneInfo.skillStoneOfPlayerId);
                    }
                }else{
                    yield return RemoveStone(allSlot[i].OnSlotStoneID);
                }
            }
            yield return ReadANineAndTwo(accountCharacterInfo);
            SeletedRender(null);
        }
        
        IEnumerator RemoveStone(string stoneID)
        {
            // 将原先九宫格对应位置的技能石卸载。即将其inUsingMonsterOfPlayerId变为null。
            SkillStoneOfPlayerInfoModel formerStoneInfo = MySkillStonesReader.Get(stoneID);
            if (formerStoneInfo != null)
            {
                if (formerStoneInfo.Inherent == "true")
                {
                    Debug.Log("无法卸载原生技能.skillID: "+ formerStoneInfo.skillId + "  skillStoneOfPlayerId" + formerStoneInfo.skillStoneOfPlayerId);
                    yield break;
                }
                if (!GetUsingStonesId().Contains(formerStoneInfo.skillStoneOfPlayerId)) //代表原来那个位置上有个技能石，但现在它在技能背包，这轮技能编辑它是要被卸载到背包里去。
                {
                    Debug.Log("技能石头："+ formerStoneInfo.skillStoneOfPlayerId + "被卸下");
                    formerStoneInfo.inUsingMonsterOfPlayerId = null;
                    formerStoneInfo.inUsingSkillSlot = null;
                    yield return MySkillStonesReader.Update(formerStoneInfo.skillStoneOfPlayerId);
                }else{
                    // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                }
            }
        }
    }
}