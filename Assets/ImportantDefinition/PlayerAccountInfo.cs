using UnityEngine;

//这个类在远程更新的过程中需要被json化。
[System.Serializable]
public class PlayerAccountInfo
{
    int coin;//智慧果实
    int diamond;
    int stoneboxsize;
    public PlayerAccountProgressStep accountprogress = PlayerAccountProgressStep.Freedom;

    public PlayerAccountInfo()
    {
        Coin = 2000;
        Diamond = 99999;
        Stoneboxsize = 200;
    }
    
    public int Stoneboxsize
    {
        get => stoneboxsize;
        set
        {
            stoneboxsize = Mathf.Clamp(value, 0, value);
        }
    }
    
    public int Diamond
    {
        get => diamond;
        set
        {
            diamond = Mathf.Clamp(value, 0, value);
        }
    }

    public int Coin
    {
        get => coin;
        set
        {
            coin = Mathf.Clamp(value, 0, value);
        }
    }

    public void PlusCoin(int plus)
    {
        Coin = Coin + plus;
    }
}

public enum PlayerAccountProgressStep : int
{
    justCreated = 1,
    Tutorial = 2,
    justNamed = 3,
    MenuTuorial = 4,
    Freedom = 5,
    Season1_Completed = 6,
}