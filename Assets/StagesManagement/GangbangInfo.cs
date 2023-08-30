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
        public int Count;
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

    public FightInfo ConvertToFightInfo()
    {
        var newInfo = new FightInfo();
        int id = 0;
        foreach (var unitInfo in UnitsData)
        {
            var solderGroupSet = Get(unitInfo.id);
            for (var i = 0; i < solderGroupSet.Count; i++)
            {
                var newUnitInfo = unitInfo.DeepCopy();
                newUnitInfo.id = id.ToString();
                newInfo.UnitsData.Add(newUnitInfo);
                id++;
            }
        }
        return newInfo;
    }
}
