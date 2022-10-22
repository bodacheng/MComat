using UnityEngine;
using dataAccess;
using System.Collections.Generic;
using System;
using DummyLayerSystem;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        private Action extraSkillEditSuccess;
        public void SetExtraSkillEditSuccess(Action extraSkillEditSuccess)
        {
            this.extraSkillEditSuccess = extraSkillEditSuccess;
        }
        
        public void UpdateStonesBaseOnSlots(UnitInfo unitInfo)
        {
            var equipping = Stones.GetEquippingStones(unitInfo.id);
            // slot stone_id
            IDictionary<string, string> beforeDic = new Dictionary<string, string>();
            for (var i = 0; i < equipping.Count; i++)
            {
                var stone = Stones.Get(equipping[i].InstanceId);
                if (stone.Slot != null)
                {
                    if (!beforeDic.ContainsKey(stone.Slot))
                        beforeDic.Add(stone.Slot, stone.InstanceId);
                    else
                    {
                        Debug.Log("unit :"+ unitInfo.id+ " has multi stones on one slot.");
                    }
                }
            }

            for (var i = 1; i < 10; i++)
            {
                if (!beforeDic.ContainsKey(i.ToString()))
                {
                    beforeDic.Add(i.ToString(), null);
                }
            }

            // slot stoneid
            IDictionary<string, string> afterDic = new Dictionary<string, string>();
            for (var i = 0; i < allSlot.Count; i++)
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
            
            for (var i = 1; i < 10; i++)
            {
                if (beforeDic[i.ToString()] != afterDic[i.ToString()])
                {
                    if (beforeDic[i.ToString()] != null && !ToEditStones.ContainsKey(beforeDic[i.ToString()]))
                    {
                        ToEditStones.Add(beforeDic[i.ToString()], Tuple.Create(string.Empty, string.Empty));
                    }
                }
            }
            
            ProgressLayer.Loading(">", PreScene.target.T);
            
            void Success(IDictionary<string, Tuple<string, string>> ChangedStoneDic)
            {
                Stones.RefreshLocalStoneParams(ChangedStoneDic);
                var skillEditLayer = UILayerLoader.Get<SkillEditLayer>();
                if (skillEditLayer != null)
                {
                    ReadANineAndTwo(unitInfo);
                    skillEditLayer.StonesBox.RestFilter();
                    SelectedRender(null);
                    skillEditLayer.SkillEditConfirmAnimation();
                }
                
                var skillConfirmLog = new MainSceneLog()
                {
                    step = ProcessesRunner.Main.currentProcess.Step,
                    description = "success"
                };
                MainSceneLogger.Logs.Add(skillConfirmLog);
                
                extraSkillEditSuccess?.Invoke();
                ProgressLayer.Close();
            }
            
            void error()
            {
                var skillEditLayer = UILayerLoader.Get<SkillEditLayer>();
                if (skillEditLayer != null)
                {
                    ReadANineAndTwo(unitInfo);
                    SelectedRender(null);
                }
                
                var skillConfirmLog = new MainSceneLog()
                {
                    step = ProcessesRunner.Main.currentProcess.Step,
                    description = "failed"
                };
                MainSceneLogger.Logs.Add(skillConfirmLog);
                
                ProgressLayer.Close();
            }
            
            CloudScript.UpdateSkillEdit(ToEditStones, Success, error);
        }
    }
}