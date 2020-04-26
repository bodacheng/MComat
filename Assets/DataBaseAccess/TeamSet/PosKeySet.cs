using System.Collections;
using Api.Dto.Model;
using dataAccess;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PosKeySet
{
    public string recordId;
    public PosNumWithLocalKey[] PosNumsWithLocalKeys = {};
    
    public PosKeySet()
    {
        PosNumsWithLocalKeys = new PosNumWithLocalKey[3] { new PosNumWithLocalKey(0, null), new PosNumWithLocalKey(1, null), new PosNumWithLocalKey(2, null) };
    }
    
    public IEnumerator LoadTeamBasedOnAccountInfo()
    {
        MultiDictionary<int, int, CharDataInfo> multiDictionary = new MultiDictionary<int, int, CharDataInfo>();
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            if (PosNumsWithLocalKeys[i].monsterOfPlayerId != null)
            {
                GetMonsterOfPlayerDetailModel GetMonsterOfPlayerDetailModel = AccountCharsSet.Get(PosNumsWithLocalKeys[i].monsterOfPlayerId);
                if (GetMonsterOfPlayerDetailModel != null)
                {
                    CharDataInfo characterDataInfo = GetMonsterOfPlayerDetailModel.GetCharDataInfo(GetMonsterOfPlayerDetailModel);
                    multiDictionary.Set(0, PosNumsWithLocalKeys[i].posNum, characterDataInfo);
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
                Debug.Log(this + ":" + monsterofplayerid);
                PosNumsWithLocalKeys[i].monsterOfPlayerId = monsterofplayerid;
                return;
            }
        }
        Debug.Log("没找着对应的位置键："+posNum);
    }
    
    public List<PosNumWithLocalKey> SetPosMemByMonsterOfPlayerID(int targetPos,string monsterlocalID)// 返回长度为2时，第一个元素是目标位置，第二个元素是被替换位置
    {
        if (targetPos == -1)
            return new List<PosNumWithLocalKey>();
        bool inTeamMemberChange = false;
        
        foreach (PosNumWithLocalKey _Set in PosNumsWithLocalKeys)
        {
            if (_Set.monsterOfPlayerId == monsterlocalID && _Set.monsterOfPlayerId != null)
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
        }
        return new List<PosNumWithLocalKey>();
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
    
    public string GetMonsterOfPlayerIdOnPos(int PosNum)
    {
        return GetPosMemInfo(PosNum) != null ? GetPosMemInfo(PosNum).monsterOfPlayerId ?? null : null;
    }
    
    public void ChangePosition(int position1, int position2)
    {
        PosNumWithLocalKey PosNumWithLocalKey1 = GetPosMemInfo(position1);
        PosNumWithLocalKey PosNumWithLocalKey2 = GetPosMemInfo(position2);
        
        string temp = PosNumWithLocalKey2.monsterOfPlayerId;
        PosNumWithLocalKey2.monsterOfPlayerId = PosNumWithLocalKey1.monsterOfPlayerId;
        PosNumWithLocalKey1.monsterOfPlayerId = temp;
    }
    
    // 暂时不再使用。最初是selffight模式下队员要求不重复的队员指定模式
    public static void ChangePositionBetweenDifferentTeamSets(int position1, PosKeySet team1, int position2, PosKeySet team2)
    {
        PosNumWithLocalKey PosNumWithLocalKey1 = team1.GetPosMemInfo(position1);
        PosNumWithLocalKey PosNumWithLocalKey2 = team2.GetPosMemInfo(position2);
        string temp = PosNumWithLocalKey2.monsterOfPlayerId;
        PosNumWithLocalKey2.monsterOfPlayerId = PosNumWithLocalKey1.monsterOfPlayerId;
        PosNumWithLocalKey1.monsterOfPlayerId = temp;
    }
    
    // 暂时不再使用。最初是selffight模式下队员要求不重复的队员指定模式
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
    
    // 暂时不再使用。最初是selffight模式下队员要求不重复的队员指定模式
    public PosNumWithLocalKey GetPosMemInfoByLocalID(string localID)
    {
        if (PosNumsWithLocalKeys == null)
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
}