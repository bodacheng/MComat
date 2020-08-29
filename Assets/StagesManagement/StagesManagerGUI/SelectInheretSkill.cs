#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UniRx;

public partial class StagesManagerGUI : Editor {

    IDictionary<string, string> InhereSks;
    IDictionary<string, string> SelectInhere;
    int selectedInhereskill;
    
    void SelectInher()
    {
        // 原生技能
        SelectInhere = new Dictionary<string, string>()
        {
            {"0","空"}
        };
        InhereSks = INHERENT_SkillTable.GetINHERENTSKIDList(focusingCharConfig.RECORD_ID);
        foreach(KeyValuePair<string,string> keyValuePair in InhereSks)
        {
            SelectInhere.Add(keyValuePair.Key, keyValuePair.Value);
        }
        selectedInhereskill = EditorGUILayout.Popup("原生技能：", selectedInhereskill, SelectInhere.Values.ToArray());
    }
}
#endif