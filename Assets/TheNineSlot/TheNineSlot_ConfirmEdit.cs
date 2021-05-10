using System.Collections;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public IEnumerator UpdateMyStonesBaseOnSlots(MonsterOfPlayerInfo accCharInfo)
        {
            NineAndTwo.SkillEditError valR = target.CheckEditBasedOnCurrent();
            if (valR != NineAndTwo.SkillEditError.Perfect)
            {
                target.ValiationWarn(valR, accCharInfo.InstanceId);
                yield break;
            }
            yield return UpdateMyStonesBaseOnSlotsExecution(accCharInfo);
        }
        
        IEnumerator UpdateMyStonesBaseOnSlotsExecution(MonsterOfPlayerInfo accCharInfo)
        {
            // 先把所有这个角色装备中的旧石头卸载。事实上，如果某个石头的装备情况没发生变化，那么就产生了一些冗余功。但我感觉不如就这样，因为客户端是可以做手脚的，
            // 如果你用客户端判断是不是有石头在九宫格内位置没发生变化，来决定是否需要通信，那万一被人做了手脚。。
            List<StoneOfPlayerInfo> equipingstones = MySkillStones.GetEquipingStones(accCharInfo.InstanceId);
            for (int i = 0; i < equipingstones.Count; i++)
            {
                StoneOfPlayerInfo removedStone = MySkillStones.Get(equipingstones[i].InstanceId);
                removedStone.inUsingMonsterOfPlayerId = null;
                removedStone.inUsingSkillSlot = null;
                yield return MySkillStones.Update(removedStone.InstanceId);
            }
            
            for (int i = 0; i < allSlot.Count; i++)
            {
                if (allSlot[i]._DragAndDropCell.GetItem() != null)
                {
                    //yield return RemoveStone(allSlot[i].OnSlotStoneID);
                    // 下面是将九宫格slot上放着的技能石正式装备到目标角色身上。
                    StoneOfPlayerInfo new_skillStoneInfo = MySkillStones.Get(allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId);
                    new_skillStoneInfo.inUsingMonsterOfPlayerId = accCharInfo.InstanceId;
                    new_skillStoneInfo.inUsingSkillSlot = allSlot[i].number.ToString();
                    yield return MySkillStones.Update(new_skillStoneInfo.InstanceId);
                }
                else{
                    //yield return RemoveStone(allSlot[i].OnSlotStoneID);
                }
            }
            yield return ReadANineAndTwo(accCharInfo);
            SeletedRender(null);
            
            yield return MemberDetail.target.SkillEditConfirmAnimation();
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
            StoneOfPlayerInfo formerStoneInfo = MySkillStones.Get(stoneID);
            if (formerStoneInfo != null)
            {
                if (formerStoneInfo.Inherent == "true")
                {
                    Debug.Log("无法卸载原生技能.skillID: "+ formerStoneInfo.skillId + "  skillStoneOfPlayerId" + formerStoneInfo.InstanceId);
                    yield break;
                }
                if (!GetUsingStonesId().Contains(formerStoneInfo.InstanceId)) //代表原来那个位置上有个技能石，但现在它在技能背包，这轮技能编辑它是要被卸载到背包里去。
                {
                    Debug.Log("技能石头："+ formerStoneInfo.InstanceId + "被卸下");
                    formerStoneInfo.inUsingMonsterOfPlayerId = null;
                    formerStoneInfo.inUsingSkillSlot = null;
                    yield return MySkillStones.Update(formerStoneInfo.InstanceId);
                }else{
                    // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                }
            }
        }
    }
}