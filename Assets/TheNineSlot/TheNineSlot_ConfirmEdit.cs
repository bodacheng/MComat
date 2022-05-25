using System.Collections;
using UnityEngine;
using dataAccess;
using System.Collections.Generic;
using System;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public void UpdateStonesBaseOnSlots(UnitInfo unitInfo)
        {
            var equipping = Stones.GetEquippingStones(unitInfo.id);
            // slot stone_id
            IDictionary<string, string> beforeDic = new Dictionary<string, string>();
            for (var i = 0; i < equipping.Count; i++)
            {
                var stone = Stones.Get(equipping[i].InstanceId);
                if (stone.slot != null)
                {
                    if (!beforeDic.ContainsKey(stone.slot))
                        beforeDic.Add(stone.slot, stone.InstanceId);
                    else
                    {
                        Debug.Log("unit :"+ unitInfo.id+ " has multi stones on one slot.");
                    }
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
                if (allSlot[i]._cell.GetItem() != null)
                {
                    if (!afterDic.ContainsKey((i + 1).ToString()))
                        afterDic.Add((i + 1).ToString(), allSlot[i]._cell.GetItem().instanceId);
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
            
            for (var i = 1; i < 10; i++)
            {
                if (beforeDic[i.ToString()] != afterDic[i.ToString()])
                {
                    if (afterDic[i.ToString()] != null)
                    {
                        ToEditStones.Add(afterDic[i.ToString()], Tuple.Create(unitInfo.id, i.ToString()));
                    }
                }
            }
            
            for (int i = 1; i < 10; i++)
            {
                if (beforeDic[i.ToString()] != afterDic[i.ToString()])
                {
                    if (beforeDic[i.ToString()] != null && !ToEditStones.ContainsKey(beforeDic[i.ToString()]))
                    {
                        ToEditStones.Add(beforeDic[i.ToString()], Tuple.Create(string.Empty, string.Empty));
                    }
                }
            }

            void Success(IDictionary<string, Tuple<string, string>> ChangedStoneDic)
            {
                Stones.RefreshLocalStoneParams(ChangedStoneDic);
                ReadANineAndTwo(unitInfo);
                var skillEditLayer = SkillEditLayer.Open();
                skillEditLayer.StonesBox.RestFilter();
                SelectedRender(null);
                SkillEditConfirmAnimation();
                
                MainSceneLog skillConfirmLog = new MainSceneLog()
                {
                    step = ProcessesRunner.Main.currentProcess.Step,
                    description = "success"
                };
                MainSceneLogger.Logs.Add(skillConfirmLog);
            }
            
            void SkillEditConfirmAnimation()
            {
                UnitConfig unitConfig = Units.GetUnitConfig(PreScene.target._focusing.r_id);
                string personalEffectsPath = FightGlobalSetting.EffectPathDefine(unitConfig.element);
                EffectsManager.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, SkillShowSupporter.FocusingC.WholeT.position, Quaternion.identity, null);
            }
            
            void error()
            {
                ReadANineAndTwo(unitInfo);
                SelectedRender(null);
                MainSceneLog skillConfirmLog = new MainSceneLog()
                {
                    step = ProcessesRunner.Main.currentProcess.Step,
                    description = "failed"
                };
                MainSceneLogger.Logs.Add(skillConfirmLog);
            }
            
            CloudScript.UpdateSkillEdit(ToEditStones, (x) => Success(x), error);
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
                    formerStoneInfo.unitInstanceId = null;
                    formerStoneInfo.slot = null;
                    //yield return MySkillStones.Update(formerStoneInfo.InstanceId);
                }else{
                    // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                }
            }
        }
    }
}