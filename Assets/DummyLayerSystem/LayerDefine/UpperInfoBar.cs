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
    
    public void Setup(Action openSetting, Action openMail)
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
        
        settingBtn.onClick.AddListener(openSetting.Invoke);
        mailBtn.onClick.AddListener(openMail.Invoke);
        diamondPlus.onClick.AddListener(() =>
        {
            PreScene.target.trySwitchToStep(MainSceneStep.ShopTop, true);
        });
    }
}
