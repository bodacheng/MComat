using System.Collections.Generic;
using UnityEngine;

namespace Api.Dto.Model
{
    // 这个信息组成了一个角色与其对应的技能   
    public class MemberOfTeam
    {
        readonly MonsterOfPlayerInfo MonsterInfo;
        readonly List<StoneOfPlayerInfo> stones;
        
        public CharDataInfo ToCharDataInfo()
        {
            CharDataInfo charData = new CharDataInfo
            {
                r_id = MonsterInfo.monsterId,
                id = MonsterInfo.InstanceId
            };
            List<StoneOfPlayerInfo> targets = stones;
            NineAndTwo nineAndTwo = new NineAndTwo();
            CharConfig _CharConfigInfo = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(charData.r_id));
            if (_CharConfigInfo == null)
            {
                Debug.Log("角色定义信息错误。monsterId：" + charData.r_id);
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
            nineAndTwo.SetPassive(_CharConfigInfo.DEFENDABLE_FLAG, _CharConfigInfo.MoveType, _CharConfigInfo.RushType);
            charData.set = nineAndTwo;
            charData.set.SortNineAndTwo();
            return charData;
        }
    }
}