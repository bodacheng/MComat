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
        public IEnumerator UpdateMyStonesBaseOnSlots(MonsterOfPlayerInfo accCharInfo)
        {
            NineAndTwo.SkillEditError valR = target.CheckEditBasedOnCurrent();
            if (valR != NineAndTwo.SkillEditError.Perfect)
            {
                target.ValiationWarn(valR, accCharInfo.InstanceId);
                yield break;
            }
            SkillEditExecution(accCharInfo);
        }

        void SkillEditExecution(MonsterOfPlayerInfo accCharInfo)
        {
            // 先把所有这个角色装备中的旧石头卸载。事实上，如果某个石头的装备情况没发生变化，那么就产生了一些冗余功。但我感觉不如就这样，因为客户端是可以做手脚的，
            // 如果你用客户端判断是不是有石头在九宫格内位置没发生变化，来决定是否需要通信，那万一被人做了手脚。。
            List<StoneOfPlayerInfo> equiping = MySkillStones.GetEquipingStones(accCharInfo.InstanceId);
            // slot stoneid
            IDictionary<string, string> beforeDic = new Dictionary<string, string>();
            for (int i = 0; i < equiping.Count; i++)
            {
                StoneOfPlayerInfo stone = MySkillStones.Get(equiping[i].InstanceId);
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
                        afterDic.Add((i + 1).ToString(), allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId);
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
                        ToEditStones.Add(afterDic[i.ToString()], Tuple.Create(accCharInfo.InstanceId, i.ToString()));
                    }
                }
            }

            foreach (KeyValuePair<string, Tuple<string, string>> kv in ToEditStones)
            {
                Debug.Log(kv.Key + ":" + kv.Value.Item1 + " : " + kv.Value.Item2);
            }

            IEnumerator successtemp(IDictionary<string, Tuple<string, string>> ee)
            {
                RefreshLocalStoneParams(ee);
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

            void sucess()
            {
                mainProcessRunner.RunAsQueued(successtemp(ToEditStones));
            }


            IEnumerator failtemp()
            {
                yield return ReadANineAndTwo(accCharInfo);
                SeletedRender(null);
                MainSceneLog skillConfirmLog = new MainSceneLog()
                {
                    step = ProcessesRunner.Main.currentProcess.Step,
                    description = "faile"
                };
                MainSceneLogger.Logs.Add(skillConfirmLog);
            }

            void error()
            {
                mainProcessRunner.RunAsQueued(failtemp());
            }

            MySkillStones.Update(ToEditStones, sucess, error);
        }

        void RefreshLocalStoneParams(IDictionary<string, Tuple<string, string>> ToEditStones)
        {
            foreach (KeyValuePair<string, Tuple<string, string>> kv in ToEditStones)
            {
                if (!MySkillStones.Dic.ContainsKey(kv.Key) || MySkillStones.Dic[kv.Key] == null)
                {
                    Debug.Log("更新对象技能石不存在。stoneOfPlayerID :" + kv.Key);
                    return;
                }
                StoneOfPlayerInfo ofPlayerInfo = MySkillStones.Dic[kv.Key];
                ofPlayerInfo.inUsingMonsterOfPlayerId = kv.Value.Item1;
                ofPlayerInfo.inUsingSkillSlot = kv.Value.Item2;
            }
        }

        // 下面这个貌似还是有地方用。。。先别删
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
                    //yield return MySkillStones.Update(formerStoneInfo.InstanceId);
                }else{
                    // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                }
            }
        }
    }
}