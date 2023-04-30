using DummyLayerSystem;
using mainMenu;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class ShopTop : MSceneProcess
{
    private ShopTopLayer shopTopLayer;
    public ShopTop()
    {
        Step = MainSceneStep.ShopTop;
    }
    
    public override void ProcessEnter()
    {
        shopTopLayer = UILayerLoader.Load<ShopTopLayer>();
        shopTopLayer.Initialize();
        var upperInfoBar = UILayerLoader.Load<UpperInfoBar>();
        upperInfoBar.Setup(null,
            null, 
            null,
            null);
        PlayFabClientAPI.GetUserReadOnlyData
        (
            new GetUserDataRequest()
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabId,
                Keys = new List<string> { "BeginnerBundleBought" }
            },
            (obj) => {
                if (!obj.Data.ContainsKey("BeginnerBundleBought"))
                {
                    shopTopLayer.ShowBeginnerBundle(true);
                }
            },
            errorCallback => {
            }
        );
        
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