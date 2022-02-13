using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public partial class CloudScript
{
    public static void ArenaDefendTeamSave(MultiDict<int, int, UnitInfo> info, Action<int> finished)
    {
        PlayFabClientAPI.ExecuteCloudScript(
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
                    finished.Invoke(1);
                }
                else
                {
                    Debug.Log("通讯错误");
                }
            },
            error =>
            {
                finished.Invoke(-1);
                Debug.Log(error.Error);
            }
        );
    }
    
    public static void GetLeaderboardAroundUser(Action<List<LeaderboardInfo>> success, Action fail)
    {
        PlayFabClientAPI.ExecuteCloudScript(
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
                    List<LeaderboardInfo> opponents = JsonConvert.DeserializeObject<List<LeaderboardInfo>>(
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
                    success.Invoke(new List<LeaderboardInfo>());
                }
            },
            error => {
                Debug.Log(error.Error);
                fail.Invoke();
        });
    }
    
    public static void ArenaPointUp(Action success, Action fail)
    {
        PlayFabClientAPI.ExecuteCloudScript(
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
            },
            error => {
                Debug.Log(error.Error);
                fail.Invoke();
            });
    }
    
    public class LeaderboardInfo
    {
        public PlayerLeaderboardEntry PlayerLeaderboardEntry;
        public MultiDict<int, int, UnitInfo>.SerializableSet[] Team;
    }
}
