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
        SkillConfigTable.RefreshSkillConfigDicForReference();
        MonstersConfigTable.LoadMonstersConfigByResource();
        MonstersConfigTable.RefreshCharacterResourceInfoDic();
        
        IDictionary<string, string> CharIDsAndNames = MonstersConfigTable.GetMonsterRecordIDsAndNamesArrayDic(focusingtype);
        List<int> Indexes = RandomSelect.Get(0, CharIDsAndNames.Count -1 , 3);
        List<string> charRecordIds = CharIDsAndNames.Keys.ToList();
        
        LocalFight target = new LocalFight();

        CharDataInfo char1 = new CharDataInfo();
        char1.ResourceID = charRecordIds[Indexes[0]];
        char1._NineAndTwo = BalanceStyle("human",1);
        
        CharDataInfo char2 = new CharDataInfo();
        char2.ResourceID = charRecordIds[Indexes[1]];
        char2._NineAndTwo = BalanceStyle("human",1);
               
        CharDataInfo char3 = new CharDataInfo();
        char3.ResourceID = charRecordIds[Indexes[2]];
        char3._NineAndTwo = BalanceStyle("human",1);

        target.EnemySets.Set(0, 1, char1);
        target.EnemySets.Set(0, 2, char2);
        target.EnemySets.Set(0, 3, char3);
        
        return target;
    }
    
    static NineAndTwo BalanceStyle(string focusingtype, int skilllevel)
    {
        IDictionary<string, string> _Skills = SkillConfigTable.GetSkillIDAndNameDic(focusingtype, new bool[4] { true, true, true, true }, -1);
        List<int> Indexes = RandomSelect.Get(0, _Skills.Count -1 , 9);
        List<string> SKillRecordIds = _Skills.Keys.ToList();

        NineAndTwo one = new NineAndTwo()
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

            A1skillid = SKillRecordIds[Indexes[0]],
            A2skillid = SKillRecordIds[Indexes[1]],
            A3skillid = SKillRecordIds[Indexes[2]],

            B1skillid = SKillRecordIds[Indexes[3]],
            B2skillid = SKillRecordIds[Indexes[4]],
            B3skillid = SKillRecordIds[Indexes[5]],

            C1skillid = SKillRecordIds[Indexes[6]],
            C2skillid = SKillRecordIds[Indexes[7]],
            C3skillid = SKillRecordIds[Indexes[8]],

            canDefend = true,
            moveType = Skill.MoveType.Move_normal,
            rushType = Skill.RushType.Rush
        };

        return one;
    }
}
