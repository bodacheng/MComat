using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using dataAccess;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public partial class PlayFabReadClient
{
    public static void LoadItems(Action<bool> finished)
    {
        dataAccess.Units.Dic.Clear();
        Stones.ClearData();
        Stones.ClearRender();
        MyMailList.Clear();
        
        PlayFabClientAPI.GetUserInventory(
            new GetUserInventoryRequest(),
            result =>
            {
                OnGetUserInventory(result, finished);
            },
            errorCallback => {
                Debug.Log(errorCallback.ErrorMessage);
                finished?.Invoke(false);
            }
        );
    }

    static async void OnGetUserInventory(GetUserInventoryResult result, Action<bool> finished)
    {
        List<UniTask> stoneLoadTasks = new List<UniTask>();
        foreach (var item in result.Inventory)
        {
            //Debug.Log(item.CatalogVersion + ":" + item.ItemId);
            if (item.CatalogVersion == PlayfabSetting._UnitCatalog)
            {
                var info = new UnitInfo
                {
                    id = item.ItemInstanceId,
                    r_id = item.ItemId
                };
                DicAdd<string, UnitInfo>.Add(dataAccess.Units.Dic, item.ItemInstanceId, info);
            }
            else if (item.CatalogVersion == PlayfabSetting._StoneCatalog)
            {
                if (SkillConfigTable.GetSkillConfig(item.ItemId) != null)
                {
                    var Info = new StoneOfPlayerInfo
                    {
                        InstanceId = item.ItemInstanceId,
                        SkillId = item.ItemId,
                        Level = (item.CustomData != null && item.CustomData.ContainsKey("level")) ? Convert.ToInt32(item.CustomData["level"]) : 1,
                        UnitInstanceId = (item.CustomData != null && item.CustomData.ContainsKey("unitInstanceId")) ? item.CustomData["unitInstanceId"] : null,
                        Slot = (item.CustomData != null && item.CustomData.ContainsKey("slot")) ? item.CustomData["slot"] : null,
                        Born = (item.CustomData != null && item.CustomData.ContainsKey("born")) ? item.CustomData["born"] : null
                    };
                    stoneLoadTasks.Add( Stones.Add(Info));
                }
                else
                {
                    // 可能是stone类型的bundle等等
                }
            }
            else if (item.CatalogVersion == PlayfabSetting._MailCatalog)
            {
                var mailData = new MailItemInstance();
                Copier<ItemInstance,MailItemInstance>.Copy(item, mailData);
                AddMailData(mailData);
            }
        }

        await UniTask.WhenAll(stoneLoadTasks);
        LoadReadMails(); // 本地逻辑。读取已读邮件。放在这里是希望和远程读取未读邮件的动作保持步调一致
        
        foreach (var kv in result.VirtualCurrency)
        {
            if (kv.Key == PlayfabSetting._GoldCode)
            {
                Currencies.CoinCount.Value = kv.Value;
            }
            else if (kv.Key == PlayfabSetting._DiamondCode)
            {
                Currencies.DiamondCount.Value = kv.Value;
            }
            else if (kv.Key == PlayfabSetting._ArenaTicketCode)
            {
                Currencies.ArenaTicket.Value = kv.Value;
            }
            else if (kv.Key == PlayfabSetting._AdTicketCode)
            {
                Currencies.AdTicket.Value = kv.Value;
            }
        }

        foreach (var kv in result.VirtualCurrencyRechargeTimes)
        {
            if (kv.Key == PlayfabSetting._ArenaTicketCode)
            {
                Currencies.SecondsToRechargeArenaTicket = kv.Value.SecondsToRecharge;
                Currencies.ArenaTicketRechargeMax = kv.Value.RechargeMax;
            }
            if (kv.Key == PlayfabSetting._AdTicketCode)
            {
                Currencies.SecondsToRechargeAdTicket = kv.Value.SecondsToRecharge;
                Currencies.AdTicketRechargeMax = kv.Value.RechargeMax;
            }
        }
        
        finished?.Invoke(true);
    }
}
