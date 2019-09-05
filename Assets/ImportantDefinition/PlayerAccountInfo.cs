using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//这个类在远程更新的过程中需要被json化。
[System.Serializable]
public class PlayerAccountInfo
{
    private int coin;//智慧果实
    private int diamond;
    public playerAccountProgressStep accountprogress = playerAccountProgressStep.Freedom;

    public PlayerAccountInfo()
    {
    }

    public PlayerAccountInfo(int diamond, int intelliCoin)//第二个参数去掉？
    {
        this.diamond = diamond;
        this.coin = intelliCoin;
    }
    
    public int Diamond
    {
        get
        {
            return diamond;
        }
        set
        {
            diamond = Mathf.Clamp(value, 0, value);
        }
    }

    public int Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin = Mathf.Clamp(value, 0, value);
        }
    }

    public void plusCoin(int plus)
    {
        this.Coin = Coin + plus;
    }
}

public enum playerAccountProgressStep : int
{
    justCreated = 1,
    Tutorial = 2,
    justNamed = 3,
    MenuTuorial = 4,
    Freedom = 5,
    Season1_Completed = 6,
}