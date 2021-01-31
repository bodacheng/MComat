using System.Collections.Generic;
using Skill;
using UnityEngine;
using mainMenu;
using System.Linq;

public partial class NineAndTwo
{
    // exceptSkIDs : 除了这些技能ID。切记是技能ID
    static string RandomSkillIDOfStone(SkillStonesBox.StoneFilterForm filterForm, List<string> exceptSkIDs)
    {
        IDictionary<string, string> _SkillIDsAndNames = SkillConfigTable.GetSkillIDAndNameDic(filterForm);
        List<string> StoneSkillIDs = _SkillIDsAndNames.Keys.ToList();
        if (StoneSkillIDs.Count == 0)
        {
            return null;
        }else{
            for (int i = 0; i < exceptSkIDs.Count; i++)
            {
                if (StoneSkillIDs.Contains(exceptSkIDs[i]))
                    StoneSkillIDs.Remove(exceptSkIDs[i]);
            }
        }
        
        int ranDom = Random.Range(0, StoneSkillIDs.Count);
        return StoneSkillIDs[ranDom];
    }

    public static List<int> RemainSlotSPLevelCal(NineAndTwo current)
    {
        int remainSlotCount = 9 - current.SkillIDList().Count;
        int currentPoint = SkillBalancePoint(current.A1skillid, current.A2skillid, current.A3skillid, current.B1skillid, current.B2skillid, current.B3skillid, current.C1skillid, current.C2skillid, current.C3skillid);
        int point = currentPoint + (remainSlotCount - 1) * 10;
        List<int> returnValue = new List<int>();
        if (point >= 30)
            returnValue.Add(3);
        if (point >= 20)
            returnValue.Add(2);
        if (point >= 10)
            returnValue.Add(1);
        returnValue.Add(0);
        return returnValue;
    }
}
