using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLogin
{
    public static void CustomIDLogin()
    {
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest
            {
                CustomId = "111",
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
