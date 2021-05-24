using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Api.Dto.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Skill;
using Json;
using PlayFab;

namespace dataAccess
{
    public partial class MySkillStones
    {
        public static void PurchaseStones(List<SkillConfig> stones, int i)
        {
            if (i != stones.Count - 1)
            {
                SkillConfig targetStoneConfig = stones[i];
                PlayFabClientAPI.PurchaseItem(
                    new PlayFab.ClientModels.PurchaseItemRequest()
                    {
                        CatalogVersion = "stoneTest2",
                        ItemId = targetStoneConfig.RECORD_ID,
                        StoreId = "stone",
                        VirtualCurrency = "GD",
                        Price = 0
                    }, result =>
                    {
                        Debug.Log("成功购买" + targetStoneConfig.RECORD_ID);
                        PurchaseStones(stones, i + 1);
                    }, error =>
                    {
                        Debug.Log(error.GenerateErrorReport());
                    }
                );
            }
        }
    }
}
