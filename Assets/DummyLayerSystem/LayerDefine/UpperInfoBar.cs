using mainMenu;
using UnityEngine.UI;
using UnityEngine;
using System;
using DummyLayerSystem;

public class UpperInfoBar : UILayer
{
    [SerializeField] Button SettingBtn;
    [SerializeField] Button MailBtn;
    [SerializeField] Text UserID;
    [SerializeField] Text accountDiamondCoin;
    [SerializeField] Button diamondPlus;
    [SerializeField] Text accountIntelliCoin;

    public void Interactable(bool on)
    {
        SettingBtn.interactable = on;
        MailBtn.interactable = on;
        diamondPlus.interactable = on;
    }
    
    public void Setup(Action openSetting, Action OpenMail)
    {
        Refresh();
        SettingBtn.onClick.AddListener(openSetting.Invoke);
        MailBtn.onClick.AddListener(OpenMail.Invoke);
        diamondPlus.onClick.AddListener(() =>
        {
            PreScene.target.trySwitchToStep(MainSceneStep.ShopTop, true);
        });
    }
    
    void Refresh()
    {
        UserID.text = PlayerAccountInfo.Me.PlayFabId; //SystemInfo.deviceUniqueIdentifier;
        accountDiamondCoin.text = Currencies.DiamondCount.ToString();
        accountIntelliCoin.text = Currencies.CoinCount.ToString();
    }
}
