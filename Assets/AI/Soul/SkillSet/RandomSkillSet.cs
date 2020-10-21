using System.Collections.Generic;
using UnityEngine;

public partial class NineAndTwo
{
    public static NineAndTwo NEW(string focusingtype, int skilllevel)
    {
        NineAndTwo nineAndTwo = new NineAndTwo();
        
        List<string> OneSkillId = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, false, false, false }, Skill.BehaviorType.NONE, -1, 1);
        nineAndTwo.A1skillid = OneSkillId[0];
        
        while (!PointCheck(nineAndTwo))
        {
            List<string> one = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            nineAndTwo.A2skillid = one[0];
        }
        
        while (!PointCheck(nineAndTwo))
        {
            List<string> one = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            nineAndTwo.A3skillid = one[0];
        }
        
        while (!PointCheck(nineAndTwo))
        {
            List<string> one = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            nineAndTwo.B1skillid = one[0];
        }
        
        while (!PointCheck(nineAndTwo))
        {
            List<string> one = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            nineAndTwo.B2skillid = one[0];
        }
        
        while (!PointCheck(nineAndTwo))
        {
            List<string> one = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            nineAndTwo.B3skillid = one[0];
        }
        
        while (!PointCheck(nineAndTwo))
        {
            List<string> one = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            nineAndTwo.C1skillid = one[0];
        }
        
        while (!PointCheck(nineAndTwo))
        {
            List<string> one = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            nineAndTwo.C2skillid = one[0];
        }
        
        while (!PointCheck(nineAndTwo))
        {
            List<string> one = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, true, true, true }, Skill.BehaviorType.NONE, -1, 1);
            nineAndTwo.C3skillid = one[0];
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
    
    static bool PointCheck(NineAndTwo current)
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

    #region 随机技能组
    public static NineAndTwo BalanceStyle(string focusingtype, int skilllevel)
    {
        List<string> _normalSkills = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, false, false, false }, Skill.BehaviorType.NONE, -1, 6);
        List<string> _Ex1Skills = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { false, true, false, false }, Skill.BehaviorType.NONE, -1, 1);
        List<string> _Ex2Skills = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { false, false, true, false }, Skill.BehaviorType.NONE, -1, 1);
        List<string> _Ex3Skills = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { false, false, false, true }, Skill.BehaviorType.NONE, -1, 1);
        
        NineAndTwo one = new NineAndTwo
        {
            A1level = skilllevel,
            A2level = skilllevel,
            A3level = skilllevel,
            B1level = skilllevel,
            B2level = skilllevel,
            B3level = skilllevel,
            C1level = skilllevel,
            C2level = skilllevel,
            C3level = skilllevel,
    
            A1skillid = _normalSkills[0],
            A2skillid = _normalSkills[1],
            A3skillid = _normalSkills[2],
            B1skillid = _normalSkills[3],
            B2skillid = _normalSkills[4],
            B3skillid = _normalSkills[5],
            C1skillid = _Ex1Skills[0],
            C2skillid = _Ex2Skills[0],
            C3skillid = _Ex3Skills[0],
    
            canDefend = true,
            moveType = Skill.MoveType.Move_normal,
            rushType = Skill.RushType.Rush
        };
    
        return one;
    }
    
    public static NineAndTwo RangedStyle(string focusingtype, int skilllevel)
    {
        List<string> _normalSkills = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, false, false, false }, Skill.BehaviorType.NONE, -1, 5);
        List<string> _Ex1Skills = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, true, true }, new bool[4] { false, true, false, false }, Skill.BehaviorType.NONE, -1, 3);
        List<string> _Ex2Skills = SkillConfigTable.RandomGetTargetSkillRecordIds(focusingtype, new bool[3] { false, true, true }, new bool[4] { false, false, true, false }, Skill.BehaviorType.NONE, -1, 1);
        
        NineAndTwo one = new NineAndTwo
        {
            A1level = skilllevel,
            A2level = skilllevel,
            A3level = skilllevel,
            B1level = skilllevel,
            B2level = skilllevel,
            B3level = skilllevel,
            C1level = skilllevel,
            C2level = skilllevel,
            C3level = skilllevel,
        
            A1skillid = _normalSkills[0],
            A2skillid = _normalSkills[1],
            A3skillid = _normalSkills[2],
            B1skillid = _Ex1Skills[0],
            B2skillid = _Ex1Skills[1],
            B3skillid = _Ex1Skills[2],
            C1skillid = _normalSkills[3],
            C2skillid = _normalSkills[4],
            C3skillid = _Ex2Skills[0],
        
            canDefend = true,
            moveType = Skill.MoveType.Move_normal,
            rushType = Skill.RushType.Rush
        };
        
        return one;
    }
    #endregion
}
