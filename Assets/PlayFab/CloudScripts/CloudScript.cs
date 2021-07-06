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

public class CloudScript
{
    public static void StartCloudHelloWorld()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "helloWorld", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { inputValue = "YOUR NAME" }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        },
        OnCloudHelloWorld,
        error => { Debug.Log("failed"); });
    }

    static void OnCloudHelloWorld(ExecuteCloudScriptResult result)
    {
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }

    public static void ExpandBox()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "expandBox", // Arbitrary function name (must exist in your uploaded cloud.js file)
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        },
        (ExecuteCloudScriptResult result) => {
            int newSize = Convert.ToInt32(result.FunctionResult);
            Account._AccInfo.Stoneboxsize = newSize;
            Debug.Log("盒子容量成功扩大到" + Account._AccInfo.Stoneboxsize);
        },
        error => { Debug.Log("failed"); });
    }

    public static void GrantMonsterTest()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "getMonsterTest", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { inputValue = "YOUR NAME" }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        },
        OnGrantMonsters,
        error => { Debug.Log("failed"); });
    }

    static void OnGrantMonsters(ExecuteCloudScriptResult result)
    {
        //Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
        Debug.Log(jsonResult);
    }

    public static void GrantStonesTest()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "getStonesTest", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { inputValue = "YOUR NAME" }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        },
        OnGrantStoness,
        error => { Debug.Log("failed"); });
    }

    static void OnGrantStoness(ExecuteCloudScriptResult result)
    {
        //Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
        Debug.Log(jsonResult);
    }

    public static void GachaTest(Action<List<StoneOfPlayerInfo>> action)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "Gacha", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new {
                    CatalogVersion = "stoneTest2",
                    tableName = "TestGotcha"
                }, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
            (ExecuteCloudScriptResult result) => {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                object messageValue;
                jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
                Debug.Log(messageValue);
                StoneOfPlayerInfo stoneOfPlayerInfo = new StoneOfPlayerInfo();
                var list = JsonConvert.DeserializeObject<List<JObject>>(messageValue.ToString()).Select(x => x?.ToObject<Dictionary<string, string>>()).ToList();
                foreach (var v in list)
                {
                    foreach (var s in v)
                    {
                        if (s.Key == "ItemInstanceId")
                        {
                            stoneOfPlayerInfo.InstanceId = s.Value;
                        }
                        if (s.Key == "ItemId")
                        {
                            stoneOfPlayerInfo.skillId = s.Value;
                        }
                        //Debug.Log(s.Key + ":" + s.Value);
                    }
                }

                Stones.Add(stoneOfPlayerInfo);
                List<StoneOfPlayerInfo> stones = new List<StoneOfPlayerInfo> { stoneOfPlayerInfo };
                action(stones);
            },
            error => { Debug.Log(error.Error); });
    }

    public static void Remove25Stones()
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "Remove25Stones", // Arbitrary function name (must exist in your uploaded cloud.js file)
                //FunctionParameter = new { inputValue = Items.ToArray() }, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
        (ExecuteCloudScriptResult result) => {
            PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
            object currentItemCount;
            jsonResult.TryGetValue("currentItemCount", out currentItemCount); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
            Debug.Log(currentItemCount);
            ItemLoader.LoadAll(new Action<int>((x) => { }));
        },
        error => { Debug.Log(error.Error); });
    }

    // k v : stoneid , equipingMonster, slot
    public static void UpdateSkillEdit(IDictionary<string, Tuple<string, string>> ToEditStones, Action success, Action fail)
    {
        List<PlayFab.ServerModels.UpdateUserInventoryItemDataRequest> Items = new List<PlayFab.ServerModels.UpdateUserInventoryItemDataRequest>();
        foreach (KeyValuePair<string, Tuple<string, string>> keyValuePair in ToEditStones)
        {
            PlayFab.ServerModels.UpdateUserInventoryItemDataRequest itemUpdate = new PlayFab.ServerModels.UpdateUserInventoryItemDataRequest
            {
                //PlayFabId = AccountSet._AccInfo.playerID,
                ItemInstanceId = keyValuePair.Key,
                Data = new Dictionary<string, string>()
                {
                    { "monsterid", keyValuePair.Value.Item1 },
                     { "slot", keyValuePair.Value.Item2 }
                },
            };
            Items.Add(itemUpdate);
        }

        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "SkillEdit", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new { inputValue = Items }, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
        (ExecuteCloudScriptResult result) => {
            PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
            object messageValue;
            jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
            Debug.Log(messageValue);
            success.Invoke();
        },
        error => {
            Debug.Log(error.Error);
            fail.Invoke();
        });
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
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;

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
            },
            error => {
                Debug.Log(error.Error);
                fail.Invoke();
            });
    }
}

public class LeaderboardInfo
{
    public PlayerLeaderboardEntry PlayerLeaderboardEntry;
    public MultiDict<int, int, CharDataInfo>.SerializableSet[] Team;
}