using System.Collections;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using Skill;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        public static IEnumerator GenerateStoneModelByAccID(string skillStoneOfPlayerId)
        {
            if (MySkillStonesReader.RenderModelDic.ContainsKey(skillStoneOfPlayerId))
            {
                if (MySkillStonesReader.RenderModelDic[skillStoneOfPlayerId] != null)
                {
                    yield break;
                }
            }
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Get(skillStoneOfPlayerId);
            IEnumerator Generate = GenerateStoneMode(skillStoneOfPlayerInfoModel.skillId);
            yield return Generate;
            SKStoneItem item = (SKStoneItem)Generate.Current;
                      
            if (!MySkillStonesReader.RenderModelDic.ContainsKey(skillStoneOfPlayerId))
                MySkillStonesReader.RenderModelDic.Add(skillStoneOfPlayerId, item);
            else
                 MySkillStonesReader.RenderModelDic[skillStoneOfPlayerId] = item;
            
            item.Inherent = skillStoneOfPlayerInfoModel.Inherent == "true";
            item._SkillConfig = SkillConfigTable.GetSkillConfigByID(MySkillStonesReader.Dic[skillStoneOfPlayerId].skillId);
            item.gameObject.name = "stone_" + item._SkillConfig.TYPE + "_" + item._SkillConfig.REAL_NAME;
            item.SkillStoneOfPlayerId = skillStoneOfPlayerId;
            item.gameObject.transform.SetParent(_stonesTempContainer);
        }
        
        public static IEnumerator GenerateStoneMode(string skillID)
        {
            if (skillID == null)
            {
                yield return null;
                yield break;
            }
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillID);
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
            yield return item;
        }
    }
}