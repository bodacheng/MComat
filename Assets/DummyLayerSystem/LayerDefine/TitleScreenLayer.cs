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
    [SerializeField] P3Button TouchScreen;
    [SerializeField] Button accountLoginBtn;
    
    // Login by pw
    [SerializeField] RectTransform loginByPwTab;
    [SerializeField] InputField ID;
    [SerializeField] InputField PASSWORD;
    [SerializeField] Button LoginBtn;
    [SerializeField] Button cancelBtn;

    private float titleAnimFactor = 0;
    public void Initialise()
    {
        TouchScreen.onClick.AddListener(TouchScreenLogin);
        accountLoginBtn.onClick.AddListener(()=> SwitchTab(2));
        cancelBtn.onClick.AddListener(()=> SwitchTab(1));
        LoginBtn.onClick.AddListener(EmailLogin);

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
        PlayFabReadClient.PlayFabEmailLogin(ID.text.Trim(), PASSWORD.text.Trim(), 
            PlayFabReadClient.LoginSuccess,
            PlayFabReadClient.LoginFail);
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
