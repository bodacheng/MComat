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
        UserID.text = Account._AccInfo.playerID; //SystemInfo.deviceUniqueIdentifier;
        accountDiamondCoin.text = Currencies.DiamondCount.ToString();
        accountIntelliCoin.text = Currencies.CoinCount.ToString();
    }
}
