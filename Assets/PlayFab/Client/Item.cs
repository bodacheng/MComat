using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using dataAccess;
using System;

public partial class PlayFabReadClient
{
    public static void LoadItems(Action<int> finished)
    {
        MyMonsters.Dic.Clear();
        Stones.Clear();

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
                            UnitInfo info = new UnitInfo
                            {
                                id = item.ItemInstanceId,
                                r_id = item.ItemId
                            };
                            DicAdd<string, UnitInfo>.Add(MyMonsters.Dic, item.ItemInstanceId, info);
                            break;
                        case "stoneTest2":
                            StoneOfPlayerInfo Info = new StoneOfPlayerInfo
                            {
                                InstanceId = item.ItemInstanceId,
                                skillId = item.ItemId,
                                inUsingMonsterOfPlayerId = (item.CustomData != null && item.CustomData.ContainsKey("monsterid")) ? item.CustomData["monsterid"] : null,
                                inUsingSkillSlot = (item.CustomData != null && item.CustomData.ContainsKey("slot")) ? item.CustomData["slot"] : null

                            };
                            Stones.Add(Info);
                            break;
                        case "BoxTest":
                            Debug.Log("One mail:" + item.ItemInstanceId);
                            MailOfPlayerModel maildata = new MailOfPlayerModel
                            {
                                mailId = item.ItemInstanceId,
                                itemId = item.ItemId,
                                title = item.DisplayName
                            };
                            MailManager.target.AddMailData(maildata);
                            break;
                    }
                }
                Debug.Log("目前技能石总数量：" + Stones.Dic.Count);
                foreach (var kv in result.VirtualCurrency)
                {
                    switch (kv.Key)
                    {
                        case "GD":
                            Currencies.CoinCount = kv.Value;
                            break;
                        case "DM":
                            Currencies.DiamondCount = kv.Value;
                            break;
                        default:
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

    public static void ClaimPresent(string itemId)
    {
        Debug.Log("try open box:" + itemId);
        PlayFabClientAPI.UnlockContainerItem(
            new UnlockContainerItemRequest
            {
                CatalogVersion = "BoxTest",
                ContainerItemId = itemId
            },
            resultCallback => {
                Debug.Log(":"+ resultCallback.UnlockedItemInstanceId);
            },
            errorCallback => {
                Debug.Log(errorCallback.Error);
            }
        );
    }
}
