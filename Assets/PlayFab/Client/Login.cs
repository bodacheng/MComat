using PlayFab;
using PlayFab.ClientModels;
using System;
using mainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class PlayFabReadClient
{
    /// <summary>
    /// 以Username和Password来登陆，
    /// 是玩家在设备迁移的时候会用的方法
    /// 其中Username并非一定是这个账户的playfabid，
    /// 而是一个账户生成后按某种主动方式给添加的，
    /// 所以一个playfab玩家账号完全可能这个登陆用的Username是空。
    /// 我们采取的策略是在玩家账号生成瞬间靠自动化流程把玩家的playfabid赋值给Username
    ///
    /// 有一个非常大的问题在于，如果玩家用这个方法登陆，
    /// 我们希望玩家输入的账号是直接与这个设备进行绑定，
    /// 那么如果当前的设备已经与其他账号进行了绑定的话，则必须先和那个账号进行松绑
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="pw"></param>
    /// <param name="success"></param>
    /// <param name="fail"></param>
    public static void PlayFabLogin(string userName, string pw, Action<LoginResult> success, Action<PlayFabError> fail)
    {
        Debug.Log("尝试登陆 userName:"+ userName +"\n"
        + "pw:"+ pw +"\n" + "TitleId:"+ PlayFabSettings.TitleId);

        PlayFabClientAPI.LoginWithPlayFab(
            new LoginWithPlayFabRequest
            {
                Username = userName,
                Password = pw,
                TitleId = PlayFabSettings.TitleId
            },
            success.Invoke,
            (x)=>
            {
                Debug.Log(x.Error);
                fail.Invoke(x);
            }
        );
    }
    
    /// <summary>
    /// 日常登陆靠这个，前提是玩家的账号已经和deviceid进行绑定
    /// </summary>
    /// <param name="success"></param>
    /// <param name="fail"></param>
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
                success.Invoke,
                fail
            );
#endif
    }
    
    public static void LoginSuccess(LoginResult result)
    {
        Debug.Log(" 登陆成功，获得下面这样一个东西： " + result.EntityToken.EntityToken);
        PlayerAccountInfo.Me = new PlayerAccountInfo
        {
            PlayFabId = result.PlayFabId
        };
        
        GetAccountInfo(PlayerAccountInfo.Me.PlayFabId, EnterMainScene);
    }
    
    public static void LoginSuccessWithAccountLink(LoginResult result)
    {
        Debug.Log(" 登陆成功，获得下面这样一个东西： " + result.EntityToken.EntityToken);
        PlayerAccountInfo.Me = new PlayerAccountInfo
        {
            PlayFabId = result.PlayFabId
        };
        
        GetAccountInfoWithLinkDevice(PlayerAccountInfo.Me.PlayFabId, EnterMainScene);
    }

    static void EnterMainScene()
    {
        CloudScript.CheckIn();
        MainMenuNote.GoingTo = MainSceneStep.FrontPage;
        SceneManager.LoadScene(1);
    }

    static void GetAccountInfo(string playFabId, Action enterMainScene)
    {
        PlayFabClientAPI.GetAccountInfo(
            new GetAccountInfoRequest
            {
                PlayFabId = playFabId
            },
            result =>
            {
                PlayerAccountInfo.Me.PlayFabUserName = result.AccountInfo.Username;
                if (PlayerAccountInfo.Me.PlayFabUserName == null)
                {
                    AddUserNameAndPw(playFabId); // 这个方法没有server版，只能客户端主动执行
                }
                enterMainScene?.Invoke();
            },
            errorCallback =>
            {
                Debug.Log(errorCallback.Error);
            }
        );
    }
    
    static void GetAccountInfoWithLinkDevice(string playFabId, Action enterMainScene)
    {
        PlayFabClientAPI.GetAccountInfo(
            new GetAccountInfoRequest
            {
                PlayFabId = playFabId
            },
            result =>
            {
                PlayerAccountInfo.Me.PlayFabUserName = result.AccountInfo.Username;
                if (PlayerAccountInfo.Me.PlayFabUserName == null)
                {
                    AddUserNameAndPw(playFabId); // 这个方法没有server版，只能客户端主动执行
                }
#if UNITY_IOS
                if (result.AccountInfo.IosDeviceInfo.IosDeviceId != SystemInfo.deviceUniqueIdentifier)
                {
                    Debug.Log("开始链接deviceID："+SystemInfo.deviceUniqueIdentifier);
                    Link(SystemInfo.deviceUniqueIdentifier);
                }
#endif
                
#if UNITY_ANDROID
                if (result.AccountInfo.AndroidDeviceInfo.AndroidDeviceId != SystemInfo.deviceUniqueIdentifier)
                {
                    Debug.Log("开始链接deviceID："+SystemInfo.deviceUniqueIdentifier);
                    Link(SystemInfo.deviceUniqueIdentifier);
                }
#endif
                enterMainScene?.Invoke();
            },
            errorCallback =>
            {
                Debug.Log(errorCallback.Error);
            }
        );
    }
    
    public static void LoginFail(PlayFabError error)
    {
        Debug.Log(error.Error);
        if (error.Error == PlayFabErrorCode.AccountDeleted)
        {
            // 官网说如果出现这个错误的话，可能需要等一些时间，账户内容才被彻底清除
        }
        Debug.Log("login fail");
    }
}
