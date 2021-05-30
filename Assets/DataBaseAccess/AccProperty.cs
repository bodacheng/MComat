using UnityEngine;

public class AccProperty
{
    public static AccProperty value = new AccProperty();

    int coin;//智慧果实
    int diamond;

    public int DiamondCount
    {
        get => diamond;
        set
        {
            diamond = Mathf.Clamp(value, 0, value);
        }
    }

    public int CoinCount
    {
        get => coin;
        set
        {
            coin = Mathf.Clamp(value, 0, value);
        }
    }
}
