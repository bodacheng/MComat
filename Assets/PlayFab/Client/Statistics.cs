using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class PlayFabReadClient
{
    public static void GetStatistics(Action<bool> finished)
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (GetPlayerStatisticsResult result) => {
                OnGetStatistics(result);
                finished(true);
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
                finished.Invoke(false);
            }
        );
    }

    static void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        foreach (StatisticValue value in result.Statistics)
        {
            if (value.StatisticName == PlayfabSetting._arenaPointCode)
            {
                PlayerAccountInfo.Me.arenaPoint = value.Value;
            }

            if (value.StatisticName == "stageProgress")
            {
                PlayerAccountInfo.Me.arcadeProcess = value.Value;
            }
        }
    }
}
