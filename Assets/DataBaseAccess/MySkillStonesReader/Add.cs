using System.Collections;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using Skill;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        public static IEnumerator Add(SkillStoneOfPlayerInfoModel one)
        {
            DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, one.skillStoneOfPlayerId, one);
            yield return GenerateStoneModelByAccID(one.skillStoneOfPlayerId);
            yield return Update(one.skillStoneOfPlayerId);
        }
        
        public static IEnumerator Read(SkillStoneOfPlayerInfoModel one)
        {
            DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, one.skillStoneOfPlayerId, one);
            yield return GenerateStoneModelByAccID(one.skillStoneOfPlayerId);
        }
        
        /// <summary>
        /// 生成账户用技能石图标，生成的模型会加入统一技能石字典作为备用
        /// </summary>
        /// <param name="skillStoneOfPlayerId">技能石账户id</param>
        public static IEnumerator GenerateStoneModelByAccID(string skillStoneOfPlayerId)
        {
            if (RenderModelDic.ContainsKey(skillStoneOfPlayerId))
            {
                if (RenderModelDic[skillStoneOfPlayerId] != null)
                    yield break;
            }
            SkillStoneOfPlayerInfoModel StoneOfPlayerInfo = Get(skillStoneOfPlayerId);
            IEnumerator Generate = GenerateNewStoneModel(StoneOfPlayerInfo.skillId, true);
            yield return Generate;
            if (Generate.Current == null)
                yield break;
            SKStoneItem item = (SKStoneItem)Generate.Current;
            item.Inherent = StoneOfPlayerInfo.Inherent == "true";
            item._SkillConfig = SkillConfigTable.GetSkillConfigByID(Dic[skillStoneOfPlayerId].skillId);
            item.gameObject.name = "stone_" + item._SkillConfig.TYPE + "_" + item._SkillConfig.REAL_NAME;
            item.SkillStoneOfPlayerId = skillStoneOfPlayerId;
            item.gameObject.transform.SetParent(SkillStonesBox._stonesTempContainer);
            
            DicAdd<string, SKStoneItem>.Add(RenderModelDic, skillStoneOfPlayerId, item);
        }
        
        // 生成展示用技能石（额外模型）
        // 有两种模式，1: “账户技能石” 2 ：纯粹展示用技能石
        public static IEnumerator GenerateNewStoneModel(string skillID, bool openStoneFeature)
        {
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
            if (skillConfig == null)
            {
                yield return null;
                yield break;
            }

            SKStoneItem item;
            IEnumerator process = null;
            switch (ResourceLoadingSetting.IconLoadingMode)
            {
                case ResourceLoadMode.CachAB:
                    process = (SkillIconsDic.Instance.FindSkillIconByCach(skillID));
                    break;
                case ResourceLoadMode.Resource:
                    process = (SkillIconsDic.Instance.FindSkillIconByResource(skillID));
                    break;
                case ResourceLoadMode.StreamingAssetAB:
                    break;
            }
            yield return process;
            if (process.Current == null)
            {
                yield return null;
                yield break;
            }
            GameObject Icon = (GameObject)process.Current;
            GameObject newIcon = Object.Instantiate(Icon);
            item = newIcon.GetComponent<SKStoneItem>();
            if (item == null)
            {
                item = newIcon.AddComponent<SKStoneItem>();
            }
            item._SkillConfig = skillConfig;
            item.enabled = openStoneFeature;
            yield return item;
        }
    }
}