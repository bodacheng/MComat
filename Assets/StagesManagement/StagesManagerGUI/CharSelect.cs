#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UniRx;

public partial class StagesManagerGUI : EditorWindow {
    
    string CharSelect()
    {
        // 角色选择
        UnitConfig focusingUnitConfig = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(focusingUnitInfo.r_id));
        focusingType = focusingUnitConfig != null ? EditorGUILayout.TextField("CharacerType", focusingUnitConfig.TYPE) : EditorGUILayout.TextField("CharacerType", focusingType);
        UnitIDsAndNames = new Dictionary<string, string>() { { "-1", "空" } };
        foreach(KeyValuePair<string,string> keyValuePair in Units.GetMonsterIDsAndNamesDic(focusingType))
        {
            UnitIDsAndNames.Add(keyValuePair.Key, keyValuePair.Value);
        }
        int index = 0;
        foreach (KeyValuePair<string, string> keyValuePair in UnitIDsAndNames)
        {
            if (keyValuePair.Key == focusingUnitInfo.r_id)
            {
                selectedUnitIndex = index;
                break;
            }
            index++;
        }
        selectedUnitIndex = EditorGUILayout.Popup("角色名：", selectedUnitIndex, UnitIDsAndNames.Values.ToArray());
        focusingUnitInfo.r_id =  UnitIDsAndNames.Count > selectedUnitIndex ? UnitIDsAndNames.ElementAt(selectedUnitIndex).Key : null;
        return focusingUnitInfo.r_id;
    }
}
#endif