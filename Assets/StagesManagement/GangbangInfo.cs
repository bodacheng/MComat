using System.Collections.Generic;
using UnityEngine;
using System;

public class GangbangInfo : FightInfo
{ 
    private readonly List<SoldierGroupSet> team1GroupSet = new List<SoldierGroupSet>();
    [SerializeField] private List<SoldierGroupSet> team2GroupSet = new List<SoldierGroupSet>();
    
    int SetTeamUnitCount(int team, string instanceID, int count)
    {
        var sets = team == 1 ? team1GroupSet : team2GroupSet;
        int ifWholeCount = GetIfGroupWholeUnitCount(team, instanceID, count);
        if (ifWholeCount <= 30)
        {
            var set = GetSoldierGroupSet(instanceID, team);
            set.Count = count;
        }
        return GetGroupWholeUnitCount(team);
    }
    
    
    // Start is called before the first frame update
    [Serializable]
    public class SoldierGroupSet
    {
        public SoldierGroupSet(string id, int count)
        {
            this.id = id;
            this.Count = count;
        }

        public string id;
        public int Count = 3;
    }
    
    public SoldierGroupSet GetTeam1GroupSet(string id)
    {
        return GetSoldierGroupSet(id, 1);
    }
    
    public SoldierGroupSet GetTeam2GroupSet(string id)
    {
        return GetSoldierGroupSet(id, 2);
    }
    
    SoldierGroupSet GetSoldierGroupSet(string id, int team)
    {
        List<SoldierGroupSet> set = team == 1 ? team1GroupSet : team2GroupSet;
        var s = set.Find(x => x.id == id);
        if (s == null)
        {
            s = new SoldierGroupSet(id, 1);
            set.Add(s);
            return s;
        }
        return s;
    }

    int GetGroupWholeUnitCount(int team)
    {
        List<SoldierGroupSet> sets = team == 1 ? team1GroupSet : team2GroupSet;
        int count = 0;
        foreach (var set in sets)
        {
            count += set.Count;
        }

        return count;
    }

    int GetIfGroupWholeUnitCount(int team, string instanceID, int count)
    {
        List<SoldierGroupSet> sets = team == 1 ? team1GroupSet : team2GroupSet;
        int wholeCount = 0;
        foreach (var set in sets)
        {
            if (set.id != instanceID)
                wholeCount += set.Count;
            else
                wholeCount += count;
        }

        return wholeCount;
    }

    // 获取的是新instance
    public FightInfo ConvertToFightInfo()
    {
        var newInfo = FightInfo.Copy(this);
        var unitsData = new List<UnitInfo>();
        int id = 0;
        foreach (var unitInfo in UnitsData)
        {
            var solderGroupSet = GetTeam2GroupSet(unitInfo.id);
            for (var i = 0; i < solderGroupSet.Count; i++)
            {
                var newUnitInfo = unitInfo.DeepCopy();
                newUnitInfo.id = id.ToString();
                unitsData.Add(newUnitInfo);
                id++;
            }
        }
        newInfo.UnitsData = unitsData;
        newInfo.Open();
        return newInfo;
    }
}
