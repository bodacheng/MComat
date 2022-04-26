using dataAccess;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UnitInfo
{
    public string id;
    public string r_id;
    public int level;
    public SkillSet set = new SkillSet();
    
    public UnitInfo Clone()
    {
        return (UnitInfo)MemberwiseClone();
    }

    public UnitInfo DeepCopy()
    {
        UnitInfo Copy = this.Clone();
        Copy.set = Copy.set.DeepCopy();
        return Copy;
    }

    public UnitInfo()
    {
    }

    public UnitInfo(string localID, string r_id, SkillSet skillSet)
    {
        id = localID;
        this.r_id = r_id;
        set = skillSet;
    }

    public static UnitInfo GetUnitInfo(UnitInfo info)
    {
        try
        {
            global::UnitInfo unitInfo = new global::UnitInfo
            {
                r_id = info.r_id,
                id = info.id
            };
            
            var targets = Stones.GetEquipingStones(info.id);
            var set = new SkillSet();
            var unitConfigInfo = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(info.r_id));
            if (unitConfigInfo == null)
            {
                Debug.Log("角色定义信息错误。monsterId：" + info.r_id);
                return null;
            }

            var levels = new List<float>();
            for (var i = 0; i < targets.Count; i++)
            {
                switch (targets[i].inUsingSkillSlot)
                {
                    case "1":
                        set.a1 = targets[i].skillId;
                        break;
                    case "2":
                        set.a2 = targets[i].skillId;
                        break;
                    case "3":
                        set.a3 = targets[i].skillId;
                        break;
                    case "4":
                        set.b1 = targets[i].skillId;
                        break;
                    case "5":
                        set.b2 = targets[i].skillId;
                        break;
                    case "6":
                        set.b3 = targets[i].skillId;
                        break;
                    case "7":
                        set.c1 = targets[i].skillId;
                        break;
                    case "8":
                        set.c2 = targets[i].skillId;
                        break;
                    case "9":
                        set.c3 = targets[i].skillId;
                        break;
                }
                levels.Add(targets[i].Level);
            }
            
            unitInfo.level = set.GetAerLevel(levels);
            set.SetPassive(unitConfigInfo.DEFENDABLE_FLAG, unitConfigInfo.MoveType, unitConfigInfo.RushType);
            unitInfo.set = set;
            unitInfo.set.SortNineAndTwo(unitInfo.level);
            return unitInfo;
        }
        catch (Exception e)
        {
            Debug.Log("数据库信息有错误:" + e);
            return null;
        }
    }

    // 这个是从角色存档来读取
    public int GetNineSlotWholePointOfMonster(string unit_instanceID)
    {
        var equipments = Stones.GetEquipingStones(unit_instanceID);
        string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
        for (var i = 0; i < equipments.Count; i++)
        {
            switch (equipments[i].inUsingSkillSlot)
            {
                case "1":
                    A1 = equipments[i].skillId;
                    break;
                case "2":
                    A2 = equipments[i].skillId;
                    break;
                case "3":
                    A3 = equipments[i].skillId;
                    break;
                case "4":
                    B1 = equipments[i].skillId;
                    break;
                case "5":
                    B2 = equipments[i].skillId;
                    break;
                case "6":
                    B3 = equipments[i].skillId;
                    break;
                case "7":
                    C1 = equipments[i].skillId;
                    break;
                case "8":
                    C2 = equipments[i].skillId;
                    break;
                case "9":
                    C3 = equipments[i].skillId;
                    break;
            }
        }
        int wholePoint = SkillSet.SkillBalancePoint(A1, A2, A3, B1, B2, B3, C1, C2, C3);
        return wholePoint;
    }
}