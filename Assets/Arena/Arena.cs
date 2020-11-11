using System.Collections;
using UnityEngine;
using Api.Dto.Model;
using Api.Common;
using Api.Dto.Form;

namespace dataAccess
{
    public partial class Arena
    {
        public static IEnumerator GetPlayerRankInfo()
        {
            GetRankInfoForm form = new GetRankInfoForm
            {
                playerID = ""
            };
            GetRankInfoOfPlayerModel GetRankInfoOfPlayerModel = null;
            yield return GetPlayerRankInfoExecution(
                form,
                model => {
                    GetRankInfoOfPlayerModel = model;
                },
                model => {
                    Debug.Log("读取玩家竞技场信息失败："+ form.playerID);
                },
                Setting.Language
            );
            yield return GetRankInfoOfPlayerModel;
        }
        
        public static IEnumerator GetPlayerRankInfoExecution(GetRankInfoForm form, SuccessDelegate<GetRankInfoOfPlayerModel> success, FailDelegate<GetRankInfoOfPlayerModel> fail, ApiLanguage apiLanguage)
        {
            switch (AccountSet.ReferenceMode)
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

public class playerArenaRankInfo
{
    string playerID;
    int ArenaPoint;
    public int rankNum;
}