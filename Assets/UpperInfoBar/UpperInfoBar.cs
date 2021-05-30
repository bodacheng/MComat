using UnityEngine;
using UnityEngine.UI;
using dataAccess;

public class UpperInfoBar : MonoBehaviour
{
    public RectTransform T;
    public Text UserID;
    public Text accountDiamondCoin;
    public Text accountIntelliCoin;
    
    public static UpperInfoBar target;

    void Awake()
    {
        target = this;
    }

    public void Refresh()
    {
        UserID.text = AccountSet._AccInfo.PlayerName; //SystemInfo.deviceUniqueIdentifier;
        accountDiamondCoin.text = AccProperty.value.DiamondCount.ToString();
        accountIntelliCoin.text = AccProperty.value.CoinCount.ToString();
    }
}
