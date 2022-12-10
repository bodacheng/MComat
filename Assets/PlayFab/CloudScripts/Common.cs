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
        PlayFabClientAPI.ExecuteCloudScript(
            request,
            resultCallback, 
            (x)=>
            {
                Debug.Log(x.Error);
                errorCallback?.Invoke(x);
                PopupLayer.ArrangeWarnWindow(x.ErrorMessage);
            }, 
            customData, extraHeaders);
    }
}
