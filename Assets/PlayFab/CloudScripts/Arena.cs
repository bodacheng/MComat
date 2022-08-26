using UnityEngine;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public partial class CloudScript
{
    public static void ArenaDefendTeamSave(MultiDic<int, int, UnitInfo> info, Action<bool> finished)
    {
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "ArenaDefendTeamSave",
                FunctionParameter = new
                {
                    Team = info._SerializableSets
                },
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) =>
            {
                var jsonResult = (PlayFab.Json.JsonObject) result.FunctionResult;
                object succeed;
                jsonResult.TryGetValue("success", out succeed);
                if ((bool)succeed)
                {
                    finished.Invoke(true);
                }
                else
                {
                    Debug.Log("通讯错误");
                }
            },
            error =>
            {
                finished.Invoke(false);
            }
        );
    }
    
    public static void GetLeaderboardAroundUser(Action<List<LeaderboardInfo>> success, Action fail)
    {
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "GetLeaderboardAroundUser",
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                try
                {
                    var jsonResult = (PlayFab.Json.JsonObject) result.FunctionResult;
                    jsonResult.TryGetValue("teamInfos", out var teamInfos);
                    var json = JsonConvert.SerializeObject(teamInfos);
                    var opponents = JsonConvert.DeserializeObject<List<LeaderboardInfo>>(
                        json,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                    success.Invoke(opponents);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    fail.Invoke();
                }
            }
        );
    }
    
    public static void ArenaPointUp(Action success)
    {
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "ArenaPointUp",
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                var jsonResult = (PlayFab.Json.JsonObject) result.FunctionResult;
                object point;
                jsonResult.TryGetValue("currentPoint", out point); 
                Debug.Log(point);
                success.Invoke();
            }
        );
    }
    
    public class LeaderboardInfo
    {
        public PlayerLeaderboardEntry PlayerLeaderboardEntry;
        public MultiDic<int, int, UnitInfo>.SerializableSet[] Team;
    }
}
