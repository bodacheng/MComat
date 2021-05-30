using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.PfEditor.Json;
using System.Collections.Generic;
using dataAccess;
using Api.Dto.Model;
using System;
using Json;
using LitJson;
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
        Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
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

    class temp
    {
        public string ItemInstanceId;
        public string ItemId;
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

    public static void RandomRemove25Items()
    {
        var Items = new JsonArray();
        List<string> ids = new List<string>();
        foreach (KeyValuePair<string, StoneOfPlayerInfo> keyValuePair in Stones.Dic)
        {
            Items.Add(
                new PlayFab.ServerModels.RevokeInventoryItem()
                {
                    ItemInstanceId = keyValuePair.Key
                }
            );
            ids.Add(keyValuePair.Key);
            if (Items.Count == 25)
            {
                break;
            }
        }

        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "RandomRemove25Items", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new { inputValue = Items.ToArray() }, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
        (ExecuteCloudScriptResult result) => {
            foreach (string id in ids)
            {
                Stones.RemoveStoneLocal(id);
            };
            PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
            object messageValue;
            jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
            Debug.Log(messageValue);
        },
        error => { Debug.Log(error.Error); });
    }

    // k v : stoneid , equipingMonster, slot
    public static void UpdateSkillEdit(IDictionary<string, Tuple<string, string>> ToEditStones, Action success, Action fail)
    {
        var Items = new JsonArray();
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
}

