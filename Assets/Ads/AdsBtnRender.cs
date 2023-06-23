using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class AdsBtnRender : MonoBehaviour
{
    [SerializeField] private Text remainCount;
    [SerializeField] private Text ticketChargeCountDown;
    [SerializeField] private RectTransform ticketChargeCountDownT;
    [SerializeField] private RewardedAdsButton rewardedAdsButton;

    //[Header("google ads")]
    //[SerializeField] private GoogleMobileAdsManager googleMobileAdsManager;
    
    private IDisposable _disposeCountDown;
    
    public void Setup()
    {
        // unity
        rewardedAdsButton.LoadAd();
        rewardedAdsButton.SetExtraEnableCondition(()=> Currencies.AdTicket.Value > 0);
        rewardedAdsButton.SetWatchedAdExtraProcess(
            () =>
            {
                // Grant a reward.
                CloudScript.SubtractVirtualCurrency(
                    "AD",1,
                    ()=> CloudScript.RequestAdReward("GD", PlayFabSetting._adTicketRewardGD)
                );
            }
        );
        // google
        //googleMobileAdsManager.SetEnableCondition(()=> Currencies.AdTicket.Value > 0);
        
        Currencies.AdTicket.Subscribe(
            x=>
            {
                _disposeCountDown?.Dispose();
                remainCount.text = "Remain x"+ x;
                if (x >= Currencies.AdTicketRechargeMax)
                {
                    ticketChargeCountDown.text = string.Empty;
                    ticketChargeCountDownT.gameObject.SetActive(false);
                }
                else
                {
                    _disposeCountDown = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Subscribe((_) =>
                    {
                        ticketChargeCountDownT.gameObject.SetActive(true);
                        if (Currencies.SecondsToRechargeAdTicket > 0)
                        {
                            var minute = Currencies.SecondsToRechargeAdTicket / 60;
                            var seconds = Currencies.SecondsToRechargeAdTicket - minute * 60;
                            ticketChargeCountDown.text = "Stamina up in "+ $"{minute :00}:{seconds:00}";
                            Currencies.SecondsToRechargeAdTicket -= 1;
                        }
                        else
                        {
                            Currencies.ArenaTicket.Value += 1;
                            Currencies.SecondsToRechargeAdTicket = 60 * 60;
                        }
                    }).AddTo(gameObject);
                }
                rewardedAdsButton.Enable(x > 0);
                //googleMobileAdsManager.Enable(x > 0);
            }
        ).AddTo(gameObject);
    }
}
