using System.Collections;
using Api.Dto.Model;
using dataAccess;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionLocalCharKeySet
{
    public string recordId;
    public PosNumWithLocalKey[] PosNumsWithLocalKeys = {};
    
    public PositionLocalCharKeySet(PosNumWithLocalKey[] PosNumsWithLocalKey)
    {
        this.PosNumsWithLocalKeys = PosNumsWithLocalKey;
    }
    public PositionLocalCharKeySet()
    {
        PosNumsWithLocalKeys = new PosNumWithLocalKey[10] { new PosNumWithLocalKey(0, null), 
                                                            new PosNumWithLocalKey(1, null), 
                                                            new PosNumWithLocalKey(2, null),
                                                            new PosNumWithLocalKey(3, null),
                                                            new PosNumWithLocalKey(4, null),
                                                            new PosNumWithLocalKey(5, null),
                                                            new PosNumWithLocalKey(6, null),
                                                            new PosNumWithLocalKey(7, null),
                                                            new PosNumWithLocalKey(8, null),
                                                            new PosNumWithLocalKey(9, null) };
    }
    
    public IEnumerator ConvertToMultiDictionary()
    {
        MultiDictionary<int, int, CharacterDataInfo> multiDictionary = new MultiDictionary<int, int, CharacterDataInfo>();
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            if (PosNumsWithLocalKeys[i].monsterOfPlayerId != null)
            {
                IEnumerator getchar = AccountCharsSet.Instance.GetAccountCharacterInfo(PosNumsWithLocalKeys[i].monsterOfPlayerId);
                yield return getchar;
                GetMonsterOfPlayerDetailModel GetMonsterOfPlayerDetailModel = (GetMonsterOfPlayerDetailModel)getchar.Current;
                if (GetMonsterOfPlayerDetailModel != null)
                {
                    CharacterDataInfo characterDataInfo = RemoteAccess.GetCharacterDataInfo(GetMonsterOfPlayerDetailModel);
                    multiDictionary.Set(0,(int)PosNumsWithLocalKeys[i].posNum,characterDataInfo);
                }
            }
        }
        yield return multiDictionary;
    }
    
    public void SetPosMemInfoByLocalID(int posNum,string monsterofplayerid)
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
    
    public List<PosNumWithLocalKey> SetPosMemInfoByLocalIDConservationMode(int targetPos,string monsterlocalID)// 返回长度为2时，第一个元素是目标位置，第二个元素是被替换位置
    {
        if (targetPos == -1)
            return new List<PosNumWithLocalKey>();
        bool inTeamMemberChange = false;
                
        foreach (PosNumWithLocalKey _Set in PosNumsWithLocalKeys)
        {
            if (_Set.monsterOfPlayerId == monsterlocalID)
            {
                if (_Set.posNum != targetPos)
                {
                    inTeamMemberChange = true;
                    ChangePosition(targetPos, _Set.posNum);
                    return new List<PosNumWithLocalKey> {GetPosMemInfo(targetPos), _Set};
                }
                else
                {
                    //那其实也就是点击了下原位置角色的头像
                }
            }
        }
        if (!inTeamMemberChange)
        {
            SetPosMemInfoByLocalID(targetPos, monsterlocalID);
            return new List<PosNumWithLocalKey> {GetPosMemInfo(targetPos)};
        }return new List<PosNumWithLocalKey>();
    }
    
    public PosNumWithLocalKey GetPosMemInfoByLocalID(string localID)
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

    public PosNumWithLocalKey GetPosMemInfo(int PosNum)
    {
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            if (PosNumsWithLocalKeys[i].posNum == PosNum)
                return PosNumsWithLocalKeys[i];
        }
        return null;
    }

    public string GetPositionMonsterOfPlayerId(int PosNum)
    {
        return GetPosMemInfo(PosNum) != null
            ? GetPosMemInfo(PosNum).monsterOfPlayerId ?? null
            : null;
    }
    
    public List<string> GetAllOnSetMonsterOfPlayerIds()
    {
        List<string> onsetMonsterOfPlayerIds = new List<string>();
        foreach (PosNumWithLocalKey _Set in PosNumsWithLocalKeys)
        {
            if (_Set.monsterOfPlayerId != null)
                onsetMonsterOfPlayerIds.Add(_Set.monsterOfPlayerId);
        }
        return onsetMonsterOfPlayerIds;
    }

    public void ChangePosition(int position1, int position2)
    {
        PosNumWithLocalKey PosNumWithLocalKey1 = GetPosMemInfo(position1);
        PosNumWithLocalKey PosNumWithLocalKey2 = GetPosMemInfo(position2);

        string temp = PosNumWithLocalKey2.monsterOfPlayerId;
        PosNumWithLocalKey2.monsterOfPlayerId = PosNumWithLocalKey1.monsterOfPlayerId;
        PosNumWithLocalKey1.monsterOfPlayerId = temp;
    }
    
    public static void ChangePositionBetweenDifferentTeamSets(int position1,PositionLocalCharKeySet team1, int position2,PositionLocalCharKeySet team2)
    {
        PosNumWithLocalKey PosNumWithLocalKey1 = team1.GetPosMemInfo(position1);
        PosNumWithLocalKey PosNumWithLocalKey2 = team2.GetPosMemInfo(position2);
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
    public int posNum;
    
    public PosNumWithLocalKey()
    {
    }

    public PosNumWithLocalKey(int posNum, string monsterOfPlayerId)
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

//[System.Serializable]
//public enum PosNum
//{
//    none = -1,
//    back = 0,
//    left = 1,
//    front = 2,
//    right = 3
//}