using Api.Dto.Model;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        // 基于角色存档对技能编辑进行合法判断
        public SkillEditError CheckEditAfterOneStoneRemoved(string monsterOfPlayerId, string SkillID) // 基于存档
        {
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.GetEquipingStones(monsterOfPlayerId);
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
            
            // 第一列技能必须有普通技能
            if (CheckStartSKills(A1, B1, C1) == SkillEditError.NoNormalStart)
            {
                return SkillEditError.NoNormalStart;
            }
            
            // 检查技能重复
            List<string> checkSame = new List<string>();
            bool CheckRepeat(string skillID)
            {
                if (checkSame.Contains(skillID))
                {
                    return true;
                }
                if (SkillConfigTable.GetSkillConfigByID(skillID) != null)
                {
                    checkSame.Add(skillID);
                }
                return false;
            }
            
            if (CheckRepeat(A1))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(A2))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(A3))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(B1))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(B2))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(B3))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(C1))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(C2))
            {
                return SkillEditError.RepeatedSkill;
            }
            if (CheckRepeat(C3))
            {
                return SkillEditError.RepeatedSkill;
            }
                        
            int wholePoint = MySkillStonesReader.SkillBalancePoint(A1, A2, A3, B1, B2, B3, C1, C2, C3);
            return wholePoint < 0 ? SkillEditError.UnBalanced : SkillEditError.Perfect;
        }
    }
}