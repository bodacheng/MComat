using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class NoAdsProductCell : MonoBehaviour
{
    [SerializeField] private Text rewardedAdDMCount;

    void Start()
    {
        RequestAdRewards();
    }
    
    public void RequestAdRewards()
    {
        // 执行云脚本
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest
        {
            FunctionName = "GetSkippedAdRewards", // 云脚本的函数名
        }, OnCloudScriptSuccess, OnCloudScriptFailure);
    }

    void OnCloudScriptSuccess(ExecuteCloudScriptResult result)
    {
        // 检查返回值并处理
        if (result.FunctionResult != null)
        {
            // 将FunctionResult解析为JSON对象
            var jsonResult = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(result.FunctionResult));
        
            // 从JSON对象中提取reward值
            if (jsonResult.ContainsKey("rewardDM"))
            {
                int reward = Convert.ToInt32(jsonResult["rewardDM"]);
                Debug.Log("Received ad reward: " + reward);
                rewardedAdDMCount.text = reward.ToString();
                // 在这里，你可以根据返回的奖励值来更新游戏状态或UI
            }
            else
            {
                Debug.LogError("Reward not found in cloud script result");
            }
        }
        else
        {
            Debug.LogError("No result received from cloud script");
        }
    }

    void OnCloudScriptFailure(PlayFabError error)
    {
        Debug.LogError("Failed to execute cloud script: " + error.GenerateErrorReport());
    }
}
