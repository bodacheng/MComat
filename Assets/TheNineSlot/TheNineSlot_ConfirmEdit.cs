using System.Collections;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;
using System;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public void UpdateStonesBaseOnSlots(MonsterOfPlayerInfo info)
        {
            SkillSet.SkillEditError valR = target.CheckEditBasedOnCurrent();
            if (valR != SkillSet.SkillEditError.Perfect)
            {
                target.ValiationWarn(valR, info.InstanceId);
                return;
            }
            List<StoneOfPlayerInfo> equiping = Stones.GetEquipingStones(info.InstanceId);
            // slot stoneid
            IDictionary<string, string> beforeDic = new Dictionary<string, string>();
            for (int i = 0; i < equiping.Count; i++)
            {
                StoneOfPlayerInfo stone = Stones.Get(equiping[i].InstanceId);
                if (stone.inUsingSkillSlot != null)
                {
                    if (!beforeDic.ContainsKey(stone.inUsingSkillSlot))
                        beforeDic.Add(stone.inUsingSkillSlot, stone.InstanceId);
                    else
                        Debug.Log("严重逻辑错误。怎么办待定");
                }
            }

            for (int i = 1; i < 10; i++)
            {
                if (!beforeDic.ContainsKey(i.ToString()))
                {
                    beforeDic.Add(i.ToString(), null);
                }
            }

            // slot stoneid
            IDictionary<string, string> afterDic = new Dictionary<string, string>();
            for (int i = 0; i < allSlot.Count; i++)
            {
                if (allSlot[i]._DragAndDropCell.GetItem() != null)
                {
                    if (!afterDic.ContainsKey((i + 1).ToString()))
                        afterDic.Add((i + 1).ToString(), allSlot[i]._DragAndDropCell.GetItem().instanceId);
                    else
                        Debug.Log("严重逻辑错误。怎么办待定");
                }
                else
                {
                    afterDic.Add((i + 1).ToString(), null);
                }
            }

            // k v : stoneid , equipingMonster, slot
            IDictionary<string, Tuple<string, string>> ToEditStones = new Dictionary<string, Tuple<string, string>>();

            for (int i = 1; i < 10; i++)
            {
                if (beforeDic[i.ToString()] != afterDic[i.ToString()])
                {
                    // 如果原来的位置上有石头
                    if (beforeDic[i.ToString()] != null)
                    {
                        ToEditStones.Add(beforeDic[i.ToString()], Tuple.Create(string.Empty, string.Empty));
                    }

                    if (afterDic[i.ToString()] != null)
                    {
                        ToEditStones.Add(afterDic[i.ToString()], Tuple.Create(info.InstanceId, i.ToString()));
                    }
                }
            }

            void sucess(IDictionary<string, Tuple<string, string>> ee)
            {
                Stones.RefreshLocalStoneParams(ee);
                ReadANineAndTwo(info);
                SkillStonesBox.target.RestFilter();
                SeletedRender(null);
                MemberDetail.target.SkillEditConfirmAnimation();

                MainSceneLog skillConfirmLog = new MainSceneLog()
                {
                    step = ProcessesRunner.Main.currentProcess.Step,
                    description = "success"
                };
                MainSceneLogger.Logs.Add(skillConfirmLog);
            }

            void error()
            {
                ReadANineAndTwo(info);
                SeletedRender(null);
                MainSceneLog skillConfirmLog = new MainSceneLog()
                {
                    step = ProcessesRunner.Main.currentProcess.Step,
                    description = "faile"
                };
                MainSceneLogger.Logs.Add(skillConfirmLog);
            }

            Stones.Update(ToEditStones, () => sucess(ToEditStones), error);
        }



        // 下面这个貌似还是有地方用。。。先别删
        IEnumerator RemoveStone(string stoneID)
        {
            // 将原先九宫格对应位置的技能石卸载。即将其inUsingMonsterOfPlayerId变为null。
            StoneOfPlayerInfo formerStoneInfo = Stones.Get(stoneID);
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
                    //yield return MySkillStones.Update(formerStoneInfo.InstanceId);
                }else{
                    // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                }
            }
        }
    }
}