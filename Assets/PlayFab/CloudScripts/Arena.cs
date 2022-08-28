using UnityEngine;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using dataAccess;
using Newtonsoft.Json;

public partial class CloudScript
{
    public static void ArenaDefendTeamSave(MultiDic<int, int, UnitInfo> info, Action<bool> finished)
    {
        if (info.GetValues().Count != 3)
        {
            Debug.Log("No enough member.");
        }
        
        foreach (var kv in info.GetValues())
        {
            var skillList = Stones.GetEquippingStones(kv.id);
            if (skillList.Count != 9)
            {
                Debug.Log("No enough skill.");
            }
        }
        
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
                jsonResult.TryGetValue("success", out var succeed);
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
                jsonResult.TryGetValue("currentPoint", out var point); 
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
