using UnityEngine;
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
        PlayFabReadClient.PlayFabLogin(ID.text.Trim(), PASSWORD.text.Trim(), 
            PlayFabReadClient.LoginSuccessWithAccountLink, PlayFabReadClient.LoginFail);
    }
    
    void TouchScreenLogin()
    {
        PlayFabReadClient.LoginByDevice(
            PlayFabReadClient.LoginSuccess,
            PlayFabReadClient.LoginFail
        );
        TouchScreen.onClick.RemoveAllListeners();
    }
}
