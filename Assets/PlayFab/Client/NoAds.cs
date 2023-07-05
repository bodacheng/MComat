using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using System.Collections.Generic;

public partial class PlayFabReadClient
{
    private static string noAdsServicePlayerDataKey = "noAds";
    public static void LoadNoAdsState(Action<bool> finished)
    {
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest()
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabId,
                Keys = new List<string>() { noAdsServicePlayerDataKey }
            },
            obj=>
            {
                if (obj.Data.ContainsKey(noAdsServicePlayerDataKey))
                {
                    var userData = obj.Data[noAdsServicePlayerDataKey];
                    Int32.TryParse(userData.Value, out var state);
                    Debug.Log("noAdsServicePlayerDataKey:"+ state);
                    PlayerAccountInfo.Me.noAdsState = state == 1;
                }
                else
                {
                    PlayerAccountInfo.Me.noAdsState = false;
                }
                finished(true);
            },
            y =>
            {
                finished(false);
                Debug.Log(y);
            }
        );
    }
}
