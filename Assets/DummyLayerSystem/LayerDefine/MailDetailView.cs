using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using System;

public class MailDetailView : UILayer
{
    public Image mailIcon;
    public Text title;
    public Text message;
    public Text expiration;
    public Button claimPresentBtn;
    
    private Action<Image, string> _iconRefresh;
    public void Setup(Action<Image, string> iconRefresh)
    {
        this._iconRefresh = iconRefresh;
    }
    
    public void Read(ItemInstance model)
    {
        title.text = model.DisplayName;
        var catalogItem = PlayFabReadClient.GetCatalogItemByDisplayName(model.DisplayName);
        message.text = catalogItem != null ? catalogItem.Description : String.Empty;

        if (model.ItemId == "normalLoginBonus")
        {
            message.text = message.text.Replace("$streak", PlayerAccountInfo.Me.loginStreak.ToString());
        }
        
        if (model.Expiration.HasValue)
            expiration.text = model.Expiration.Value.ToString("yyyy-MM-dd");
        else
        {
            expiration.text = "无时间限制？";
        }
        claimPresentBtn.onClick.RemoveAllListeners();
        claimPresentBtn.onClick.AddListener(
            () => PlayFabReadClient.ClaimPresent(model.ItemId, 
                PlayFabReadClient.SaveReadMailAsJson)
        );
        _iconRefresh(mailIcon, model.ItemId);
    }
}
