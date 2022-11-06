using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

/// <summary>
/// 这一部分我们的目标是不用它。link设备的流程应该靠playfab那边的自动化
/// </summary>
public partial class PlayFabReadClient
{
    public static void LinkDevice(Action success, Action<PlayFabError> fail)
    {
#if UNITY_IOS
        PlayFabClientAPI.LinkIOSDeviceID(
            new LinkIOSDeviceIDRequest
            {
                DeviceId = CustomId,
                ForceLink = true
            },
            (x) =>
            {
                Debug.Log(x);
                PlayerAccountInfo.Me.currentLinkedDeviceId = CustomId;
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log(x);
                fail.Invoke(x);
            }
        );
#endif
        
#if UNITY_ANDROID
        PlayFabClientAPI.LinkAndroidDeviceID(
            new LinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = CustomId,
                ForceLink = true
            },
            (x) =>
            {
                Debug.Log(x);
                PlayerAccountInfo.Me.currentLinkedDeviceId = CustomId;
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log(x);
                fail.Invoke(x);
            }
        );
#endif
    }

    static void UnLinkDevice(string unlinkDeviceId ,Action success, Action fail = null)
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
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log(x);
                fail?.Invoke();
            }
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
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log(x);
                fail?.Invoke();
            }
        );
#endif
    }
}
