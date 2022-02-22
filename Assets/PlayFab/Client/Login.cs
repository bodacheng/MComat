using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class PlayFabReadClient
{
    public static void PlayFabLogin(string userName, string pw,
        Action<LoginResult> success, Action<PlayFabError> fail)
    {
        PlayFabClientAPI.LoginWithPlayFab(
            new LoginWithPlayFabRequest
            {
                Username = userName,
                Password = pw,
                TitleId = PlayFabSettings.TitleId
            },
            (x)=>
            {
                LinkDevice();
                success.Invoke(x);
            },
            (x)=>
            {
                fail.Invoke(x);
            });
    }
    
    public static void LoginByDevice(Action<LoginResult> success, Action<PlayFabError> fail)
    {
#if UNITY_IOS
            PlayFabClientAPI.LoginWithIOSDeviceID(
                new LoginWithIOSDeviceIDRequest
                {
                    DeviceId = SystemInfo.deviceUniqueIdentifier,
                    CreateAccount = true
                },
                (x) =>
                {
                    Debug.Log(x);
                    PlayerAccountInfo.Me = new PlayerAccountInfo
                    {
                        PlayFabUsername = x.PlayFabId
                    };
                    AddUserNameAndPw(x.PlayFabId);
                    success.Invoke(x);
                },
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
                (x) =>
                {
                    AddUserNameAndPw(x.PlayFabId);
                    success.Invoke(x);
                },
                fail
            );
#endif
    }
}
