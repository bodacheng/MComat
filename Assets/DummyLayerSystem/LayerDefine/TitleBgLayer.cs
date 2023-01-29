using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TitleBgLayer : UILayer
{
    [SerializeField] Image targetImage;
    [SerializeField] LanguageConverter languageConverter;
    [SerializeField] Sprite targetSprite;
    [SerializeField] RectTransform content;
    [SerializeField] Scrollbar vScrollbar;
    [SerializeField] List<string> subtitleCodes;
    [SerializeField] int scrollDelayFromSeconds = 2;
    [SerializeField] float scrollDelayInMilliSecond = 0.001f;
    
    private IDisposable _disposable;
    public void Setup(bool subtitleOn)
    {
        content.sizeDelta = new Vector2(content.rect.width ,  targetSprite.rect.height * content.rect.width / targetSprite.rect.width);
        targetImage.sprite = targetSprite;
        vScrollbar.value = 1;
        targetImage.color = Color.white;
        languageConverter.gameObject.SetActive(subtitleOn);
        
        void ChangeSubtitle(int codeIndex)
        {
            if (subtitleCodes.Count > codeIndex)
                languageConverter.ChangeAtOnce(subtitleCodes[codeIndex]);
        }
        _disposable = Observable.Timer(TimeSpan.FromSeconds(scrollDelayFromSeconds), TimeSpan.FromMilliseconds(0.1)).Subscribe(
            (_) =>
            {
                vScrollbar.value = Mathf.Clamp(vScrollbar.value - scrollDelayInMilliSecond, 0, 1);
                if (subtitleOn)
                {
                    var indexOfSubtitleCodes = (int)((1 - vScrollbar.value) / (1f / subtitleCodes.Count));
                    ChangeSubtitle(indexOfSubtitleCodes);
                }
                
                if (vScrollbar.value <= 0)
                    _disposable.Dispose();
            }).AddTo(gameObject);
    }
}
