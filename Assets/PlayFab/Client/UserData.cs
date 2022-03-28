using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using dataAccess;
using Newtonsoft.Json;

public partial class PlayFabReadClient
{
    public static void GetUserData(GetUserDataRequest req, Action<bool> finished)
    {
        PlayFabClientAPI.GetUserData
        (
            req,
            (GetUserDataResult obj) => {
                //_AccInfo.PlayerName = obj.Data["PlayerName"].Value;
                finished.Invoke(true);
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
            (UpdateUserDataResult obj) => {
                finished.Invoke(true);
            },
            errorCallback => {
                Debug.Log("fail:" + errorCallback.ErrorMessage);
                finished.Invoke(false);
            }
        );
    }

    public static void LoadTeamSet(string Mode, Action<bool> finished)
    {
        string targetModeCode = Mode;
        PlayFabClientAPI.GetUserData(
            new GetUserDataRequest()
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabUsername,
                Keys = new List<string>() { targetModeCode }
            },
            (GetUserDataResult obj) => {
                if (obj.Data.ContainsKey(targetModeCode))
                {
                    UserDataRecord userData = obj.Data[targetModeCode];
                    switch (Mode)
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
                    switch (Mode)
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
                PlayFabId = PlayerAccountInfo.Me.PlayFabUsername,
                Keys = new List<string>() { "last_Level_completed", "stone_box_size" }
            },
            (GetUserDataResult obj) => {
                if (obj.Data.ContainsKey("last_Level_completed"))
                {
                    PlayerAccountInfo.Me.ArcadeProcess = int.Parse(obj.Data["last_Level_completed"].Value);
                }
                else
                {
                    PlayerAccountInfo.Me.ArcadeProcess = 0;
                }

                if (obj.Data.ContainsKey("stone_box_size"))
                {
                    PlayerAccountInfo.Me.StoneBoxSize = int.Parse(obj.Data["stone_box_size"].Value);
                }
                else
                {
                    PlayerAccountInfo.Me.StoneBoxSize = 50;
                    Debug.Log("玩家数据出错 boxsize");
                }
                finished.Invoke(true);
            },
            errorCallback => {
                Debug.Log("Basic accInfo fail:" + errorCallback.ErrorMessage);
                finished.Invoke(false);
            }
        );
    }
}
