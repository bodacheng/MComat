using System.Collections;
using UnityEngine;
using Api.Dto.Model;
using Api.Common;
using Api.Dto.Form;

namespace dataAccess
{
    public partial class Arena
    {
        public static RankOpponentsModel rankOpponentsModel;
        public static IEnumerator GetOpponents()
        {
            GetRankInfoForm form = new GetRankInfoForm
            {
                playerID = ""
            };
            yield return GetOpponentsE(
                form,
                model => {
                    rankOpponentsModel = model;
                },
                model => {
                    Debug.Log("读取玩家竞技场信息失败："+ form.playerID);
                },
                Setting.Language
            );
            yield return rankOpponentsModel;
        }
        
        public static IEnumerator GetOpponentsE(GetRankInfoForm form, SuccessDelegate<RankOpponentsModel> success, FailDelegate<RankOpponentsModel> fail, ApiLanguage apiLanguage)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.localTestSaveData:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return ApiCaller.Instance.Post<RankOpponentsModel, GetRankInfoForm> 
                    (
                        "http://160.16.187.230/AssetStoreFight/arena/getOpponentsInfo", 
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
            }
        }
    }
}