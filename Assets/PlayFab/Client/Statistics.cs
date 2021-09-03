using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class PlayFabReadClient
{
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
        foreach (StatisticValue value in result.Statistics)
        {
            switch (value.StatisticName)
            {
                default:
                    break;
            }
        }
    }
}
