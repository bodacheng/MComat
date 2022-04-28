using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;

public partial class CloudScript
{
    // k v : stoneid , equipingMonster, slot
    public static void UpdateSkillEdit(IDictionary<string, Tuple<string, string>> ToEditStones, Action success, Action fail)
    {
        List<PlayFab.ServerModels.UpdateUserInventoryItemDataRequest> Items = new List<PlayFab.ServerModels.UpdateUserInventoryItemDataRequest>();
        foreach (KeyValuePair<string, Tuple<string, string>> keyValuePair in ToEditStones)
        {
            var itemUpdate = new PlayFab.ServerModels.UpdateUserInventoryItemDataRequest
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
                FunctionName = "skillEdit", // Arbitrary function name (must exist in your uploaded cloud.js file)
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
