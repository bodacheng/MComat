using System.Collections.Generic;
using UnityEngine;
using System;

public class GangbangInfo : FightInfo
{
    [SerializeField] private List<SoldierGroupSet> soldierGroupSet = new List<SoldierGroupSet>();
    
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
    
    public SoldierGroupSet Get(string id)
    {
        var s = soldierGroupSet.Find(x => x.id == id);
        if (s == null)
        {
            s = new SoldierGroupSet(id, 1);
            soldierGroupSet.Add(s);
            return s;
        }
        return s;
    }

    // 获取的是新instance
    public FightInfo ConvertToFightInfo()
    {
        var newInfo = FightInfo.Copy(this);
        var unitsData = new List<UnitInfo>();
        int id = 0;
        foreach (var unitInfo in UnitsData)
        {
            var solderGroupSet = Get(unitInfo.id);
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
