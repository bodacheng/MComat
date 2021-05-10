using System.Collections.Generic;
using UnityEngine;
using Skill;
using System.Collections;
using Log;
using Json;
using Newtonsoft.Json;
using System.Linq;

public partial class LocalMasterDataTool : MonoBehaviour
{
    /// <summary>
    /// 生成gs2 技能石master更新文件
    /// </summary>
    /// <param name="textAsset"></param>
    /// <returns></returns>
    ///
    public void OutputSKStonesCatalog()
    {
        SkillConfigTable.LoadAllSkillConfigs();
        PFDefine pFSKDefine = new PFDefine();
        pFSKDefine.CatalogVersion = "stoneTest2";
        List<PFDefine.Item> items = new List<PFDefine.Item>();
        List<SkillConfig> stoneDefinationList = SkillConfigTable.SkillConfigRefDic.Values.ToList();
        for (int i = 0; i < stoneDefinationList.Count; i++)
        {
            PFDefine.Item item = new PFDefine.Item()
            {
                ItemId = stoneDefinationList[i].RECORD_ID,
                DisplayName = stoneDefinationList[i].REAL_NAME
            };
            item.CustomData = null;
            items.Add(item);
        }
        pFSKDefine.Catalog = items.ToArray();
        string json = JsonConvert.SerializeObject(pFSKDefine, Formatting.Indented);

        LocalJson.SaveInfoToJsonFile_persistentDataPath("PlayFab", "StoneDefinationsJson.json", json);
    }

    public void OutputSKStonesStore()
    {
        SkillConfigTable.LoadAllSkillConfigs();
        PFStoreDefine pFSKDefine = new PFStoreDefine();
        pFSKDefine.StoreId = "stone";
        List<PFStoreDefine.StoreItem> storeitems = new List<PFStoreDefine.StoreItem>();

        List<SkillConfig> stoneDefinationList = SkillConfigTable.SkillConfigRefDic.Values.ToList();
        for (int i = 0; i < stoneDefinationList.Count; i++)
        {
            PFStoreDefine.StoreItem storeitem = new PFStoreDefine.StoreItem()
            {
                ItemId = stoneDefinationList[i].RECORD_ID,
                VirtualCurrencyPrices = new PFStoreDefine.VirtualCurrencyPrices
                {
                    GD = 0
                }
            };
            storeitems.Add(storeitem);
        }
        pFSKDefine.Store = storeitems.ToArray();
        pFSKDefine.MarketingData = new PFStoreDefine._MarketingData
        {
            DisplayName = "stonestore"
        };

        string json = JsonConvert.SerializeObject(pFSKDefine, Formatting.Indented);
        json = "[" + json + "]";
        LocalJson.SaveInfoToJsonFile_persistentDataPath("PlayFab", "StoneStoreDefinationsJson.json", json);
    }

    public void OutputCloudScriptPart_GetAllStones()
    {
        SkillConfigTable.LoadAllSkillConfigs();
        string text =
        "handlers.getStonesTest = function (args, context) {" +
        "var request = {" +
        "\"CatalogVersion\": \"stoneTest2\"," +
        "\"ItemGrants\": [";

        List<SkillConfig> stoneDefinationList = SkillConfigTable.SkillConfigRefDic.Values.ToList();
        for (int i = 0; i < stoneDefinationList.Count; i++)
        {
            text +=
            "{" +
                "\"PlayFabId\": currentPlayerId," +
                "\"ItemId\": \"" + stoneDefinationList[i].RECORD_ID + "\"" +
            "}";
            if (i != stoneDefinationList.Count - 1)
                text += ",";
        }

        text += " ]};" +
        "var playerStatResult = server.GrantItemsToUsers(request);" +
        "};";

        LocalJson.SaveInfoToJsonFile_persistentDataPath("PlayFab", "GetAllStonesCloudScriptPart.text", text);
    }
}
