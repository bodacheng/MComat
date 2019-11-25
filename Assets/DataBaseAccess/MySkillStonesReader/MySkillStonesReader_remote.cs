using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LitJson;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Form.Common;
using Api.Dto.Model;
using Api.Dto.Model.Common;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        private IEnumerator loadMySkillstonesRemote(ApiLanguage apiLanguage) {
            List<SkillStoneOfPlayerInfoModel> infos;
            GetSkillStoneOfPlayerInfoForm form = new GetSkillStoneOfPlayerInfoForm();
            form.sessionId = AccountSet.Instance.sessionId;
            yield return ApiCaller.Instance.Post<BaseModel<GetSkillStoneOfPlayerInfoModel>, GetSkillStoneOfPlayerInfoForm> 
            ("http://160.16.187.230/AssetStoreFight/skillStone/getSkillStoneOfPlayerInfo", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model => {
                     infos = model.data.skillStoneOfPlayerInfoList;
                     Debug.Log("拥有技能石情报成功,玩家拥有以下技能石：");
                    foreach (SkillStoneOfPlayerInfoModel SkillStoneOfPlayerInfoModel in infos)
                    {
                        Debug.Log("skillStoneOfPlayerId:"+SkillStoneOfPlayerInfoModel.skillStoneOfPlayerId + ",skillId:"  + SkillStoneOfPlayerInfoModel.skillId);
                    }
                    Debug.Log("以上是查找到的玩家拥有的技能石");
                    mySkillStonesDataDic = ConvertSKillStoneNumListToDic(infos.ToList());
                 }
                ,
                 model => {
                    mySkillStonesDataDic.Clear();
                 }
            );
            yield break;
        }
        
        private IEnumerator skillStoneGotcha(string gotchaPolicyKey, ApiLanguage apiLanguage)
        {
            List<SkillStoneGotchaInfoModel> infos;
            SkillStoneGotchaForm form = new SkillStoneGotchaForm();
            form.sessionId = AccountSet.Instance.sessionId;
            form.gotchaPolicyKey = gotchaPolicyKey;
            
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