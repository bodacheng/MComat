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
                    Keys = new List<string>() { "PlayerName", "Stoneboxsize", "ArcadeProcess"}
                },
                (GetUserDataResult obj) => {

                    AccountSet._AccInfo.PlayerName = obj.Data["PlayerName"].Value;

                    Dictionary<string, string> NoDatas = new Dictionary<string, string>();

                    int size;
                    if (obj.Data.ContainsKey("Stoneboxsize"))
                    {
                        int.TryParse(obj.Data["Stoneboxsize"].Value, out size);
                        AccountSet._AccInfo.Stoneboxsize = size;
                    }
                    else
                    {
                        NoDatas.Add("Stoneboxsize", "50");
                    }

                    int process;
                    if (obj.Data.ContainsKey("ArcadeProcess"))
                    {
                        int.TryParse(obj.Data["ArcadeProcess"].Value, out process);
                        AccountSet._AccInfo.ArcadeProcess = process;
                    }
                    else
                    {
                        NoDatas.Add("ArcadeProcess", "0");
                    }

                    if (NoDatas.Count > 0)
                    {
                        SetUserData(NoDatas);
                    }

                    finished.Invoke(1);
                },
                errorCallback => {
                    Debug.Log("Basic accInfo fail:" + errorCallback.ErrorMessage);
                    finished.Invoke(-1);
                }
            );
        }

        static void SetUserData(Dictionary<string, string> values)
        {
            PlayFabClientAPI.UpdateUserData(
                new UpdateUserDataRequest{
                    Data = values
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