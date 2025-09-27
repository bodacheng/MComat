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
    [SerializeField] BOButton skipBtn;
    [SerializeField] LanguageConverter languageConverter;
    [SerializeField] Sprite targetSprite;
    [SerializeField] RectTransform content;
    [SerializeField] Scrollbar vScrollbar;
    [SerializeField] List<string> subtitleCodes;
    [SerializeField] float scrollDelayFromSeconds = 1;
    [SerializeField] float scrollSpeedPerSecondTitle = 0.2f;
    [SerializeField] float scrollSpeedPerSecondStory = 0.1f;
    [SerializeField] float ScrollbarMinValue = 0;
    [SerializeField] float ScrollbarMaxValue = 1;
    float milliSecondCounter = 0;
    IDisposable _disposable;
    public void Setup(float scrollValue) // false: titleMode
    {
        var parentRect = transform.GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(parentRect.rect.width ,  targetSprite.rect.height * parentRect.rect.width / targetSprite.rect.width);
        content.anchoredPosition = Vector2.zero;
        targetImage.sprite = targetSprite;
        vScrollbar.value = scrollValue;
        targetImage.color = Color.white;
    }
    
    public void Rotate(bool storyMode, Action onClickProcess = null)
    {
        if (storyMode)
        {
            touchScreenBtn.gameObject.SetActive(false); // 开始时隐藏，故事结束后显示
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
        
        float delay = storyMode ? scrollDelayFromSeconds : 0f;
        float elapsed = 0f;

        _disposable = Observable.EveryUpdate()
            .TakeUntilDisable(this)
            .Subscribe(_ =>
            {
                float dt = Time.unscaledDeltaTime; // 真实Δt
                elapsed += dt;

                if (elapsed < delay) return;

                vScrollbar.value = Mathf.Clamp(
                    vScrollbar.value - (storyMode ? scrollSpeedPerSecondStory : scrollSpeedPerSecondTitle) * dt,
                    ScrollbarMinValue, ScrollbarMaxValue
                );

                if (storyMode)
                {
                    languageConverter.gameObject.SetActive(true);
                    int idx = Mathf.Clamp(
                        (int)((1f - vScrollbar.value) * subtitleCodes.Count),
                        0, subtitleCodes.Count - 1
                    );
                    ChangeSubtitle(idx);

                    // 当滚动到底部时结束故事
                    if (vScrollbar.value <= ScrollbarMinValue)
                    {
                        languageConverter.gameObject.SetActive(false);
                        touchScreenBtn.gameObject.SetActive(true);
                        _disposable?.Dispose();
                    }
                }
                else
                {
                    if (vScrollbar.value <= ScrollbarMinValue)
                        _disposable?.Dispose();
                }
            })
            .AddTo(gameObject);
    }
}
