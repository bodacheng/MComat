#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public partial class StagesManagerGUI : Editor 
{
    int targetSlot = 0;
    void SetSkillId(string skillID)
    {
        switch(targetSlot)
        {
            case 1:
            focusingCharInfo.set.a1 = skillID;
            break;
            case 2:
            focusingCharInfo.set.a2 = skillID;
            break;
            case 3:
            focusingCharInfo.set.a3 = skillID;
            break;
            case 4:
            focusingCharInfo.set.b1 = skillID;
            break;
            case 5:
            focusingCharInfo.set.b2 = skillID;
            break;
            case 6:
            focusingCharInfo.set.b3 = skillID;
            break;
            case 7:
            focusingCharInfo.set.c1 = skillID;
            break;
            case 8:
            focusingCharInfo.set.c2 = skillID;
            break;
            case 9:
            focusingCharInfo.set.c3 = skillID;
            break;
        }
        focusingCharInfo.set.SortNineAndTwo();
    }
    
    string GetFocusSkillId()
    {
        switch(targetSlot)
        {
            case 1:
            return focusingCharInfo.set.a1;
            case 2:
            return focusingCharInfo.set.a2;
            case 3:
            return focusingCharInfo.set.a3;
            case 4:
            return focusingCharInfo.set.b1;
            case 5:
            return focusingCharInfo.set.b2;
            case 6:
            return focusingCharInfo.set.b3;
            case 7:
            return focusingCharInfo.set.c1;
            case 8:
            return focusingCharInfo.set.c2;
            case 9:
            return focusingCharInfo.set.c3;
            default:
                return null;
        }
    }
}
#endif