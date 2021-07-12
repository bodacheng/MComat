using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ConfigureOptions))]
public class ConfigureOptionsGUI : Editor
{
    ConfigureOptions _Setting;
    public override void OnInspectorGUI()
    {
        _Setting = (ConfigureOptions)target;
        _Setting.ConfigFileLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("ConfigFileLoadingMode:", _Setting.ConfigFileLoadingMode);
        _Setting.ModelLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("ModelLoadingMode:", _Setting.ModelLoadingMode);
        _Setting.AnimationLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("AnimationLoadingMode:", _Setting.AnimationLoadingMode);
        _Setting.MagicLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("MagicLoadingMode:", _Setting.MagicLoadingMode);
        _Setting.IconLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("IconLoadingMode:", _Setting.IconLoadingMode);
    }
}

#endif

public class ConfigureOptions : MonoBehaviour
{
    public ResourceLoadMode ConfigFileLoadingMode;
    public ResourceLoadMode ModelLoadingMode;
    public ResourceLoadMode AnimationLoadingMode;
    public ResourceLoadMode MagicLoadingMode;
    public ResourceLoadMode IconLoadingMode;
    public ResourceLoadMode bgmAndCvs;
}

public enum ResourceLoadMode
{
    CachAB = 1,
    StreamingAssetAB = 2,
    Resource = 3
}
