#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.ServerModels;

public class SKillAnalyzerGUI : EditorWindow
{
    readonly SKillAnalyzer target = new SKillAnalyzer();
    string _focusingType = "human";
    string _targetSkillName;
    string _targetEventName;
    float _attackFrameStartAtMAX, _attackFrameStartAtMIN, _attackFrameEndToCancelFrameTimeMAX, _attackFrameEndToCancelFrameTimeMIN;
    string skillTypeFolderName = "G_Attack_State";
    readonly string[] _skillTypeFolderNames = { "G_Attack_State", "G_Attack_State_Stay", "GMStates"};
    string old_name, new_name;
    void OnGUI()
    {
        EditorGUILayout.LabelField(" 技能参数统计类  ");
        _focusingType = EditorGUILayout.TextField("统计以下类型角色的技能信息", _focusingType);
        _targetEventName = EditorGUILayout.TextField("选择拥有该事件的技能动画片段", _targetEventName);
        _attackFrameStartAtMAX = EditorGUILayout.FloatField("攻击帧启动时间小于等于：", _attackFrameStartAtMAX);
        _attackFrameStartAtMIN = EditorGUILayout.FloatField("攻击帧启动时间大于：", _attackFrameStartAtMIN);
        _attackFrameEndToCancelFrameTimeMAX = EditorGUILayout.FloatField("收手时间小于等于", _attackFrameEndToCancelFrameTimeMAX);
        _attackFrameEndToCancelFrameTimeMIN = EditorGUILayout.FloatField("收手时间大于：", _attackFrameEndToCancelFrameTimeMIN);
        if (GUILayout.Button("满足以上条件的技能资源名如下：(console显示)"))
        {
            target.SkillsAnalyzeByFrames(_focusingType, _targetEventName, _attackFrameStartAtMIN, _attackFrameStartAtMAX, _attackFrameEndToCancelFrameTimeMIN, _attackFrameEndToCancelFrameTimeMAX).Forget();
        }
        EditorGUILayout.LabelField(" 整体替换动画事件名(千万慎用。一般用不上此功能）");
        old_name = EditorGUILayout.TextField("寻找该动画事件名", old_name);
        new_name = EditorGUILayout.TextField("替换成以下动画事件名", new_name);
        if (GUILayout.Button("该动画事件名替换(请慎用此功能）"))
        {
            target.ReplaceAnimEventName(_focusingType, old_name, new_name);
        }
        // 
        _focusingType = EditorGUILayout.TextField("统计以下类型角色的技能信息", _focusingType);
        skillTypeFolderName = _skillTypeFolderNames[EditorGUILayout.Popup("技能文件夹", Array.IndexOf(_skillTypeFolderNames, skillTypeFolderName), _skillTypeFolderNames)];
        _targetSkillName = EditorGUILayout.TextField("技能名", _targetSkillName);
        if (GUILayout.Button("分析以下技能"))
        {
            UnityEngine.Object animObject = Resources.Load("Animations/" + _focusingType + "/" + skillTypeFolderName + "/" + _targetSkillName, typeof(AnimationClip));
            if (animObject)
                target.EvaluateSKill(animObject as AnimationClip);
            else
                Debug.Log("没找到对应技能文件");
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Login"))
        {
            PlayFabReadClient.LoginByDevice(
                (x, y) => {
                    Debug.Log(" 登陆成功，获得下面这样一个东西： " + x.EntityToken.EntityToken);
                }
            );
        }
        GUILayout.Space(10);

        if (GUILayout.Button("任意CloudScript测试"))
        {
            CloudScript.ExecuteCloudScriptMainSceneCommon(
                new ExecuteCloudScriptRequest
                {
                    FunctionName = "azureTest",
                    //FunctionParameter = new { stage = 10 },
                    GeneratePlayStreamEvent = true
                },
                (x) =>
                {
                    Debug.Log(x);
                }
            );
        }
        
        if (GUILayout.Button("任意Azure Function测试"))
        {
            CloudScript.ExecuteFunctionCommon(
                new ExecuteFunctionRequest()
                {
                    FunctionName = "azureTest",
                    //FunctionParameter = new { stage = 10 },
                    GeneratePlayStreamEvent = true
                },
                (x) =>
                {
                    Debug.Log(x);
                }
            );
        }
    }
}
#endif