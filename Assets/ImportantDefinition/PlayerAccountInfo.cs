using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//这个类在远程更新的过程中需要被json化。
[System.Serializable]
public class PlayerAccountInfo
{
    public int intelliCoin;//智慧果实

    public PlayerAccountInfo()
    {
    }

    public PlayerAccountInfo(int intelliCoin)//第二个参数去掉？
    {
        this.intelliCoin = intelliCoin;
    }

    public int IntelliCoin
    {
        get
        {
            return intelliCoin;
        }
        set
        {
            intelliCoin = Mathf.Clamp(value, 0, value);
        }
    }

    public void plusIntelliCoin(int plus)
    {
        this.IntelliCoin = IntelliCoin + plus;
    }
}