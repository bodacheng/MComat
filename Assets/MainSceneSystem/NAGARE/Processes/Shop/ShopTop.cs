using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using PlayFab;
using PlayFab.ClientModels;
using System.Linq;

public class ShopTop : MSceneProcess
{
    private ShopTopLayer shopTopLayer;
    public ShopTop()
    {
        Step = MainSceneStep.ShopTop;
    }
    
    public override void ProcessEnter()
    {
        var upperInfoBar = UILayerLoader.Load<UpperInfoBar>();
        upperInfoBar.Setup(null,
            null, 
            null,
            null,
            PlayerAccountInfo.Me.noAdsState);

        var stoneCatalog = IAPManager.StoneProductCatalog;
        var stoneProductIds = stoneCatalog.Select(x=> x.ItemId).ToList();
        if (stoneProductIds.Count > 0)
        {
            PlayFabClientAPI.GetUserReadOnlyData
            (
                new GetUserDataRequest()
                {
                    PlayFabId = PlayerAccountInfo.Me.PlayFabId,
                    Keys = stoneProductIds
                },
                (obj) =>
                {
                    var showStoneBundleIds = new List<string>();
                    foreach (var productId in stoneProductIds)
                    {
                        if (!obj.Data.ContainsKey(productId))
                        {
                            showStoneBundleIds.Add(productId);
                        }
                    }
                    shopTopLayer = UILayerLoader.Load<ShopTopLayer>();
                    shopTopLayer.Initialize();
                    shopTopLayer.ShowStoneBundle(showStoneBundleIds);
                },
                errorCallback => {
                }
            );
        }
        else
        {
            shopTopLayer = UILayerLoader.Load<ShopTopLayer>();
            shopTopLayer.Initialize();
        }
        
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<UpperInfoBar>();
        UILayerLoader.Remove<ShopTopLayer>();
    }
    
    public override void LocalUpdate()
    {
    
    }
}