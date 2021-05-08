using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using Skill;

namespace dataAccess
{
    public partial class MySkillStones
    {
        static IDictionary<string, StoneOfPlayerInfoModel> Dic = new Dictionary<string, StoneOfPlayerInfoModel>();
        static IDictionary<string, SKStoneItem> RenderModelDic = new Dictionary<string, SKStoneItem>();

        public static void Clear()
        {
            Dic.Clear();
            RenderModelDic.Clear();
        }
        
        public static StoneOfPlayerInfoModel Get(string id)
        {
            return id == null ? null : Dic.ContainsKey(id) ? Dic[id] : null;
        }
        
        public static List<string> GetMyStonesBySkillID(string skillID)
        {
            List<string> infoModels = new List<string>();
            foreach (KeyValuePair<string, StoneOfPlayerInfoModel> keyValuePair in Dic)
            {
                if (keyValuePair.Value.skillId == skillID)
                {
                    infoModels.Add(keyValuePair.Value.skillStoneOfPlayerId);
                }
            }
            return infoModels;
        }

        public static SKStoneItem GetRenderModel(string ItemId)
        {
            return ItemId == null ? null : RenderModelDic.ContainsKey(ItemId) ? RenderModelDic[ItemId] : null;
        }
        
        public static void LoadTutorial()
        {
            Dic.Clear();
            //LoadAll_Json(Application.persistentDataPath + "/TutorialStones");
            // 上面的步骤已经完成了Dic的适配
            RenderModelDic.Clear();
            foreach (KeyValuePair<string, StoneOfPlayerInfoModel> pair in Dic)
            {
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(pair.Value.skillId);
                if (_SkillConfig == null)
                {
                    Debug.Log("巨大问题,技能id似乎未定义：" + pair.Value.skillId);
                    return;
                }
                GenerateStoneModelByAccID(pair.Value.skillStoneOfPlayerId);
            }
        }
    }
}