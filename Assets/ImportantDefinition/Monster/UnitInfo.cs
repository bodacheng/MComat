using dataAccess;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UnitInfo
{
    public string id;
    public string r_id;
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

    public UnitInfo(string localID, string ResourceID,SkillSet _NineAndTwo)
    {
        id = localID;
        this.r_id = ResourceID;
        this.set = _NineAndTwo;
    }

    public static UnitInfo GetUnitInfo(UnitInfo accUnitInfo)
    {
        try
        {
            global::UnitInfo unitInfo = new global::UnitInfo
            {
                r_id = accUnitInfo.r_id,
                id = accUnitInfo.id
            };

            List<StoneOfPlayerInfo> targets = Stones.GetEquipingStones(accUnitInfo.id);
            SkillSet set = new SkillSet();
            CharConfig _CharConfigInfo = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(accUnitInfo.r_id));
            if (_CharConfigInfo == null)
            {
                Debug.Log("角色定义信息错误。monsterId：" + accUnitInfo.r_id);
                return null;
            }
            for (int i = 0; i < targets.Count; i++)
            {
                switch (targets[i].inUsingSkillSlot)
                {
                    case "1":
                        set.a1 = targets[i].skillId;
                        set.A1lv = targets[i].GetLevel();
                        break;
                    case "2":
                        set.a2 = targets[i].skillId;
                        set.A2lv = targets[i].GetLevel();
                        break;
                    case "3":
                        set.a3 = targets[i].skillId;
                        set.A3lv = targets[i].GetLevel();
                        break;
                    case "4":
                        set.b1 = targets[i].skillId;
                        set.B1lv = targets[i].GetLevel();
                        break;
                    case "5":
                        set.b2 = targets[i].skillId;
                        set.B2lv = targets[i].GetLevel();
                        break;
                    case "6":
                        set.b3 = targets[i].skillId;
                        set.B3lv = targets[i].GetLevel();
                        break;
                    case "7":
                        set.c1 = targets[i].skillId;
                        set.C1lv = targets[i].GetLevel();
                        break;
                    case "8":
                        set.c2 = targets[i].skillId;
                        set.C2lv = targets[i].GetLevel();
                        break;
                    case "9":
                        set.c3 = targets[i].skillId;
                        set.C3lv = targets[i].GetLevel();
                        break;
                }
            }
            set.SetPassive(_CharConfigInfo.DEFENDABLE_FLAG, _CharConfigInfo.MoveType, _CharConfigInfo.RushType);
            unitInfo.set = set;
            unitInfo.set.SortNineAndTwo();
            return unitInfo;
        }
        catch (Exception e)
        {
            Debug.Log("数据库信息有错误:" + e);
            return null;
        }
    }

    // 这个是从角色存档来读取
    public int GetNineSlotWholePointOfMonster(string monsterOfPlayerId)
    {
        List<StoneOfPlayerInfo> equipingstones = Stones.GetEquipingStones(monsterOfPlayerId);
        string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
        for (int i = 0; i < equipingstones.Count; i++)
        {
            switch (equipingstones[i].inUsingSkillSlot)
            {
                case "1":
                    A1 = equipingstones[i].skillId;
                    break;
                case "2":
                    A2 = equipingstones[i].skillId;
                    break;
                case "3":
                    A3 = equipingstones[i].skillId;
                    break;
                case "4":
                    B1 = equipingstones[i].skillId;
                    break;
                case "5":
                    B2 = equipingstones[i].skillId;
                    break;
                case "6":
                    B3 = equipingstones[i].skillId;
                    break;
                case "7":
                    C1 = equipingstones[i].skillId;
                    break;
                case "8":
                    C2 = equipingstones[i].skillId;
                    break;
                case "9":
                    C3 = equipingstones[i].skillId;
                    break;
            }
        }
        int wholePoint = SkillSet.SkillBalancePoint(A1, A2, A3, B1, B2, B3, C1, C2, C3);
        return wholePoint;
    }
}