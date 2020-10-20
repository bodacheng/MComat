using System.Collections;
using System.Collections.Generic;

public partial class NineAndTwo
{
    #region 随机技能组
    public static NineAndTwo BalanceStyle(string focusingtype, int skilllevel)
    {
        List<string> _normalSkills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, false, false, false }, Skill.BehaviorType.NONE, -1, 6);
        List<string> _Ex1Skills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { false, true, false, false }, Skill.BehaviorType.NONE, -1, 1);
        List<string> _Ex2Skills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { false, false, true, false }, Skill.BehaviorType.NONE, -1, 1);
        List<string> _Ex3Skills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { false, false, false, true }, Skill.BehaviorType.NONE, -1, 1);
    
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
        List<string> _normalSkills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[3] { false, false, false }, new bool[4] { true, false, false, false }, Skill.BehaviorType.NONE, -1, 5);
        List<string> _Ex1Skills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[3] { false, true, true }, new bool[4] { false, true, false, false }, Skill.BehaviorType.NONE, -1, 3);
        List<string> _Ex2Skills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[3] { false, true, true }, new bool[4] { false, false, true, false }, Skill.BehaviorType.NONE, -1, 1);
        
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
