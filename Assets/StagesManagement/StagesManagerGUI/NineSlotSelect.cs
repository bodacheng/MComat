#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Skill;
using System.Collections.Generic;

public partial class StagesManagerGUI : Editor {

    string A1ButtonText = "A1";
    string A2ButtonText = "A2";
    string A3ButtonText = "A3";
    string B1ButtonText = "B1";
    string B2ButtonText = "B2";
    string B3ButtonText = "B3";
    string C1ButtonText = "C1";
    string C2ButtonText = "C2";
    string C3ButtonText = "C3";
    
    void NineSlotPart()
    {
        GUILayout.BeginHorizontal();
        void SlotColorCal(SkillConfig targetC)
        {
            GUI.backgroundColor = Repeated(focusingCharInfo, targetC, targetC.RECORD_ID) ? Color.red : SlotHasSkill(targetC.RECORD_ID);
        }
        
        Color SlotHasSkill(string RECORD_ID)
        {
            SkillConfig defaultSkillConfig = SkillConfigTable.GetSkillConfigByID(RECORD_ID);
            return defaultSkillConfig != null ? InhereSks.Key == RECORD_ID ? new Color(0.2f, 0.7f, 1) : Color.yellow : Color.white;
        }

        SlotColorCal(focusingCharInfo._NineAndTwo.GetA1Config());
        if (GUILayout.Button(A1ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetA1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA1Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetA2Config());
        if (GUILayout.Button(A2ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetA2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA2Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetA3Config());
        if (GUILayout.Button(A3ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetA3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetA3Config();
            selectedInhereskill = 0;
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        SlotColorCal(focusingCharInfo._NineAndTwo.GetB1Config());
        if (GUILayout.Button(B1ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetB1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB1Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetB2Config());
        if (GUILayout.Button(B2ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetB2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB2Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetB3Config());
        if (GUILayout.Button(B3ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetB3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetB3Config();
            selectedInhereskill = 0;
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC1Config());
        if (GUILayout.Button(C1ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetC1Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC1Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC2Config());
        if (GUILayout.Button(C2ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetC2Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC2Config();
            selectedInhereskill = 0;
        }
        SlotColorCal(focusingCharInfo._NineAndTwo.GetC3Config());
        if (GUILayout.Button(C3ButtonText, targetSC != focusingCharInfo._NineAndTwo.GetC3Config() ? ButtonStyle_NineAndTwo : ButtonStyle_NineAndTwo_Selected))
        {
            targetSC = focusingCharInfo._NineAndTwo.GetC3Config();
            selectedInhereskill = 0;
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndHorizontal();
    }
    
    bool Repeated(CharDataInfo _focusingCharInfo , SkillConfig _target,string recordID)
    {
        foreach (KeyValuePair<SkillConfig,string> keyValuePair in GetFocusingCharSkillList(_focusingCharInfo))
        {
            if (keyValuePair.Value == recordID && keyValuePair.Key != _target)
            {
                return true;
            }
        }
        return false;
    }
    
    // 服务于Repeated函数，获取当前编辑中角色的技能列表
    IDictionary<SkillConfig,string> GetFocusingCharSkillList(CharDataInfo _focusingCharInfo)
    {
        IDictionary<SkillConfig,string> list = new Dictionary<SkillConfig,string>();
        
        SkillConfig A1 = _focusingCharInfo._NineAndTwo.GetA1Config();
        SkillConfig A2 = _focusingCharInfo._NineAndTwo.GetA2Config();
        SkillConfig A3 = _focusingCharInfo._NineAndTwo.GetA3Config();
        SkillConfig B1 = _focusingCharInfo._NineAndTwo.GetB1Config();
        SkillConfig B2 = _focusingCharInfo._NineAndTwo.GetB2Config();
        SkillConfig B3 = _focusingCharInfo._NineAndTwo.GetB3Config();
        SkillConfig C1 = _focusingCharInfo._NineAndTwo.GetC1Config();
        SkillConfig C2 = _focusingCharInfo._NineAndTwo.GetC2Config();
        SkillConfig C3 = _focusingCharInfo._NineAndTwo.GetC3Config();
        
        A1ButtonText = RefreshButtonText(A1);
        A2ButtonText = RefreshButtonText(A2);
        A3ButtonText = RefreshButtonText(A3);
        B1ButtonText = RefreshButtonText(B1);
        B2ButtonText = RefreshButtonText(B2);
        B3ButtonText = RefreshButtonText(B3);
        C1ButtonText = RefreshButtonText(C1);
        C2ButtonText = RefreshButtonText(C2);
        C3ButtonText = RefreshButtonText(C3);
        
        if (A1 != null && A1.RECORD_ID != null)
        {
            list.Add(A1, A1.RECORD_ID);
        }
        if (A2 != null && A2.RECORD_ID != null)
        {
            list.Add(A2, A2.RECORD_ID);
        }
        if (A3 != null && A3.RECORD_ID != null)
        {
            list.Add(A3, A3.RECORD_ID);
        }
        if (B1 != null && B1.RECORD_ID != null)
        {
            list.Add(B1, B1.RECORD_ID);
        }
        if (B2 != null && B2.RECORD_ID != null)
        {
            list.Add(B2, B2.RECORD_ID);
        }
        if (B3 != null && B3.RECORD_ID != null)
        {
            list.Add(B3, B3.RECORD_ID);
        }
        if (C1 != null && C1.RECORD_ID != null)
        {
            list.Add(C1, C1.RECORD_ID);
        }
        if (C2 != null && C2.RECORD_ID != null)
        {
            list.Add(C2, C2.RECORD_ID);
        }
        if (C3 != null && C3.RECORD_ID != null)
        {
            list.Add(C3, C3.RECORD_ID);
        }
        return list;
    }
    
    string RefreshButtonText(SkillConfig skillConfig)
    {
        switch(skillConfig.SP_LEVEL)
        {
            case 0:
                return "●";
            case 1:
                return "★";
            case 2:
                return "★★";
            case 3:
                return "★★★";
            default:
                return "-";
        }
    }
}
#endif