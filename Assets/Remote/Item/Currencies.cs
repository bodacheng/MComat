using UnityEngine;

public static class Currencies
{
    private static int coin;//智慧果实
    private static int diamond;

    public static int DiamondCount
    {
        get => diamond;
        set => diamond = Mathf.Clamp(value, 0, value);
    }

    public static int CoinCount
    {
        get => coin;
        set => coin = Mathf.Clamp(value, 0, value);
    }
}
