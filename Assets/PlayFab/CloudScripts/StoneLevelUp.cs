using UnityEngine;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using dataAccess;

public partial class CloudScript
{
    public static void UpdateStone(SkillStoneLevelUpForm form, Action<string, List<string>> success)
    {
        var Items = new List<PlayFab.AdminModels.RevokeInventoryItem>();

        if (form.M1Stone == null || form.M2Stone == null || form.M3Stone == null || form.M4Stone == null)
        {
            Debug.Log("error");
            return;
        }
        
        var resource1 = new PlayFab.AdminModels.RevokeInventoryItem()
        {
            ItemInstanceId = form.M1Stone,
            PlayFabId = PlayerAccountInfo.Me.PlayFabId
        };
        
        var resource2 = new PlayFab.AdminModels.RevokeInventoryItem()
        {
            ItemInstanceId = form.M2Stone,
            PlayFabId = PlayerAccountInfo.Me.PlayFabId
        };
        
        var resource3 = new PlayFab.AdminModels.RevokeInventoryItem()
        {
            ItemInstanceId = form.M3Stone,
            PlayFabId = PlayerAccountInfo.Me.PlayFabId
        };
        
        var resource4 = new PlayFab.AdminModels.RevokeInventoryItem()
        {
            ItemInstanceId = form.M4Stone,
            PlayFabId = PlayerAccountInfo.Me.PlayFabId
        };
        
        Items.Add(resource1);
        Items.Add(resource2);
        Items.Add(resource3);
        Items.Add(resource4);
        
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "updateStone",
                FunctionParameter = new
                {
                    targetItemInstanceId = form.targetStoneID,
                    resources = Items,
                    addLevel = form.addLevel
                }, 
                GeneratePlayStreamEvent = true,
            },
            (result) => {
                var jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
                object successReturn, level;
                jsonResult.TryGetValue("success", out successReturn);
                jsonResult.TryGetValue("level", out level);
                
                Debug.Log(successReturn + ", Level:" + level);
                int newLevel = Convert.ToInt32(level);
                if ((bool)successReturn)
                {
                    var targetInfo = Stones.Get(form.targetStoneID);
                    targetInfo.Level = newLevel;
                    success.Invoke(
                        form.targetStoneID,
                        new List<string>()
                        {
                            form.M1Stone,
                            form.M2Stone,
                            form.M3Stone,
                            form.M4Stone,
                        }
                    );
                }
            }
        );
    }
}
