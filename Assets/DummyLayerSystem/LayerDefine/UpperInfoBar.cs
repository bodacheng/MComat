using mainMenu;
using UnityEngine.UI;
using dataAccess;
using UnityEngine;
using System;

public class UpperInfoBar : UILayer
{
    public Button SettingBtn;
    public Button MailBtn;
    [SerializeField] Text UserID;
    [SerializeField] Text accountDiamondCoin;
    [SerializeField] Text accountIntelliCoin;
    
    public static UpperInfoBar Open(Action openSetting, Action OpenMail)
    {
        var returnValue = UILayerLoader.Load(PreScene.target.T,"UpperInfoBar") as UpperInfoBar;
        returnValue.Refresh();
        returnValue.SettingBtn.onClick.AddListener(openSetting.Invoke);
        returnValue.MailBtn.onClick.AddListener(OpenMail.Invoke);
        
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("UpperInfoBar");
    }

    public void Refresh()
    {
        UserID.text = Account._AccInfo.playerID; //SystemInfo.deviceUniqueIdentifier;
        accountDiamondCoin.text = Currencies.DiamondCount.ToString();
        accountIntelliCoin.text = Currencies.CoinCount.ToString();
    }
}
