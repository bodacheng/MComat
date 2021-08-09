using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class CloudScript
{
    public static void ArcadeProgress(string targetLevel ,Action<ExecuteCloudScriptResult> success, Action fail)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "completedLevel",
                FunctionParameter = new { level = "2" },
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                object level;
                jsonResult.TryGetValue("progressLevel", out level);

                Debug.Log(level.ToString());
                success.Invoke(result);
            },
            error => {
                Debug.Log(error.Error);
                fail.Invoke();
            });
    }
}