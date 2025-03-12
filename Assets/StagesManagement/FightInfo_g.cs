using System.Collections.Generic;
using System;
using UnityEngine;

public partial class FightInfo : ScriptableObject
{
    private List<SoldierGroupSet> team1GroupSet = new List<SoldierGroupSet>();
    [SerializeField] private List<SoldierGroupSet> team2GroupSet = new List<SoldierGroupSet>();
    
    public List<SoldierGroupSet> Team1GroupSet
    {
        get => team1GroupSet;
        set => team1GroupSet = value;
    }

    public List<string> GetNonZeroInstanceIds(int team)
    {
        var sets = team == 1 ? team1GroupSet : team2GroupSet;
        List<string> returnValue = new List<string>();
        foreach (var set in sets)
        {
            if (set.Count > 0)
            {
                returnValue.Add(set.id);
            }
        }
        return returnValue;
    }
    
    public List<SoldierGroupSet> Team2GroupSet
    {
        get => team2GroupSet;
        set => team2GroupSet = value;
    }

    public int SetTeamUnitCount(int team, string instanceID, int count, int teamMaxCount)
    {
        if (count < 0) { count = 0; }
        var set = GetSoldierGroupSet(instanceID, team);
        var ifWholeCount = GetIfGroupWholeUnitCount(team, instanceID, count);
        if (ifWholeCount <= teamMaxCount)
        {
            set.Count = count;
        }
        else
        {
            set.Count = Mathf.Clamp((count - (ifWholeCount - teamMaxCount)), 0, Int32.MaxValue);
        }
        return GetGroupWholeUnitCount(team);
    }

    public int GetTeamUnitCount(int team, string instanceID, bool useLocalData = false)
    {
        var set = GetSoldierGroupSet(instanceID, team, useLocalData);
        return set.Count;
    }
    
    // Start is called before the first frame update
    [Serializable]
    public class SoldierGroupSet
    {
        public SoldierGroupSet(string id, int count)
        {
            this.id = id;
            this.Count = count;
            OriginCount = count;
        }
        
        public string id;
        public int Count = 1;
        public int OriginCount { get; set; }
    }
    
    SoldierGroupSet GetTeam1GroupSet(string id)
    {
        return GetSoldierGroupSet(id, 1);
    }
    
    public SoldierGroupSet GetTeam2GroupSet(string id)
    {
        return GetSoldierGroupSet(id, 2);
    }
    
    SoldierGroupSet GetSoldierGroupSet(string id, int team, bool useLocalSet = false)
    {
        var sets = team == 1 ? team1GroupSet : team2GroupSet;
        var s = sets.Find(x => x.id == id);
        if (s == null)
        {
            s = new SoldierGroupSet(id, useLocalSet ? PlayerPrefs.GetInt("gangbangPos"+ id, 8) : 8);
            sets.Add(s);
            return s;
        }
        return s;
    }

    public void ClearWholeUnitCount(int team)
    {
        var sets = team == 1 ? team1GroupSet : team2GroupSet;
        foreach (var set in sets)
        {
            set.Count = 0;
        }
    }
    
    public int GetGroupWholeUnitCount(int team)
    {
        var sets = team == 1 ? team1GroupSet : team2GroupSet;
        int wholeUnitCount = 0;
        foreach (var set in sets)
        {
            wholeUnitCount += set.Count;
        }
        return wholeUnitCount;
    }

    int GetIfGroupWholeUnitCount(int team, string instanceID, int count)
    {
        var sets = team == 1 ? team1GroupSet : team2GroupSet;
        int wholeUnitCount = 0;
        foreach (var set in sets)
        {
            if (set.id != instanceID)
                wholeUnitCount += set.Count;
            else
                wholeUnitCount += count;
        }
        return wholeUnitCount;
    }
    
    public void ConvertTeamToGangbang()
    {
        var id = 0;
        var newHeroSets = new MultiDic<int, int, UnitInfo>();
        foreach (var unitInfo in this.FightMembers.HeroSets.GetValues())
        {
            var soldierSet = GetTeam1GroupSet(unitInfo.id);
            for (var i = 0; i < soldierSet.Count; i++)
            {
                var newUnitInfo = unitInfo.DeepCopy();
                newUnitInfo.id = id.ToString();
                newHeroSets.Set(0,id, newUnitInfo);
                id++;
            }
        }
        this.FightMembers.HeroSets = newHeroSets;
        
        id = 0;
        var newEnemySets = new MultiDic<int, int, UnitInfo>();
        foreach (var unitInfo in this.FightMembers.EnemySets.GetValues())
        {
            var soldierSet = GetTeam2GroupSet(unitInfo.id);
            for (var i = 0; i < soldierSet.Count; i++)
            {
                var newUnitInfo = unitInfo.DeepCopy();
                newUnitInfo.id = id.ToString();
                newEnemySets.Set(0,id, newUnitInfo);
                id++;
            }
        }
        this.FightMembers.EnemySets = newEnemySets;
    }
    
    public int GangbangAutoAdjustTeamUnitByMaxCount(int team, List<UnitInfo> unitSets, int selectedMaxTeamCount, bool adaptMode = false)
    {
        ClearWholeUnitCount(team);
        int wholeTeamCount = 0;
        foreach(var unitInfo in unitSets)
        {
            wholeTeamCount += GetTeamUnitCount(team, unitInfo.id);
        }
        
        if (adaptMode)
        {
            foreach(var unitInfo in unitSets)
            {
                wholeTeamCount = SetTeamUnitCount(team, unitInfo.id, 0, selectedMaxTeamCount);
            }
            // reset to origin first;
            if (team == 2)
            {
                foreach(var unitInfo in unitSets)
                {
                    var set = GetSoldierGroupSet(unitInfo.id, team);
                    wholeTeamCount = SetTeamUnitCount(team, unitInfo.id, set.OriginCount, selectedMaxTeamCount);
                }
            }
            
            var toBeAdd = selectedMaxTeamCount - wholeTeamCount;
            for (var index = 0; index < unitSets.Count; index++)
            {
                if (toBeAdd > 0)
                {
                    var unitInfo = unitSets[index];
                    var addCount = (index != unitSets.Count - 1) ? (int)((float)toBeAdd / unitSets.Count) : toBeAdd;
                    wholeTeamCount = SetTeamUnitCount(team, unitInfo.id,  GetTeamUnitCount(team, unitInfo.id) + addCount, selectedMaxTeamCount);
                    toBeAdd = selectedMaxTeamCount - wholeTeamCount;
                }
            }
        }
        
        return wholeTeamCount;
    }
}
