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
    
    public static UpperInfoBar Open(Action openSetting, Action OpenMail)
    {
        var current = UILayerLoader.Get<UpperInfoBar>();
        if (current != null)
            return current;
        
        var returnValue = UILayerLoader.Load(PreScene.target.T,"UpperInfoBar") as UpperInfoBar;
        returnValue.Refresh();
        returnValue.SettingBtn.onClick.AddListener(openSetting.Invoke);
        returnValue.MailBtn.onClick.AddListener(OpenMail.Invoke);
        
        returnValue.diamondPlus.onClick.AddListener(() =>
        {
            PreScene.target.trySwitchToStep(MainSceneStep.ShopTop, true);
        });
        
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("UpperInfoBar");
    }

    void Refresh()
    {
        UserID.text = PlayerAccountInfo.Me.PlayFabId; //SystemInfo.deviceUniqueIdentifier;
        accountDiamondCoin.text = Currencies.DiamondCount.ToString();
        accountIntelliCoin.text = Currencies.CoinCount.ToString();
    }
}
