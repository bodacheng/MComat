using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using dataAccess;

public partial class CloudScript
{
    public static void ArcadeProgress(string targetLevel ,Action<ExecuteCloudScriptResult> success, Action fail)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "completedLevel",
                FunctionParameter = new { level = targetLevel },
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                object level;
                jsonResult.TryGetValue("progressLevel", out level);
                
                string levelstring = level.ToString();
                int le;
                int.TryParse(levelstring, out le) ;
                Account._AccInfo.ArcadeProcess = le;
                Debug.Log("new progress:" + le);
                success.Invoke(result);
            },
            error => {
                Debug.Log(error.Error);
                fail.Invoke();
            });
    }
}