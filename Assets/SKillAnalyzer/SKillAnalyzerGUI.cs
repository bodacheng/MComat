#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

[CustomEditor(typeof(SKillAnalyzer))]
public class SKillAnalyzerGUI : Editor
{
    SKillAnalyzer sKillAnalyzer;
    string focusingType = "human";
    string targetSkillName;
    string targetEventName;
    float attackframestartat_max,attackframestartat_min,attackframeendtocancelframetime_max,attackframeendtocancelframetime_min;
    string skilltypefoldername = "G_Attack_State";
    readonly string[] skilltypefoldernames = { "G_Attack_State", "G_Attack_State_Stay", "GMStates"};
    string old_name, new_name;
    public override void OnInspectorGUI()
    {
        sKillAnalyzer = (SKillAnalyzer)target;
        EditorGUILayout.LabelField(" 技能参数统计类  ");
        focusingType = EditorGUILayout.TextField("统计以下类型角色的技能信息", focusingType);
        if (GUILayout.Button("输出技能攻击力预估"))
        {
            SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
            PowerEstimateTable.Save(focusingType);
        }

        targetEventName = EditorGUILayout.TextField("选择拥有该事件的技能动画片段", targetEventName);
        attackframestartat_max = EditorGUILayout.FloatField("攻击帧启动时间小于等于：", attackframestartat_max);
        attackframestartat_min = EditorGUILayout.FloatField("攻击帧启动时间大于：", attackframestartat_min);
        attackframeendtocancelframetime_max = EditorGUILayout.FloatField("收手时间小于等于", attackframeendtocancelframetime_max);
        attackframeendtocancelframetime_min = EditorGUILayout.FloatField("收手时间大于：", attackframeendtocancelframetime_min);
        if (GUILayout.Button("满足以上条件的技能资源名如下：(console显示)"))
        {
            sKillAnalyzer.SkillsAnalyzeByFrames(focusingType, targetEventName, attackframestartat_min, attackframestartat_max, attackframeendtocancelframetime_min, attackframeendtocancelframetime_max);
        }
        EditorGUILayout.LabelField(" 整体替换动画事件名(千万慎用。一般用不上此功能）");
        old_name = EditorGUILayout.TextField("寻找该动画事件名", old_name);
        new_name = EditorGUILayout.TextField("替换成以下动画事件名", new_name);
        if (GUILayout.Button("该动画事件名替换(请慎用此功能）"))
        {
            sKillAnalyzer.ReplaceAnimEventName(focusingType, old_name, new_name);
        }
        // 
        focusingType = EditorGUILayout.TextField("统计以下类型角色的技能信息", focusingType);
        skilltypefoldername = skilltypefoldernames[EditorGUILayout.Popup("技能文件夹", Array.IndexOf(skilltypefoldernames, skilltypefoldername), skilltypefoldernames)];
        targetSkillName = EditorGUILayout.TextField("技能名", targetSkillName);
        if (GUILayout.Button("分析以下技能"))
        {
            UnityEngine.Object tartget = Resources.Load("Animations/" + focusingType + "/" + skilltypefoldername + "/" + targetSkillName, typeof(AnimationClip));
            if (tartget)
                sKillAnalyzer.EvaluateSKill(tartget as AnimationClip);
            else
                Debug.Log("没找到对应技能文件");
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Login"))
        {
            PlayFabLogin.CustomIDLogin(
                result => {
                    Debug.Log(" 登陆成功，获得下面这样一个东西： " + result.EntityToken.EntityToken);
                },
                fail => {
                    Debug.Log("login fail");
                }
            );
        }
        GUILayout.Space(10);

        if (GUILayout.Button("任意函数测试"))
        {
            PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "completedLevel",
                FunctionParameter = new { level = "2" },
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                object level;
                jsonResult.TryGetValue("progressLevel", out level);

                Debug.Log(level.ToString());
            },
            error => {
                Debug.Log(error.Error);
            });

            //TitleData.SetArcadeRewards();
            //PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest()
            //{
            //    Body = new Dictionary<string, object>() {
            //        { "ChestType", "sdf" },
            //        { "LevelId", "sdf" }
            //    },
            //    EventName = "EveryThing"
            //},
            //result => Debug.Log("Success"),
            //error => Debug.LogError(error.GenerateErrorReport()));
        }
    }
}
#endif