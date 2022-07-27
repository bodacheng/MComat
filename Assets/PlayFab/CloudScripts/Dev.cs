using UnityEngine;
using PlayFab.ClientModels;

public partial class CloudScript
{
    public static void GrantMonsterTest()
    {
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "getMonsterTest", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new { inputValue = "YOUR NAME" }, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
            OnGrantMonsters
        );
    }

    static void OnGrantMonsters(ExecuteCloudScriptResult result)
    {
        //Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
        Debug.Log(jsonResult);
    }

    public static void GrantStonesTest()
    {
        ExecuteCloudScriptMainSceneCommon(new ExecuteCloudScriptRequest()
            {
                FunctionName = "getStonesTest", // Arbitrary function name (must exist in your uploaded cloud.js file)
                FunctionParameter = new { inputValue = "YOUR NAME" }, // The parameter provided to your function
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
            OnGrantStoness
        );
    }

    static void OnGrantStoness(ExecuteCloudScriptResult result)
    {
        //Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
        Debug.Log(jsonResult);
    }
    
    public static void Remove25Stones()
    {
        ExecuteCloudScriptMainSceneCommon(
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
                PlayFabReadClient.LoadItems((x) =>{});
            }
        );
    }
}
