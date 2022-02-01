#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public partial class StagesManager : EditorWindow {
    
    string UnitSelect()
    {
        // 角色选择
        var focusingUnitConfig = Units.RowToCharConfigInfo(Units.Find_RECORD_ID(focusingUnitInfo.r_id));
        focusingType = focusingUnitConfig != null ? EditorGUILayout.TextField("Unit Type", focusingUnitConfig.TYPE) : EditorGUILayout.TextField("Unit Type", focusingType);
        UnitIDsAndNames = new Dictionary<string, string>() { { "-1", "空" } };
        foreach(var keyValuePair in Units.GetMonsterIDsAndNamesDic(focusingType))
        {
            UnitIDsAndNames.Add(keyValuePair.Key, keyValuePair.Value);
        }
        var index = 0;
        foreach (var keyValuePair in UnitIDsAndNames)
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