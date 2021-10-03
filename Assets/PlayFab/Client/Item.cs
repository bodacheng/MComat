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
        _myMailList.Clear();

        PlayFabClientAPI.GetUserInventory(
            new GetUserInventoryRequest(),
            (GetUserInventoryResult result) =>
            {
                foreach (var item in result.Inventory)
                {
                    //Debug.Log(item.CatalogVersion + ":" + item.ItemId);
                    if (item.CatalogVersion == PlayfabSetting._UnitCatalog)
                    {
                        UnitInfo info = new UnitInfo
                        {
                            id = item.ItemInstanceId,
                            r_id = item.ItemId
                        };
                        DicAdd<string, UnitInfo>.Add(MyMonsters.Dic, item.ItemInstanceId, info);
                    }
                    else if (item.CatalogVersion == PlayfabSetting._StoneCatalog)
                    {
                        StoneOfPlayerInfo Info = new StoneOfPlayerInfo
                        {
                            InstanceId = item.ItemInstanceId,
                            skillId = item.ItemId,
                            inUsingMonsterOfPlayerId = (item.CustomData != null && item.CustomData.ContainsKey("monsterid")) ? item.CustomData["monsterid"] : null,
                            inUsingSkillSlot = (item.CustomData != null && item.CustomData.ContainsKey("slot")) ? item.CustomData["slot"] : null
                        };
                        Stones.Add(Info);
                    }
                    else if (item.CatalogVersion == PlayfabSetting._MailCatalog)
                    {
                        MailItemInstance maildata = new MailItemInstance();
                        Copier<ItemInstance,MailItemInstance>.Copy(item, maildata);
                        AddMailData(maildata);
                    }
                }
                LoadReadMails(); // 本地逻辑。读取已读邮件。放在这里是希望和远程读取未读邮件的动作保持步调一致
                
                foreach (var kv in result.VirtualCurrency)
                {
                    if (kv.Key == PlayfabSetting._GoldCode)
                    {
                        Currencies.CoinCount = kv.Value;
                    }
                    else if (kv.Key == PlayfabSetting._DiamondCode)
                    {
                        Currencies.DiamondCount = kv.Value;
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
