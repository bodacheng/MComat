using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;

public partial class CloudScript
{
    static void ExecuteCloudScriptMainSceneCommon(
        ExecuteCloudScriptRequest request, 
        Action<ExecuteCloudScriptResult> resultCallback, 
        Action<PlayFabError> errorCallback = null, 
        object customData = null, Dictionary<string, string> extraHeaders = null)
    {
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
                Debug.Log(x.Error);
                errorCallback?.Invoke(x);
                ProgressLayer.Close();
                PopupLayer.ArrangeWarnWindow(x.ErrorMessage);
            }, 
            customData, extraHeaders);
    }
}
