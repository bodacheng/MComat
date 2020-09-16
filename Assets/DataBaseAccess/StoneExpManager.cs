using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneExpManager
{
    #region 智慧果实与经验值转换关系 可能改变位置
    public static int GoldToExp(int gold)
    {
        return (gold) / 10 * 1;
    }
    
    public static int ExpToGold(int Exp)
    {
        return Exp * 10;
    }
    #endregion
}
