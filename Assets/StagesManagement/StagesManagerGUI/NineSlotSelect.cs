#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Skill;
using System.Collections.Generic;

public partial class StagesManager : EditorWindow {

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
        
        void SlotAnalyze(int _targetSlot)
        {
            string nowSkillID = null;
            SkillConfig defaultSkillConfig = null;
            switch(_targetSlot)
            {
                case 1:
                nowSkillID = focusingUnitInfo.set.a1;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                A1ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
                case 2:
                nowSkillID = focusingUnitInfo.set.a2;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                A2ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
                case 3:
                nowSkillID = focusingUnitInfo.set.a3;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                A3ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
                case 4:
                nowSkillID = focusingUnitInfo.set.b1;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                B1ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
                case 5:
                nowSkillID = focusingUnitInfo.set.b2;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                B2ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
                case 6:
                nowSkillID = focusingUnitInfo.set.b3;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                B3ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
                case 7:
                nowSkillID = focusingUnitInfo.set.c1;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                C1ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
                case 8:
                nowSkillID = focusingUnitInfo.set.c2;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                C2ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
                case 9:
                nowSkillID = focusingUnitInfo.set.c1;
                defaultSkillConfig = SkillConfigTable.GetSkillConfig(nowSkillID);
                C3ButtonText = RefreshButtonText(defaultSkillConfig);
                break;
            }
            var kv = SkillConfigTable.GetPassiveSkills(focusingUnitInfo.r_id) ?? new SkillConfig
            {
                RECORD_ID = null
            };
            GUI.backgroundColor = Repeated(focusingUnitInfo.set, nowSkillID) ? Color.red : (defaultSkillConfig != null ? kv.RECORD_ID == nowSkillID ? new Color(0.2f, 0.7f, 1) : Color.yellow : Color.white);
        }
        
        SlotAnalyze(1);
        if (GUILayout.Button(A1ButtonText, targetSlot == 1 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 1;
        }
        SlotAnalyze(2);
        if (GUILayout.Button(A2ButtonText, targetSlot == 2 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 2;
        }
        SlotAnalyze(3);
        if (GUILayout.Button(A3ButtonText, targetSlot == 3 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 3;
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        SlotAnalyze(4);
        if (GUILayout.Button(B1ButtonText, targetSlot == 4 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 4;
        }
        SlotAnalyze(5);
        if (GUILayout.Button(B2ButtonText, targetSlot == 5 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 5;
        }
        SlotAnalyze(6);
        if (GUILayout.Button(B3ButtonText, targetSlot == 6 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 6;
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        SlotAnalyze(7);
        if (GUILayout.Button(C1ButtonText, targetSlot == 7 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 7;
        }
        SlotAnalyze(8);
        if (GUILayout.Button(C2ButtonText, targetSlot == 8 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 8;
        }
        SlotAnalyze(9);
        if (GUILayout.Button(C3ButtonText, targetSlot == 9 ? ButtonStyle_NineAndTwo_Selected : ButtonStyle_NineAndTwo))
        {
            selectedInhereskill = 0;
            targetSlot = 9;
        }
        GUI.backgroundColor = Color.white;
        GUILayout.EndHorizontal();
    }
    
    bool Repeated(SkillSet _NineAndTwo, string recordID)
    {
        List<string> currentSkillList = _NineAndTwo.SkillIDList();
        int count = 0;
        for (int i = 0; i < currentSkillList.Count; i++)
        {
            if (currentSkillList[i] == recordID)
                count += 1;
        }
        return count > 1;
    }
        
    string RefreshButtonText(SkillConfig skillConfig)
    {
        if (skillConfig == null)
        {
            return "-";
        }
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