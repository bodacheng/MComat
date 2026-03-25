using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

public class TimeLimitedBundleCell : MonoBehaviour
{
    [SerializeField] Text countDownText;
    [SerializeField] Text msg;
    [SerializeField] Text dmAmount;
    
    private IDisposable _disposeSeasonCountDown;
    
    public void ShowTimeLimitedBundle(TimeLimitedBuyData data)
    {
        _disposeSeasonCountDown?.Dispose();
        _disposeSeasonCountDown = null;

        var on = ShopTop.HasTimeLimitSale(data);
        if (!on)
        {
            gameObject.SetActive(false);
            return;
        }
        
        gameObject.SetActive(true);
        msg.text = data.message;
        dmAmount.text = data.dmAmount.ToString();
        
        DateTime endTime = DateTime.Parse(data.endTime);
        _disposeSeasonCountDown = 
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Subscribe(
                (_) =>
                {
                    var timeRemaining = endTime - DateTime.UtcNow;
                    countDownText.text = timeRemaining.ToString(@"dd\:hh\:mm\:ss");
                    if (timeRemaining.TotalSeconds <= 0)
                    {
                        gameObject.SetActive(false);
                        _disposeSeasonCountDown.Dispose();
                        _disposeSeasonCountDown = null;
                    }
                }).AddTo(gameObject);
    }

    void OnDisable()
    {
        _disposeSeasonCountDown?.Dispose();
        _disposeSeasonCountDown = null;
    }
}
