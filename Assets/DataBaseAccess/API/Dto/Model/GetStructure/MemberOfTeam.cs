using System.Collections.Generic;
using UnityEngine;

namespace Api.Dto.Model
{
    // 这个信息组成了一个角色与其对应的技能   
    public class MemberOfTeam
    {
        readonly MonsterOfPlayerDetailModel MonsterInfo;
        readonly List<SkillStoneOfPlayerInfoModel> stones;
        
        public CharDataInfo ToCharDataInfo()
        {
            CharDataInfo charData = new CharDataInfo
            {
                ResourceID = MonsterInfo.monsterId,
                monsterOfPlayerId = MonsterInfo.monsterOfPlayerId
            };
            List<SkillStoneOfPlayerInfoModel> targets = stones;
            NineAndTwo nineAndTwo = new NineAndTwo();
            CharConfig _CharConfigInfo = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(charData.ResourceID));
            if (_CharConfigInfo == null)
            {
                Debug.Log("角色定义信息错误。monsterId：" + charData.ResourceID);
                return null;
            }
            for (int i = 0; i < targets.Count; i++)
            {
                switch(targets[i].inUsingSkillSlot)
                {
                    case "1":
                        nineAndTwo.A1skillid = targets[i].skillId;
                        nineAndTwo.A1level = targets[i].GetLevel();
                    break;
                    case "2":
                        nineAndTwo.A2skillid = targets[i].skillId;
                        nineAndTwo.A2level = targets[i].GetLevel();
                    break;
                    case "3":
                        nineAndTwo.A3skillid = targets[i].skillId;
                        nineAndTwo.A3level = targets[i].GetLevel();
                    break;
                    case "4":
                        nineAndTwo.B1skillid = targets[i].skillId;
                        nineAndTwo.B1level = targets[i].GetLevel();
                    break;
                    case "5":
                        nineAndTwo.B2skillid = targets[i].skillId;
                        nineAndTwo.B2level = targets[i].GetLevel();
                    break;
                    case "6":
                        nineAndTwo.B3skillid = targets[i].skillId;
                        nineAndTwo.B3level = targets[i].GetLevel();
                    break;
                    case "7":
                        nineAndTwo.C1skillid = targets[i].skillId;
                        nineAndTwo.C1level = targets[i].GetLevel();
                    break;
                    case "8":
                        nineAndTwo.C2skillid = targets[i].skillId;
                        nineAndTwo.C2level = targets[i].GetLevel();
                    break;
                    case "9":
                        nineAndTwo.C3skillid = targets[i].skillId;
                        nineAndTwo.C3level = targets[i].GetLevel();
                    break;
                }
            }
            nineAndTwo.moveType = _CharConfigInfo.MoveType;
            nineAndTwo.rushType = _CharConfigInfo.RushType;
            nineAndTwo.canDefend = _CharConfigInfo.DEFENDABLE_FLAG;
            charData._NineAndTwo = nineAndTwo;
            charData._NineAndTwo.SortNineAndTwo();
            return charData;
        }
    }
}