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
            focusingCharInfo._NineAndTwo.A1skillid = skillID;
            break;
            case 2:
            focusingCharInfo._NineAndTwo.A2skillid = skillID;
            break;
            case 3:
            focusingCharInfo._NineAndTwo.A3skillid = skillID;
            break;
            case 4:
            focusingCharInfo._NineAndTwo.B1skillid = skillID;
            break;
            case 5:
            focusingCharInfo._NineAndTwo.B2skillid = skillID;
            break;
            case 6:
            focusingCharInfo._NineAndTwo.B3skillid = skillID;
            break;
            case 7:
            focusingCharInfo._NineAndTwo.C1skillid = skillID;
            break;
            case 8:
            focusingCharInfo._NineAndTwo.C2skillid = skillID;
            break;
            case 9:
            focusingCharInfo._NineAndTwo.C3skillid = skillID;
            break;
        }
        focusingCharInfo._NineAndTwo.SortNineAndTwo();
    }
    
    string GetFocusSkillId()
    {
        switch(targetSlot)
        {
            case 1:
            return focusingCharInfo._NineAndTwo.A1skillid;
            case 2:
            return focusingCharInfo._NineAndTwo.A2skillid;
            case 3:
            return focusingCharInfo._NineAndTwo.A3skillid;
            case 4:
            return focusingCharInfo._NineAndTwo.B1skillid;
            case 5:
            return focusingCharInfo._NineAndTwo.B2skillid;
            case 6:
            return focusingCharInfo._NineAndTwo.B3skillid;
            case 7:
            return focusingCharInfo._NineAndTwo.C1skillid;
            case 8:
            return focusingCharInfo._NineAndTwo.C2skillid;
            case 9:
            return focusingCharInfo._NineAndTwo.C3skillid;
            default:
                return null;
        }
    }
}
#endif