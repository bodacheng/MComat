#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UniRx;

public partial class StagesManager : EditorWindow {

    KeyValuePair<string, string> InhereSks;
    IDictionary<string, string> SelectInhere;
    int selectedInhereskill;
    
    void SelectInher(string CharResourceID)
    {
        // 原生技能
        SelectInhere = new Dictionary<string, string>()
        {
            {"0","空"}
        };
        InhereSks = INHERENT_SkillTable.GetINHERENTSkill(CharResourceID);
        if (InhereSks.Key != null)
        {
            SelectInhere.Add(InhereSks.Key, InhereSks.Value);
        }
        selectedInhereskill = EditorGUILayout.Popup("原生技能：", selectedInhereskill, SelectInhere.Values.ToArray());
        SetSkillId(SelectInhere.ElementAt(selectedInhereskill).Key);
    }
}
#endif