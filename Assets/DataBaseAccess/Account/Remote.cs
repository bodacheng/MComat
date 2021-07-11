using UnityEngine;
using mainMenu;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
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

        static void GetUserReadOnlyDataRemote(Action<int> finished)
        {
            PlayFabClientAPI.GetUserReadOnlyData
            (
                new GetUserDataRequest()
                {
                    PlayFabId = _AccInfo.playerID,
                    Keys = new List<string>() { "ArcadeProcess", "StoneBoxSize" }
                },
                (GetUserDataResult obj) => {
                    _AccInfo.ArcadeProcess = int.Parse(obj.Data["ArcadeProcess"].Value);
                    _AccInfo.Stoneboxsize = int.Parse(obj.Data["StoneBoxSize"].Value);
                    Debug.Log("读取的盒子容量是："+ _AccInfo.Stoneboxsize);
                    PreScene.target._SkillStonesBox_NineSlot.GenerateCells();
                    PreScene.target._SkillStonesBox_Show.GenerateCells();
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