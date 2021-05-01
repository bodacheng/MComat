using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using Api.Common;
using Api.Dto.Form;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        public static void Load_List(Action<bool> finished)
        {
            AccountCharInfoDic.Clear();
            List<MonsterOfPlayerDetailModel> charList = default;
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    charList = LoadAll_Json(Application.persistentDataPath + "/AccountCharacterInfos");
                    foreach (MonsterOfPlayerDetailModel one in charList)
                    {
                        if (!AccountCharInfoDic.ContainsKey(one.monsterOfPlayerId))
                            AccountCharInfoDic.Add(one.monsterOfPlayerId, one);
                        else
                            Debug.Log("重复的角色存档id：" + one.monsterOfPlayerId);
                    }
                    finished.Invoke(true);
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    PlayFabClientAPI.GetUserData(
                        new GetUserDataRequest() {
                            PlayFabId = AccountSet._AccInfo.PlayerName,
                            Keys = new List<string>() { "charList" }
                        },
                        (GetUserDataResult obj) => {
                            UserDataRecord userDataRecord = obj.Data["charList"];
                            charList = JsonConvert.DeserializeObject<List<MonsterOfPlayerDetailModel>>(userDataRecord.Value);
                            foreach (MonsterOfPlayerDetailModel one in charList)
                            {
                                if (!AccountCharInfoDic.ContainsKey(one.monsterOfPlayerId))
                                    AccountCharInfoDic.Add(one.monsterOfPlayerId, one);
                                else
                                    Debug.Log("重复的角色存档id：" + one.monsterOfPlayerId);
                            }
                            finished.Invoke(true);
                        },
                        errorCallback => {
                            Debug.Log(errorCallback.Error);
                            finished.Invoke(false);
                        });
                    break;
            }
        }
        
        #region 获取单个角色
        /// <summary>
        /// Load the specified monsterlocalid.
        /// </summary>
        /// <returns>The load.</returns>
        /// <param name="monsterlocalid">Monsterlocalid.</param>       
        public static IEnumerator Load(string monsterlocalid)
        {
            GetMonsterOfPlayerDetailForm form = new GetMonsterOfPlayerDetailForm
            {
                monsterOfPlayerId = monsterlocalid
            };
            MonsterOfPlayerDetailModel accountCharInfo = null;
            yield return Load_execution(
                form,
                model => {
                    accountCharInfo = model;
                    DicAdd<string, MonsterOfPlayerDetailModel>.Add(AccountCharInfoDic, monsterlocalid, accountCharInfo);
                },
                model => {
                    Debug.Log("读取角色失败："+ monsterlocalid);
                },
                Setting.Language
            );
            yield return accountCharInfo;
        }
        
        public static IEnumerator Load_execution(GetMonsterOfPlayerDetailForm form, SuccessDelegate<MonsterOfPlayerDetailModel> success, FailDelegate<MonsterOfPlayerDetailModel> fail, ApiLanguage apiLanguage)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    IEnumerator enumerator = LoadAccCharInfoViaJsonFile(form.monsterOfPlayerId);
                    yield return enumerator;
                    if (enumerator.Current != null)
                    {
                        success((MonsterOfPlayerDetailModel)enumerator.Current);
                    }else{
                        fail(null);
                    }
                    break;
                case PlayerInfoRefMode.formalVersion:
                    break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    yield return ApiCaller.Instance.Post<MonsterOfPlayerDetailModel, GetMonsterOfPlayerDetailForm> 
                    (
                        "http://160.16.187.230/AssetStoreFight/team/setMonsterTeamOfPlayer", 
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
        #endregion
    }
}