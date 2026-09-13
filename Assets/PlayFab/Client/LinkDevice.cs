using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public partial class PlayFabReadClient
{
    public static string CurrentDeviceLoginId
    {
        get
        {
#if UNITY_STANDALONE_WIN
            if (SteamManager.Initialized)
                return Steamworks.SteamUser.GetSteamID().ToString();
#endif
            return CustomId;
        }
    }

    public static bool IsCurrentDeviceLinked =>
        !string.IsNullOrEmpty(PlayerAccountInfo.Me.currentLinkedDeviceId) &&
        PlayerAccountInfo.Me.currentLinkedDeviceId == CurrentDeviceLoginId;

    public static void LinkDevice(Action success)
    {
        // Keep the identifier sent to PlayFab and the cached success state identical.
        var deviceId = CurrentDeviceLoginId;
        Action linked = () =>
        {
            PlayerAccountInfo.Me.currentLinkedDeviceId = deviceId;
            success?.Invoke();
        };
#if UNITY_IOS
        PlayFabClientAPI.LinkIOSDeviceID(
            new LinkIOSDeviceIDRequest
            {
                DeviceId = deviceId,
                ForceLink = true
            },
            (x) =>
            {
                Debug.Log(x);
                linked();
            },
            ErrorReport
        );
#endif
        
#if UNITY_ANDROID
        PlayFabClientAPI.LinkAndroidDeviceID(
            new LinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = deviceId,
                ForceLink = true
            },
            (x) =>
            {
                Debug.Log(x);
                linked();
            },
            ErrorReport
        );
#endif
#if UNITY_STANDALONE_WIN
        if (SteamManager.Initialized)
        {
            StartSteamLinkAsync(linked, ErrorReport);
        }
        else
        {
            PlayFabClientAPI.LinkCustomID(
                new LinkCustomIDRequest { CustomId = deviceId, ForceLink = true },
                result => linked(),
                ErrorReport);
        }
#endif
    }

    static void UnLinkDevice(string unlinkDeviceId, Action success = null)
    {
#if UNITY_IOS
        PlayFabClientAPI.UnlinkIOSDeviceID(
            new UnlinkIOSDeviceIDRequest
            {
                DeviceId = unlinkDeviceId
            },
            (x) =>
            {
                Debug.Log(x);
                PlayerAccountInfo.Me.currentLinkedDeviceId = null;
                success?.Invoke();
            },
            ErrorReport
        );
#endif
        
#if UNITY_ANDROID
        PlayFabClientAPI.UnlinkAndroidDeviceID(
            new UnlinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = unlinkDeviceId
            },
            (x) =>
            {
                Debug.Log(x);
                PlayerAccountInfo.Me.currentLinkedDeviceId = null;
                success?.Invoke();
            },
            ErrorReport
        );
#endif

#if UNITY_STANDALONE_WIN
        if (SteamManager.Initialized)
        {
            PlayFabClientAPI.UnlinkSteamAccount(
                new UnlinkSteamAccountRequest(),
                (x) =>
                {
                    Debug.Log(x);
                    PlayerAccountInfo.Me.currentLinkedDeviceId = null;
                    success?.Invoke();
                },
                ErrorReport
            );
        }
        else
        {
            PlayFabClientAPI.UnlinkCustomID(
                new UnlinkCustomIDRequest
                {
                    CustomId = unlinkDeviceId
                },
                (x) =>
                {
                    Debug.Log(x);
                    PlayerAccountInfo.Me.currentLinkedDeviceId = null;
                    success?.Invoke();
                },
                ErrorReport
            );
        }
#endif
    }
    
    static void DeletePlayer()
    {
        return;//暂时放弃
        CloudScript.ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "DeletePlayer",
                GeneratePlayStreamEvent = false, // Optional - Shows this event in PlayStream
            },
            (x) =>
            {
                Debug.Log(x.FunctionResult);
            },
            (x) =>
            {
                Debug.Log(x.Error);
            }
        );
    }
}
