using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.UI;

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
    
    Action<LoginResult> success;
    Action<PlayFabError> fail;
    
    public void Initialise(Action<LoginResult> success, Action<PlayFabError> fail)
    {
        TouchScreen.onClick.AddListener(TouchScreenLogin);
        accountLoginBtn.onClick.AddListener(()=> SwitchTab(2));
        cancelBtn.onClick.AddListener(()=> SwitchTab(1));
        
        this.success = success;
        this.fail = fail;
        LoginBtn.onClick.AddListener(PWLogin);
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
        PlayFabReadClient.PlayFabLogin(ID.text.Trim(), PASSWORD.text.Trim(), success, fail);
    }
    
    void TouchScreenLogin()
    {
        PlayFabReadClient.LoginByDevice(
            result => {
                this.success.Invoke(result);
            },
            error => {
                this.fail.Invoke(error);
            }
        );
    }
}
