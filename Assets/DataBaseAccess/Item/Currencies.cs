using UnityEngine;

public class Currencies
{
    public static int coin;//智慧果实
    public static int diamond;

    static public int DiamondCount
    {
        get => diamond;
        set
        {
            diamond = Mathf.Clamp(value, 0, value);
        }
    }

    static public int CoinCount
    {
        get => coin;
        set
        {
            coin = Mathf.Clamp(value, 0, value);
        }
    }
}
