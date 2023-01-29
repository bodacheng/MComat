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
    static readonly List<MailItemInstance> MyMailList = new ();
    static readonly Dictionary<string, CatalogItem> CatalogItems = new ();
    
    public static List<MailItemInstance> GetMailsData(bool onlyUnRead = false)
    {
        if (!onlyUnRead)
            return MyMailList;
        return MyMailList.FindAll(x=> x.NotClaimed());
    }

    public static CatalogItem GetCatalogItemByDisplayName(string displayName)
    {
        if (!CatalogItems.ContainsKey(displayName)) return null;
        var item = CatalogItems[displayName];
        return item;
    }

    public static void GetMailCatalogItems(Action<bool> finished)
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
                    DicAdd<string, CatalogItem>.Add(CatalogItems, v.DisplayName, v);
                }
                finished?.Invoke(true);
            },
            (x) =>
            {
                finished?.Invoke(false);
                Debug.Log(x.ErrorMessage);
            }
        );
    }
    
    /// <summary>
    /// 点击某邮件后打开邮件会使用此函数
    /// </summary>
    public static MailItemInstance Get(string itemInstanceId)
    {
        for (var i = 0; i < MyMailList.Count; i++)
        {
            if (MyMailList[i].ItemInstanceId == itemInstanceId)
                return MyMailList[i];
        }
        return null;
    }
    
    /// <summary>
    /// 添加实际邮件信息
    /// </summary>
    /// <param name="mailData"></param>
    static void AddMailData(MailItemInstance mailData)
    {
        MyMailList.Add(mailData);
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
        var toDelete = MyMailList.FindAll(x => (x.RemainingUses.Value <= 0));
        foreach (var d in toDelete)
        {
            MyMailList.Remove(d);
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
    
    public static void ClaimPresent(string itemId, Action<ItemInstance> saveToLocal)
    {
        PlayFabClientAPI.UnlockContainerItem(
            new UnlockContainerItemRequest
            {
                CatalogVersion = PlayfabSetting._MailCatalog,
                ContainerItemId = itemId
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
                
                foreach (var data in MyMailList)
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
        CloudScript.ClaimAllPresentMails(MyMailList, saveToLocal);
    }
    
    #endregion
}
