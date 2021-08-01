using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
public partial class CloudScript
{
    public static void ArcadeProgress(string targetLevel ,Action<ExecuteCloudScriptResult> success, Action fail)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "completedLevel",
                FunctionParameter = new { levelName = targetLevel },
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject) result.FunctionResult;
                foreach (var  f in jsonResult.Values)
                {
                    Debug.Log(f);
                }
                
                
                success.Invoke(result);
            },
            error => {
                Debug.Log(error.Error);
                fail.Invoke();
            });
    }
}