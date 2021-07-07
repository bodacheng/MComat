using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
//using PlayFab.PfEditor.Json;
using System.Collections.Generic;
using dataAccess;
using Api.Dto.Model;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

public partial class CloudScript
{
    public static void ArenaDefendTeamSave(MultiDict<int, int, CharDataInfo> info)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "ArenaDefendTeamSave",
                FunctionParameter = new { inputValue = info._SerializableSets },
                GeneratePlayStreamEvent = true
            },
            result =>
            {
                Debug.Log(result.FunctionResult);
            },
            error =>
            {
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
                    jsonResult.TryGetValue("teamInfos",
                        out teamInfos); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
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
    
    public class LeaderboardInfo
    {
        public PlayerLeaderboardEntry PlayerLeaderboardEntry;
        public MultiDict<int, int, CharDataInfo>.SerializableSet[] Team;
    }
}
