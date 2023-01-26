using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleBgLayer : UILayer
{
    [SerializeField] Image targetImage;
    [SerializeField] Sprite targetSprite;
    [SerializeField] ScrollRect bgScrollRect;
    [SerializeField] RectTransform content;
    [SerializeField] Scrollbar vScrollbar;
    public void Setup()
    {
        content.sizeDelta = new Vector2(content.rect.width ,  targetSprite.rect.height * content.rect.width / targetSprite.rect.width);
        targetImage.sprite = targetSprite;
    }
}
