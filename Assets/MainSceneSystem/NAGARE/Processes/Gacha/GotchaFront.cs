using System;
using System.Collections.Generic;
using dataAccess;
using DummyLayerSystem;
using mainMenu;
using PlayFab.ClientModels;
using PlayFab;

public class GotchaFront : MSceneProcess
{
    private GotchaLayer layer;
    
    public GotchaFront()
    {
        Step = MainSceneStep.GotchaFront;
    }
    
    private int startIndex;
    private Action extraSuccessAction;
    public void SetExtraSuccessAction(Action _extraSuccessAction)
    {
        extraSuccessAction = _extraSuccessAction;
    }
    
    void MoveNext(int direction, List<DropTablePage> dropTables)
    {
        if (direction > 0)
        {
            startIndex = startIndex + 1;
            if (startIndex == dropTables.Count)
            {
                startIndex = 0;
            }
        }
        else if (direction < 0)
        {
            startIndex = startIndex - 1;
            if (startIndex < 0)
            {
                startIndex = dropTables.Count - 1;
            }
        }

        for (var i = 0; i < dropTables.Count; i++)
        {
            var dropTable = dropTables[i];
            dropTable.parentT.gameObject.SetActive(startIndex == i);
        }
    }
     
    public override void ProcessEnter()
    {
        StarsFall.target.gameObject.SetActive(true);
        var CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        BackGroundPS.target.Off();
        layer = UILayerLoader.Load<GotchaLayer>();
        layer.Setup(NineTimes, DropTableInfo, MoveNext,
            GetAllSK, GetAllM, Remove25Stones);
        
        var upperInfoBar = UILayerLoader.Load<UpperInfoBar>();
        upperInfoBar.Setup(null,
            null,null,
            () =>
            {
                PreScene.target.trySwitchToStep(MainSceneStep.ShopTop, true);
            });
        
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<UpperInfoBar>();
        UILayerLoader.Remove<GotchaLayer>();
        StarsFall.target.gameObject.SetActive(false);
    }
    
    void DropTableInfo(string dropTableId)
    {
        PreScene.target.trySwitchToStep(MainSceneStep.DropTableInfo, dropTableId, true);
    }
    
    /// <summary>
    /// 缺少消费关联处理
    /// </summary>
    void NineTimes(string itemId, string currencyCode, int currencyCount)
    {
        switch (currencyCode)
        {
            case "DM":
                if (Currencies.DiamondCount.Value < currencyCount)
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.ShopTop);
                    return;
                }
                break;
            case "GD":
                if (Currencies.CoinCount.Value < currencyCount)
                {
                    PopupLayer.ArrangeWarnWindow(Translate.Get("NoEnoughGD"));
                    return;
                }
                break;
        }
        
        UILayerLoader.Remove<GotchaLayer>();// 点击按钮瞬间关闭layer。
        PlayFabClientAPI.PurchaseItem(
            new PurchaseItemRequest
            {
                CatalogVersion = "stone",
                StoreId = "StoneGotcha",
                ItemId = itemId,
                VirtualCurrency = currencyCode,
                Price = currencyCount
            },
            (x) =>
            {
                var GotStones = new List<StoneOfPlayerInfo> ();
                if (x.Items.Count > 0)
                {
                    foreach (var skillId in x.Items[0].BundleContents)
                    {
                        var stoneOfPlayerInfo = new StoneOfPlayerInfo
                        {
                            SkillId = skillId
                        };
                        GotStones.Add(stoneOfPlayerInfo);
                    }
                }

                switch (currencyCode)
                {
                    case "DM":
                        Currencies.DiamondCount.Value -= currencyCount;
                        break;
                    case "GD":
                        Currencies.CoinCount.Value -= currencyCount;
                        break;
                }
                
                GotchaResult.Result = GotStones;
                PreScene.target.trySwitchToStep(MainSceneStep.GotchaResult, true);
                extraSuccessAction?.Invoke();
            },
            (x) =>
            {
                PopupLayer.ArrangeWarnWindow(x.ErrorMessage);
            });
    }
    
    static void GetAllSK()
    {
        CloudScript.GrantStonesTest();
    }

    static void GetAllM()
    {
        CloudScript.GrantMonsterTest();
    }

    static void Remove25Stones()
    {
        CloudScript.Remove25Stones();
    }
}
