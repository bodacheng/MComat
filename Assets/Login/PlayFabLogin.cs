using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabLogin
{
    public static void CustomIDLogin(Action<LoginResult> sucess, Action<PlayFabError> fail)
    {
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest
            {
                CustomId = "111",
                CreateAccount = true
            },
            sucess,
            fail
        );
    }

    public static void IOSDeviceIDLogin()
    {
        PlayFabClientAPI.LoginWithIOSDeviceID (
            new LoginWithIOSDeviceIDRequest
            {
                CreateAccount = true
            },
            result =>
            {
                Debug.Log("playfab login successed?");
            },
            error =>
            {
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }
}
