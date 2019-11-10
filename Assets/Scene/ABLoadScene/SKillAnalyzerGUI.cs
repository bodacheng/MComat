#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(SKillAnalyzer))]
public class SKillAnalyzerGUI : Editor
{
    SKillAnalyzer sKillAnalyzer;
    string focusingType;
    string targetEventName;
    float attackframestartat_max,attackframestartat_min,attackframeendtocancelframetime_max,attackframeendtocancelframetime_min;

    string old_name, new_name;
    public override void OnInspectorGUI()
    {
        sKillAnalyzer = (SKillAnalyzer)target;
        EditorGUILayout.LabelField(" 技能参数统计类  ");
        focusingType = EditorGUILayout.TextField("统计以下类型角色的技能信息",focusingType);
        targetEventName = EditorGUILayout.TextField("选择拥有该事件的技能动画片段",targetEventName);
        attackframestartat_max = EditorGUILayout.FloatField("攻击帧启动时间小于等于：",attackframestartat_max);
        attackframestartat_min = EditorGUILayout.FloatField("攻击帧启动时间大于：",attackframestartat_min);
        attackframeendtocancelframetime_max = EditorGUILayout.FloatField("收手时间小于等于",attackframeendtocancelframetime_max);
        attackframeendtocancelframetime_min = EditorGUILayout.FloatField("收手时间大于：",attackframeendtocancelframetime_min);
        if (GUILayout.Button("满足以上条件的技能资源名如下：(console显示)"))
        {
            sKillAnalyzer.SkillsAnalyzeByFrames(focusingType, targetEventName,attackframestartat_min,attackframestartat_max, attackframeendtocancelframetime_min,attackframeendtocancelframetime_max);
        }
        
        EditorGUILayout.LabelField(" 整体替换动画事件名(千万慎用。一般用不上此功能）");
        old_name = EditorGUILayout.TextField("寻找该动画事件名",old_name);
        new_name = EditorGUILayout.TextField("替换成以下动画事件名",new_name);
        if (GUILayout.Button("该动画事件名替换(请慎用此功能）"))
        {
            sKillAnalyzer.ReplaceAnimEventName(focusingType,old_name,new_name);
        }
        
    }
}
#endif