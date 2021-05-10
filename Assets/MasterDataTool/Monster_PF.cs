using System.Collections.Generic;
using UnityEngine;
using Json;
using Newtonsoft.Json;
using System.Linq;

public partial class LocalMasterDataTool : MonoBehaviour
{
    public void OutputMonstersCatalog()
    {
        MonstersConfigTable.LoadMonstersConfig();
        PFDefine pFSKDefine = new PFDefine
        {
            CatalogVersion = "Monsters"
        };
        List<PFDefine.Item> items = new List<PFDefine.Item>();
        List<CharConfig> charsConfigs = MonstersConfigTable.Dic.Values.ToList();
        for (int i = 0; i < charsConfigs.Count; i++)
        {
            PFDefine.Item item = new PFDefine.Item()
            {
                ItemId = charsConfigs[i].RECORD_ID,
                DisplayName = charsConfigs[i].REAL_NAME
            };
            PFDefine.C_CustomData c_CustomData = new PFDefine.C_CustomData();
            c_CustomData.zokusei = ((int)charsConfigs[i]._zokusei).ToString();
            item.CustomData = c_CustomData.AsPlayFabVer();
            items.Add(item);
        }
        pFSKDefine.Catalog = items.ToArray();
        string json = JsonConvert.SerializeObject(pFSKDefine, Formatting.Indented);
        LocalJson.SaveInfoToJsonFile_persistentDataPath("PlayFab", "MonsterDefinationsJson.json", json);
    }

    public void OutputMonsterStore()
    {
        MonstersConfigTable.LoadMonstersConfig();
        PFStoreDefine pFSKDefine = new PFStoreDefine();
        pFSKDefine.StoreId = "monster";
        List<PFStoreDefine.StoreItem> storeitems = new List<PFStoreDefine.StoreItem>();

        List<CharConfig> charsConfigs = MonstersConfigTable.Dic.Values.ToList();
        for (int i = 0; i < charsConfigs.Count; i++)
        {
            PFStoreDefine.StoreItem storeitem = new PFStoreDefine.StoreItem()
            {
                ItemId = charsConfigs[i].RECORD_ID,
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
            DisplayName = "monsterstore"
        };

        string json = JsonConvert.SerializeObject(pFSKDefine, Formatting.Indented);
        json = "[" + json + "]";
        LocalJson.SaveInfoToJsonFile_persistentDataPath("PlayFab", "MonsterStoresDefinationsJson.json", json);
    }

    public void OutputCloudScriptPart_GetAllMonsters()
    {
        MonstersConfigTable.LoadMonstersConfig();
        string text =
        "handlers.getMonsterTest = function (args, context) {" +
        "var request = {" +
        "\"CatalogVersion\": \"Monsters\"," +
        "\"ItemGrants\": [";

        List<CharConfig> charsConfigs = MonstersConfigTable.Dic.Values.ToList();
        for (int i = 0; i < charsConfigs.Count; i++)
        {
            text +=
            "{" +
                "\"PlayFabId\": currentPlayerId," +
                "\"ItemId\": \""+ charsConfigs[i].RECORD_ID + "\"" +
            "}";
            if (i != charsConfigs.Count - 1)
                text += ",";
        }

        text += " ]};" +
        "var playerStatResult = server.GrantItemsToUsers(request);" +
        "};";

        LocalJson.SaveInfoToJsonFile_persistentDataPath("PlayFab", "GetAllMonstersCloudScriptPart.text", text);
    }
}
