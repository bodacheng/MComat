using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Model;
using mainMenu;
using Skill;

namespace dataAccess
{
    public partial class MySkillStonesReader
    {
        public static IEnumerator LoadAMySkillstones(ApiLanguage apiLanguage)
        {
            List<SkillStoneOfPlayerInfoModel> infos = new List<SkillStoneOfPlayerInfoModel>();
            GetSkillStoneOfPlayerInfoForm form = new GetSkillStoneOfPlayerInfoForm
            {
            };
            yield return Load(
                form,
                model => {
                    infos = model.skillStoneOfPlayerInfoList;
                    //Debug.Log("拥有技能石情报成功,玩家拥有以下技能石：");
                    //foreach (SkillStoneOfPlayerInfoModel SkillStoneOfPlayerInfoModel in infos)
                    //{
                    //    Debug.Log("skillStoneOfPlayerId:" + SkillStoneOfPlayerInfoModel.skillStoneOfPlayerId + ",skillId:" + SkillStoneOfPlayerInfoModel.skillId);
                    //}
                    foreach (SkillStoneOfPlayerInfoModel one in infos)
                    {
                        DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, one.skillStoneOfPlayerId, one);
                    }
                    IEnumerator GenerateStoneModel()
                    {
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
                    }
                    SingleThreadProcesser.backup.Run(GenerateStoneModel());
                },
                model => {
                    Dic.Clear();
                    RenderModelDic.Clear();
                },
                apiLanguage
            );
        }
        
        static IEnumerator Load(GetSkillStoneOfPlayerInfoForm form, SuccessDelegate<GetSkillStoneOfPlayerInfoModel> success, FailDelegate<GetSkillStoneOfPlayerInfoModel> fail, ApiLanguage apiLanguage)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    yield return LoadAll_Json(Application.persistentDataPath + "/MyStones",
                        form,
                        ApiCaller.Instance.getHeader(apiLanguage), 
                        model => {
                            success(model);
                        },
                        model => {
                            fail(model);
                        }
                    );
                break;
            case PlayerInfoRefMode.remoteTestPlayer:
                yield return ApiCaller.Instance.Post<GetSkillStoneOfPlayerInfoModel, GetSkillStoneOfPlayerInfoForm>
                    (   "http://160.16.187.230/AssetStoreFight/skillStone/getSkillStoneOfPlayerInfo",
                        form, 
                        ApiCaller.Instance.getHeader(apiLanguage), 
                        model => {
                            success(model.data);
                        },
                        model => {
                            fail(model.data);
                        }
                    );
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
            }
        }
        
        static IEnumerator SkillStoneGotcha(string gotchaPolicyKey, ApiLanguage apiLanguage)
        {
            List<SkillStoneGotchaInfoModel> infos;
            SkillStoneGotchaForm form = new SkillStoneGotchaForm
            {
                gotchaPolicyKey = gotchaPolicyKey
            };

            yield return ApiCaller.Instance.Post<SkillStoneGotchaModel, SkillStoneGotchaForm>
            ("http://160.16.187.230/AssetStoreFight/skillStone/skillStoneGotcha", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model =>
                 {
                     infos = model.data.skillStoneGotchaInfoList;
                     Debug.Log("以下是gotcha到的技能石");
                     foreach (SkillStoneGotchaInfoModel _SkillStoneGotchaInfoModel in infos)
                     {
                         Debug.Log("skillId:" + _SkillStoneGotchaInfoModel.skillId + ",rare:" + _SkillStoneGotchaInfoModel.rarityLevel);
                     }
                 }
                ,
                 model =>
                 {

                 }
            );
            yield break;
        }
        
        public IEnumerator LevelUpMySkillStone_Remote(string skillstoneid, string targetLevel, ApiLanguage apiLanguage)
        {
            yield break;
        }
    }
}