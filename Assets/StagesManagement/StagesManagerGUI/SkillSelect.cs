#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UniRx;
using mainMenu;

public partial class StagesManagerGUI : Editor {

    int selectSkillExLevel = -1;
    int[] rares = {};
    readonly int[] rareOptions = {-1, 0, 1, 2, 3};
    readonly string[] rareOptionsRender = { "ALL", "★", "★★", "★★★" , "★★★★" };
    readonly int[] exLevels = {-1, 0, 1, 2, 3 };
    readonly string[] exLevelShows = { "ALL", "普攻", "一级必杀", "二级必杀", "三级必杀" };
    int[] SPselected = { 0, 1, 2, 3 };
    int selectskillrarelevel = -1;
    readonly bool[] skillrangeselectfilter = { true, true, true };
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
            
            selectskillrarelevel = EditorGUILayout.IntPopup("技能rank:", selectskillrarelevel, rareOptionsRender, rareOptions);
            EditorGUILayout.LabelField(" ~~~~~  以下将陈列根据条件删选出的技能  ~~~~~ ", Title);
            GUILayout.Space(10f);
        }
        
        switch(selectskillrarelevel)
        {
            case 1:
                rares = new int[] {1};
                break;
            case 2:
                rares = new int[] {2};
                break;
            case 3:
                rares = new int[] {3};
                break;
            default:
                rares = new int[] {1,2,3,4};
                break;
        }
        SkillStonesBox.StoneFilterForm filterForm = new SkillStonesBox.StoneFilterForm
        {
            type = focusingtype,
            close = skillrangeselectfilter[0],
            near = skillrangeselectfilter[1],
            far = skillrangeselectfilter[2],
            exType = SPselected,
            BType = Skill.BehaviorType.NONE,
            rare = rares
        };        
        _SkillIDsAndNames = SkillList(filterForm);// 待研究
        
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