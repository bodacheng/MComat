using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using System;

public class MailDetailView : UILayer
{
    #region MailDetail
    public RectTransform detailPartT;
    public Image mailIcon;
    public Text title;
    public Text message;
    public Text presentlifeRemain;
    public Button ClaimPresentBtn;
    #endregion
    
    public void Read(ItemInstance model)
    {
        title.text = model.DisplayName;
        var catalogItem = PlayFabReadClient.GetCatalogItemByDisplayName(model.DisplayName);
        message.text = catalogItem != null ? catalogItem.Description : String.Empty;
        if (model.Expiration.HasValue)
            presentlifeRemain.text = model.Expiration.Value.ToString("yyyy-MM-dd");
        else
        {
            presentlifeRemain.text = "无时间限制？";
        }
        ClaimPresentBtn.onClick.RemoveAllListeners();
        ClaimPresentBtn.onClick.AddListener(
            () => PlayFabReadClient.ClaimPresent(model.ItemId, 
                PlayFabReadClient.SaveReadMailAsJson)
        );
    }
}
