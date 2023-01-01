using UnityEngine;
using PlayFab.ClientModels;
using System.Collections.Generic;
using dataAccess;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using PlayFab;
using PlayFab.ServerModels;
using ExecuteCloudScriptResult = PlayFab.ClientModels.ExecuteCloudScriptResult;

public partial class CloudScript
{
    public static void GetDropTableInfo(Action<RandomResultTableListing> success, string tableID)
    {
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest
            {
                FunctionName = "stoneDropTableInfo",
                FunctionParameter = new
                {
                    TableID = tableID//"GotchaX9"
                }
            } ,
            (x) =>
            {
                var jsonResult = (PlayFab.Json.JsonObject)x.FunctionResult;
                jsonResult.TryGetValue("result", out var messageValue);
                var result = JsonConvert.DeserializeObject<GetRandomResultTablesResult>(messageValue.ToString());
                foreach (var tableInfo in result.Tables)
                {
                    Debug.Log("Table:"+ tableInfo.Key);
                    foreach (var stoneRate in tableInfo.Value.Nodes)
                    {
                        Debug.Log(stoneRate.ResultItem + ":"+ stoneRate.Weight);
                    }
                    success(tableInfo.Value);
                }
            }, (x) =>
            {
                Debug.Log(x);
            });
    }
    
    public static void GotchaX9(Action<List<StoneOfPlayerInfo>> action)
    {
        PlayFabClientAPI.PurchaseItem(
            new PurchaseItemRequest
            {
                CatalogVersion = "stone",
                StoreId = "Gotcha",
                ItemId = "GotchaX9",
                VirtualCurrency = "DM",
                Price = 90
            },
            (x) =>
            {
                var GotStones = new List<StoneOfPlayerInfo> ();
                if (x.Items.Count > 0)
                {
                    foreach (var skillId in x.Items[0].BundleContents)
                    {
                        var stoneOfPlayerInfo = new StoneOfPlayerInfo
                        {
                            SkillId = skillId
                        };
                        GotStones.Add(stoneOfPlayerInfo);
                    }
                }
                action(GotStones);
                Currencies.DiamondCount.Value -= 90;
            },
            (x) =>
            {
                PopupLayer.ArrangeWarnWindow(x.ErrorMessage);
            });
    }
}