using System.Collections;
using Api.Dto.Model;
using dataAccess;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PosKeySet
{
    public PosNumWithLocalKey[] PosNumsWithLocalKeys = {};
    
    public PosKeySet()
    {
        PosNumsWithLocalKeys = new PosNumWithLocalKey[] { new PosNumWithLocalKey(0, null), new PosNumWithLocalKey(1, null), new PosNumWithLocalKey(2, null) };
    }
    
    public TeamPos ToTeamPos()
    {
        TeamPos model = new TeamPos();
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            switch (PosNumsWithLocalKeys[i].posNum)
            {
                case 1:
                    model.l = PosNumsWithLocalKeys[i].instanceID;
                break;
                case 2:
                    model.r = PosNumsWithLocalKeys[i].instanceID;
                break;
                case 0:
                    model.f = PosNumsWithLocalKeys[i].instanceID;
                break;
            }
        }
        return model;
    }
    
    public MultiDictionary<int, int, CharDataInfo> LoadTeamDic()
    {
        MultiDictionary<int, int, CharDataInfo> multiDictionary = new MultiDictionary<int, int, CharDataInfo>();
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            if (PosNumsWithLocalKeys[i].instanceID != null)
            {
                MonsterOfPlayerInfo GetMonsterOfPlayerDetailModel = MyMonsters.Get(PosNumsWithLocalKeys[i].instanceID);
                if (GetMonsterOfPlayerDetailModel != null)
                {
                    CharDataInfo charDataInfo = MonsterOfPlayerInfo.GetCharDataInfo(GetMonsterOfPlayerDetailModel);
                    multiDictionary.Set(0, PosNumsWithLocalKeys[i].posNum, charDataInfo);
                }
            }
        }
        return multiDictionary;
    }
    
    public void SetPosMemInfoByLocalID(int posNum, string monsterofplayerid)
    {
        for (int i = 0; i < PosNumsWithLocalKeys.Length; i++)
        {
            if (PosNumsWithLocalKeys[i].posNum == posNum)
            {
                Debug.Log(this + ":" + monsterofplayerid);
                PosNumsWithLocalKeys[i].instanceID = monsterofplayerid;
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
            if (_Set.instanceID == monsterlocalID && _Set.instanceID != null)
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
        return GetPosMemInfo(PosNum) != null ? GetPosMemInfo(PosNum).instanceID ?? null : null;
    }
    
    public void ChangePosition(int position1, int position2)
    {
        PosNumWithLocalKey PosNumWithLocalKey1 = GetPosMemInfo(position1);
        PosNumWithLocalKey PosNumWithLocalKey2 = GetPosMemInfo(position2);
        
        string temp = PosNumWithLocalKey2.instanceID;
        PosNumWithLocalKey2.instanceID = PosNumWithLocalKey1.instanceID;
        PosNumWithLocalKey1.instanceID = temp;
    }
    
    // 暂时不再使用。最初是selffight模式下队员要求不重复的队员指定模式
    public static void ChangePositionBetweenDifferentTeamSets(int position1, PosKeySet team1, int position2, PosKeySet team2)
    {
        PosNumWithLocalKey PosNumWithLocalKey1 = team1.GetPosMemInfo(position1);
        PosNumWithLocalKey PosNumWithLocalKey2 = team2.GetPosMemInfo(position2);
        string temp = PosNumWithLocalKey2.instanceID;
        PosNumWithLocalKey2.instanceID = PosNumWithLocalKey1.instanceID;
        PosNumWithLocalKey1.instanceID = temp;
    }
    
    // 暂时不再使用。最初是selffight模式下队员要求不重复的队员指定模式
    public List<string> GetAllOnSetMonsterOfPlayerIds()
    {
        List<string> onsetMonsterOfPlayerIds = new List<string>();
        foreach (PosNumWithLocalKey _Set in PosNumsWithLocalKeys)
        {
            if (_Set.instanceID != null)
                onsetMonsterOfPlayerIds.Add(_Set.instanceID);
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
            if (_set.instanceID != null)
            {
                if (_set.instanceID == localID)
                    return _set;
            }
        }
        return null;
    }
}

[System.Serializable]
public class PosNumWithLocalKey
{
    public string instanceID;
    public int posNum;
    
    public PosNumWithLocalKey()
    {
    }

    public PosNumWithLocalKey(int posNum, string monsterOfPlayerId)
    {
        this.posNum = posNum;
        this.instanceID = monsterOfPlayerId;
    }
}