using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class TitleBgLayer : UILayer
{
    [SerializeField] Image targetImage;
    [SerializeField] Text subtitle;
    [SerializeField] Sprite targetSprite;
    [SerializeField] RectTransform content;
    [SerializeField] Scrollbar vScrollbar;
    [SerializeField] float duration = 10;
    [SerializeField] List<string> subtitleCodes;
    
    private IDisposable _disposable;
    public void Setup()
    {
        content.sizeDelta = new Vector2(content.rect.width ,  targetSprite.rect.height * content.rect.width / targetSprite.rect.width);
        targetImage.sprite = targetSprite;
        vScrollbar.value = 1;
        targetImage.color = new Color(1,1,1,1);
        //var tweener = DOTween.To(() => vScrollbar.value, x => vScrollbar.value = x, 0, duration);
        //OnDestroyCallback.AddOnDestroyCallback(this.gameObject, ()=>{tweener.Kill();});
        _disposable = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(0.1)).Subscribe(
            (_) =>
            {
                vScrollbar.value = Mathf.Clamp(vScrollbar.value - 0.01f, 0, 1);
                
                int indexOfSubtitleCodes = (int)((1 - vScrollbar.value)/(vScrollbar.value/subtitleCodes.Count));
                if (subtitleCodes.Count > indexOfSubtitleCodes)
                    subtitle.text = subtitleCodes[indexOfSubtitleCodes];
                
                if (vScrollbar.value <= 0)
                    _disposable.Dispose();
            }).AddTo(gameObject);
    }
}
