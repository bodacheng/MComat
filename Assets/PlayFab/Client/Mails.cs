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
    static List<MailItemInstance> _myMailList = new List<MailItemInstance>();
    public static List<MailItemInstance> GetMailsData()
    {
        return _myMailList;
    }
    /// <summary>
    /// 点击某邮件后打开邮件会使用此函数
    /// </summary>
    /// <param name="mailID"></param>
    /// <returns></returns>
    public static ItemInstance Get(string itemInstanceId)
    {
        for (int i = 0; i < _myMailList.Count; i++)
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
        string json = JsonConvert.SerializeObject(mailOfPlayer);
        LocalJson.SaveToJsonFile_persistentDataPath("readmail", mailOfPlayer.ItemInstanceId + ".json", json);
    }
    
    /// <summary>
    /// 已读取邮件的获取
    /// </summary>
    public static void LoadReadMails()
    {
        string path = Application.persistentDataPath + "/readmail";
        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                try
                {
                    string dataAsJson = File.ReadAllText(file);
                    //Debug.Log("邮件信息已经读取："+ dataAsJson);
                    MailItemInstance mailOfPlayerModel = JsonConvert.DeserializeObject<MailItemInstance>(dataAsJson);
                    PlayFabReadClient.AddMailData(mailOfPlayerModel);
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
        string path = Application.persistentDataPath + "/readmail";
        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                try
                {
                    string dataAsJson = File.ReadAllText(file);
                    MailItemInstance mailOfPlayerModel = JsonConvert.DeserializeObject<MailItemInstance>(dataAsJson);
                    MailItemInstance v = _myMailList.Find(x => x.ItemInstanceId == mailOfPlayerModel.ItemInstanceId);
                    _myMailList.Remove(v);
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }
    }
    
    public static void ClaimPresent(string ItemId, Action<ItemInstance> Success)
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
                    Success.Invoke(target);
            },
            errorCallback => {
                Debug.Log(errorCallback.Error);
            }
        );
    }
    
    #endregion
}
