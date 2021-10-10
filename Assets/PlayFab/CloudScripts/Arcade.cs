using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class CloudScript
{
    public static void ArcadeProgress(string targetLevel, Action<ExecuteCloudScriptResult> success, Action fail)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "completedLevel",
                FunctionParameter = new { level = targetLevel },
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                success.Invoke(result);
            },
            error => {
                Debug.Log(error.Error);
                fail.Invoke();
            });
    }
}