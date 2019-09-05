using System.Collections;
using Api.Dto.Model;
using dataAccess;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class positionLocalCharKeySet
{
    public string recordId;
    public PosNumWithLocalKey[] PosNumsWithLocalKeys;
    
    public positionLocalCharKeySet(PosNumWithLocalKey[] PosNumsWithLocalKey)
    {
        this.PosNumsWithLocalKeys = PosNumsWithLocalKey;
    }
    public positionLocalCharKeySet()
    {
        PosNumsWithLocalKeys = new PosNumWithLocalKey[4] { new PosNumWithLocalKey(PosNum.back, null), 
                                                            new PosNumWithLocalKey(PosNum.left, null), 
                                                            new PosNumWithLocalKey(PosNum.front, null),
                                                            new PosNumWithLocalKey(PosNum.right, null)};
    }
    
    public IEnumerator convertToMultiDictionary()
    {
        MultiDictionary<int, int, CharacterDataInfo> multiDictionary = new MultiDictionary<int, int, CharacterDataInfo>();
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            if (PosNumsWithLocalKeys[i].monsterOfPlayerId != null)
            {
                IEnumerator getchar = AccountCharsSet.Instance.getAccountCharacterInfo(PosNumsWithLocalKeys[i].monsterOfPlayerId);
                yield return getchar;
                GetMonsterOfPlayerDetailModel GetMonsterOfPlayerDetailModel = (GetMonsterOfPlayerDetailModel)getchar.Current;
                if (GetMonsterOfPlayerDetailModel != null)
                {
                    CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(GetMonsterOfPlayerDetailModel);
                    multiDictionary.Set(0,(int)PosNumsWithLocalKeys[i].posNum,characterDataInfo);
                }
            }
        }
        yield return multiDictionary;
    }
    
    public void setPosMemInfoByLocalID(PosNum posNum,string monsterofplayerid)
    {
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            if (PosNumsWithLocalKeys[i].posNum == posNum)
            {
                PosNumsWithLocalKeys[i].monsterOfPlayerId = monsterofplayerid;
                return;
            }
        }
        Debug.Log("没找着对应的位置键："+posNum);
    }
    
    public List<PosNumWithLocalKey> setPosMemInfoByLocalIDConservationMode(PosNum targetPos,string monsterlocalID)// 返回长度为2时，第一个元素是目标位置，第二个元素是被替换位置
    {
        if (targetPos == PosNum.none)
            return new List<PosNumWithLocalKey>();
        bool inTeamMemberChange = false;
                
        foreach (PosNumWithLocalKey _Set in PosNumsWithLocalKeys)
        {
            if (_Set.monsterOfPlayerId == monsterlocalID)
            {
                if (_Set.posNum != targetPos)
                {
                    inTeamMemberChange = true;
                    changePosition(targetPos, _Set.posNum);
                    return new List<PosNumWithLocalKey> {getPosMemInfo(targetPos), _Set};
                }
                else
                {
                    //那其实也就是点击了下原位置角色的头像
                }
            }
        }
        if (!inTeamMemberChange)
        {
            setPosMemInfoByLocalID(targetPos, monsterlocalID);
            return new List<PosNumWithLocalKey> {getPosMemInfo(targetPos)};
        }return new List<PosNumWithLocalKey>();
    }
    
    public PosNumWithLocalKey getPosMemInfoByLocalID(string localID)
    {
        if (this.PosNumsWithLocalKeys == null)
            return null;
        foreach (PosNumWithLocalKey _set in this.PosNumsWithLocalKeys)
        {
            if (_set.monsterOfPlayerId != null)
            {
                if (_set.monsterOfPlayerId == localID)
                    return _set;
            }
        }
        return null;
    }

    public PosNumWithLocalKey getPosMemInfo(PosNum PosNum)
    {
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            if (PosNumsWithLocalKeys[i].posNum == PosNum)
                return PosNumsWithLocalKeys[i];
        }
        return null;
    }

    public string getPositionMonsterOfPlayerId(PosNum PosNum)
    {
        if (getPosMemInfo(PosNum) != null)
        {
            if (getPosMemInfo(PosNum).monsterOfPlayerId != null)
                return getPosMemInfo(PosNum).monsterOfPlayerId;
            else
                return null;
        }
        else
            return null;
    }
    
    public List<string> getAllOnSetMonsterOfPlayerIds()
    {
        List<string> onsetMonsterOfPlayerIds = new List<string>();
        foreach (PosNumWithLocalKey _Set in PosNumsWithLocalKeys)
        {
            if (_Set.monsterOfPlayerId != null)
                onsetMonsterOfPlayerIds.Add(_Set.monsterOfPlayerId);
        }
        return onsetMonsterOfPlayerIds;
    }

    public void changePosition(PosNum position1, PosNum position2)
    {
        PosNumWithLocalKey PosNumWithLocalKey1 = getPosMemInfo(position1);
        PosNumWithLocalKey PosNumWithLocalKey2 = getPosMemInfo(position2);

        string temp = PosNumWithLocalKey2.monsterOfPlayerId;
        PosNumWithLocalKey2.monsterOfPlayerId = PosNumWithLocalKey1.monsterOfPlayerId;
        PosNumWithLocalKey1.monsterOfPlayerId = temp;
    }
    
    public static void changePositionBetweenDifferentTeamSets(PosNum position1,positionLocalCharKeySet team1, PosNum position2,positionLocalCharKeySet team2)
    {
        PosNumWithLocalKey PosNumWithLocalKey1 = team1.getPosMemInfo(position1);
        PosNumWithLocalKey PosNumWithLocalKey2 = team2.getPosMemInfo(position2);
        string temp = PosNumWithLocalKey2.monsterOfPlayerId;
        PosNumWithLocalKey2.monsterOfPlayerId = PosNumWithLocalKey1.monsterOfPlayerId;
        PosNumWithLocalKey1.monsterOfPlayerId = temp;

        Debug.Log(PosNumWithLocalKey1.monsterOfPlayerId+"and "+ PosNumWithLocalKey2.monsterOfPlayerId);
    }
}

[System.Serializable]
public class PosNumWithLocalKey
{
    public string monsterOfPlayerId;
    public PosNum posNum;
    
    public PosNumWithLocalKey()
    {
    }

    public PosNumWithLocalKey(PosNum posNum, string monsterOfPlayerId)
    {
        this.posNum = posNum;
        this.monsterOfPlayerId = monsterOfPlayerId;
    }
    
    //public positionLocalCharKeySet getPositionLocalCharKeySet()
    //{
    //    positionLocalCharKeySet positionLocalCharKeySet = new positionLocalCharKeySet();
    //    positionLocalCharKeySet.getPosMemInfo(PosNum.back).monsterOfPlayerId = (bMonsterOfPlayerId);
    //    positionLocalCharKeySet.getPosMemInfo(PosNum.front).monsterOfPlayerId =(frontLocalID);
    //    positionLocalCharKeySet.getPosMemInfo(PosNum.left).monsterOfPlayerId = (fMonsterOfPlayerId);
    //    positionLocalCharKeySet.getPosMemInfo(PosNum.right).monsterOfPlayerId = (rightLocalID);
    //    return positionLocalCharKeySet;
    //}
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