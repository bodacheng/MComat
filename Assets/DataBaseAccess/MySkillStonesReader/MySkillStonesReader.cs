using Api.Dto.Model;
using System.Collections.Generic;
using UnityEngine;
using Skill;
using System;

namespace dataAccess
{
    public partial class MySkillStones
    {
        public static IDictionary<string, StoneOfPlayerInfo> Dic = new Dictionary<string, StoneOfPlayerInfo>();
        static IDictionary<string, SKStoneItem> RenderModelDic = new Dictionary<string, SKStoneItem>();

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

        public static void RefreshLocalStoneParams(IDictionary<string, Tuple<string, string>> ToEditStones)
        {
            foreach (KeyValuePair<string, Tuple<string, string>> kv in ToEditStones)
            {
                if (!Dic.ContainsKey(kv.Key) || Dic[kv.Key] == null)
                {
                    Debug.Log("更新对象技能石不存在。stoneOfPlayerID :" + kv.Key);
                    return;
                }
                StoneOfPlayerInfo ofPlayerInfo = MySkillStones.Dic[kv.Key];
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
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(pair.Value.skillId);
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