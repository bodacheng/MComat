using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 这个layer的问题在于，它必须灵活的适应未来可能做出的一些改动
/// 就是说它既可能出现在"标题战斗"上，也可能出现在主界面
/// </summary>
public class TitleScreenLayer : UILayer
{
    // Main
    [SerializeField] RectTransform mainTab;
    [SerializeField] Image title;
    [SerializeField] BOButton touchScreenBtn;
    [SerializeField] Button accountLoginBtn;
    
    // Login by pw
    [SerializeField] RectTransform loginByPwTab;
    [SerializeField] InputField id;
    [SerializeField] InputField password;
    [SerializeField] Button loginBtn;
    [SerializeField] Button cancelBtn;

    // Dev login
    [SerializeField] InputField devId;
    [SerializeField] Button devEnter;
    [SerializeField] Button devLoginBtn;
    
    private float titleAnimFactor = 0;
    public void Initialise()
    {
        touchScreenBtn.onClick.AddListener(TouchScreenLogin);
        accountLoginBtn.onClick.AddListener(()=> SwitchTab(2));
        cancelBtn.onClick.AddListener(()=> SwitchTab(1));
        loginBtn.onClick.AddListener(EmailLogin);

        if (CommonSetting.DevMode)
        {
            devEnter.gameObject.SetActive(true);
            devLoginBtn.onClick.AddListener(DevUserLogin);
        }
        
        DOTween.To(() => titleAnimFactor, (x) => titleAnimFactor = x, 2, 10).OnUpdate(() =>
        {
            title.material.SetFloat("_Animation_Factor", titleAnimFactor);
        });
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
    
    void EmailLogin()
    {
        PlayFabReadClient.PlayFabEmailLogin(
            id.text.Trim(), password.text.Trim(), 
            PlayFabReadClient.LoginSuccess,
            PlayFabReadClient.LoginFail);
    }
    
    void TouchScreenLogin()
    {
        PlayFabReadClient.LoginByDevice(
            PlayFabReadClient.LoginSuccess,
            PlayFabReadClient.LoginFail);
    }

    void DevUserLogin()
    {
        PlayFabReadClient.LoginByCustomId(
            devId.text,
            PlayFabReadClient.LoginSuccess,
            PlayFabReadClient.LoginFail);
    }
}
