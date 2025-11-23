using System.Collections.Generic;
using UnityEngine;
using System;

namespace dataAccess
{
    public static partial class Stones
    {
        static readonly IDictionary<string, StoneOfPlayerInfo> Dic = new Dictionary<string, StoneOfPlayerInfo>();
        static readonly IDictionary<string, SKStoneItem> RenderModelDic = new Dictionary<string, SKStoneItem>();
        static readonly IDictionary<string, string> TempUnitUsageDic = new Dictionary<string, string>(); // 临时用于界面显示的装备信息
        
        public static void ClearData()
        {
            Dic.Clear();
            TempUnitUsageDic.Clear();
        }

        public static bool TooManyStones()
        {
            return Dic.Count > CommonSetting.MaxStoneCount;
        }
        
        public static void ClearRender()
        {
            foreach (var kv in RenderModelDic)
            {
                GameObject.Destroy(kv.Value);
            }
            RenderModelDic.Clear();
        }

        /// <summary>
        /// 设置临时的装备角色，用于界面展示当前编辑状态。
        /// 传入null时移除临时记录；传入string.Empty时表示明确未装备，避免回退到持久化的unitInstanceId。
        /// </summary>
        public static void SetTempUnitUsage(string stoneInstanceId, string unitInstanceId)
        {
            if (string.IsNullOrEmpty(stoneInstanceId))
                return;
            if (unitInstanceId == null)
            {
                if (TempUnitUsageDic.ContainsKey(stoneInstanceId))
                    TempUnitUsageDic.Remove(stoneInstanceId);
            }
            else
            {
                TempUnitUsageDic[stoneInstanceId] = unitInstanceId;
            }
        }

        public static void ClearTempUnitUsage()
        {
            TempUnitUsageDic.Clear();
        }

        /// <summary>
        /// 优先返回临时的装备角色，用于UI显示当前编辑状态。
        /// </summary>
        public static string GetUnitInstanceIdForDisplay(string stoneInstanceId)
        {
            if (stoneInstanceId != null && TempUnitUsageDic.TryGetValue(stoneInstanceId, out var tempUnit))
                return tempUnit;
            return Get(stoneInstanceId)?.unitInstanceId;
        }
        
        public static StoneOfPlayerInfo Get(string id)
        {
            return id == null ? null : Dic.ContainsKey(id) ? Dic[id] : null;
        }
        
        public static List<string> GetMyStonesBySkillID(string skillID)
        {
            var infoModels = new List<string>();
            foreach (var kv in Dic)
            {
                if (kv.Value.SkillId == skillID)
                {
                    infoModels.Add(kv.Value.InstanceId);
                }
            }
            return infoModels;
        }

        public static bool StoneCanLevelUp(string instanceID)
        {
            var stoneInfo = Stones.Get(instanceID);
            if (stoneInfo == null)
                return false;

            var form = SSLevelUpManager.DecideForm(stoneInfo.SkillId, stoneInfo.InstanceId);
            return form != null;
        }
        
        public static SKStoneItem GetRenderModel(string itemId)
        {
            return itemId == null ? null : RenderModelDic.ContainsKey(itemId) ? RenderModelDic[itemId] : null;
        }
        
        public static void HighLight(string skillId)
        {
            foreach (var kv in RenderModelDic)
            {
                if (kv.Value._SkillConfig.RECORD_ID == skillId)
                {
                    kv.Value.image.color = Color.white;
                    kv.Value.enabled = true;
                }
                else
                {
                    kv.Value.image.color = new Color(1,1,1,0.5f);
                    kv.Value.enabled = false;
                }
            }
        }

        public static void ResetHighLight()
        {
            foreach (var kv in RenderModelDic)
            {
                kv.Value.image.color = Color.white;
                kv.Value.enabled = true;
            }
        }

        public static void RefreshLocalStoneParams(IDictionary<string, Tuple<string, string>> toEditStones)
        {
            foreach (var kv in toEditStones)
            {
                if (!Dic.ContainsKey(kv.Key) || Dic[kv.Key] == null)
                {
                    Debug.Log("更新对象技能石不存在。stoneOfPlayerID :" + kv.Key);
                    return;
                }
                var stoneOfPlayerInfo = Dic[kv.Key];
                stoneOfPlayerInfo.unitInstanceId = kv.Value.Item1;
                stoneOfPlayerInfo.slot = kv.Value.Item2;
            }
        }
    }
}
