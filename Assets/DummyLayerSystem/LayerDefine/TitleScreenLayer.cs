using PlayFab;
using PlayFab.ClientModels;
using System;
using mainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 这个layer的问题在于，它必须灵活的适应未来可能做出的一些改动
/// 就是说它既可能出现在"标题战斗"上，也可能出现在主界面
/// </summary>
public class TitleScreenLayer : UILayer
{
    // Main
    [SerializeField] private RectTransform mainTab;
    [SerializeField] private P3Button TouchScreen;
    [SerializeField] private Button accountLoginBtn;
    
    // Login by pw
    [SerializeField] private RectTransform loginByPwTab;
    [SerializeField] private InputField ID;
    [SerializeField] private InputField PASSWORD;
    [SerializeField] private Button LoginBtn;
    [SerializeField] private Button cancelBtn;
    
    public void Initialise()
    {
        TouchScreen.onClick.AddListener(TouchScreenLogin);
        accountLoginBtn.onClick.AddListener(()=> SwitchTab(2));
        cancelBtn.onClick.AddListener(()=> SwitchTab(1));
        LoginBtn.onClick.AddListener(PWLogin);
    }

    void LoginSuccess(LoginResult result)
    {
        Debug.Log(" 登陆成功，获得下面这样一个东西： " + result.EntityToken.EntityToken);
        PlayerAccountInfo.Me = new PlayerAccountInfo
        {
            PlayFabUsername = result.PlayFabId
        };
        CloudScript.CheckIn();
        MainMenuNote.GoingTo = MainSceneStep.FrontPage;
        SceneManager.LoadScene(1);
    }

    void LoginFail(PlayFabError error)
    {
        Debug.Log(error.Error);
        if (error.Error == PlayFabErrorCode.AccountDeleted)
        {
            // 官网说如果出现这个错误的话，可能需要等一些时间，账户内容才被彻底清除
        }
        Debug.Log("login fail");
    }
    
    void SwitchTab(int step) // step 1:main ,step 2: login by pw
    {
        if (step == 1)
        {
            mainTab.gameObject.SetActive(true);
            loginByPwTab.gameObject.SetActive(false);
        }
        else if (step == 2)
        {
            mainTab.gameObject.SetActive(false);
            loginByPwTab.gameObject.SetActive(true);
        }
    }
    
    void PWLogin()
    {
        PlayFabReadClient.PlayFabLogin(ID.text.Trim(), PASSWORD.text.Trim(), LoginSuccess, LoginFail);
    }
    
    void TouchScreenLogin()
    {
        PlayFabReadClient.LoginByDevice(
            result => {
                LoginSuccess(result);
            },
            error => {
                LoginFail(error);
            }
        );
        TouchScreen.onClick.RemoveAllListeners();
    }
}
