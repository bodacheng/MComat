using System.Collections;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using Skill;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        /// <summary>
        /// 生成账户用技能石图标，生成的模型会加入统一技能石字典作为备用
        /// </summary>
        /// <param name="skillStoneOfPlayerId">技能石账户id</param>
        public static IEnumerator GenerateStoneModelByAccID(string skillStoneOfPlayerId)
        {
            if (MySkillStonesReader.RenderModelDic.ContainsKey(skillStoneOfPlayerId))
            {
                if (MySkillStonesReader.RenderModelDic[skillStoneOfPlayerId] != null)
                {
                    yield break;
                }
            }
            SkillStoneOfPlayerInfoModel StoneOfPlayerInfo = MySkillStonesReader.Get(skillStoneOfPlayerId);
            IEnumerator Generate = GenerateNewStoneModel(StoneOfPlayerInfo.skillId , 1);
            yield return Generate;
            SKStoneItem item = (SKStoneItem)Generate.Current;

            if (!MySkillStonesReader.RenderModelDic.ContainsKey(skillStoneOfPlayerId))
                MySkillStonesReader.RenderModelDic.Add(skillStoneOfPlayerId, item);
            else
                 MySkillStonesReader.RenderModelDic[skillStoneOfPlayerId] = item;
                 
            item.Inherent = StoneOfPlayerInfo.Inherent == "true";
            item._SkillConfig = SkillConfigTable.GetSkillConfigByID(MySkillStonesReader.Dic[skillStoneOfPlayerId].skillId);
            item.gameObject.name = "stone_" + item._SkillConfig.TYPE + "_" + item._SkillConfig.REAL_NAME;
            item.SkillStoneOfPlayerId = skillStoneOfPlayerId;
            item.gameObject.transform.SetParent(_stonesTempContainer);
        }
        
        // 生成展示用技能石（额外模型）
        // 有两种模式，1: “账户技能石” 2 ：纯粹展示用技能石
        public static IEnumerator GenerateNewStoneModel(string skillID, int mode)
        {
            if (skillID == null)
                yield break;
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
            if (skillConfig == null)
            {
                yield return null;
                yield break;
            }
            
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
            yield return (process);
            GameObject Icon = (GameObject)process.Current;
            if (Icon == null)
                Icon = Instantiate(SkillIconsDic.Instance.GetDefaultSkillIconByResource(skillConfig.SP_LEVEL));
            SKStoneItem item = Icon.GetComponent<SKStoneItem>();
            if (item == null)
            {
                item = Icon.AddComponent<SKStoneItem>();
            }
            item._SkillConfig = skillConfig;
            if (mode == 2)
                item.enabled = false;
            yield return item;
        }
    }
}