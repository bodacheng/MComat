#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UniRx;

public partial class StagesManagerGUI : Editor {

    int selectSkillExLevel = -1;
    readonly int[] skillrarelevels = {-1, 0, 1, 2, 3 };
    readonly string[] skillrarelevelShow = { "ALL", "0", "★", "★★", "★★★" };
    readonly int[] exLevels = {-1, 0, 1, 2, 3 };
    readonly string[] exLevelShows = { "ALL", "普攻", "一级必杀", "二级必杀", "三级必杀" };
    bool[] SPselected = { true, true, true, true };
    int selectskillrarelevel = -1;
    readonly bool[] skillrangeselectfilter = { true, true, true, true }; //close, near, far, out
    bool skillselectfilter;
    bool filterallranges = true;
    IDictionary<string, string> _SkillIDsAndNames;
    
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
                SPselected = new bool[4] { true, false, false, false };
                break;
                case 1:
                SPselected = new bool[4] { false, true, false, false };
                break;
                case 2:
                SPselected = new bool[4] { false, false, true, false };
                break;
                case 3:
                SPselected = new bool[4] { false, false, false, true };
                break;
                default:
                SPselected = new bool[4] { true, true, true, true };
                break;
            }
            
            filterallranges = EditorGUILayout.BeginToggleGroup("限定攻击范围", filterallranges);
            if (!filterallranges)
            {
                skillrangeselectfilter[0] = true;
                skillrangeselectfilter[1] = true;
                skillrangeselectfilter[2] = true;
                skillrangeselectfilter[3] = true;
            }
            skillrangeselectfilter[0] = EditorGUILayout.Toggle("近", skillrangeselectfilter[0], AttackRangeToggleGUI);
            skillrangeselectfilter[1] = EditorGUILayout.Toggle("中", skillrangeselectfilter[1], AttackRangeToggleGUI);
            skillrangeselectfilter[2] = EditorGUILayout.Toggle("远", skillrangeselectfilter[2], AttackRangeToggleGUI);
            skillrangeselectfilter[3] = EditorGUILayout.Toggle("超", skillrangeselectfilter[3], AttackRangeToggleGUI);
            EditorGUILayout.EndToggleGroup();
            
            selectskillrarelevel = EditorGUILayout.IntPopup("技能rank:", selectskillrarelevel, skillrarelevelShow, skillrarelevels);
            EditorGUILayout.LabelField(" ~~~~~  以下将陈列根据条件删选出的技能  ~~~~~ ", Title);
            GUILayout.Space(10f);
        }
        
        _SkillIDsAndNames = SkillConfigTable.GetSkillIDAndNameDic(focusingtype, new bool[4] { skillrangeselectfilter[0], skillrangeselectfilter[1], skillrangeselectfilter[2], skillrangeselectfilter[3]}, SPselected, selectskillrarelevel);
        
        int index2 = 0;
        int selectedskillindex = 0;
        foreach (KeyValuePair<string, string> keyValuePair in _SkillIDsAndNames)
        {
            if (keyValuePair.Key == targetSC.RECORD_ID)
            {
                selectedskillindex = index2;
                break;
            }
            index2++;
        }
        
        selectedskillindex = EditorGUILayout.Popup("技能：", selectedskillindex, _SkillIDsAndNames.Values.ToArray());
        targetSC.RECORD_ID = selectedskillindex == 0 ? null : _SkillIDsAndNames.ElementAt(selectedskillindex).Key;
    }
}
#endif