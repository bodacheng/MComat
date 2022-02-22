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
                TitleId = "MY GAME"
            },
            success,
            fail
        );
    }
    
    public static void CustomIDLogin(Action<LoginResult> success, Action<PlayFabError> fail)
    {
        PlayerAccountInfo.Load();
        if (PlayerAccountInfo.Me != null)
        {
            PlayFabClientAPI.LoginWithCustomID(
                new LoginWithCustomIDRequest
                {
                    CustomId = PlayerAccountInfo.Me.PlayFabUsername,
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
                (x) =>
                {
                    var guidValue = Guid.NewGuid();
                    Debug.Log(guidValue.ToString());
                    PlayFabClientAPI.AddUsernamePassword(new PlayFab.ClientModels.AddUsernamePasswordRequest
                        {
                            Username = x.PlayFabId,
                            Email = "xxx@xxx.com",
                            Password = guidValue.ToString()
                        }, addUsernamePasswordResult =>
                        {
                            Debug.Log("我们把玩家的PlayFab username设置成了他的PlayFabId:" + addUsernamePasswordResult.Username);
                        }, 
                        (x) =>
                        {
                            Debug.Log(x.Error);
                        }
                    );
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
              success,
              fail
            );
            #endif
        }
    }
}
