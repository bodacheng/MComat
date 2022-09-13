using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

/// <summary>
/// 这一部分我们的目标是不用它。link设备的流程应该靠playfab那边的自动化
/// </summary>
public partial class PlayFabReadClient
{
    public static void LinkDevice(Action success)
    {
#if UNITY_IOS
        PlayFabClientAPI.LinkIOSDeviceID(
            new LinkIOSDeviceIDRequest
            {
                DeviceId = SystemInfo.deviceUniqueIdentifier
            },
            (x) =>
            {
                Debug.Log(x);
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log(x);
            }
        );
#endif
        
#if UNITY_ANDROID
        PlayFabClientAPI.LinkAndroidDeviceID(
            new LinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = SystemInfo.deviceUniqueIdentifier
            },
            (x) =>
            {
                Debug.Log(x);
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log(x);
            }
        );
#endif
    }

    public static void UnLinkDevice(Action success)
    {
#if UNITY_IOS
        PlayFabClientAPI.UnlinkIOSDeviceID(
            new UnlinkIOSDeviceIDRequest
            {
                DeviceId = SystemInfo.deviceUniqueIdentifier
            },
            (x) =>
            {
                Debug.Log(x);
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log(x);
            }
        );
#endif
        
#if UNITY_ANDROID
        PlayFabClientAPI.UnlinkAndroidDeviceID(
            new UnlinkAndroidDeviceIDRequest
            {
                AndroidDeviceId = SystemInfo.deviceUniqueIdentifier
            },
            (x) =>
            {
                Debug.Log(x);
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log(x);
            }
        );
#endif
    }
}
