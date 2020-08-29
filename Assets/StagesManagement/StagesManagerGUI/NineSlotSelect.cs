#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Skill;
using System.Collections.Generic;

public partial class StagesManagerGUI : Editor {

    void NineSlotPart()
    {
        GUILayout.BeginHorizontal();
        void SlotColorCal(SkillConfig targetC)
        {
            GUI.backgroundColor = Repeated(targetC, targetC.RECORD_ID) ? Color.red : SlotHasSkill(targetC.RECORD_ID) ? Color.yellow : Color.white;
        }
        
        SlotColorCal(focusingCharInfo._NineAndTwo.GetA1Config());
        if (GUILayout.Button("A1", targetSC != focusingCharInfo._NineAndTwo.GetA1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA1Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetA2Config());
        if (GUILayout.Button("A2", targetSC != focusingCharInfo._NineAndTwo.GetA2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA2Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetA3Config());
        if (GUILayout.Button("A3", targetSC != focusingCharInfo._NineAndTwo.GetA3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA3Config();
            selectedInhereskill = 0;
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        SlotColorCal(focusingCharInfo._NineAndTwo.GetB1Config());
        if (GUILayout.Button("B1", targetSC != focusingCharInfo._NineAndTwo.GetB1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB1Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetB2Config());
        if (GUILayout.Button("B2", targetSC != focusingCharInfo._NineAndTwo.GetB2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB2Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetB3Config());
        if (GUILayout.Button("B3", targetSC != focusingCharInfo._NineAndTwo.GetB3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB3Config();
            selectedInhereskill = 0;
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC1Config());
        if (GUILayout.Button("C1", targetSC != focusingCharInfo._NineAndTwo.GetC1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC1Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC2Config());
        if (GUILayout.Button("C2", targetSC != focusingCharInfo._NineAndTwo.GetC2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC2Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC3Config());
        if (GUILayout.Button("C3", targetSC != focusingCharInfo._NineAndTwo.GetC3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC3Config();
            selectedInhereskill = 0;
        }
        GUILayout.EndHorizontal();
    }
    
    bool Repeated(SkillConfig _target,string recordID)
    {
        foreach (KeyValuePair<SkillConfig,string> keyValuePair in GetFocusingCharSkillList())
        {
            if (keyValuePair.Value == recordID && keyValuePair.Key != _target)
            {
                return true;
            }
        }
        return false;
    }
    
    // 服务于Repeated函数，获取当前编辑中角色的技能列表
    IDictionary<SkillConfig,string> GetFocusingCharSkillList()
    {
        IDictionary<SkillConfig,string> list = new Dictionary<SkillConfig,string>();
        
        SkillConfig A1 = focusingCharInfo._NineAndTwo.GetA1Config();
        SkillConfig A2 = focusingCharInfo._NineAndTwo.GetA2Config();
        SkillConfig A3 = focusingCharInfo._NineAndTwo.GetA3Config();
        SkillConfig B1 = focusingCharInfo._NineAndTwo.GetB1Config();
        SkillConfig B2 = focusingCharInfo._NineAndTwo.GetB2Config();
        SkillConfig B3 = focusingCharInfo._NineAndTwo.GetB3Config();
        SkillConfig C1 = focusingCharInfo._NineAndTwo.GetC1Config();
        SkillConfig C2 = focusingCharInfo._NineAndTwo.GetC2Config();
        SkillConfig C3 = focusingCharInfo._NineAndTwo.GetC3Config();
        
        if (A1 != null && A1.RECORD_ID != null)
        {
            list.Add(A1,A1.RECORD_ID);
        }
        if (A2 != null && A2.RECORD_ID != null)
        {
            list.Add(A2,A2.RECORD_ID);
        }
        if (A3 != null && A3.RECORD_ID != null)
        {
            list.Add(A3,A3.RECORD_ID);
        }
        if (B1 != null && B1.RECORD_ID != null)
        {
            if (!list.ContainsKey(B1))
            list.Add(B1,B1.RECORD_ID);
        }
        if (B2 != null && B2.RECORD_ID != null)
        {
            list.Add(B2,B2.RECORD_ID);
        }
        if (B3 != null && B3.RECORD_ID != null)
        {
            list.Add(B3,B3.RECORD_ID);
        }
        if (C1 != null && C1.RECORD_ID != null)
        {
            list.Add(C1,C1.RECORD_ID);
        }
        if (C2 != null && C2.RECORD_ID != null)
        {
            list.Add(C2,C2.RECORD_ID);
        }
        if (C3 != null && C3.RECORD_ID != null)
        {
            list.Add(C3,C3.RECORD_ID);
        }
        return list;
    }
    
    bool SlotHasSkill(string RECORD_ID)
    {
        SkillConfig defaultSkillConfig = SkillConfigTable.GetSkillConfigByID(RECORD_ID);
        return defaultSkillConfig != null;
    }
}
#endif