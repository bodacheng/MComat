using UnityEngine;
using PlayFab;
using System.Collections.Generic;
using PlayFab.ClientModels;
using System;
using Newtonsoft.Json;
using Json;
using System.IO;

public partial class PlayFabReadClient
{
    #region MAIL
    static readonly List<MailItemInstance> _myMailList = new ();
    static readonly Dictionary<string, CatalogItem> _catalogItems = new ();
    
    public static List<MailItemInstance> GetMailsData()
    {
        return _myMailList;
    }

    public static CatalogItem GetCatalogItemByDisplayName(string displayName)
    {
        if (!_catalogItems.ContainsKey(displayName)) return null;
        var item = _catalogItems[displayName];
        return item;
    }

    public static void GetPresentGetCatalogItems()
    {
        PlayFabClientAPI.GetCatalogItems(
            new GetCatalogItemsRequest
            {
                CatalogVersion = PlayfabSetting._MailCatalog
            },
            (x)=>
            {
                foreach (var v in x.Catalog)
                {
                    DicAdd<string, CatalogItem>.Add(_catalogItems, v.DisplayName, v);
                }
            },
            (x) =>
            {
                Debug.Log(x.Error);
            }
        );
    }
    
    /// <summary>
    /// 点击某邮件后打开邮件会使用此函数
    /// </summary>
    public static ItemInstance Get(string itemInstanceId)
    {
        for (var i = 0; i < _myMailList.Count; i++)
        {
            if (_myMailList[i].ItemInstanceId == itemInstanceId)
                return _myMailList[i];
        }
        return null;
    }
    
    /// <summary>
    /// 添加实际邮件信息
    /// </summary>
    /// <param name="mailData"></param>
    static void AddMailData(MailItemInstance mailData)
    {
        _myMailList.Add(mailData);
    }
    
    public static void SaveReadMailAsJson(ItemInstance mailOfPlayer)
    {
        var json = JsonConvert.SerializeObject(mailOfPlayer);
        LocalJson.SaveToJsonFile_persistentDataPath("readmail", mailOfPlayer.ItemInstanceId + ".json", json);
    }
    
    /// <summary>
    /// 已读取邮件的获取
    /// </summary>
    static void LoadReadMails()
    {
        var path = Application.persistentDataPath + "/readmail";
        if (Directory.Exists(path))
        {
            foreach (var file in Directory.GetFiles(path))
            {
                try
                {
                    var dataAsJson = File.ReadAllText(file);
                    //Debug.Log("邮件信息已经读取："+ dataAsJson);
                    var mailOfPlayerModel = JsonConvert.DeserializeObject<MailItemInstance>(dataAsJson);
                    AddMailData(mailOfPlayerModel);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }
    }
    
    public static void DeleteAllLocalMails()
    {
        var toDelete = _myMailList.FindAll(x => (x.RemainingUses.Value <= 0));
        foreach (var d in toDelete)
        {
            _myMailList.Remove(d);
        }
        
        string path = Application.persistentDataPath + "/readmail";
        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }
    }
    
    public static void ClaimPresent(string ItemId, Action<ItemInstance> saveToLocal)
    {
        PlayFabClientAPI.UnlockContainerItem(
            new UnlockContainerItemRequest
            {
                CatalogVersion = PlayfabSetting._MailCatalog,
                ContainerItemId = ItemId
            },
            resultCallback => {
                Debug.Log(":"+ resultCallback.UnlockedItemInstanceId);
                ItemInstance target = null;

                foreach (var kv in resultCallback.VirtualCurrency)
                {
                    if (kv.Key == PlayfabSetting._GoldCode)
                    {
                        Debug.Log("get gold:"+ kv.Value);
                        Currencies.CoinCount.Value += (int)kv.Value;
                        
                    }
                    if (kv.Key == PlayfabSetting._DiamondCode)
                    {
                        Debug.Log("get diamond:"+ kv.Value);
                        Currencies.DiamondCount.Value += (int)kv.Value;
                    }
                }
                
                foreach (var data in _myMailList)
                {
                    if (data.ItemInstanceId == resultCallback.UnlockedItemInstanceId)
                    {
                        Debug.Log(resultCallback.UnlockedItemInstanceId + " is unlocked");
                        data.RemainingUses = 0;
                        data.Set();
                        target = data;
                    }
                }
                if (target != null)
                    saveToLocal.Invoke(target);
            },
            errorCallback => {
                Debug.Log(errorCallback.Error);
            }
        );
    }
    
    public static void ClaimAllPresentMails(Action<ItemInstance> saveToLocal)
    {
        CloudScript.ClaimAllPresentMails(_myMailList, saveToLocal);
    }
    
    #endregion
}
