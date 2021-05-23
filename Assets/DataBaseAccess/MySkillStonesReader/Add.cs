using System.Collections;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using Skill;

namespace dataAccess
{
    public partial class MySkillStones
    {
        public static void Add(StoneOfPlayerInfo one)
        {
            DicAdd<string, StoneOfPlayerInfo>.Add(Dic, one.InstanceId, one);
            GenerateStoneModelByAccID(one.InstanceId);
        }

        /// <summary>
        /// 生成账户用技能石图标，生成的模型会加入统一技能石字典作为备用
        /// </summary>
        /// <param name="skillStoneOfPlayerId">技能石账户id</param>
        public static void GenerateStoneModelByAccID(string skillStoneOfPlayerId)
        {
            if (RenderModelDic.ContainsKey(skillStoneOfPlayerId))
            {
                if (RenderModelDic[skillStoneOfPlayerId] != null)
                    return;
            }
            StoneOfPlayerInfo StoneOfPlayerInfo = Get(skillStoneOfPlayerId);
            SKStoneItem item = GenerateNewStoneModel_Resource(StoneOfPlayerInfo.skillId, true);
            item.Inherent = StoneOfPlayerInfo.Inherent == "true";
            item._SkillConfig = SkillConfigTable.GetSkillConfigByID(Dic[skillStoneOfPlayerId].skillId);
            item.gameObject.name = "stone_" + item._SkillConfig.TYPE + "_" + item._SkillConfig.REAL_NAME;
            item.equipingId = skillStoneOfPlayerId;
            item.gameObject.transform.SetParent(SkillStonesBox._stonesTempContainer);

            DicAdd<string, SKStoneItem>.Add(RenderModelDic, skillStoneOfPlayerId, item);
        }

        public static SKStoneItem GenerateNewStoneModel_Memory(string skillID, bool openStoneFeature)
        {
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
            if (skillConfig == null)
            {
                return null;
            }

            GameObject pretab = SkillIconsDic.Instance.Get(skillID);
            GameObject newIcon = Object.Instantiate(pretab);
            SKStoneItem item = newIcon.GetComponent<SKStoneItem>();
            if (item == null)
            {
                item = newIcon.AddComponent<SKStoneItem>();
            }
            item._SkillConfig = skillConfig;
            item.enabled = openStoneFeature;
            return item;
        }

        // 生成展示用技能石（额外模型）
        // 有两种模式，1: “账户技能石” 2 ：纯粹展示用技能石
        public static SKStoneItem GenerateNewStoneModel_Resource(string skillID, bool openStoneFeature)
        {
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
            if (skillConfig == null)
            {
                return null;
            }

            SKStoneItem item;
            GameObject Icon = SkillIconsDic.Instance.FindSkillIconByResource_P(skillID);
            GameObject newIcon = Object.Instantiate(Icon);
            item = newIcon.GetComponent<SKStoneItem>();
            if (item == null)
            {
                item = newIcon.AddComponent<SKStoneItem>();
            }
            item._SkillConfig = skillConfig;
            item.enabled = openStoneFeature;
            return item;
        }
    }
}