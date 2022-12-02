#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public partial class StageEditor {
    
    string UnitSelect()
    {
        // 角色选择
        var focusingUnitConfig = Units.RowToUnitConfigInfo(Units.Find_RECORD_ID(_focusingUnitInfo.r_id));
        _focusingType = focusingUnitConfig != null ? EditorGUILayout.TextField("Unit Type", focusingUnitConfig.TYPE) : EditorGUILayout.TextField("Unit Type", _focusingType);
        _unitIDsAndNames = new Dictionary<string, string>() { { "-1", "空" } };
        foreach(var keyValuePair in Units.GetMonsterIDsAndNamesDic(_focusingType))
        {
            _unitIDsAndNames.Add(keyValuePair.Key, keyValuePair.Value);
        }
        var index = 0;
        foreach (var keyValuePair in _unitIDsAndNames)
        {
            if (keyValuePair.Key == _focusingUnitInfo.r_id)
            {
                selectedUnitIndex = index;
                break;
            }
            index++;
        }
        selectedUnitIndex = EditorGUILayout.Popup("角色名：", selectedUnitIndex, _unitIDsAndNames.Values.ToArray());
        _focusingUnitInfo.r_id =  _unitIDsAndNames.Count > selectedUnitIndex ? _unitIDsAndNames.ElementAt(selectedUnitIndex).Key : null;
        return _focusingUnitInfo.r_id;
    }
}
#endif