using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Api.Dto.Model;
using Skill;
using Newtonsoft.Json;
using System;

namespace dataAccess
{
    public partial class MySkillStones
    {
        static void Read(SkillStoneOfPlayerInfoModel one)
        {
            DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, one.skillStoneOfPlayerId, one);
            GenerateStoneModelByAccID(one.skillStoneOfPlayerId);
        }

        static void ConvertListToDic(List<SkillStoneOfPlayerInfoModel> list)
        {
            foreach (SkillStoneOfPlayerInfoModel stoneinfo in list)
            {
                Read(stoneinfo);
            }
        }

        public static void LoadAMySkillstones(Action<int> finished)
        {
            Dic.Clear();
            RenderModelDic.Clear();

            List<SkillStoneOfPlayerInfoModel> list;
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    list = LoadAll_Json(Application.persistentDataPath + "/MyStones");
                    ConvertListToDic(list);
                    finished(1);
                break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    PlayFabClientAPI.GetUserData(
                        new GetUserDataRequest()
                        {
                            PlayFabId = AccountSet._AccInfo.PlayerName,
                            Keys = new List<string>() { "stoneList" }
                        },
                        (GetUserDataResult obj) => {
                            UserDataRecord userDataRecord = obj.Data["stoneList"];
                            list = JsonConvert.DeserializeObject<List<SkillStoneOfPlayerInfoModel>>(userDataRecord.Value);
                            ConvertListToDic(list);
                            finished(1);
                        },
                        errorCallback => {
                            Debug.Log(errorCallback.Error);
                            finished(-1);
                        });
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
            }
        }
    }
}