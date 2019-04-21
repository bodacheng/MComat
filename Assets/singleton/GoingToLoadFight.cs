using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToLoadFight
{
    private static GoingToLoadFight instance;

    public Stage nextBattle;

    private GoingToLoadFight()
    {
        nextBattle = new Stage();//这个地方无非是意思意思
    }

    public static GoingToLoadFight Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GoingToLoadFight();
            }
            return instance;
        }
    }
}