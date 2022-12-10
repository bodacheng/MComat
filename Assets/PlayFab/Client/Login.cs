using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using mainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class PlayFabReadClient
{
    const string PLAYFAB_CUSTOM_ID = "PLAYFAB_CUSTOM_ID";
    
    public static string CustomId
    {
        get
        {
            var customId = PlayerPrefs.GetString(PLAYFAB_CUSTOM_ID, Guid.NewGuid().ToString());
            PlayerPrefs.SetString(PLAYFAB_CUSTOM_ID, customId);
            PlayerPrefs.Save();
            return customId;
        }
    }
    
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
    public static void PlayFabEmailLogin(string email, string pw, Action<LoginResult> success, Action<PlayFabError> fail)
    {
        Debug.Log("try login by email:"+ email +"\n"
        + "pw:"+ pw +"\n" + "TitleId:"+ PlayFabSettings.TitleId);
        
        PlayFabClientAPI.LoginWithEmailAddress(
            new LoginWithEmailAddressRequest()
            {
                Email = email,
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
                    DeviceId = CustomId,
                    CreateAccount = true
                },
                success.Invoke,
                fail
            );
#endif

#if UNITY_ANDROID
        PlayFabClientAPI.LoginWithAndroidDeviceID(
            new LoginWithAndroidDeviceIDRequest
            {
                AndroidDeviceId = CustomId,
                CreateAccount = true
            },
            success.Invoke,
            fail
        );
#endif
    }
    
    static MissionWatcher _missionWatcher;
    static bool _accountIsInitialized;
    static bool _tutorialProgressGot;
    public static void LoginSuccess(LoginResult result)
    {
        Debug.Log(" login success： " + result.EntityToken.EntityToken);
        PlayerAccountInfo.Me = new PlayerAccountInfo
        {
            PlayFabId = result.PlayFabId
        };
        
        CloudScript.CheckIn();
        _missionWatcher = new MissionWatcher(
            new List<string>
            {
                "accountIsInitialized", "tutorialProgressGot"
            },
            EnterMainScene,
            () =>
            {
                
                Debug.Log("错误，怎么办？");
            }
        );
        TryProcessWithLimitedTimes(CheckAccountInitialized, ()=> _accountIsInitialized, 0);
        TryProcessWithLimitedTimes(CheckTutorialProgressGot, ()=> _tutorialProgressGot, 0);
    }

    private static readonly int MAXTry = 5;
    private static readonly float tryInterval = 1f;
    static void TryProcessWithLimitedTimes(Action tryProcess, Func<bool> check, int tryTime)
    {
        UniTask.Delay(TimeSpan.FromSeconds(tryInterval)).ContinueWith(()=>
        {
            tryTime += 1;
            if (tryTime == MAXTry || check())
            {
                return;
            }
            Debug.Log("wait for a initialized account, try time : "+ tryTime);
            tryProcess();
            TryProcessWithLimitedTimes(tryProcess, check, tryTime);
        });
    }

    static void CheckTutorialProgressGot()
    {
        PlayFabClientAPI.GetUserData
        (
            new GetUserDataRequest
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabId
            },
            (GetUserDataResult obj) =>
            {
                if (obj.Data.ContainsKey("TutorialProgress"))
                {
                    PlayerAccountInfo.Me.tutorialProgress = obj.Data["TutorialProgress"].Value;
                    _tutorialProgressGot = true;
                    _missionWatcher.Finish("tutorialProgressGot", true);
                }
                else
                {
                    UpdateUserData(
                        new UpdateUserDataRequest()
                        {
                            Data = new Dictionary<string, string>
                            {
                                { "TutorialProgress", "Started" }
                            }
                        },
                        (x) =>
                        {
                            PlayerAccountInfo.Me.tutorialProgress = "Started";
                            _tutorialProgressGot = true;
                            _missionWatcher.Finish("tutorialProgressGot", true);
                        }
                    );
                }
            },
            errorCallback => {
                Debug.Log("Basic accInfo fail:" + errorCallback.ErrorMessage);
            }
        );
    }

    static void CheckAccountInitialized()
    {
        PlayFabClientAPI.GetUserReadOnlyData
        (
            new GetUserDataRequest
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabId,
                Keys = new List<string> { "basicItemGranted", "playerInitialized" }
            },
            (obj) =>
            {
                string basicItemGranted = null;
                if (obj.Data.ContainsKey("basicItemGranted"))
                {
                    basicItemGranted = obj.Data["basicItemGranted"].Value;
                }
                
                string playerInitialized = null;
                if (obj.Data.ContainsKey("playerInitialized"))
                {
                    playerInitialized = obj.Data["playerInitialized"].Value;
                }
                
                _accountIsInitialized = basicItemGranted == "true" && playerInitialized == "true";
                if (_accountIsInitialized)
                {
                    _missionWatcher.Finish("accountIsInitialized", true);
                }
            },
            errorCallback => {
                Debug.Log("Basic accInfo fail:" + errorCallback.ErrorMessage);
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
    
    static void EnterMainScene()
    {
        MainMenuNote.GoingTo = MainSceneStep.FrontPage;
        SceneManager.LoadScene(1);
    }
}
