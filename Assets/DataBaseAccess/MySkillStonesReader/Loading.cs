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
        static void ConvertToDic(List<SkillStoneOfPlayerInfoModel> list)
        {
            foreach (SkillStoneOfPlayerInfoModel stoneinfo in list)
            {
                Read(stoneinfo);
            }
        }

        public static void LoadAMySkillstones(Action<bool> finished)
        {
            Dic.Clear();
            RenderModelDic.Clear();
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    List<SkillStoneOfPlayerInfoModel> list = LoadAll_Json(Application.persistentDataPath + "/MyStones");
                    ConvertToDic(list);
                    finished(true);
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
                            List<SkillStoneOfPlayerInfoModel> info = JsonConvert.DeserializeObject<List<SkillStoneOfPlayerInfoModel>>(userDataRecord.Value);
                            ConvertToDic(info);
                            finished(true);
                        },
                        errorCallback => {
                            Debug.Log(errorCallback.Error);
                            finished(false);
                        });
                break;
            case PlayerInfoRefMode.formalVersion:
                break;
            }
        }
        
        public IEnumerator LevelUpMySkillStone_Remote(string skillstoneid, string targetLevel, ApiLanguage apiLanguage)
        {
            yield break;
        }
    }
}