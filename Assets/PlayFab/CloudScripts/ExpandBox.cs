using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public partial class CloudScript
{
    public static void ExpandBox10()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
            {
                FunctionName = "expandBox10", // Arbitrary function name (must exist in your uploaded cloud.js file)
                GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
            },
            (ExecuteCloudScriptResult result) => {
                int newSize = Convert.ToInt32(result.FunctionResult);
                PlayerAccountInfo.Me.StoneBoxSize = newSize;
                Debug.Log("盒子容量成功扩大到" + PlayerAccountInfo.Me.StoneBoxSize);
            },
            error => { Debug.Log("failed"); });
    }
}
