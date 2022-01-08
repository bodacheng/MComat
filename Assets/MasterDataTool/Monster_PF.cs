using System.Collections.Generic;
using UnityEngine;
using Json;
using Newtonsoft.Json;
using System.Linq;

public partial class MasterDataTool : MonoBehaviour
{
    public void OutputMonstersCatalog()
    {
        Units.LoadMonstersConfig();
        PFDefine pFSKDefine = new PFDefine
        {
            CatalogVersion = "Monsters"
        };
        List<PFDefine.Item> items = new List<PFDefine.Item>();
        List<UnitConfig> charsConfigs = Units.Dic.Values.ToList();
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
        LocalJson.SaveToJsonFile_persistentDataPath("PlayFab", "MonsterDefinationsJson.json", json);
    }

    public void OutputMonsterStore()
    {
        Units.LoadMonstersConfig();
        PFStoreDefine pFSKDefine = new PFStoreDefine();
        pFSKDefine.StoreId = "monster";
        List<PFStoreDefine.StoreItem> storeitems = new List<PFStoreDefine.StoreItem>();

        List<UnitConfig> charsConfigs = Units.Dic.Values.ToList();
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
        LocalJson.SaveToJsonFile_persistentDataPath("PlayFab", "MonsterStoresDefinationsJson.json", json);
    }

    public void OutputCloudScriptPart_GetAllMonsters()
    {
        Units.LoadMonstersConfig();
        string text =
        "handlers.getMonsterTest = function (args, context) {" +
        "var request = {" +
        "\"CatalogVersion\": \"Monsters\"," +
        "\"ItemGrants\": [";

        List<UnitConfig> charsConfigs = Units.Dic.Values.ToList();
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

        LocalJson.SaveToJsonFile_persistentDataPath("PlayFab", "GetAllMonstersCloudScriptPart.text", text);
    }
}
