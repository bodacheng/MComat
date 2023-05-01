using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener {

    public static IAPManager Target;
    // The Unity Purchasing system
    private static IStoreController _mStoreController;
    // Items list, configurable via inspector
    private static List<CatalogItem> _productCatalog;
    private static List<CatalogItem> _stoneCatalog;
    private string productClassName = "Product";
    private string ProductCatalogVersion = "Product";
    private string StoneProductCatalogVersion = "stone";
    private bool productCatalogInitialised = false;
    private bool stoneCatalogInitialised = false;

    public static List<CatalogItem> StoneCatalog => _stoneCatalog;

    string ProductCatalog(string productId)
    {
        var productProductIds = _productCatalog.Select(x=> x.ItemId).ToList();
        var stoneProductIds = _stoneCatalog.Select(x=> x.ItemId).ToList();
        if (productProductIds.Contains(productId))
        {
            return ProductCatalogVersion;
        }
        if (stoneProductIds.Contains(productId))
        {
            return StoneProductCatalogVersion;
        }
        return null;
    }
    
    private bool StoneProductCatalogInitialised
    {
        get => stoneCatalogInitialised;
        set {
            stoneCatalogInitialised = value;
            if (productCatalogInitialised && stoneCatalogInitialised)
            {
                InitializePurchasing();
            }
        }
    }
    
    private bool ProductCatalogInitialised
    {
        get => productCatalogInitialised;
        set {
            productCatalogInitialised = value;
            if (productCatalogInitialised && stoneCatalogInitialised)
            {
                InitializePurchasing();
            }
        }
    }
    
    // Bootstrap the whole thing
    public void Start() {
        // Make PlayFab log in
        Target = this;
        RefreshIAPItems();
    }
    
    void RefreshIAPItems() {
        if (IsInitialized)
        {
            return;
        }
        PlayFabClientAPI.GetCatalogItems(
            new GetCatalogItemsRequest
            {
                CatalogVersion = ProductCatalogVersion
            },
        result => {
                _productCatalog = result.Catalog;
                // Make UnityIAP initialize
                ProductCatalogInitialised = true;
            }, 
        error => Debug.LogError(error.GenerateErrorReport())
        );
        
        PlayFabClientAPI.GetCatalogItems(
            new GetCatalogItemsRequest
            {
                CatalogVersion = StoneProductCatalogVersion
            },
            result => {
                _stoneCatalog = result.Catalog;
                // Make UnityIAP initialize
                StoneProductCatalogInitialised = true;
            }, 
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    public string GetProductLocalPriceString(string productId)
    {
        var productInfo = _mStoreController.products.WithID(productId);
        if (productInfo != null)
            return productInfo.metadata.localizedPriceString;
        return "Not Available";
    }

    // This is invoked manually on Start to initialize UnityIAP
    void InitializePurchasing() {
        // If IAP is already initialized, return gently
        
        if (IsInitialized) return;
        
#if UNITY_IOS
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.AppleAppStore));
#endif

#if UNITY_ANDROID
        // Create a builder for IAP service
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay));
#endif
        // Register each item from the catalog
        foreach (var item in _productCatalog) {
            if (item.ItemClass == productClassName)
                builder.AddProduct(item.ItemId, ProductType.Consumable);
        }
        
        foreach (var item in _stoneCatalog) {
            if (item.ItemClass == productClassName)
                builder.AddProduct(item.ItemId, ProductType.Consumable);
        }
        
        // Trigger IAP service initialization
        UnityPurchasing.Initialize(this, builder);
    }

    // We are initialized when StoreController and Extensions are set and we are logged in
    public bool IsInitialized => _mStoreController != null;

    // This is automatically invoked automatically when IAP service is initialized
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
        Debug.Log("Initialized ：" + controller);
        _mStoreController = controller;
    }

    // This is automatically invoked automatically when IAP service failed to initialized
    public void OnInitializeFailed(InitializationFailureReason error) {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string? message)
    {
        throw new NotImplementedException();
    }

    // This is automatically invoked automatically when purchase failed
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    int DMAmount(string productId)
    {
        var rewardDMAmount = 0;
        switch (productId)
        {
            case "diamond100":
                rewardDMAmount = 100;
                break;
            case "diamond300":
                rewardDMAmount = 300;
                break;
            case "diamond500":
                rewardDMAmount = 500;
                break;
            case "diamond1000":
                rewardDMAmount = 1000;
                break;
        }
        return rewardDMAmount;
    }
    
    // This is invoked automatically when successful purchase is ready to be processed
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e) {
        // NOTE: this code does not account for purchases that were pending and are
        // delivered on application start.
        // Production code should account for such case:
        // More: https://docs.unity3d.com/ScriptReference/Purchasing.PurchaseProcessingResult.Pending.html

        Debug.Log("ProcessPurchase");
        
        if (!IsInitialized) {
            return PurchaseProcessingResult.Complete;
        }
        
        // Test edge case where product is unknown
        if (e.purchasedProduct == null) {
            Debug.LogWarning("Attempted to process purchase with unknown product. Ignoring");
            return PurchaseProcessingResult.Complete;
        }

        // Test edge case where purchase has no receipt
        if (string.IsNullOrEmpty(e.purchasedProduct.receipt)) {
            Debug.LogWarning("Attempted to process purchase with no receipt: ignoring");
            return PurchaseProcessingResult.Complete;
        }
        
        var boughtItemCatalog = ProductCatalog(e.purchasedProduct.definition.id);
        
        #if UNITY_IOS
        var wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(e.purchasedProduct.receipt);
        var store = (string)wrapper["Store"];
        var payload = (string)wrapper["Payload"]; // For Apple this will be the base64 encoded ASN.1 receipt

        //Debug.Log("CurrencyCode:"+e.purchasedProduct.metadata.isoCurrencyCode);
        //Debug.Log("PurchasePrice:"+(int)e.purchasedProduct.metadata.localizedPrice);
        
        PlayFabClientAPI.ValidateIOSReceipt(
            new ValidateIOSReceiptRequest
            {
                CatalogVersion = boughtItemCatalog,
                CurrencyCode = e.purchasedProduct.metadata.isoCurrencyCode,
                PurchasePrice = (int)(e.purchasedProduct.metadata.localizedPrice * 100),//(int)e.purchasedProduct.metadata.localizedPrice * DMAmount(e.purchasedProduct.definition.id),
                ReceiptData = payload
            }, result => {
                ProgressLayer.Close();
                PopupLayer.ArrangeWarnWindow(Translate.Get("PurchaseSuccess"));
                _mStoreController.ConfirmPendingPurchase(e.purchasedProduct);
                CloudScript.BoughtBundle(
                    e.purchasedProduct.definition.id, 
                    () =>
                    {
                        var shopTopLayer = UILayerLoader.Get<ShopTopLayer>();
                        if (shopTopLayer != null)
                            shopTopLayer.DisableStoneBundle(e.purchasedProduct.definition.id);
                    }
                );
                PlayFabReadClient.LoadItems(null);
            },
            error => {
                ProgressLayer.Close();
                PopupLayer.ArrangeWarnWindow(Translate.Get("PurchaseFail"));
                Debug.Log("Validation failed: " + error.GenerateErrorReport());
            }
        );
        #endif
        
        #if UNITY_ANDROID
        // Deserialize receipt
        var googleReceipt = GooglePurchase.FromJson(e.purchasedProduct.receipt);

        // Invoke receipt validation
        // This will not only validate a receipt, but will also grant player corresponding items
        // only if receipt is valid.
        
        PlayFabClientAPI.ValidateGooglePlayPurchase(
            new ValidateGooglePlayPurchaseRequest() {
                CatalogVersion = boughtItemCatalog,
                // Pass in currency code in ISO format
                CurrencyCode = e.purchasedProduct.metadata.isoCurrencyCode,
                // Convert and set Purchase price
                PurchasePrice = (uint)(e.purchasedProduct.metadata.localizedPrice * 100),//(uint)(e.purchasedProduct.metadata.localizedPrice * DMAmount(e.purchasedProduct.definition.id)),
                // Pass in the receipt
                ReceiptJson = googleReceipt.PayloadData.json,
                // Pass in the signature
                Signature = googleReceipt.PayloadData.signature
            }, result => {
                Debug.Log("Validation successful!");
                _mStoreController.ConfirmPendingPurchase(e.purchasedProduct);
                CloudScript.BoughtBundle(
                    e.purchasedProduct.definition.id, 
                    () =>
                    {
                        var shopTopLayer = UILayerLoader.Get<ShopTopLayer>();
                        if (shopTopLayer != null)
                            shopTopLayer.DisableStoneBundle(e.purchasedProduct.definition.id);
                    }
                );
                PlayFabReadClient.LoadItems(null);
            },
            error => Debug.Log("Validation failed: " + error.GenerateErrorReport())
        );
        #endif
        
        return PurchaseProcessingResult.Complete;
    }
        
    // This is invoked manually to initiate purchase
    public void BuyProductID(string productId) {
        // If IAP service has not been initialized, fail hard
        
        if (!IsInitialized) throw new Exception("IAP Service is not initialized!");
        ProgressLayer.Loading(Translate.Get("PurchaseProcessing"));
        // Pass in the product id to initiate purchase
        _mStoreController.InitiatePurchase(productId);
    }
}

// The following classes are used to deserialize JSON results provided by IAP Service
// Please, note that JSON fields are case-sensitive and should remain fields to support Unity Deserialization via JsonUtilities
public class JsonData {
    // JSON Fields, ! Case-sensitive

    public string orderId;
    public string packageName;
    public string productId;
    public long purchaseTime;
    public int purchaseState;
    public string purchaseToken;
}

public class PayloadData {
    public JsonData JsonData;

    // JSON Fields, ! Case-sensitive
    public string signature;
    public string json;

    public static PayloadData FromJson(string json) {
        var payload = JsonUtility.FromJson<PayloadData>(json);
        payload.JsonData = JsonUtility.FromJson<JsonData>(payload.json);
        return payload;
    }
}

public class GooglePurchase {
    public PayloadData PayloadData;

    // JSON Fields, ! Case-sensitive
    public string Store;
    public string TransactionID;
    public string Payload;

    public static GooglePurchase FromJson(string json) {
        var purchase = JsonUtility.FromJson<GooglePurchase>(json);
        purchase.PayloadData = PayloadData.FromJson(purchase.Payload);
        return purchase;
    }
}