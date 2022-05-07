#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using Skill;

public partial class StagesManager : EditorWindow {

    SkillConfig InhereSks;
    IDictionary<string, string> SelectInhere;
    int selectedInhereskill;
    
    void SelectInher(string CharResourceID)
    {
        // 原生技能
        SelectInhere = new Dictionary<string, string>()
        {
            {"0","空"}
        };
        InhereSks = SkillConfigTable.GetPassiveSkills(CharResourceID);
        if (InhereSks.RECORD_ID != null)
        {
            SelectInhere.Add(InhereSks.RECORD_ID, InhereSks.REAL_NAME);
        }
        selectedInhereskill = EditorGUILayout.Popup("原生技能：", selectedInhereskill, SelectInhere.Values.ToArray());
        SetSkillId(SelectInhere.ElementAt(selectedInhereskill).Key);
    }
}
#endif