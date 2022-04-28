using UnityEngine;
using mainMenu;

namespace dataAccess
{
    public static partial class Stones
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
            item._SkillConfig = SkillConfigTable.GetSkillConfig(Dic[instanceId].skillId);
            item.gameObject.name = "stone_" + item._SkillConfig.TYPE + "_" + item._SkillConfig.REAL_NAME;
            item.instanceId = instanceId;
            item.gameObject.transform.SetParent(PreScene.target.stonesTempContainer);
            DicAdd<string, SKStoneItem>.Add(RenderModelDic, instanceId, item);
        }

        // 生成展示用技能石（额外模型）
        // 有两种模式，1: “账户技能石” 2 ：纯粹展示用技能石
        public static SKStoneItem GenerateStoneModel(string skillID, bool openStoneFeature)
        {
            var skillConfig = SkillConfigTable.GetSkillConfig(skillID);
            if (skillConfig == null)
            {
                return null;
            }
            
            var Icon = SkillIconsDic.Instance.FindSkillIconPrefabByResource(skillID);
            var ob = GameObject.Instantiate(Icon);
            ob.gameObject.name = "skillIcon_" + skillID;
            var item = ob.GetComponent<SKStoneItem>();
            if (item == null)
            {
                item = Icon.AddComponent<SKStoneItem>();
            }
            item._SkillConfig = skillConfig;
            item.enabled = openStoneFeature;
            return item;
        }
    }
}