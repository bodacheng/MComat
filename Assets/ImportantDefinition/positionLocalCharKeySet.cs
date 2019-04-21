using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class positionLocalCharKeySet
{
    public PosNumWithLocalKey[] PosNumsWithLocalKeys;
    private IDictionary<PosNum, PosNumWithLocalKey> PosMemDic;

    public positionLocalCharKeySet()
    {
        PosNumsWithLocalKeys =
            new PosNumWithLocalKey[4] { new PosNumWithLocalKey(PosNum.back, -1), 
                                        new PosNumWithLocalKey(PosNum.left, -1), 
                                        new PosNumWithLocalKey(PosNum.front, -1),
                                        new PosNumWithLocalKey(PosNum.right, -1) };
    }

    public void clearSets()
    {
        PosNumsWithLocalKeys = new PosNumWithLocalKey[0];
        refreshDic();
    }

    public void refreshDic()
    {
        PosMemDic = new Dictionary<PosNum, PosNumWithLocalKey>();
        if (this.PosNumsWithLocalKeys != null)
        {
            foreach (PosNumWithLocalKey _set in this.PosNumsWithLocalKeys)
            {
                if (!PosMemDic.ContainsKey(_set.posNum))
                {
                    PosMemDic.Add(new KeyValuePair<PosNum, PosNumWithLocalKey>(_set.posNum, _set));
                }
                else
                {
                    Debug.Log("队伍配置信息致命错误");
                }
            }
        }
    }

    public PosNumWithLocalKey getPosMemInfoByLocalID(int localID)
    {
        foreach (PosNumWithLocalKey _set in this.PosNumsWithLocalKeys)
        {
            if (_set.LocalID == localID)
                return _set;
        }
        return null;
    }

    public PosNumWithLocalKey getPosMemInfo(PosNum PosNum)
    {
        refreshDic();
        PosNumWithLocalKey _PosNumWithLocalKey;
        PosMemDic.TryGetValue(PosNum, out _PosNumWithLocalKey);
        return _PosNumWithLocalKey;
    }

    public int getPositionLocalID(PosNum PosNum)
    {
        refreshDic();
        PosNumWithLocalKey _PosNumWithLocalKey = null;
        PosMemDic.TryGetValue(PosNum, out _PosNumWithLocalKey);
        if (_PosNumWithLocalKey != null)
        {
            return _PosNumWithLocalKey.LocalID;
        }
        else
        {
            return -99999;//都有什么地方会去处理这个值？如果某个地方用的是一些类似强制修复一类的逻辑，那就可能出错。
        }
    }

    public positionLocalCharKeySet(PosNumWithLocalKey[] PosNumsWithLocalKey)
    {
        this.PosNumsWithLocalKeys = PosNumsWithLocalKey;
        refreshDic();
    }

    // 这个函数没法检查阵容设置下各个位置的角色localid是不是合法，只是看4个位置槽上的位置号码对不对 
    // 返回false说明有错，返回true说明没问题，出错的情况下应该出现一些引导，逼玩家重新对4v4模式阵容进行设置。
    public bool check4V4ModeTeamPositionNums()
    {
        List<PosNum> positionNums = new List<PosNum>();
        List<PosNum> positionNumsShouldBe = new List<PosNum>() { PosNum.back, PosNum.left, PosNum.front, PosNum.right };
        if (PosNumsWithLocalKeys != null)
        {
            foreach (PosNumWithLocalKey _set in PosNumsWithLocalKeys)
            {
                if (!positionNumsShouldBe.Contains(_set.posNum))
                {
                    Debug.Log("致命错误：4v4战斗模式不存在的阵容位置号码");
                    return false;
                }

                if (!positionNums.Contains(_set.posNum))
                {
                    positionNums.Add(_set.posNum);
                }
                else
                {
                    Debug.Log("致命错误：4v4战斗模式重复的阵容位置号码");
                    return false;
                }
            }
        }
        return true;
    }

    public void changePositionLocalKey(PosNum position, int localID)
    {
        refreshDic();
        PosNumWithLocalKey PosNumWithLocalKey = null;
        PosMemDic.TryGetValue(position, out PosNumWithLocalKey);
        if (PosNumWithLocalKey != null)
        {
            if (PosNumWithLocalKey.LocalID != localID)
            {
                PosNumWithLocalKey.LocalID = localID;
            }
        }
        else
        {
            Debug.Log("队伍号码" + position + "下出现严重问题");
        }
        int i = 0;
        PosNumsWithLocalKeys = new PosNumWithLocalKey[PosMemDic.Count];
        foreach (KeyValuePair<PosNum, PosNumWithLocalKey> _set in PosMemDic)
        {
            PosNumsWithLocalKeys[i] = _set.Value;
            i++;
        }
        refreshDic();
    }

    public void changePosition(PosNum position1, PosNum position2)
    {
        refreshDic();
        PosNumWithLocalKey PosNumWithLocalKey1;
        PosMemDic.TryGetValue(position1, out PosNumWithLocalKey1);
        PosNumWithLocalKey PosNumWithLocalKey2;
        PosMemDic.TryGetValue(position2, out PosNumWithLocalKey2);

        if (PosNumWithLocalKey1.LocalID != PosNumWithLocalKey2.LocalID)
        {
            int temp = PosNumWithLocalKey2.LocalID;
            PosNumWithLocalKey2.LocalID = PosNumWithLocalKey1.LocalID;
            PosNumWithLocalKey1.LocalID = temp;
        }
        PosNumsWithLocalKeys = new PosNumWithLocalKey[PosMemDic.Count];
        int i = 0;
        foreach (KeyValuePair<PosNum, PosNumWithLocalKey> _set in PosMemDic)
        {
            PosNumsWithLocalKeys[i] = _set.Value;
            i++;
        }
        refreshDic();
    }
}

[System.Serializable]
public class PosNumWithLocalKey
{
    public int LocalID;
    public PosNum posNum;

    public PosNumWithLocalKey()
    {
    }

    public PosNumWithLocalKey(PosNum posNum, int LocalID)
    {
        this.posNum = posNum;
        this.LocalID = LocalID;
    }
}

[System.Serializable]
public enum PosNum
{
    none = -1,
    back = 0,
    left = 1,
    front = 2,
    right = 3
}
