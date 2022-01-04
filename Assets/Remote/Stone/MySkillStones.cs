using System.Collections.Generic;
using UnityEngine;
using Skill;
using System;

namespace dataAccess
{
    public partial class Stones
    {
        public static readonly IDictionary<string, StoneOfPlayerInfo> Dic = new Dictionary<string, StoneOfPlayerInfo>();
        static readonly IDictionary<string, SKStoneItem> RenderModelDic = new Dictionary<string, SKStoneItem>();
        
        public static void Clear()
        {
            Dic.Clear();
            RenderModelDic.Clear();
        }
        
        public static StoneOfPlayerInfo Get(string id)
        {
            return id == null ? null : Dic.ContainsKey(id) ? Dic[id] : null;
        }
        
        public static List<string> GetMyStonesBySkillID(string skillID)
        {
            List<string> infoModels = new List<string>();
            foreach (KeyValuePair<string, StoneOfPlayerInfo> keyValuePair in Dic)
            {
                if (keyValuePair.Value.skillId == skillID)
                {
                    infoModels.Add(keyValuePair.Value.InstanceId);
                }
            }
            return infoModels;
        }

        public static SKStoneItem GetRenderModel(string ItemId)
        {
            return ItemId == null ? null : RenderModelDic.ContainsKey(ItemId) ? RenderModelDic[ItemId] : null;
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

        public static void RefreshLocalStoneParams(IDictionary<string, Tuple<string, string>> ToEditStones)
        {
            foreach (KeyValuePair<string, Tuple<string, string>> kv in ToEditStones)
            {
                if (!Dic.ContainsKey(kv.Key) || Dic[kv.Key] == null)
                {
                    Debug.Log("更新对象技能石不存在。stoneOfPlayerID :" + kv.Key);
                    return;
                }
                StoneOfPlayerInfo ofPlayerInfo = Stones.Dic[kv.Key];
                ofPlayerInfo.inUsingMonsterOfPlayerId = kv.Value.Item1;
                ofPlayerInfo.inUsingSkillSlot = kv.Value.Item2;
            }
        }

        public static void LoadTutorial()
        {
            Dic.Clear();
            //LoadAll_Json(Application.persistentDataPath + "/TutorialStones");
            // 上面的步骤已经完成了Dic的适配
            RenderModelDic.Clear();
            foreach (KeyValuePair<string, StoneOfPlayerInfo> pair in Dic)
            {
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfig(pair.Value.skillId);
                if (_SkillConfig == null)
                {
                    Debug.Log("巨大问题,技能id似乎未定义：" + pair.Value.skillId);
                    return;
                }
                GenerateStoneModelByAccID(pair.Value.InstanceId);
            }
        }
    }
}