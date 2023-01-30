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

    private IDisposable disposeCountDown;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        rewardedAdsButton.SetEnableCondition(()=> Currencies.AdTicket.Value > 0);
        Currencies.AdTicket.Subscribe(
            x=>
            {
                disposeCountDown?.Dispose();
                remainCount.text = x.ToString();
                if (x >= Currencies.AdTicketRechargeMax)
                {
                    ticketChargeCountDown.text = string.Empty;
                    ticketChargeCountDownT.gameObject.SetActive(false);
                }
                else
                {
                    disposeCountDown = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Subscribe((_) =>
                    {
                        ticketChargeCountDownT.gameObject.SetActive(true);
                        if (Currencies.SecondsToRechargeAdTicket > 0)
                        {
                            var minute = Currencies.SecondsToRechargeAdTicket / 60;
                            var seconds = Currencies.SecondsToRechargeAdTicket - minute * 60;
                            ticketChargeCountDown.text = $"{minute :00}:{seconds:00}";
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
            }
        ).AddTo(gameObject);
    }
}
