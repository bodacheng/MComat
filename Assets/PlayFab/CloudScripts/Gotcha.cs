using UnityEngine;
using PlayFab.ClientModels;
using System.Collections.Generic;
using dataAccess;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using PlayFab.ServerModels;
using ExecuteCloudScriptResult = PlayFab.ClientModels.ExecuteCloudScriptResult;

public partial class CloudScript
{
    public static void GetDropTableInfo(Action<RandomResultTableListing> success, string tableID)
    {
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest
            {
                FunctionName = "stoneDropTableInfo",
                FunctionParameter = new
                {
                    TableID = tableID//"GotchaX9"
                }
            } ,
            (x) =>
            {
                var jsonResult = (PlayFab.Json.JsonObject)x.FunctionResult;
                jsonResult.TryGetValue("result", out var messageValue);
                var result = JsonConvert.DeserializeObject<GetRandomResultTablesResult>(messageValue.ToString());
                foreach (var tableInfo in result.Tables)
                {
                    Debug.Log("Table:"+ tableInfo.Key);
                    foreach (var stoneRate in tableInfo.Value.Nodes)
                    {
                        Debug.Log(stoneRate.ResultItem + ":"+ stoneRate.Weight);
                    }
                    success(tableInfo.Value);
                }
            }, (x) =>
            {
                Debug.Log(x);
            });
    }
    
    public static void GotchaX9(Action<List<StoneOfPlayerInfo>> action)
    {
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "GotchaX9", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new {
                    CatalogVersion = PlayfabSetting._StoneCatalog,
                    tableName = "GotchaX9"
                }, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
            (ExecuteCloudScriptResult result) => {
                var jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                jsonResult.TryGetValue("messageValue", out var messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
                Debug.Log(messageValue);
                
                var list = JsonConvert.DeserializeObject<List<JObject>>(messageValue.ToString()).Select(x => x?.ToObject<Dictionary<string, string>>()).ToList();
                var GotStones = new List<StoneOfPlayerInfo> ();
                foreach (var v in list)
                {
                    var stoneOfPlayerInfo = new StoneOfPlayerInfo();
                    foreach (var s in v)
                    {
                        if (s.Key == "ItemInstanceId")
                        {
                            stoneOfPlayerInfo.InstanceId = s.Value;
                        }
                        if (s.Key == "ItemId")
                        {
                            stoneOfPlayerInfo.SkillId = s.Value;
                            Debug.Log("Got this:" + stoneOfPlayerInfo.SkillId);
                        }
                    }
                    Stones.Add(stoneOfPlayerInfo);
                    GotStones.Add(stoneOfPlayerInfo);
                }
                action(GotStones);
            }
        );
    }
}