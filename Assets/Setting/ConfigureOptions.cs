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
        _Setting.IconLoadingMode = (ResourceLoadMode)EditorGUILayout.EnumPopup("IconLoadingMode:", _Setting.IconLoadingMode);
    }
}

#endif

public class ConfigureOptions : MonoBehaviour
{
    public ResourceLoadMode ConfigFileLoadingMode;
    public ResourceLoadMode IconLoadingMode;
    public ResourceLoadMode bgmAndCvs;
}

public enum ResourceLoadMode
{
    CachAB = 1,
    StreamingAssetAB = 2,
    Resource = 3
}
