using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public partial class PlayFabReadClient
{
    static void LinkDevice()
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
                    },
                    (x) =>
                    {
                        Debug.Log(x);
                    }
                );
#endif
    }
}
