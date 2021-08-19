using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using mainMenu;

namespace dataAccess
{
    public partial class Account
    {
        public static PlayerAccountInfo _AccInfo;//本单例模式的处理对象,一个参照数据库来定值的变量。
        
        public static void GetUserData(Action<int> finished)
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
        
        public static void GetUserReadOnlyData(Action<int> finished)
        {
            PlayFabClientAPI.GetUserReadOnlyData
            (
                new GetUserDataRequest()
                {
                    PlayFabId = _AccInfo.playerID,
                    Keys = new List<string>() { "lastLevelCompleted", "StoneBoxSize" }
                },
                (GetUserDataResult obj) => {
                    if (obj.Data.ContainsKey("lastLevelCompleted"))
                    {
                        _AccInfo.ArcadeProcess = int.Parse(obj.Data["lastLevelCompleted"].Value);
                    }
                    else
                    {
                        _AccInfo.ArcadeProcess = 0;
                    }

                    if (obj.Data.ContainsKey("StoneBoxSize"))
                    {
                        _AccInfo.Stoneboxsize = int.Parse(obj.Data["StoneBoxSize"].Value);
                    }
                    else
                    {
                        _AccInfo.Stoneboxsize = 50;
                        Debug.Log("玩家数据出错 boxsize");
                    }
                    
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

        public static void GetStatistics(Action<int> finished)
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
    }
}