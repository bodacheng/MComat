using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using PlayFab.CloudScriptModels;
using ExecuteCloudScriptResult = PlayFab.ClientModels.ExecuteCloudScriptResult;

public partial class CloudScript
{
    public static void ExecuteCloudScriptMainSceneCommon(
        ExecuteCloudScriptRequest request, 
        Action<ExecuteCloudScriptResult> resultCallback, 
        Action<PlayFabError> errorCallback = null, 
        object customData = null, Dictionary<string, string> extraHeaders = null)
    {
        if (Application.isPlaying)
            ProgressLayer.Loading(string.Empty);
        
        PlayFabClientAPI.ExecuteCloudScript(
            request,
            (x)=>
            {
                resultCallback(x);
                ProgressLayer.Close();
            },
            (x)=>
            {
                errorCallback?.Invoke(x);
                PlayFabReadClient.ErrorReport(x);
            },
            customData, extraHeaders);
    }
    
    public static void ExecuteFunctionCommon(
        ExecuteFunctionRequest request, 
        Action<ExecuteFunctionResult> resultCallback, 
        Action<PlayFabError> errorCallback = null, 
        object customData = null, Dictionary<string, string> extraHeaders = null,
        bool showLoading = true, bool showErrorPopup = true)
    {
        if (showLoading && Application.isPlaying)
            ProgressLayer.Loading(string.Empty);
        
        PlayFabCloudScriptAPI.ExecuteFunction( request,
            (x)=>
            {
                resultCallback(x);
                if (showLoading)
                    ProgressLayer.Close();
            },
            (x)=>
            {
                errorCallback?.Invoke(x);
                if (showErrorPopup)
                    PlayFabReadClient.ErrorReport(x);
            },
            customData, extraHeaders);
    }
}
