#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UniRx;

public partial class StagesManagerGUI : Editor {
    
    string CharSelect()
    {
        // 角色选择
        UnitConfig focusingUnitConfig = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(focusingCharInfo.r_id));
        focusingtype = focusingUnitConfig != null ? EditorGUILayout.TextField("CharacerType", focusingUnitConfig.TYPE) : EditorGUILayout.TextField("CharacerType", focusingtype);
        CharIDsAndNames = new Dictionary<string, string>() { { "-1", "空" } };
        foreach(KeyValuePair<string,string> keyValuePair in Units.GetMonsterIDsAndNamesDic(focusingtype))
        {
            CharIDsAndNames.Add(keyValuePair.Key, keyValuePair.Value);
        }
        int index = 0;
        foreach (KeyValuePair<string, string> keyValuePair in CharIDsAndNames)
        {
            if (keyValuePair.Key == focusingCharInfo.r_id)
            {
                selectedmonsterindex = index;
                break;
            }
            index++;
        }
        selectedmonsterindex = EditorGUILayout.Popup("角色名：", selectedmonsterindex, CharIDsAndNames.Values.ToArray());
        focusingCharInfo.r_id =  CharIDsAndNames.Count > selectedmonsterindex ? CharIDsAndNames.ElementAt(selectedmonsterindex).Key : null;
        return focusingCharInfo.r_id;
    }
}
#endif