using System.Collections.Generic;
using Api.Dto.Model;
using dataAccess;
using UnityEngine;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        // 基于当前九宫格对技能编辑进行合法判断 包括首发技能检测
        public NineAndTwo.SkillEditError CheckEditBasedOnCurrent()
        {
            List<string> nineskillids = target.GetCurrentNineSlotAllSkillIds();
            return NineAndTwo.CheckEdit(nineskillids[0], nineskillids[1], nineskillids[2], 
                                        nineskillids[3], nineskillids[4], nineskillids[5],
                                        nineskillids[6], nineskillids[7], nineskillids[8]);
        }
        
        // 基于角色存档对技能编辑进行合法判断. 必须接受完整validation检测
        public NineAndTwo.SkillEditError CheckEditAfterOneStoneRemoved(string monsterOfPlayerId, string SkillID)
        {
            List<StoneOfPlayerInfo> equipingstones = MySkillStones.GetEquipingStones(monsterOfPlayerId);
            string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "2":
                        A2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "3":
                        A3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "4":
                        B1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "5":
                        B2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "6":
                        B3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "7":
                        C1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "8":
                        C2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "9":
                        C3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                }
            }
            return NineAndTwo.CheckEdit(A1, A2, A3, B1, B2, B3, C1, C2, C3);
        }
    }
}