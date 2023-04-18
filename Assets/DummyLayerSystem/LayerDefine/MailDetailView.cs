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
    public void Read(MailItemInstance instance)
    {
        title.text = instance.DisplayName;
        var catalogItem = PlayFabReadClient.GetCatalogItemByDisplayName(instance.DisplayName);
        message.text = catalogItem != null ? catalogItem.Description : String.Empty;
        
        if (instance.ItemId.Contains("LoginBonus"))
        {
            title.text = Translate.Get(instance.ItemId);
            if (instance.CustomData != null && instance.CustomData.ContainsKey("streak"))
            {
                int streak = Convert.ToInt32(instance.CustomData["streak"]);
                message.text = Translate.Get(instance.ItemId+ "Intro").
                    Replace("$streak", instance.CustomData["streak"]).
                    Replace("$extraRemain", (((streak/ 7)+1)*7 -streak).ToString());
            }
        }
        
        if (instance.NotClaimed())
        {
            expirationT.gameObject.SetActive(instance.Expiration.HasValue);
            if (instance.Expiration.HasValue)
            {
                disposeCountDown = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Subscribe((_) =>
                {
                    var difference = instance.Expiration.Value - DateTime.Now;
                    difference = difference.Subtract(TimeSpan.FromSeconds(1));
                    expiration.text = difference.ToString(@"dd\:hh\:mm\:ss");
                    if (difference.TotalSeconds <= 0)
                    {
                        expirationT.gameObject.SetActive(false);
                        disposeCountDown.Dispose();
                    }
                }).AddTo(gameObject);
            }
            
            claimPresentBtn.gameObject.SetActive(true);
            claimPresentBtn.onClick.RemoveAllListeners();
            claimPresentBtn.onClick.AddListener(
                () => PlayFabReadClient.ClaimPresent(
                    instance.ItemId, 
                    (x)=>
                    {
                        PlayFabReadClient.SaveReadMailAsJson(x);
                        claimPresentBtn.gameObject.SetActive(false);
                        expirationT.gameObject.SetActive(false);
                    })
            );
        }
        else
        {
            claimPresentBtn.gameObject.SetActive(false);
            expirationT.gameObject.SetActive(false);
        }
        
        _iconRefresh(mailIcon, instance.ItemId);
    }
}
