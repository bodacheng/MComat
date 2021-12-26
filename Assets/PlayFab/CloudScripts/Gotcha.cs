using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using dataAccess;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

public partial class CloudScript
{
    public static void GotchaX9(Action<List<StoneOfPlayerInfo>> action)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "GotchaX9", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new {
                    CatalogVersion = PlayfabSetting._StoneCatalog,
                    tableName = "TestGotcha"
                }, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
            (ExecuteCloudScriptResult result) => {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                object messageValue;
                jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
                Debug.Log(messageValue);
                
                var list = JsonConvert.DeserializeObject<List<JObject>>(messageValue.ToString()).Select(x => x?.ToObject<Dictionary<string, string>>()).ToList();
                List<StoneOfPlayerInfo> GotStones = new List<StoneOfPlayerInfo> ();
                foreach (var v in list)
                {
                    StoneOfPlayerInfo stoneOfPlayerInfo = new StoneOfPlayerInfo();
                    foreach (var s in v)
                    {
                        if (s.Key == "ItemInstanceId")
                        {
                            stoneOfPlayerInfo.InstanceId = s.Value;
                        }
                        if (s.Key == "ItemId")
                        {
                            stoneOfPlayerInfo.skillId = s.Value;
                            Debug.Log("Got this:" + stoneOfPlayerInfo.skillId);
                        }
                    }
                    Stones.Add(stoneOfPlayerInfo);
                    GotStones.Add(stoneOfPlayerInfo);
                }
                action(GotStones);
            },
            error => { Debug.Log(error.Error); });
    }
}