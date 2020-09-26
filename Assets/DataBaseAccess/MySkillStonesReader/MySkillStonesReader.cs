using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using Skill;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        public static IDictionary<string, SkillStoneOfPlayerInfoModel> Dic = new Dictionary<string, SkillStoneOfPlayerInfoModel>();
        public static IDictionary<string, SKStoneItem> RenderModelDic = new Dictionary<string, SKStoneItem>();
        
        public static SkillStoneOfPlayerInfoModel Get(string id)
        {
            return id == null ? null : Dic.ContainsKey(id) ? Dic[id] : null;
        }
        
        public static SKStoneItem GetRenderModel(string localStoneid)
        {
            return localStoneid == null ? null : RenderModelDic.ContainsKey(localStoneid) ? RenderModelDic[localStoneid] : null;
        }
        
        public static IEnumerator Add(SkillStoneOfPlayerInfoModel one)
        {
            DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, one.skillStoneOfPlayerId, one);
            yield return Update(one.skillStoneOfPlayerId);
        }

        public static IEnumerator LoadTutorial()
        {
            Dic.Clear();
            //LoadAll_Json(Application.persistentDataPath + "/TutorialStones");
            // 上面的步骤已经完成了Dic的适配
            RenderModelDic.Clear();
            foreach (KeyValuePair<string, SkillStoneOfPlayerInfoModel> pair in Dic)
            {
                SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(pair.Value.skillId);
                if (_SkillConfig == null)
                {
                    Debug.Log("巨大问题,技能id似乎未定义：" + pair.Value.skillId);
                    yield break;
                }
                yield return SkillStonesBox.GenerateStoneModelByAccID(pair.Value.skillStoneOfPlayerId);
            }
            yield break;
        }
    }
}