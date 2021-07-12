using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using Skill;

namespace dataAccess
{
    public partial class Stones
    {
        public static void Add(StoneOfPlayerInfo one)
        {
            DicAdd<string, StoneOfPlayerInfo>.Add(Dic, one.InstanceId, one);
            GenerateStoneModelByAccID(one.InstanceId);
        }

        /// <summary>
        /// 生成账户用技能石图标，生成的模型会加入统一技能石字典作为备用
        /// </summary>
        /// <param name="instanceId">技能石账户id</param>
        public static void GenerateStoneModelByAccID(string instanceId)
        {
            if (RenderModelDic.ContainsKey(instanceId))
            {
                if (RenderModelDic[instanceId] != null)
                    return;
            }
            StoneOfPlayerInfo info = Get(instanceId);
            SKStoneItem item = GenerateStoneModel(info.skillId, true);
            item.Inherent = info.Inherent == "true";
            item._SkillConfig = SkillConfigTable.GetSkillConfigByID(Dic[instanceId].skillId);
            item.gameObject.name = "stone_" + item._SkillConfig.TYPE + "_" + item._SkillConfig.REAL_NAME;
            item.instanceId = instanceId;
            item.gameObject.transform.SetParent(SkillStonesBox._stonesTempContainer);

            DicAdd<string, SKStoneItem>.Add(RenderModelDic, instanceId, item);
        }

        // 生成展示用技能石（额外模型）
        // 有两种模式，1: “账户技能石” 2 ：纯粹展示用技能石
        public static SKStoneItem GenerateStoneModel(string skillID, bool openStoneFeature)
        {
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
            if (skillConfig == null)
            {
                return null;
            }
            SKStoneItem item;
            GameObject pretab = SkillIconsDic.Instance.Get(skillID);
            if (pretab == null)
            {
                pretab = SkillIconsDic.Instance.FindSkillIconByResource_P(skillID);
            }
            GameObject newIcon = Object.Instantiate(pretab);
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