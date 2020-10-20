using System.Collections;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        // 撤销编辑
        public void ResetNineSlot()
        {
            mainProcessRunner.Run(TheNineSlot.target.ReadANineAndTwo(MemberDetail.target._focusing));
        }
        
        public IEnumerator UpdateMyStonesBaseOnSlots(GetMonsterOfPlayerDetailModel accCharInfo)
        {
            NineAndTwo.SkillEditError valR = target.CheckEditBasedOnCurrent();
            if (valR != NineAndTwo.SkillEditError.Perfect)
            {
                target.ValiationWarn(valR, MemberDetail.target._focusing.monsterOfPlayerId);
                yield break;
            }
            
            for (int i = 0; i < allSlot.Count; i++)
            {
                if (allSlot[i]._DragAndDropCell.GetItem() != null)
                {
                    if (allSlot[i].OnSlotStoneID != allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId) 
                    {
                        yield return RemoveStone(allSlot[i].OnSlotStoneID);
                        // 下面是将九宫格slot上放着的技能石正式装备到目标角色身上。
                        SkillStoneOfPlayerInfoModel new_skillStoneInfo = MySkillStonesReader.Get(allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId);
                        new_skillStoneInfo.inUsingMonsterOfPlayerId = accCharInfo.monsterOfPlayerId;
                        new_skillStoneInfo.inUsingSkillSlot = allSlot[i].number.ToString();
                        yield return MySkillStonesReader.Update(new_skillStoneInfo.skillStoneOfPlayerId);
                    }
                }else{
                    yield return RemoveStone(allSlot[i].OnSlotStoneID);
                }
            }
            yield return ReadANineAndTwo(accCharInfo);
            SeletedRender(null);
            
            MemberDetail.target.presentationProcessRunner.Run(MemberDetail.target.SkillEditConfirmAnimation());
            MainSceneLog skillConfirmLog = new MainSceneLog()
            {
                step = ProcessesRunner.Main.currentProcess.Step,
                description = "success"
            };
            MainSceneLogger.Logs.Add(skillConfirmLog);
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