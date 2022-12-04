using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using dataAccess;
using Newtonsoft.Json;

public partial class PlayFabReadClient
{
    public static void GetUserData(GetUserDataRequest req, Action<bool> finished) // 目前没用？？
    {
        PlayFabClientAPI.GetUserData
        (
            req,
            (GetUserDataResult obj) =>
            {
                finished.Invoke(true);
                Debug.Log("bodacheng");
                Debug.Log(obj.Data);
                
                if (obj.Data.ContainsKey("TutorialProgress"))
                {
                    PlayerAccountInfo.Me.tutorialProgress = obj.Data["TutorialProgress"].Value;
                }
                else
                {
                    UpdateUserData(
                        new UpdateUserDataRequest()
                        {
                            Data = new Dictionary<string, string>
                            {
                                { "TutorialProgress", "Started" }
                            }
                        },
                        (x) =>
                        {
                            
                        }
                    );
                }
                
            },
            errorCallback => {
                Debug.Log("Basic accInfo fail:" + errorCallback.ErrorMessage);
                finished.Invoke(false);
            }
        );
    }

    public static void UpdateUserData(UpdateUserDataRequest req, Action<bool> finished)
    {
        PlayFabClientAPI.UpdateUserData
        (
            req,
            obj => {
                finished.Invoke(true);
            },
            errorCallback => {
                Debug.Log("fail:" + errorCallback.ErrorMessage);
                finished.Invoke(false);
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
                Debug.Log(errorCallback);
                finished.Invoke(false);
            }
        );
    }

    public static void GetUserReadOnlyData(Action<bool> finished)
    {
        PlayFabClientAPI.GetUserReadOnlyData
        (
            new GetUserDataRequest()
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabId,
                Keys = new List<string>() { "arcadeProcess", "stone_box_size", "arenaCountToday" }
            },
            (obj) => {
                if (obj.Data.ContainsKey("arcadeProcess"))
                {
                    PlayerAccountInfo.Me.arcadeProcess = int.Parse(obj.Data["arcadeProcess"].Value);
                }
                else
                {
                    PlayerAccountInfo.Me.arcadeProcess = 0;
                }
                
                PlayerAccountInfo.Me.arenaCountToday = 
                    obj.Data.ContainsKey("arenaCountToday") ? int.Parse(obj.Data["arenaCountToday"].Value) : 0;
                
                finished.Invoke(true);
            },
            errorCallback => {
                Debug.Log("Basic accInfo fail:" + errorCallback.ErrorMessage);
                finished.Invoke(false);
            }
        );
    }

    public static void UpdateUserTitleDisplayName(string DisplayName, Action<UpdateUserTitleDisplayNameResult> finished, Action<PlayFabError> error)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = DisplayName
            },
            finished.Invoke,
        (x) =>
            {
                Debug.Log(x);
                error(x);
            }
        );
    }
}
