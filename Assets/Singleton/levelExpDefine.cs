using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelExpDefine
{
    public static int levelOfExp(int Exp)
    {
        return (1 + (int)Exp / 10);
    }
    public static float percentOfCurrentExpOfLevel(int Exp)
    {
       return (float)(Exp % 10) / (float)10;
    }
}
