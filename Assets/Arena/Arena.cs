using System.Collections;
using UnityEngine;
using Api.Dto.Model;
using Api.Common;
using Api.Dto.Form;

namespace dataAccess
{
    public partial class Arena
    {
        static PlayerArenaRankInfo myRankInfo;
        public static IEnumerator GetPlayerRankInfo()
        {
            GetRankInfoForm form = new GetRankInfoForm
            {
                playerID = ""
            };
            yield return GetPlayerRankInfoExecution(
                form,
                model => {
                    myRankInfo = model.playerArenaRankInfo;
                },
                model => {
                    Debug.Log("读取玩家竞技场信息失败："+ form.playerID);
                },
                Setting.Language
            );
        }
        
        public static IEnumerator GetPlayerRankInfoExecution(GetRankInfoForm form, SuccessDelegate<GetRankInfoOfPlayerModel> success, FailDelegate<GetRankInfoOfPlayerModel> fail, ApiLanguage apiLanguage)
        {
            switch (Account.ReferenceMode)
            {
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.localTestSaveData:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return ApiCaller.Instance.Post<GetRankInfoOfPlayerModel, GetRankInfoForm> 
                    (
                        "http://160.16.187.230/AssetStoreFight/arena/getPlayerRankInfo", 
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