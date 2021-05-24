using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Api.Dto.Model;
using dataAccess;
using System;

public static class PlayFabRead
{
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
                    //Debug.Log(item.CatalogVersion + ":" + item.ItemId);
                    switch (item.CatalogVersion)
                    {
                        case "Monsters":
                            MonsterOfPlayerInfo info = new MonsterOfPlayerInfo
                            {
                                InstanceId = item.ItemInstanceId,
                                monsterId = item.ItemId
                            };
                            DicAdd<string, MonsterOfPlayerInfo>.Add(MyMonsters.Dic, item.ItemInstanceId, info);
                            break;
                        case "stoneTest2":
                            StoneOfPlayerInfo Info = new StoneOfPlayerInfo
                            {
                                InstanceId = item.ItemInstanceId,
                                skillId = item.ItemId,
                                inUsingMonsterOfPlayerId = (item.CustomData != null && item.CustomData.ContainsKey("monsterid")) ? item.CustomData["monsterid"] : null,
                                inUsingSkillSlot = (item.CustomData != null && item.CustomData.ContainsKey("slot")) ? item.CustomData["slot"] : null

                            };
                            MySkillStones.Add(Info);
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