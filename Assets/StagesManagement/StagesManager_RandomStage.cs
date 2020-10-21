using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public partial class StagesManager : MonoBehaviour
{
    public static LocalFight RandomFight()
    {
        string focusingtype = "human";

        IDictionary<string, string> CharIDsAndNames = MonstersConfigTable.GetMonsterRecordIDsAndNamesArrayDic(focusingtype);
        List<int> Indexes = RandomSelect.Get(0, CharIDsAndNames.Count - 1, 3);
        List<string> charRecordIds = CharIDsAndNames.Keys.ToList();

        LocalFight target = new LocalFight();

        CharDataInfo char1 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[0]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };
        CharDataInfo char2 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[1]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };
        CharDataInfo char3 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[2]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };

        target.EnemySets.Set(0, 0, char1);
        target.EnemySets.Set(0, 1, char2);
        target.EnemySets.Set(0, 2, char3);

        return target;
    }

    public static LocalFight RandomSkillTest(TeamMode teamMode)
    {
        string focusingtype = "human";

        // 这几个东西用不用执行待定
        SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
        MonstersConfigTable.LoadMonstersConfigByResource();
        MonstersConfigTable.RefreshCharacterResourceInfoDic();

        IDictionary<string, string> CharIDsAndNames = MonstersConfigTable.GetMonsterRecordIDsAndNamesArrayDic(focusingtype);
        List<int> Indexes = RandomSelect.Get(0, CharIDsAndNames.Count - 1, 6);
        List<string> charRecordIds = CharIDsAndNames.Keys.ToList();

        LocalFight target = new LocalFight();

        CharDataInfo char1 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[0]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };
        CharDataInfo char2 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[1]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };
        CharDataInfo char3 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[2]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };

        CharDataInfo char4 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[3]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };
        CharDataInfo char5 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[4]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };
        CharDataInfo char6 = new CharDataInfo
        {
            ResourceID = charRecordIds[Indexes[5]],
            _NineAndTwo = NineAndTwo.RandomSkillSet("human", 1)
        };

        switch (teamMode)
        {
            case TeamMode.multiraid:
                target.EnemySets.Set(0, 0, char1);
                target.EnemySets.Set(0, 1, char6);
                target.HeroSets.Set(0, 0, char4);
                target.HeroSets.Set(0, 1, char5);
                break;
            case TeamMode.rotation:
                target.EnemySets.Set(0, 0, char1);
                target.EnemySets.Set(0, 1, char2);
                target.EnemySets.Set(0, 2, char3);
                target.HeroSets.Set(0, 0, char4);
                target.HeroSets.Set(0, 1, char5);
                target.HeroSets.Set(0, 2, char6);
                break;
        }

        return target;
    }
}
