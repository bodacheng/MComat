using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using mainMenu;

public partial class StagesManager : MonoBehaviour
{
    public static FightMembers RandomFight()
    {
        string focusingtype = "human";
        
        IDictionary<string, string> CharIDsAndNames = MonstersConfigTable.GetMonsterIDsAndNamesDic(focusingtype);
        List<int> Indexes = RandomSelect.Get(0, CharIDsAndNames.Count - 1, 3);
        List<string> charRecordIds = CharIDsAndNames.Keys.ToList();

        FightMembers target = new FightMembers();

        UnitInfo char1 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[0]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
        };
        UnitInfo char2 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[1]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
        };
        UnitInfo char3 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[2]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
        };

        target.EnemySets.Set(0, 0, char1);
        target.EnemySets.Set(0, 1, char2);
        target.EnemySets.Set(0, 2, char3);

        return target;
    }

    public static FightMembers RandomSkillTest(TeamMode teamMode)
    {
        string focusingtype = "human";
        
        IDictionary<string, string> CharIDsAndNames = MonstersConfigTable.GetMonsterIDsAndNamesDic(focusingtype);
        List<int> Indexes = RandomSelect.Get(0, CharIDsAndNames.Count - 1, 6);
        List<string> charRecordIds = CharIDsAndNames.Keys.ToList();

        FightMembers target = new FightMembers();

        UnitInfo char1 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[0]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
        };
        UnitInfo char2 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[1]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
        };
        UnitInfo char3 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[2]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
        };

        UnitInfo char4 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[3]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
        };
        UnitInfo char5 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[4]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
        };
        UnitInfo char6 = new UnitInfo
        {
            r_id = charRecordIds[Indexes[5]],
            set = SkillSet.RandomSkillSet("human", null, 1, false)
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
    
    public static FightMembers ScreenSaver(TeamMode teamMode)
    {
        string focusingtype = "human";
        
        IDictionary<string, string> CharIDsAndNames = MonstersConfigTable.GetMonsterIDsAndNamesDic(focusingtype);
        List<int> Indexes = RandomSelect.Get(0, CharIDsAndNames.Count - 1, 6);
        List<string> monsterIds = CharIDsAndNames.Keys.ToList();

        FightMembers target = new FightMembers();
        
        SkillStonesBox.StoneFilterForm filterForm = new SkillStonesBox.StoneFilterForm
        {
            type = focusingtype,
            exType = new int[1] { 0 },
            rare = new List<int> { 0, 1, 2 },
            close = false,
            near = false,
            far = false
        };
        
        UnitInfo char1 = new UnitInfo
        {
            r_id = monsterIds[Indexes[0]],
            set = SkillSet.RandomSkillSet(focusingtype, null, 1, false, filterForm)
        };
        UnitInfo char2 = new UnitInfo
        {
            r_id = monsterIds[Indexes[1]],
            set = SkillSet.RandomSkillSet(focusingtype, null, 1, false, filterForm)
        };
        
        switch (teamMode)
        {
            case TeamMode.multiraid:
                target.EnemySets.Set(0, 0, char1);
                target.HeroSets.Set(0, 0, char2);
                break;
            case TeamMode.rotation:
                target.EnemySets.Set(0, 0, char1);
                target.HeroSets.Set(0, 0, char2);
                break;
        }

        return target;
    }
}
