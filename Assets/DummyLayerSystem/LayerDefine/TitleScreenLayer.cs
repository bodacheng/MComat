using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

/// <summary>
/// 这个layer的问题在于，它必须灵活的适应未来可能做出的一些改动
/// 就是说它既可能出现在"标题战斗"上，也可能出现在主界面
/// </summary>
public class TitleScreenLayer : UILayer
{
    // Main
    [SerializeField] RectTransform mainTab;
    [SerializeField] List<Image> title;
    [SerializeField] BOButton touchScreenBtn;
    [SerializeField] BOButton accountLoginBtn;
    
    // Login by pw
    [SerializeField] RectTransform loginByPwTab;
    [SerializeField] InputField id;
    [SerializeField] InputField password;
    [SerializeField] BOButton loginBtn;
    [SerializeField] BOButton cancelBtn;

    // Dev login
    [SerializeField] InputField devId;
    [SerializeField] Button devEnter;
    [SerializeField] Button devLoginBtn;

    [SerializeField] Text version;
    
    private float titleAnimFactor = 0;
    private TweenerCore<float, float, FloatOptions> titleTween;
    public void Initialise()
    {
        version.text = Application.version;
        touchScreenBtn.onClick.AddListener(TouchScreenLogin);
        accountLoginBtn.onClick.AddListener(()=> SwitchTab(2));
        cancelBtn.onClick.AddListener(()=> SwitchTab(1));
        loginBtn.onClick.AddListener(EmailLogin);
        
        devEnter.gameObject.SetActive(CommonSetting.DevMode);
        if (CommonSetting.DevMode)
        {
            devLoginBtn.onClick.AddListener(DevUserLogin);
        }
        
        titleTween = DOTween.To(() => titleAnimFactor, (x) => titleAnimFactor = x, 2, 10)
            .SetLink(gameObject)
            .OnUpdate(() =>
            {
                if (this == null || title == null)
                {
                    return;
                }

                title.ToList().ForEach(x =>
                {
                    if (x != null)
                    {
                        x.material.SetFloat("_Animation_Factor", titleAnimFactor);
                    }
                });
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
        ProgressLayer.Loading("");
        PlayFabReadClient.PlayFabEmailLogin(
            id.text.Trim(), password.text.Trim(), 
            PlayFabReadClient.LoginSuccess);
    }
    
    void TouchScreenLogin()
    {
        ProgressLayer.Loading("");
        PlayFabReadClient.LoginByDevice(PlayFabReadClient.LoginSuccess);
    }

    void DevUserLogin()
    {
        PlayFabReadClient.LoginByCustomId(
            devId.text,
            PlayFabReadClient.LoginSuccess);
    }

    public override void OnDestroy()
    {
        titleTween?.Kill();
        titleTween = null;
        base.OnDestroy();
    }
}
