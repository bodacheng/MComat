#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using Skill;

public partial class StagesManagerGUI : Editor {
    
    void AutoSetInherSkill()
    {
        if (!InheretedSkillSet(focusingCharInfo))
        {
            SkillConfig A1 = focusingCharInfo._NineAndTwo.GetA1Config();
            CharConfig CharConfig = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(focusingCharInfo.ResourceID));
            KeyValuePair<string, string> _InhereSks = INHERENT_SkillTable.GetINHERENTSkill(CharConfig.RECORD_ID);
            A1.RECORD_ID = _InhereSks.Key;
        }
    }
    
    bool InheretedSkillSet(CharDataInfo _focusingCharInfo)
    {
        IDictionary<SkillConfig, string> FocusingCharSkillList = GetFocusingCharSkillList(_focusingCharInfo);
        CharConfig CharConfig = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(focusingCharInfo.ResourceID));
        InhereSks = INHERENT_SkillTable.GetINHERENTSkill(CharConfig.RECORD_ID);
        if (InhereSks.Key != null)
        {
            return FocusingCharSkillList.Values.Contains(InhereSks.Key);
        }
        return true; // 没有原生技能算true，即没有适配原生技能的相关任务
    }
}
#endif