#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Setting))]
public class SettingGUI : Editor
{
    Setting _Setting;
    public override void OnInspectorGUI()
    {
        _Setting = (Setting)target;
        _Setting.bgmSource = EditorGUILayout.ObjectField("BGMSource", _Setting.bgmSource, typeof(AudioSource), true) as AudioSource;
        _Setting.bgmSLider = EditorGUILayout.ObjectField("BGMVolumnSlider", _Setting.bgmSLider, typeof(Slider), true) as Slider;
        _Setting.CVSlider = EditorGUILayout.ObjectField("CVVolumnSlider", _Setting.CVSlider, typeof(Slider), true) as Slider;
        _Setting.effectsSoundsSlider = EditorGUILayout.ObjectField("EffectsVolumnSlider", _Setting.effectsSoundsSlider, typeof(Slider), true) as Slider;
    }
}
#endif