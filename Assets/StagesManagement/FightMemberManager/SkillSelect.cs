#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using mainMenu;

public partial class FightMemberManager {

    int selectSkillExLevel = -1;
    readonly int[] exLevels = {-1, 0, 1, 2, 3 };
    readonly string[] exLevelShows = { "ALL", "普攻", "一级必杀", "二级必杀", "三级必杀" };
    int[] SPselected = { 0, 1, 2, 3 };
    readonly bool[] skillrangeselectfilter = { false, false, false };
    bool skillselectfilter;
    bool filterranges = true;
    IDictionary<string, string> _SkillIDsAndNames = new Dictionary<string, string>();
    
    void SkillSelect()
    {
        skillselectfilter = EditorGUILayout.Toggle("限制技能选择条件", skillselectfilter, AttackRangeToggleGUI);
        if (skillselectfilter)
        {
            EditorGUILayout.LabelField(" ~~~~~  限制技能条件  ~~~~~ ", Title);
            selectSkillExLevel = EditorGUILayout.IntPopup("必杀技等级:", selectSkillExLevel, exLevelShows, exLevels);
            switch(selectSkillExLevel)
            {
                case 0:
                    SPselected = new int[] { 0 };
                break;
                case 1:
                    SPselected = new int[] { 1 };
                break;
                case 2:
                    SPselected = new int[] { 2 };
                break;
                case 3:
                    SPselected = new int[] { 3 };
                break;
                default:
                    SPselected = new int[] { 0, 1, 2, 3 };
                break;
            }
            
            filterranges = EditorGUILayout.BeginToggleGroup("限定攻击范围", filterranges);
            if (!filterranges)
            {
                skillrangeselectfilter[0] = false;
                skillrangeselectfilter[1] = false;
                skillrangeselectfilter[2] = false;
            }else{
                skillrangeselectfilter[0] = EditorGUILayout.Toggle("近", skillrangeselectfilter[0], AttackRangeToggleGUI);
                skillrangeselectfilter[1] = EditorGUILayout.Toggle("中", skillrangeselectfilter[1], AttackRangeToggleGUI);
                skillrangeselectfilter[2] = EditorGUILayout.Toggle("远", skillrangeselectfilter[2], AttackRangeToggleGUI);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.LabelField(" ~~~~~  以下将陈列根据条件删选出的技能  ~~~~~ ", Title);
            GUILayout.Space(10f);
        }
        
        SkillStonesBox.StoneFilterForm filterForm = new SkillStonesBox.StoneFilterForm
        {
            Type = focusingType,
            Close = skillrangeselectfilter[0],
            Near = skillrangeselectfilter[1],
            Far = skillrangeselectfilter[2],
            ExType = SPselected,
            BType = Skill.BehaviorType.NONE
        };        
        _SkillIDsAndNames = SkillList(filterForm);// 待研究
        
        int index2 = 0;
        int selectedskillindex = 0;
        foreach (KeyValuePair<string, string> keyValuePair in _SkillIDsAndNames)
        {
            if (keyValuePair.Key == GetFocusSkillId())
            {
                selectedskillindex = index2;
                break;
            }
            index2++;
        }
        
        selectedskillindex = EditorGUILayout.Popup("技能：", selectedskillindex, _SkillIDsAndNames.Values.ToArray());
        SetSkillId(selectedskillindex == 0 ? null : _SkillIDsAndNames.ElementAt(selectedskillindex).Key);
    }
    
    IDictionary<string,string> SkillList(SkillStonesBox.StoneFilterForm filterForm)
    {
        IDictionary<string, string> returnvalue = new Dictionary<string, string>
        {
            { "-1", "空" }
        };
        IDictionary<string, string> SkillIDAndNameDic = SkillConfigTable.GetSkillIDAndNameDic(filterForm);
        foreach(KeyValuePair<string, string> keyValuePair in SkillIDAndNameDic)
        {
            returnvalue.Add(keyValuePair.Key, keyValuePair.Value);
        }
        return returnvalue;
    }
}
#endif