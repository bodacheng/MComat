using dataAccess;
using UnityEngine;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        // 基于当前九宫格对技能编辑进行合法判断 包括首发技能检测
        public SkillSet.SkillEditError CheckEditBasedOnCurrent()
        {
            var nineSkillIds = GetCurrentNineSlotAllSkillIds();
            return SkillSet.CheckEdit(nineSkillIds[0], nineSkillIds[1], nineSkillIds[2], 
                                        nineSkillIds[3], nineSkillIds[4], nineSkillIds[5],
                                        nineSkillIds[6], nineSkillIds[7], nineSkillIds[8]);
        }
        
        // 基于角色存档对技能编辑进行合法判断. 必须接受完整validation检测
        public SkillSet.SkillEditError CheckEditAfterOneStoneRemoved(string unitInstanceID, string SkillID)
        {
            var equipped = Stones.GetEquipingStones(unitInstanceID);
            string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
            foreach (var t in equipped)
            {
                switch (t.inUsingSkillSlot)
                {
                    case "1":
                        A1 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                    case "2":
                        A2 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                    case "3":
                        A3 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                    case "4":
                        B1 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                    case "5":
                        B2 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                    case "6":
                        B3 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                    case "7":
                        C1 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                    case "8":
                        C2 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                    case "9":
                        C3 = (t.skillId != SkillID) ? t.skillId : "-1";
                        break;
                }
            }
            return SkillSet.CheckEdit(A1, A2, A3, B1, B2, B3, C1, C2, C3);
        }
    }
}