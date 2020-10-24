using System.Collections.Generic;
using Skill;

public partial class NineAndTwo
{
    // 为技能组的特定位置安排技能。内部while循环直到技能组合法的做法，让这个函数相关的任何处理一定不能用于正式版本。
    // targetSlot : 1~9
    static void SkillRandomAdd(string focusingtype, NineAndTwo nineAndTwo, int targetSlot) 
    {
        do
        {
            List<string> one = SkillConfigTable.RandomGetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            switch (targetSlot)
            {
                case 1:
                    List<string> OneSkillId = SkillConfigTable.RandomGetSkillRecordIds(
                    focusingtype,
                    new bool[3] { false, false, false },
                    new bool[4] { true, false, false, false }, BehaviorType.NONE, -1, 1);
                    nineAndTwo.A1skillid = OneSkillId[0];
                    break;
                case 2:
                    nineAndTwo.A2skillid = one[0];
                    break;
                case 3:
                    nineAndTwo.A3skillid = one[0];
                    break;
                case 4:
                    nineAndTwo.B1skillid = one[0];
                    break;
                case 5:
                    nineAndTwo.B2skillid = one[0];
                    break;
                case 6:
                    nineAndTwo.B3skillid = one[0];
                    break;
                case 7:
                    nineAndTwo.C1skillid = one[0];
                    break;
                case 8:
                    nineAndTwo.C2skillid = one[0];
                    break;
                case 9:
                    nineAndTwo.C3skillid = one[0];
                    break;
            }
        }
        while (!InProcessPointLegalCheck(nineAndTwo) ||
                !CheckRepeat(nineAndTwo.A1skillid, nineAndTwo.A2skillid, nineAndTwo.A3skillid,
                            nineAndTwo.B1skillid, nineAndTwo.B2skillid, nineAndTwo.B3skillid,
                            nineAndTwo.C1skillid, nineAndTwo.C2skillid, nineAndTwo.C3skillid));
    }
    
    // 随机生成技能组，并且安排一个指定技能（原生技能）
    // 角色的原生技能如果是普攻则会被安排在A1，如果是非普攻则会被安排在A2
    public static NineAndTwo RandomSkillSet(string focusingtype, string originSkill, int skilllevel)
    {
        NineAndTwo nineAndTwo = new NineAndTwo();
        SkillConfig originSkillConfig = SkillConfigTable.GetSkillConfigByID(originSkill);
        
        for (int i = 1; i <= 9; i++)
        {
            if (i == 1)
            {
                if (originSkillConfig != null && originSkillConfig.SP_LEVEL == 0)
                {
                    nineAndTwo.A1skillid = originSkillConfig.RECORD_ID;
                    continue;
                }
                SkillRandomAdd(focusingtype, nineAndTwo, i);
            }
            else if (i == 2) // A2
            {
                if (originSkillConfig != null && originSkillConfig.SP_LEVEL != 0)
                {
                    nineAndTwo.A2skillid = originSkillConfig.RECORD_ID; 
                }else{
                    SkillRandomAdd(focusingtype, nineAndTwo, i);
                }
            }
            else
            {
                SkillRandomAdd(focusingtype, nineAndTwo, i);
            }
        }
        
        nineAndTwo.A1level = skilllevel;
        nineAndTwo.A2level = skilllevel;
        nineAndTwo.A3level = skilllevel;
        nineAndTwo.B1level = skilllevel;
        nineAndTwo.B2level = skilllevel;
        nineAndTwo.B3level = skilllevel;
        nineAndTwo.C1level = skilllevel;
        nineAndTwo.C2level = skilllevel;
        nineAndTwo.C3level = skilllevel;
        
        return nineAndTwo;
    }
    
    // 这个能在技能槽没有填满的情况下分析技能组是否合法。比如有三个3级超杀那其他格子无论怎么配置都注定非法。
    static bool InProcessPointLegalCheck(NineAndTwo current)
    {
        int remainSlotCount = 0;
        if (current.A1skillid == null)
            remainSlotCount++;
        if (current.A2skillid == null)
            remainSlotCount++;
        if (current.A3skillid == null)
            remainSlotCount++;
        if (current.B1skillid == null)
            remainSlotCount++;
        if (current.B2skillid == null)
            remainSlotCount++;
        if (current.B3skillid == null)
            remainSlotCount++;
        if (current.C1skillid == null)
            remainSlotCount++;
        if (current.C2skillid == null)
            remainSlotCount++;
        if (current.C3skillid == null)
            remainSlotCount++;
            
        int currentPoint = SkillBalancePoint(current.A1skillid, current.A2skillid, current.A3skillid, current.B1skillid, current.B2skillid, current.B3skillid, current.C1skillid, current.C2skillid, current.C3skillid);
        if (currentPoint + remainSlotCount * 10 < 0)
        {
            return false;
        }
        return true;
    }
    
    static bool RemainSlotMustBeAllNormal(NineAndTwo current)
    {
        int remainSlotCount = 0;
        if (current.A1skillid == null)
            remainSlotCount++;
        if (current.A2skillid == null)
            remainSlotCount++;
        if (current.A3skillid == null)
            remainSlotCount++;
        if (current.B1skillid == null)
            remainSlotCount++;
        if (current.B2skillid == null)
            remainSlotCount++;
        if (current.B3skillid == null)
            remainSlotCount++;
        if (current.C1skillid == null)
            remainSlotCount++;
        if (current.C2skillid == null)
            remainSlotCount++;
        if (current.C3skillid == null)
            remainSlotCount++;
            
        int currentPoint = SkillBalancePoint(current.A1skillid, current.A2skillid, current.A3skillid, current.B1skillid, current.B2skillid, current.B3skillid, current.C1skillid, current.C2skillid, current.C3skillid);
        if (currentPoint + remainSlotCount * 10 == 0)
        {
            return false;
        }
        return true;
    }
}
