using System.Collections.Generic;
using UnityEngine;

namespace dataAccess
{
    // 这个信息组成了一个角色与其对应的技能   
    public class MemberOfTeam
    {
        readonly UnitInfo MonsterInfo;
        readonly List<StoneOfPlayerInfo> stones;
        
        public global::UnitInfo ToCharDataInfo()
        {
            global::UnitInfo unit = new global::UnitInfo
            {
                r_id = MonsterInfo.r_id,
                id = MonsterInfo.id
            };
            List<StoneOfPlayerInfo> targets = stones;
            SkillSet nineAndTwo = new SkillSet();
            UnitConfig unitConfigInfo = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(unit.r_id));
            if (unitConfigInfo == null)
            {
                Debug.Log("角色定义信息错误。monsterId：" + unit.r_id);
                return null;
            }
            for (int i = 0; i < targets.Count; i++)
            {
                switch(targets[i].inUsingSkillSlot)
                {
                    case "1":
                        nineAndTwo.a1 = targets[i].skillId;
                        nineAndTwo.A1lv = targets[i].GetLevel();
                    break;
                    case "2":
                        nineAndTwo.a2 = targets[i].skillId;
                        nineAndTwo.A2lv = targets[i].GetLevel();
                    break;
                    case "3":
                        nineAndTwo.a3 = targets[i].skillId;
                        nineAndTwo.A3lv = targets[i].GetLevel();
                    break;
                    case "4":
                        nineAndTwo.b1 = targets[i].skillId;
                        nineAndTwo.B1lv = targets[i].GetLevel();
                    break;
                    case "5":
                        nineAndTwo.b2 = targets[i].skillId;
                        nineAndTwo.B2lv = targets[i].GetLevel();
                    break;
                    case "6":
                        nineAndTwo.b3 = targets[i].skillId;
                        nineAndTwo.B3lv = targets[i].GetLevel();
                    break;
                    case "7":
                        nineAndTwo.c1 = targets[i].skillId;
                        nineAndTwo.C1lv = targets[i].GetLevel();
                    break;
                    case "8":
                        nineAndTwo.c2 = targets[i].skillId;
                        nineAndTwo.C2lv = targets[i].GetLevel();
                    break;
                    case "9":
                        nineAndTwo.c3 = targets[i].skillId;
                        nineAndTwo.C3lv = targets[i].GetLevel();
                    break;
                }
            }
            nineAndTwo.SetPassive(unitConfigInfo.DEFENDABLE_FLAG, unitConfigInfo.MoveType, unitConfigInfo.RushType);
            unit.set = nineAndTwo;
            unit.set.SortNineAndTwo();
            return unit;
        }
    }
}