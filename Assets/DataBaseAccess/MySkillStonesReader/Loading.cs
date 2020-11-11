using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Model;
using Skill;
using mainMenu;

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
                    int i = 1;
                    foreach (SkillStoneOfPlayerInfoModel one in infos)
                    {
                        SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(one.skillId);
                        if (_SkillConfig == null)
                        {
                            Debug.Log("巨大问题,技能id似乎未定义：" + one.skillId);
                            LoadingCanvas.target.TurnOnProcessDescription(false);
                            continue;
                        }
                        IEnumerator readOne(int a)
                        {
                            if (a == 1)
                                LoadingCanvas.target.TurnOnProcessDescription(true);
                            LoadingCanvas.target.NowProcess("正在构成技能石模型", (float) a / infos.Count);
                            yield return Read(one);
                            if (a == infos.Count)
                            {
                                LoadingCanvas.target.TurnOnProcessDescription(false);
                            }
                        }
                        PreScene.target.mainProcessRunner.Run(readOne(i));
                        i++;
                    }
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

            yield return ApiCaller.Instance.Post<GetSkillStoneGotchaModel, SkillStoneGotchaForm>
            ("http://160.16.187.230/AssetStoreFight/skillStone/skillStoneGotcha", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model =>
                 {
                     infos = model.data.skillStoneGotchaInfoList;
                     Debug.Log("以下是gotcha到的技能石");
                     foreach (SkillStoneGotchaInfoModel _SkillStoneGotchaInfoModel in infos)
                     {
                         Debug.Log("skillId:" + _SkillStoneGotchaInfoModel.skillId);
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