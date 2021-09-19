using UnityEngine;
using PlayFab;
using System.Collections.Generic;
using PlayFab.ClientModels;
using dataAccess;
using System;

public partial class PlayFabReadClient
{
    #region MAIL
    static List<MailOfPlayerModel> _myMailList = new List<MailOfPlayerModel>();
    public static List<MailOfPlayerModel> GetMailsData()
    {
        return _myMailList;
    }
    /// <summary>
    /// 点击某邮件后打开邮件会使用此函数
    /// </summary>
    /// <param name="mailID"></param>
    /// <returns></returns>
    public static MailOfPlayerModel Get(string mailID)
    {
        for (int i = 0; i < _myMailList.Count; i++)
        {
            if (_myMailList[i].mailId == mailID)
                return _myMailList[i];
        }
        return null;
    }
    /// <summary>
    /// 添加实际邮件信息
    /// </summary>
    /// <param name="mailData"></param>
    public static void AddMailData(MailOfPlayerModel mailData)
    {
        _myMailList.Add(mailData);
    }
    #endregion
    
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
                        Debug.Log("One mail:" + item.ItemInstanceId);
                        MailOfPlayerModel maildata = new MailOfPlayerModel
                        {
                            mailId = item.ItemInstanceId,
                            itemId = item.ItemId,
                            title = item.DisplayName
                        };
                        AddMailData(maildata);
                    }
                }
                MailBox.LoadReadMails(); // 本地逻辑。读取已读邮件。放在这里是希望和远程读取未读邮件的动作保持步调一致


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

    public static void ClaimPresent(string itemId, Action saveAsRead)
    {
        Debug.Log("try open box:" + itemId);
        PlayFabClientAPI.UnlockContainerItem(
            new UnlockContainerItemRequest
            {
                CatalogVersion = PlayfabSetting._MailCatalog,
                ContainerItemId = itemId
            },
            resultCallback => {
                Debug.Log(":"+ resultCallback.UnlockedItemInstanceId);

                foreach (var data in _myMailList)
                {
                    if (data.mailId == itemId)
                    {
                        data.read = true;
                    }
                }
                saveAsRead.Invoke();
            },
            errorCallback => {
                Debug.Log(errorCallback.Error);
            }
        );
    }
}
