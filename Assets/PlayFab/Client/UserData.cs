using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using dataAccess;
using Newtonsoft.Json;

public partial class PlayFabReadClient
{
    public static void UpdateUserData(UpdateUserDataRequest req, Action finished, Action fail = null)
    {
        ProgressLayer.Loading(Translate.Get("Updating"));
        PlayFabClientAPI.UpdateUserData
        (
            req,
            obj => {
                finished.Invoke();
                ProgressLayer.Close();
            },
            errorCallback => {
                fail?.Invoke();
                ProgressLayer.Close();
                ErrorReport(errorCallback);
            }
        );
    }

    public static void LoadTeamSet(string mode, Action<bool> finished)
    {
        var targetModeCode = mode;
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest()
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabId,
                Keys = new List<string>() { targetModeCode }
            },
            obj => {
                if (obj.Data.ContainsKey(targetModeCode))
                {
                    var userData = obj.Data[targetModeCode];
                    switch (mode)
                    {
                        case "arcade":
                            TeamSet.Default = JsonConvert.DeserializeObject<TeamPos>(userData.Value).ToPosKeySet();
                            break;
                        case "arena":
                            TeamSet.Arena3V3 = JsonConvert.DeserializeObject<TeamPos>(userData.Value).ToPosKeySet();
                            break;
                        default:
                            Debug.Log("队伍阵型信息不明");
                            break;
                    }
                }
                else
                {
                    switch (mode)
                    {
                        case "arcade":
                            TeamSet.Default = new PosKeySet();
                            break;
                        case "arena":
                            TeamSet.Arena3V3 = new PosKeySet();
                            break;
                        default:
                            Debug.Log("队伍阵型信息不明");
                            break;
                    }
                }
                finished.Invoke(true);
            },
            errorCallback => {
                finished.Invoke(false);
                ErrorReport(errorCallback);
            }
        );
    }
    
    public static void UpdateUserTitleDisplayName(string displayName, Action<UpdateUserTitleDisplayNameResult> finished, Action<PlayFabError> error)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = displayName
            },
            finished.Invoke,
            (x)=>
            {
                error(x);
                ErrorReport(x);
            });
    }
}
