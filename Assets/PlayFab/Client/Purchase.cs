using UnityEngine;
using System.Collections.Generic;
using Skill;
using PlayFab;

public partial class PlayFabReadClient
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
