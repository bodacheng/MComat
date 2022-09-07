#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class FightMemberManager
{
    int targetSlot = 0;
    void SetSkillId(string skillID)
    {
        switch(targetSlot)
        {
            case 1:
            focusingUnitInfo.set.a1 = skillID;
            break;
            case 2:
            focusingUnitInfo.set.a2 = skillID;
            break;
            case 3:
            focusingUnitInfo.set.a3 = skillID;
            break;
            case 4:
            focusingUnitInfo.set.b1 = skillID;
            break;
            case 5:
            focusingUnitInfo.set.b2 = skillID;
            break;
            case 6:
            focusingUnitInfo.set.b3 = skillID;
            break;
            case 7:
            focusingUnitInfo.set.c1 = skillID;
            break;
            case 8:
            focusingUnitInfo.set.c2 = skillID;
            break;
            case 9:
            focusingUnitInfo.set.c3 = skillID;
            break;
        }
        focusingUnitInfo.set.SortNineAndTwo();
    }
    
    string GetFocusSkillId()
    {
        switch(targetSlot)
        {
            case 1:
            return focusingUnitInfo.set.a1;
            case 2:
            return focusingUnitInfo.set.a2;
            case 3:
            return focusingUnitInfo.set.a3;
            case 4:
            return focusingUnitInfo.set.b1;
            case 5:
            return focusingUnitInfo.set.b2;
            case 6:
            return focusingUnitInfo.set.b3;
            case 7:
            return focusingUnitInfo.set.c1;
            case 8:
            return focusingUnitInfo.set.c2;
            case 9:
            return focusingUnitInfo.set.c3;
            default:
            return null;
        }
    }
}
#endif