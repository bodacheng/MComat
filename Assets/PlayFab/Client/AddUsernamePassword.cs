using UnityEngine;
using PlayFab;
using System;
using PlayFab.ClientModels;

public partial class PlayFabReadClient
{
    /// <summary>
    /// 希望弃用
    /// </summary>
    /// <param name="PlayFabUsername"></param>
    public static void AddUserNameAndEmail(string PlayFabUsername, string email, Action success)
    {
        Debug.Log("尝试把Username设置成"+ PlayFabUsername);
        var guidValue = Guid.NewGuid();
        Debug.Log(guidValue.ToString());
        PlayFabClientAPI.AddUsernamePassword(new PlayFab.ClientModels.AddUsernamePasswordRequest
            {
                Username = PlayFabUsername.ToLower(),
                Email = email,
                Password = guidValue.ToString()
            }, result =>
            {
                PlayerAccountInfo.Me.PlayFabUserName = result.Username;
                Debug.Log("我们把玩家的PlayFab username设置成了他的PlayFabId:" + result.Username);
                success.Invoke();
            },
            (x) =>
            {
                Debug.Log("添加username失败："+ x.Error);
            }
        );
    }
    
    /// <summary>
    /// 主界面设置画面玩家可以自由指定邮件地址，发送用来设定密码的邮件。然后玩家可以靠
    /// Username和设定的密码登陆。
    /// 但是，玩家不能改username，这个是我们自己定的规矩，我们希望这个username就是玩家的playfabid。
    /// </summary>
    /// <param name="email"></param>
    public static void SendPwResetEmail(string email)
    {
        Debug.Log("send mail to this address:" + email.Trim() );
        Debug.Log("titleID:" + PlayFabSettings.TitleId );

        PlayFabClientAPI.SendAccountRecoveryEmail(
            new SendAccountRecoveryEmailRequest
            {
                Email = email.Trim(),
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
    }
    
    void AddOrUpdateContactEmail(string playFabId, string emailAddress)
    {
        var request = new AddOrUpdateContactEmailRequest
        {
            EmailAddress = emailAddress,
        };
        PlayFabClientAPI.AddOrUpdateContactEmail(
            request, 
            result =>
            {
                Debug.Log("The player's account has been updated with a contact email");
            }, 
            (x) =>
            {
                Debug.Log(x);
            }
        );
    }
}
