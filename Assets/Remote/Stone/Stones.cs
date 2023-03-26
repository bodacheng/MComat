using System.Collections.Generic;
using UnityEngine;
using System;

namespace dataAccess
{
    public static partial class Stones
    {
        static readonly IDictionary<string, StoneOfPlayerInfo> Dic = new Dictionary<string, StoneOfPlayerInfo>();
        static readonly IDictionary<string, SKStoneItem> RenderModelDic = new Dictionary<string, SKStoneItem>();
        
        public static void ClearData()
        {
            Dic.Clear();
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
            var targetData = Stones.Get(instanceID);
            if (targetData == null)
                return false;

            if (targetData.Level >= PlayFabSetting._VersionMaxStoneLevel)
            {
                return false;
            }
            
            var infoModels = new List<string>();
            int usingCount = 0;
            foreach (var kv in Dic)
            {
                if (kv.Value.SkillId == targetData.SkillId)
                {
                    var itemData = Stones.Get(kv.Value.InstanceId);
                    if (itemData.UnitInstanceId != null && itemData.InstanceId != instanceID)
                    {
                        usingCount++;
                    }
                        
                    if (itemData.UnitInstanceId == null && kv.Value.InstanceId != targetData.InstanceId)
                        infoModels.Add(kv.Value.InstanceId);
                }
            }
            usingCount = Mathf.Clamp(usingCount, 3, Int32.MaxValue);
            // 队伍最多3个人所以起码保留3个石头，而升级一个石头则
            Debug.Log("盈余技能石数量："+(infoModels.Count - usingCount));
            return infoModels.Count - usingCount >= 4;
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
                stoneOfPlayerInfo.UnitInstanceId = kv.Value.Item1;
                stoneOfPlayerInfo.Slot = kv.Value.Item2;
            }
        }
    }
}