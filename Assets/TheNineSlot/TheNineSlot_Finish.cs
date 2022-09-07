using dataAccess;
using UnityEngine;
using mainMenu;

public partial class SkillSet
{
    // 根据账户内拥有的技能石来补完当前九宫格
    public static SkillSet FixSkillSet(string type, SkillSet originSkillSet, bool baseOnAcc)
    {
        var skillSet = SkillSetRandomFix(type, originSkillSet, 1, baseOnAcc);
        if (skillSet == null)
        {
            Debug.Log("无法根据现在的技能石安排合法补全九宫格");
            return null;
        }
        
        skillSet.SortNineAndTwo();
        return skillSet;
    }

    static SkillSet SkillSetRandomFix(string type, SkillSet _skillSet, int targetSlot, bool baseOnAcc)
    {
        if (targetSlot == 10)
        {
            return _skillSet;
        }
        
        string skillId = null;
        switch (targetSlot)
        {
            case 1:
                skillId = _skillSet.a1;
                break;
            case 2:
                skillId = _skillSet.a2;
                break;
            case 3:
                skillId = _skillSet.a3;
                break;
            case 4:
                skillId = _skillSet.b1;
                break;
            case 5:
                skillId = _skillSet.b2;
                break;
            case 6:
                skillId = _skillSet.b3;
                break;
            case 7:
                skillId = _skillSet.c1;
                break;
            case 8:
                skillId = _skillSet.c2;
                break;
            case 9:
                skillId = _skillSet.c3;
                break;
        }
        
        // 已经有技能石的格子不做修改
        if (SkillConfigTable.GetSkillConfig(skillId) != null)
            return SkillSetRandomFix(type, _skillSet, targetSlot + 1, baseOnAcc);

        skillId = null;
        
        SkillStonesBox.StoneFilterForm filterForm;
        
        if (targetSlot == 7)
        {
            // 第一列技能必须有普通技能
            var A1skillConfig = SkillConfigTable.GetSkillConfig(_skillSet.a1);
            var B1skillConfig = SkillConfigTable.GetSkillConfig(_skillSet.b1);
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
            exType = RemainSlotSPLevelCal(_skillSet).ToArray(),
            close = false,
            near = false,
            far = false
        };

        A:

        var exceptSkIds = _skillSet.SkillIDList();
        if (baseOnAcc)
        {
            var stoneInfoModel = Stones.SearchStoneForRandomSet(filterForm, exceptSkIds);
            if (stoneInfoModel == null) // 如果账户已经没有符合要求的石头
            {
                Debug.Log("无法为" + targetSlot + "找到合适技能石");
                return null;
            }
            skillId = stoneInfoModel.SkillId;
        }
        else
        {
            var skid = RandomSkillIDOfStone(filterForm, exceptSkIds);
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
                _skillSet.a1 = skillId;
                break;
            case 2:
                _skillSet.a2 = skillId;
                break;
            case 3:
                _skillSet.a3 = skillId;
                break;
            case 4:
                _skillSet.b1 = skillId;
                break;
            case 5:
                _skillSet.b2 = skillId;
                break;
            case 6:
                _skillSet.b3 = skillId;
                break;
            case 7:
                _skillSet.c1 = skillId;
                break;
            case 8:
                _skillSet.c2 = skillId;
                break;
            case 9:
                _skillSet.c3 = skillId;
                break;
        }
        
        if (targetSlot == 9)
        {
            var valR = CheckEdit(
                _skillSet.a1, _skillSet.a2, _skillSet.a3,
                _skillSet.b1, _skillSet.b2, _skillSet.b3,
                _skillSet.c1, _skillSet.c2, _skillSet.c3);
            
            return valR == SkillEditError.Perfect ? _skillSet : null;
        }
        return SkillSetRandomFix(type, _skillSet, targetSlot + 1, baseOnAcc);
    }
}
