using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.PfEditor.Json;
using Api.Dto.Model;
using dataAccess;
using System;

public class CloudScript
{
    public static void StartCloudHelloWorld()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "helloWorld", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { inputValue = "YOUR NAME" }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        },
        OnCloudHelloWorld,
        error => { Debug.Log("failed"); });
    }

    static void OnCloudHelloWorld(ExecuteCloudScriptResult result)
    {
        Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }
}

public static class PlayFabRead {

    public static void LoadItems(Action<int> finished)
    {
        MyMonsters.Dic.Clear();
        MySkillStones.Clear();

        PlayFabClientAPI.GetUserInventory(
            new GetUserInventoryRequest(),
            (GetUserInventoryResult result) =>
            {
                foreach (var item in result.Inventory)
                {
                    Debug.Log(item.CatalogVersion + ":" + item.ItemId);
                    switch (item.CatalogVersion)
                    {
                        case "chars":
                            MonsterOfPlayerDetailModel info = new MonsterOfPlayerDetailModel
                            {
                                monsterOfPlayerId = item.ItemId,
                                monsterId = item.CustomData["monsterID"]
                            };
                            DicAdd<string, MonsterOfPlayerDetailModel>.Add(MyMonsters.Dic, item.ItemId, info);
                            break;
                        case "stone":
                            StoneOfPlayerInfoModel skillStoneOfPlayerInfo = new StoneOfPlayerInfoModel
                            {
                                skillStoneOfPlayerId = item.ItemId,
                                skillId = item.CustomData["skillId"]
                            };
                            MySkillStones.Read(skillStoneOfPlayerInfo);
                        break;
                    }
                }
                finished.Invoke(1);
            },
            errorCallback => {
                Debug.Log(errorCallback.Error);
                finished.Invoke(-1);
            });
    }
}