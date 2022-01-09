using System.Collections.Generic;
using dataAccess;
using UnityEngine;
using mainMenu;
using Skill;

public partial class SkillSet
{
    // 根据账户内拥有的技能石来补完当前九宫格
    public static SkillSet FixSkillSet(string type, SkillSet originSkillSet, int skilllevel, bool baseOnAcc)
    {
        SkillSet nineAndTwo = SkillSetRandomFix(type, originSkillSet, 1, baseOnAcc);
        if (nineAndTwo == null)
        {
            Debug.Log("无法根据现在的技能石安排合法补全九宫格");
            return null;
        }
        nineAndTwo.SetSkillLevel(skilllevel);
        nineAndTwo.SortNineAndTwo();
        return nineAndTwo;
    }

    static SkillSet SkillSetRandomFix(string type, SkillSet nineAndTwo, int targetSlot, bool baseOnAcc)
    {
        if (targetSlot == 10)
        {
            return nineAndTwo;
        }

        string skillId = null;
        switch (targetSlot)
        {
            case 1:
                skillId = nineAndTwo.a1;
                break;
            case 2:
                skillId = nineAndTwo.a2;
                break;
            case 3:
                skillId = nineAndTwo.a3;
                break;
            case 4:
                skillId = nineAndTwo.b1;
                break;
            case 5:
                skillId = nineAndTwo.b2;
                break;
            case 6:
                skillId = nineAndTwo.b3;
                break;
            case 7:
                skillId = nineAndTwo.c1;
                break;
            case 8:
                skillId = nineAndTwo.c2;
                break;
            case 9:
                skillId = nineAndTwo.c3;
                break;
        }

        // 已经有技能石的格子不做修改
        if (skillId != null)
            return SkillSetRandomFix(type, nineAndTwo, targetSlot + 1, baseOnAcc);

        skillId = null;
        
        SkillStonesBox.StoneFilterForm filterForm;

        if (targetSlot == 7)
        {
            // 第一列技能必须有普通技能
            SkillConfig A1skillConfig = SkillConfigTable.GetSkillConfig(nineAndTwo.a1);
            SkillConfig B1skillConfig = SkillConfigTable.GetSkillConfig(nineAndTwo.b1);
            if (A1skillConfig.SP_LEVEL != 0 && B1skillConfig.SP_LEVEL != 0)
            {
                filterForm = new SkillStonesBox.StoneFilterForm
                {
                    type = type,
                    exType = new int[1] { 0 },
                    close = false,
                    near = false,
                    far = false
                };
                goto A;
            }
        }

        filterForm = new SkillStonesBox.StoneFilterForm
        {
            type = type,
            exType = RemainSlotSPLevelCal(nineAndTwo).ToArray(),
            close = false,
            near = false,
            far = false
        };

        A:

        List<string> exceptSKIds = nineAndTwo.SkillIDList();

        if (baseOnAcc)
        {
            StoneOfPlayerInfo stoneInfoModel = Stones.SearchStoneForRandomSet(filterForm, exceptSKIds);
            if (stoneInfoModel == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return null;
            }
            skillId = stoneInfoModel.skillId;
        }
        else
        {
            string skid = RandomSkillIDOfStone(filterForm, exceptSKIds);
            if (skid == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return null;
            }
            skillId = skid;
        }

        switch (targetSlot)
        {
            case 1:
                nineAndTwo.a1 = skillId;
                break;
            case 2:
                nineAndTwo.a2 = skillId;
                break;
            case 3:
                nineAndTwo.a3 = skillId;
                break;
            case 4:
                nineAndTwo.b1 = skillId;
                break;
            case 5:
                nineAndTwo.b2 = skillId;
                break;
            case 6:
                nineAndTwo.b3 = skillId;
                break;
            case 7:
                nineAndTwo.c1 = skillId;
                break;
            case 8:
                nineAndTwo.c2 = skillId;
                break;
            case 9:
                nineAndTwo.c3 = skillId;
                break;
        }
        if (targetSlot == 9)
        {
            SkillEditError valR = CheckEdit(
                nineAndTwo.a1, nineAndTwo.a2, nineAndTwo.a3,
                nineAndTwo.b1, nineAndTwo.b2, nineAndTwo.b3,
                nineAndTwo.c1, nineAndTwo.c2, nineAndTwo.c3);
            return valR == SkillEditError.Perfect ? nineAndTwo : null;
        }
        return SkillSetRandomFix(type, nineAndTwo, targetSlot + 1, baseOnAcc);
    }
}
