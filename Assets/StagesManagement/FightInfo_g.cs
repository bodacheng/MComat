using System.Collections.Generic;
using System;
using MCombat.Shared.CombatGroup;
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
        return GroupUnitCountUtility.GetNonZeroIds(GetTeamGroupSets(team));
    }
    
    public List<SoldierGroupSet> Team2GroupSet
    {
        get => team2GroupSet;
        set => team2GroupSet = value;
    }

    public int SetTeamUnitCount(int team, string instanceID, int count, int teamMaxCount)
    {
        return GroupUnitCountUtility.SetTeamUnitCount(
            GetTeamGroupSets(team),
            instanceID,
            count,
            teamMaxCount,
            false,
            CreateSoldierGroupSet);
    }

    public int GetTeamUnitCount(int team, string instanceID, bool useLocalData = false)
    {
        var set = GetSoldierGroupSet(instanceID, team, useLocalData);
        return set.Count;
    }
    
    // Start is called before the first frame update
    [Serializable]
    public class SoldierGroupSet : IGroupUnitCountEntry
    {
        public SoldierGroupSet(string id, int count)
        {
            this.id = id;
            this.Count = count;
            OriginCount = count;
        }
        
        public string id;
        public string Id => id;
        public int Count = 1;
        public int OriginCount { get; set; }
        int IGroupUnitCountEntry.Count
        {
            get => Count;
            set => Count = value;
        }
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
        return GroupUnitCountUtility.GetOrCreate(
            GetTeamGroupSets(team),
            id,
            CreateSoldierGroupSet,
            useLocalSet ? PlayerPrefs.GetInt("gangbangPos"+ id, 8) : 8);
    }

    public void ClearWholeUnitCount(int team)
    {
        GroupUnitCountUtility.ClearWholeCount(GetTeamGroupSets(team));
    }
    
    public int GetGroupWholeUnitCount(int team)
    {
        return GroupUnitCountUtility.GetWholeCount(GetTeamGroupSets(team));
    }

    int GetIfGroupWholeUnitCount(int team, string instanceID, int count)
    {
        return GroupUnitCountUtility.GetWholeCountIfSet(GetTeamGroupSets(team), instanceID, count);
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
        return GroupUnitCountUtility.AutoAdjustTeamUnitByMaxCount(
            GetTeamGroupSets(team),
            unitSets,
            selectedMaxTeamCount,
            adaptMode,
            team == 2,
            unitInfo => unitInfo?.id,
            CreateSoldierGroupSet);
    }

    List<SoldierGroupSet> GetTeamGroupSets(int team)
    {
        return team == 1 ? team1GroupSet : team2GroupSet;
    }

    static SoldierGroupSet CreateSoldierGroupSet(string id, int count)
    {
        return new SoldierGroupSet(id, count);
    }
}
