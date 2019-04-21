#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Setting))]
public class SettingGUI : Editor {

    Setting _Setting;
    public override void OnInspectorGUI()
    {
        _Setting = (Setting)target;
        _Setting._playerinfoReferenceMode = (playerinfoReferenceMode)EditorGUILayout.EnumPopup("账户信息索引模式:", _Setting._playerinfoReferenceMode);
        _Setting.ConfigFileLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("ConfigFileLoadingMode:", _Setting.ConfigFileLoadingMode);
        _Setting.ModelLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("ModelLoadingMode:", _Setting.ModelLoadingMode);
        _Setting.AnimationLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("AnimationLoadingMode:", _Setting.AnimationLoadingMode);
        _Setting.MagicLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("MagicLoadingMode:", _Setting.MagicLoadingMode);
        _Setting.IconLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("IconLoadingMode:", _Setting.IconLoadingMode);
    }
}
#endif