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
                FunctionName = "arenaDefendTeamSave",
                FunctionParameter = new { inputValue = info._SerializableSets },
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) =>
            {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject) result.FunctionResult;
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
                    PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject) result.FunctionResult;
                    
                    object teamInfos;
                    jsonResult.TryGetValue("teamInfos", out teamInfos); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
                    string json = JsonConvert.SerializeObject(teamInfos);
                    Debug.Log(json);
                    List<LeaderboardInfo> oo = JsonConvert.DeserializeObject<List<LeaderboardInfo>>(json,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                    success.Invoke(oo);
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
    
    public static void ArenaPointUpBy1(Action success, Action fail)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "arenaPointUpBy1",
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject) result.FunctionResult;
                object playerStatResult;
                jsonResult.TryGetValue("playerStatResult",
                    out playerStatResult); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
                string json = JsonConvert.SerializeObject(playerStatResult);
                Debug.Log(json);
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
