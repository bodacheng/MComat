using PlayFab.ClientModels;
using System;

public partial class CloudScript
{
    public static void ArcadeProgress(string stage, Action<ExecuteCloudScriptResult> success)
    {
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "completedLevel",
                FunctionParameter = new { level = stage },
                GeneratePlayStreamEvent = true
            },
            success.Invoke);
    }
}