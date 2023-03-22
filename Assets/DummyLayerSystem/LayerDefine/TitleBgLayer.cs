using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DummyLayerSystem;

public class TitleBgLayer : UILayer
{
    [SerializeField] Image targetImage;
    [SerializeField] Button touchScreenBtn;
    [SerializeField] Button skipBtn;
    [SerializeField] LanguageConverter languageConverter;
    [SerializeField] Sprite targetSprite;
    [SerializeField] RectTransform content;
    [SerializeField] Scrollbar vScrollbar;
    [SerializeField] List<string> subtitleCodes;
    [SerializeField] float scrollDelayFromSeconds = 1;
    [SerializeField] float scrollDelayInMilliSecond = 0.001f;
    [SerializeField] float ScrollbarMinValue = 0;
    [SerializeField] float ScrollbarMaxValue = 1;
    private float milliSecondCounter = 0;
    private IDisposable _disposable;
    public void Setup(bool storyMode, Action onClickProcess = null) // false: titleMode
    {
        RectTransform parentRect = transform.GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(parentRect.rect.width ,  targetSprite.rect.height * parentRect.rect.width / targetSprite.rect.width);
        content.anchoredPosition = Vector2.zero;
        targetImage.sprite = targetSprite;
        vScrollbar.value = 1;
        targetImage.color = Color.white;
        languageConverter.gameObject.SetActive(storyMode);

        if (storyMode)
        {
            touchScreenBtn.onClick.AddListener(() =>
            {
                UILayerLoader.Remove<TitleBgLayer>();
                onClickProcess?.Invoke();
            });
            skipBtn.gameObject.SetActive(true);
            skipBtn.onClick.AddListener(() =>
            {
                UILayerLoader.Remove<TitleBgLayer>();
                onClickProcess?.Invoke();
            });
        }
        
        void ChangeSubtitle(int codeIndex)
        {
            if (subtitleCodes.Count > codeIndex)
                languageConverter.ChangeAtOnce(subtitleCodes[codeIndex]);
        }
        _disposable = Observable.Timer(TimeSpan.FromSeconds(storyMode? scrollDelayFromSeconds : 0), TimeSpan.FromMilliseconds(0.1)).Subscribe(
            (_) =>
            {
                milliSecondCounter += scrollDelayInMilliSecond;
                vScrollbar.value = Mathf.Clamp(vScrollbar.value - scrollDelayInMilliSecond, ScrollbarMinValue, ScrollbarMaxValue);
                if (storyMode)
                {
                    var indexOfSubtitleCodes = (int)((1 - vScrollbar.value) / (1f / subtitleCodes.Count));
                    ChangeSubtitle(indexOfSubtitleCodes);
                    if (milliSecondCounter >= 1.2)
                    {
                        languageConverter.gameObject.SetActive(false);
                        touchScreenBtn.gameObject.SetActive(true);
                        _disposable.Dispose();
                    }
                }
                else
                {
                    if (vScrollbar.value <= ScrollbarMinValue)
                        _disposable.Dispose();
                }
            }).AddTo(gameObject);
    }
}
