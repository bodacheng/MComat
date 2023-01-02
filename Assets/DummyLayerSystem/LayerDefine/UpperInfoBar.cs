using mainMenu;
using UnityEngine.UI;
using UnityEngine;
using System;
using UniRx;

public class UpperInfoBar : UILayer
{
    [SerializeField] Button settingBtn;
    [SerializeField] Button mailBtn;
    [SerializeField] Text titleDisplayName;
    [SerializeField] Text accountDiamondCoin;
    [SerializeField] Button diamondPlus;
    [SerializeField] Text accountIntelliCoin;

    public void Interactable(bool on)
    {
        settingBtn.interactable = on;
        mailBtn.interactable = on;
        diamondPlus.interactable = on;
    }
    
    public void Setup(Action openSetting, Action openMail, Action openDMShop)
    {
        titleDisplayName.text = PlayerAccountInfo.Me.TitleDisplayName; //SystemInfo.deviceUniqueIdentifier;
        Currencies.DiamondCount.Subscribe(x =>
        {
            accountDiamondCoin.text = x.ToString();
        }).AddTo(this.gameObject);
        
        Currencies.CoinCount.Subscribe(x =>
        {
            accountIntelliCoin.text = x.ToString();
        }).AddTo(this.gameObject);

        if (openSetting != null)
        {
            settingBtn.onClick.AddListener(openSetting.Invoke);
            settingBtn.gameObject.SetActive(true);
        }
        else
        {
            settingBtn.gameObject.SetActive(false);
        }

        if (openMail != null)
        {
            mailBtn.onClick.AddListener(openMail.Invoke);
            mailBtn.gameObject.SetActive(true);
        }
        else
        {
            mailBtn.gameObject.SetActive(false);
        }

        if (diamondPlus != null)
        {
            diamondPlus.onClick.AddListener(openDMShop.Invoke);
            diamondPlus.gameObject.SetActive(true);
        }
        else
        {
            diamondPlus.gameObject.SetActive(false);
        }
    }
}
