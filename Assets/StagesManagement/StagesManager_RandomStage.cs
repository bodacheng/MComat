using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public partial class StagesManager : MonoBehaviour
{
    public static LocalFight RandomFight()
    {
        string focusingtype = "human";
        
        // 这几个东西用不用执行待定
        SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
        MonstersConfigTable.LoadMonstersConfigByResource();
        MonstersConfigTable.RefreshCharacterResourceInfoDic();
        
        IDictionary<string, string> CharIDsAndNames = MonstersConfigTable.GetMonsterRecordIDsAndNamesArrayDic(focusingtype);
        List<int> Indexes = RandomSelect.Get(0, CharIDsAndNames.Count -1 , 3);
        List<string> charRecordIds = CharIDsAndNames.Keys.ToList();
        
        LocalFight target = new LocalFight();
        
        CharDataInfo char1 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[0]],
            _NineAndTwo = BalanceStyle("human", 1)
        };
        CharDataInfo char2 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[1]],
            _NineAndTwo = BalanceStyle("human", 1)
        };        
        CharDataInfo char3 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[2]],
            _NineAndTwo = BalanceStyle("human", 1)
        };
        
        target.EnemySets.Set(0, 0, char1);
        target.EnemySets.Set(0, 1, char2);
        target.EnemySets.Set(0, 2, char3);
        
        return target;
    }
    
    public static LocalFight RandomSkillTest()
    {
        string focusingtype = "human";
        
        // 这几个东西用不用执行待定
        SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
        MonstersConfigTable.LoadMonstersConfigByResource();
        MonstersConfigTable.RefreshCharacterResourceInfoDic();
        
        IDictionary<string, string> CharIDsAndNames = MonstersConfigTable.GetMonsterRecordIDsAndNamesArrayDic(focusingtype);
        List<int> Indexes = RandomSelect.Get(0, CharIDsAndNames.Count -1 , 3);
        List<string> charRecordIds = CharIDsAndNames.Keys.ToList();
        
        LocalFight target = new LocalFight();

        CharDataInfo char1 = new CharDataInfo
        {
            ResourceID = "2",//charRecordIds[Indexes[0]],
            _NineAndTwo = BalanceStyle("human", 1)
        };
        CharDataInfo char2 = new CharDataInfo
        {
            ResourceID = "3",//charRecordIds[Indexes[1]],
            _NineAndTwo = BalanceStyle("human", 1)
        };        
        CharDataInfo char3 = new CharDataInfo
        {
            ResourceID = "3",//charRecordIds[Indexes[2]],
            _NineAndTwo = BalanceStyle("human", 1)
        };
        
        CharDataInfo char4 = new CharDataInfo
        {
            ResourceID = "3",//charRecordIds[Indexes[0]],
            _NineAndTwo = BalanceStyle("human", 1)
        };
        CharDataInfo char5 = new CharDataInfo
        {
            ResourceID = "3",//charRecordIds[Indexes[1]],
            _NineAndTwo = BalanceStyle("human", 1)
        };        
        CharDataInfo char6 = new CharDataInfo
        {
            ResourceID = "2",//charRecordIds[Indexes[2]],
            _NineAndTwo = BalanceStyle("human", 1)
        };
        
        target.EnemySets.Set(0, 0, char1);
        target.EnemySets.Set(0, 1, char2);
        target.EnemySets.Set(0, 2, char3);
        
        target.HeroSets.Set(0, 0, char4);
        target.HeroSets.Set(0, 1, char5);
        target.HeroSets.Set(0, 2, char6);
        
        return target;
    }
    
    public static NineAndTwo BalanceStyle(string focusingtype, int skilllevel)
    {
        List<string> _normalSkills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[4] { true, true, true, true }, new bool[4] { true, false, false, false }, -1, 6);
        List<string> _Ex1Skills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[4] { true, true, true, true }, new bool[4] { false, true, false, false }, -1, 1);
        List<string> _Ex2Skills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[4] { true, true, true, true }, new bool[4] { false, false, true, false }, -1, 1);
        List<string> _Ex3Skills = SkillConfigTable.GetTargetSkillRecordIds(focusingtype, new bool[4] { true, true, true, true }, new bool[4] { false, false, false, true }, -1, 1);
        
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
}
