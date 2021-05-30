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
    public partial class Account
    {
        static void GetUserDataRemote(Action<int> finished)
        {
            PlayFabClientAPI.GetUserData
            (
                new GetUserDataRequest() {
                    PlayFabId = _AccInfo.playerID,
                    Keys = new List<string>() { "PlayerName" }
                },
                (GetUserDataResult obj) => {
                    //_AccInfo.PlayerName = obj.Data["PlayerName"].Value;
                    finished.Invoke(1);
                },
                errorCallback => {
                    Debug.Log("Basic accInfo fail:" + errorCallback.ErrorMessage);
                    finished.Invoke(-1);
                }
            );
        }

        static void GetStatisticsRemote(Action<int> finished)
        {
            PlayFabClientAPI.GetPlayerStatistics(
                new GetPlayerStatisticsRequest(),
                (GetPlayerStatisticsResult result) => {
                    OnGetStatistics(result);
                    finished(1);
                },
                error =>
                {
                    Debug.Log(error.GenerateErrorReport());
                    finished.Invoke(-1);
                }
            );
        }

        static void OnGetStatistics(GetPlayerStatisticsResult result)
        {
            foreach(StatisticValue value in result.Statistics)
            {
                switch (value.StatisticName)
                {
                    case "Stoneboxsize":
                        _AccInfo.Stoneboxsize = value.Value;
                        break;
                    case "ArcadeProcess":
                        _AccInfo.ArcadeProcess = value.Value;
                        break;
                    default:
                        break;
                }
            }
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