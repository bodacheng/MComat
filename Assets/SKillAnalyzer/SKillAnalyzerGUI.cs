#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using PlayFab;
using PlayFab.ClientModels;

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
        if (GUILayout.Button("输出技能攻击力预估"))
        {
            SkillConfigTable.LoadAllSkillConfigFromLocalConfigFile();
            PowerEstimateTable.Save(_focusingType);
        }
        
        _targetEventName = EditorGUILayout.TextField("选择拥有该事件的技能动画片段", _targetEventName);
        _attackFrameStartAtMAX = EditorGUILayout.FloatField("攻击帧启动时间小于等于：", _attackFrameStartAtMAX);
        _attackFrameStartAtMIN = EditorGUILayout.FloatField("攻击帧启动时间大于：", _attackFrameStartAtMIN);
        _attackFrameEndToCancelFrameTimeMAX = EditorGUILayout.FloatField("收手时间小于等于", _attackFrameEndToCancelFrameTimeMAX);
        _attackFrameEndToCancelFrameTimeMIN = EditorGUILayout.FloatField("收手时间大于：", _attackFrameEndToCancelFrameTimeMIN);
        if (GUILayout.Button("满足以上条件的技能资源名如下：(console显示)"))
        {
            target.SkillsAnalyzeByFrames(_focusingType, _targetEventName, _attackFrameStartAtMIN, _attackFrameStartAtMAX, _attackFrameEndToCancelFrameTimeMIN, _attackFrameEndToCancelFrameTimeMAX);
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
            Debug.Log((decimal)1 / 12);
            return;
            
            PlayFabClientAPI.ExecuteCloudScript(
                new ExecuteCloudScriptRequest()
                {
                    FunctionName = "GetLeaderboardAroundUser",
                    GeneratePlayStreamEvent = true
                },
                (ExecuteCloudScriptResult result) => {
                    var jsonResult = (PlayFab.Json.JsonObject) result.FunctionResult;
                    Debug.Log(jsonResult);
                },
                error => {
                    Debug.Log(error.Error);
                }
            );
            
            // PlayFabClientAPI.ExecuteCloudScript(
            //     new ExecuteCloudScriptRequest()
            //     {
            //         FunctionName = "ArenaPointUp",
            //         GeneratePlayStreamEvent = true
            //     },
            //     (ExecuteCloudScriptResult result) => {
            //         PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
            //         Debug.Log(" 竞技场分数增加 返回：" + jsonResult);
            //     },
            //     error => {
            //         Debug.Log(error.Error);
            //     }
            // );
            
            // PlayFabClientAPI.ExecuteCloudScript(
            //     new ExecuteCloudScriptRequest()
            //     {
            //         FunctionName = "claimAllPresentMails",
            //         GeneratePlayStreamEvent = true
            //     },
            //     (ExecuteCloudScriptResult result) => {
            //         PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
            //         object gd, dm, unlockedidlist;
            //         jsonResult.TryGetValue("DM", out dm);
            //         jsonResult.TryGetValue("GD", out gd);
            //         jsonResult.TryGetValue("UnlockedItemInstanceIds", out unlockedidlist);
            //         Debug.Log(
            //             " 获得黄金"+ gd.ToString()+
            //             " 宝石"+ dm.ToString()
            //         );
            //         List<string> ids =JsonConvert.DeserializeObject<List<string>>(unlockedidlist.ToString());
            //         for (int i = 0; i < ids.Count; i++)
            //         {
            //             Debug.Log("unlockedid" + ids[i]);
            //         }
            //         
            //     },
            //     error => {
            //         Debug.Log(error.Error);
            //     }
            // );
            
            // PlayFabClientAPI.ExecuteCloudScript(
            //     new ExecuteCloudScriptRequest()
            //     {
            //         FunctionName = "completedLevel",
            //         FunctionParameter = new { level = "5" },
            //         GeneratePlayStreamEvent = true
            //     },
            //     (ExecuteCloudScriptResult result) => {
            //         PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
            //         object level;
            //         jsonResult.TryGetValue("progressLevel", out level);
            //         Debug.Log(level.ToString());
            //     },
            //     error => {
            //         Debug.Log(error.Error);
            //     }
            // );

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