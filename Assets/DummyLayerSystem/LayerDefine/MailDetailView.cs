using UnityEngine.UI;
using System;
using UniRx;
using UnityEngine;

public class MailDetailView : UILayer
{
    [SerializeField] Image mailIcon;
    [SerializeField] Text title;
    [SerializeField] Text message;
    [SerializeField] RectTransform expirationT;
    [SerializeField] Text expiration;
    [SerializeField] Button claimPresentBtn;
    
    private Action<Image, string> _iconRefresh;
    public void Setup(Action<Image, string> iconRefresh)
    {
        this._iconRefresh = iconRefresh;
    }
    
    private IDisposable disposeCountDown;
    public void Read(MailItemInstance model)
    {
        title.text = model.DisplayName;
        var catalogItem = PlayFabReadClient.GetCatalogItemByDisplayName(model.DisplayName);
        message.text = catalogItem != null ? catalogItem.Description : String.Empty;
        
        if (model.ItemId == "normalLoginBonus")
        {
            message.text = message.text.Replace("$streak", PlayerAccountInfo.Me.loginStreak.ToString());
        }
        
        if (model.Expiration.HasValue)
        {
            disposeCountDown = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Subscribe((_) =>
            {
                var difference = model.Expiration.Value - DateTime.Now;
                difference = difference.Subtract(TimeSpan.FromSeconds(1));
                expiration.text = difference.ToString(@"dd\:hh\:mm\:ss");
            }).AddTo(gameObject);
        }
        else
        {
            expirationT.gameObject.SetActive(false);
        }
        
        if (model.NotRead())
        {
            claimPresentBtn.gameObject.SetActive(true);
            claimPresentBtn.onClick.RemoveAllListeners();
            claimPresentBtn.onClick.AddListener(
                () => PlayFabReadClient.ClaimPresent(model.ItemId, 
                    PlayFabReadClient.SaveReadMailAsJson)
            );
        }
        else
        {
            claimPresentBtn.gameObject.SetActive(false);
        }
        _iconRefresh(mailIcon, model.ItemId);
    }
}
