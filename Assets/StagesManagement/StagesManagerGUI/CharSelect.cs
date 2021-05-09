#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UniRx;

public partial class StagesManagerGUI : Editor {
    
    string CharSelect()
    {
        // 角色选择
        CharConfig focusingCharConfig = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(focusingCharInfo.ResourceID));
        focusingtype = focusingCharConfig != null ? EditorGUILayout.TextField("CharacerType", focusingCharConfig.TYPE) : EditorGUILayout.TextField("CharacerType", focusingtype);
        CharIDsAndNames = new Dictionary<string, string>() { { "-1", "空" } };
        foreach(KeyValuePair<string,string> keyValuePair in MonstersConfigTable.GetMonsterRecordIDsAndNamesArrayDic(focusingtype))
        {
            CharIDsAndNames.Add(keyValuePair.Key, keyValuePair.Value);
        }
        int index = 0;
        foreach (KeyValuePair<string, string> keyValuePair in CharIDsAndNames)
        {
            if (keyValuePair.Key == focusingCharInfo.ResourceID)
            {
                selectedmonsterindex = index;
                break;
            }
            index++;
        }
        selectedmonsterindex = EditorGUILayout.Popup("角色名：", selectedmonsterindex, CharIDsAndNames.Values.ToArray());
        focusingCharInfo.ResourceID =  CharIDsAndNames.Count > selectedmonsterindex ? CharIDsAndNames.ElementAt(selectedmonsterindex).Key : null;
        return focusingCharInfo.ResourceID;
    }
}
#endif