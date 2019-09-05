using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FightReward
{
    public int intell;
    public int diamond;
    public CharacterDataInfo[] _CharacterDataInfos;//奖励队员

    float[] petsRewardList;//安排这个列表来表达各个宠物队员获得的概率

    public FightReward()
    {

    }

    public FightReward(int intell, int diamond, CharacterDataInfo[] _CharacterDataInfos)
    {
        this.intell = intell;
        this.diamond = diamond;
        this._CharacterDataInfos = _CharacterDataInfos;
    }
}
