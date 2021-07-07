using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using dataAccess;
using System;

public partial class CloudScript
{
    public static void ExpandBox5()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "expandBox5", // Arbitrary function name (must exist in your uploaded cloud.js file)
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        },
        (ExecuteCloudScriptResult result) => {
            int newSize = Convert.ToInt32(result.FunctionResult);
            Account._AccInfo.Stoneboxsize = newSize;
            Debug.Log("盒子容量成功扩大到" + Account._AccInfo.Stoneboxsize);
        },
        error => { Debug.Log("failed"); });
    }
    
    public static void ExpandBox10()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "expandBox10", // Arbitrary function name (must exist in your uploaded cloud.js file)
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
            (ExecuteCloudScriptResult result) => {
                int newSize = Convert.ToInt32(result.FunctionResult);
                Account._AccInfo.Stoneboxsize = newSize;
                Debug.Log("盒子容量成功扩大到" + Account._AccInfo.Stoneboxsize);
            },
            error => { Debug.Log("failed"); });
    }
}
