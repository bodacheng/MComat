using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace dataAccess
{
    public partial class AccountSet
    {
        static void LoadAccInfoRemote(Action<int> finished)
        {
            PlayFabClientAPI.GetUserData
            (
                new GetUserDataRequest() {
                    PlayFabId = AccountSet._AccInfo.playerID,
                    Keys = new List<string>() { "CustomerInfo" }
                },
                (GetUserDataResult obj) => {
                    
                    foreach (var a in obj.Data)
                    {
                        Debug.Log(a);
                    }

                    if (!obj.Data.ContainsKey("CustomerInfo"))
                    {
                        SetUserData();
                    }
                    else
                    {
                        UserDataRecord userDataRecord = obj.Data["CustomerInfo"];
                        _AccInfo = JsonConvert.DeserializeObject<PlayerAccountInfo>(userDataRecord.Value);
                    }
                    finished.Invoke(1);
                },
                errorCallback => {
                    Debug.Log("Basic accInfo fail:" + errorCallback.ErrorMessage);
                    finished.Invoke(-1);
                }
            );
        }

        static void SetUserData()
        {
            PlayFabClientAPI.UpdateUserData(
                new UpdateUserDataRequest{
                    Data = new Dictionary<string, string>
                    {
                        { "PlayerName",  AccountSet._AccInfo.PlayerName },
                        { "coinCount", AccountSet._AccInfo.coinCount.ToString() },
                        { "diamondCount", AccountSet._AccInfo.diamondCount.ToString() }
                    }
                },
                result =>
                {
                    Debug.Log("账户数据修改成功");
                },
                error =>
                {
                    Debug.Log(error.GenerateErrorReport());
                }
            );
        }
    }
}