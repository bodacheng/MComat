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
                CustomId = "boda",
                CreateAccount = true
            },
            sucess,
            fail
        );
    }
}
