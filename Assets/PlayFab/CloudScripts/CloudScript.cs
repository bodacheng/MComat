using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.PfEditor.Json;

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
}

