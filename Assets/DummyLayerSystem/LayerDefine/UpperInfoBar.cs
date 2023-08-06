using UnityEngine.UI;
using UnityEngine;
using System;
using UniRx;
using DG.Tweening;

public class UpperInfoBar : UILayer
{
    [SerializeField] BOButton settingBtn;
    [SerializeField] BOButton mailBtn;
    [SerializeField] GameObject unReadFlag;
    [SerializeField] Text titleDisplayName;
    [SerializeField] Text accountDiamondCoin;
    [SerializeField] BOButton diamondPlus;
    [SerializeField] Text accountIntelliCoin;
    [SerializeField] float currencyTextChangeDuration = 2f;
    
    public void Interactable(bool on)
    {
        settingBtn.interactable = on;
        mailBtn.interactable = on;
        diamondPlus.interactable = on;
    }
    
    public void Setup(string titleDisplayName, Action openSetting, Action openMail, Action openDmShop)
    {
        this.titleDisplayName.text = titleDisplayName;
        accountDiamondCoin.text = Currencies.DiamondCount.Value.ToString();
        Currencies.DiamondCount.Subscribe(x =>
        {
            int.TryParse(accountDiamondCoin.text, out int currentValue);
            int targetValue = currentValue;
            DOTween.To(
                () => targetValue,
                setterValue => targetValue = setterValue,
                x,
                currencyTextChangeDuration
            ).OnUpdate(() =>
            {
                accountDiamondCoin.text = targetValue.ToString();
            });
        }).AddTo(this.gameObject);
        
        accountIntelliCoin.text = Currencies.CoinCount.Value.ToString();
        Currencies.CoinCount.Subscribe(x =>
        {
            int.TryParse(accountIntelliCoin.text, out int currentValue);
            int targetValue = currentValue;
            DOTween.To(
                () => targetValue,
                setterValue => targetValue = setterValue,
                x,
                currencyTextChangeDuration
            ).OnUpdate(() =>
            {
                accountIntelliCoin.text = targetValue.ToString();
            });
        }).AddTo(this.gameObject);
        
        unReadFlag.gameObject.SetActive(PlayFabReadClient.GetMailsData(true).Count > 0);
        if (openSetting != null)
        {
            settingBtn.onClick.AddListener(openSetting.Invoke);
            settingBtn.gameObject.SetActive(true);
        }
        else
        {
            settingBtn.gameObject.SetActive(false);
        }

        if (openMail != null)
        {
            mailBtn.onClick.AddListener(openMail.Invoke);
            mailBtn.gameObject.SetActive(true);
        }
        else
        {
            mailBtn.gameObject.SetActive(false);
        }

        if (openDmShop != null)
        {
            diamondPlus.onClick.AddListener(openDmShop.Invoke);
            diamondPlus.gameObject.SetActive(true);
        }
        else
        {
            diamondPlus.gameObject.SetActive(false);
        }
    }
}
