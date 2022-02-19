using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class PlayFabReadClient
{
    public static void CustomIDLogin(Action<LoginResult> success, Action<PlayFabError> fail)
    {
        PlayerAccountInfo.Load();
        if (PlayerAccountInfo.Me != null)
        {
            PlayFabClientAPI.LoginWithCustomID(
                new LoginWithCustomIDRequest
                {
                    CustomId = PlayerAccountInfo.Me.playerID,
                    CreateAccount = false
                },
                success,
                fail
            );
        }
        else // 本地无信息
        {
            #if UNITY_IOS
            PlayFabClientAPI.LoginWithIOSDeviceID(
                new LoginWithIOSDeviceIDRequest
                {
                    DeviceId = SystemInfo.deviceUniqueIdentifier,
                    CreateAccount = true
                },
                success,
                fail
            );
            #endif
            
            #if UNITY_ANDROID
            PlayFabClientAPI.LoginWithAndroidDeviceID(
              new LoginWithAndroidDeviceIDRequest
              {
                  AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
                  CreateAccount = true
              },
              success,
              fail
            );
            #endif
        }
    }
}
