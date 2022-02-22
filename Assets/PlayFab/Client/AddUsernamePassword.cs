using UnityEngine;
using PlayFab;
using System;
using PlayFab.ClientModels;

public partial class PlayFabReadClient
{
    static void AddUserNameAndPw(string PlayFabUsername)
    {
        var guidValue = Guid.NewGuid();
        Debug.Log(guidValue.ToString());
        PlayFabClientAPI.AddUsernamePassword(new PlayFab.ClientModels.AddUsernamePasswordRequest
            {
                Username = PlayFabUsername,
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
    }
    
    

    public static void SendPwResetEmail(string email)
    {
        Debug.Log("send mail to this address" + email);
        var request = new AddOrUpdateContactEmailRequest
        {
            EmailAddress = email,
            
        };
        PlayFabClientAPI.AddOrUpdateContactEmail(
            request, 
            result =>
            {
                Debug.Log("The player's account has been updated with a contact email");
                PlayFabClientAPI.SendAccountRecoveryEmail(
                    new SendAccountRecoveryEmailRequest
                    {
                        Email = email,
                        TitleId = PlayFabSettings.TitleId
                    },
                    (x) =>
                    {
                        Debug.Log(x);
                    },
                    (x)=>
                    {
                        Debug.Log(x);
                    }
                );
            }, 
            (x) =>
            {
                Debug.Log(x);
            }
        );
    }
    
    void AddOrUpdateContactEmail(string playFabId, string emailAddress)
    {

    }
}
