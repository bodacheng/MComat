#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ResourceSetting))]
public class ResourceSettingGUI : Editor {

    ResourceSetting _Setting;
    public override void OnInspectorGUI()
    {
        _Setting = (ResourceSetting)target;
        _Setting.ConfigFileLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("ConfigFileLoadingMode:", _Setting.ConfigFileLoadingMode);
        _Setting.ModelLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("ModelLoadingMode:", _Setting.ModelLoadingMode);
        _Setting.AnimationLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("AnimationLoadingMode:", _Setting.AnimationLoadingMode);
        _Setting.MagicLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("MagicLoadingMode:", _Setting.MagicLoadingMode);
        _Setting.IconLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("IconLoadingMode:", _Setting.IconLoadingMode);
    }
}
#endif