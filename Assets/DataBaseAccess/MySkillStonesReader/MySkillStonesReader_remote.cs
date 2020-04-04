using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using mainMenu;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Model;
using Api.Dto.Model.Common;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        private IEnumerator LoadMySkillstonesRemote(ApiLanguage apiLanguage) {
            List<SkillStoneOfPlayerInfoModel> infos = new List<SkillStoneOfPlayerInfoModel>();
            GetSkillStoneOfPlayerInfoForm form = new GetSkillStoneOfPlayerInfoForm
            {
                sessionId = AccountSet.Instance.sessionId
            };
            yield return ApiCaller.Instance.Post<BaseModel<GetSkillStoneOfPlayerInfoModel>, GetSkillStoneOfPlayerInfoForm> 
            (   
                "http://160.16.187.230/AssetStoreFight/skillStone/getSkillStoneOfPlayerInfo", 
                form, 
                ApiCaller.Instance.getHeader(apiLanguage),
                model => {
                    infos = model.data.skillStoneOfPlayerInfoList;
                    Debug.Log("拥有技能石情报成功,玩家拥有以下技能石：");                   
                    foreach (SkillStoneOfPlayerInfoModel SkillStoneOfPlayerInfoModel in infos)
                    {
                        Debug.Log("skillStoneOfPlayerId:"+SkillStoneOfPlayerInfoModel.skillStoneOfPlayerId + ",skillId:"  + SkillStoneOfPlayerInfoModel.skillId);
                    }
                 }
                ,
                 model => {
                    mySkillStonesDataDic.Clear();
                 }
            );
            foreach (SkillStoneOfPlayerInfoModel SkillStoneOfPlayerInfoModel in infos)
            {
                yield return SkillStonesBox.GenerateOneStone(SkillStoneOfPlayerInfoModel);
            }
            yield break;
        }

        public IEnumerator LevelUpMySkillStone(string skillstoneid, string targetLevel, ApiLanguage apiLanguage)
        {
            // 最后可能也是个form提交的格式。。。但这个先放这儿吧。。
            yield return true; // 代表请求成功
        }

        private IEnumerator SkillStoneGotcha(string gotchaPolicyKey, ApiLanguage apiLanguage)
        {
            List<SkillStoneGotchaInfoModel> infos;
            SkillStoneGotchaForm form = new SkillStoneGotchaForm
            {
                sessionId = AccountSet.Instance.sessionId,
                gotchaPolicyKey = gotchaPolicyKey
            };

            yield return ApiCaller.Instance.Post<BaseModel<SkillStoneGotchaModel>, SkillStoneGotchaForm> 
            ("http://160.16.187.230/AssetStoreFight/skillStone/skillStoneGotcha", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model => {
                     infos = model.data.skillStoneGotchaInfoList;
                     Debug.Log("以下是gotcha到的技能石");
                    foreach (SkillStoneGotchaInfoModel _SkillStoneGotchaInfoModel in infos)
                    {
                        Debug.Log("skillId:"+_SkillStoneGotchaInfoModel.skillId + ",rare:"  + _SkillStoneGotchaInfoModel.rarityLevel);
                    }
                 }
                ,
                 model => {
                    
                 }
            );
            yield break;
        }
        
        
    }
}