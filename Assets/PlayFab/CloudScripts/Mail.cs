using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
public partial class CloudScript
{
    public static void claimAllPresentMails(List<MailItemInstance> _myMailList, Action<ItemInstance> saveToLocal)
    {
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "claimAllPresentMails",
                GeneratePlayStreamEvent = true
            },
            (ExecuteCloudScriptResult result) => {
                PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                object gd, dm, unlockedidlist;
                jsonResult.TryGetValue("diamond", out dm);
                jsonResult.TryGetValue("gold", out gd);
                jsonResult.TryGetValue("UnlockedItemInstanceIds", out unlockedidlist);
                Debug.Log(" 获得黄金"+ gd.ToString()+" 宝石"+ dm.ToString());
                List<string> unlockedids =JsonConvert.DeserializeObject<List<string>>(unlockedidlist.ToString());

                foreach (var data in _myMailList)
                {
                    if (unlockedids.Contains(data.ItemInstanceId))
                    {
                        data.RemainingUses = 0;
                        data.Set();
                        saveToLocal(data);
                    }
                }
            },
            error => {
                Debug.Log(error.Error);
            }
        );
    }
}
