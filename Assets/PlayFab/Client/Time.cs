using System;
using PlayFab;
using UnityEngine;

public partial class PlayFabReadClient
{
    public static void GetServerTime(Action<DateTime> success, Action fail)
    {
        PlayFabClientAPI.GetTime(
            new PlayFab.ClientModels.GetTimeRequest(), 
            (x) =>
            {
                success(x.Time);
            },
            (x) =>
            {
                Debug.Log(x.ErrorMessage);
                fail.Invoke();
            }
        );
    }
}
