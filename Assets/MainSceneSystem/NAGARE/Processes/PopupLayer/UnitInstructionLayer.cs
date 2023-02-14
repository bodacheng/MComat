using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NoSuchStudio.Common;

public class UnitInstructionLayer : UILayer
{
    [SerializeField] private List<string> unitRecordIds;
    [SerializeField] private Image bgImage;
    [SerializeField] private Image unitImage;
    [SerializeField] private Text unitName;
    [SerializeField] private Text unitIntro;
    [SerializeField] private float unitImageEndPosX = -100f;
    [SerializeField] private float bgEndPosX = 100f;
    [SerializeField] private float nameEndPosX = 100f;
    [SerializeField] private float emergeDuration = 2f;

    private readonly List<TweenerCore<float, float, FloatOptions>> _tweenerCores = new ();
    
    async void ChangeUnitTheme(string RECORD_ID)
    {
        var value = await AddressablesLogic.LoadT<Sprite>("unit_image/"+RECORD_ID);
        if (value == null)
        {
            return;
        }
        
        var unitImageRect = unitImage.transform.GetComponent<RectTransform>();
        unitImageRect.sizeDelta = new Vector2(value.rect.width * unitImageRect.rect.height / value.rect.height, unitImageRect.rect.height);
        unitImage.sprite = value;
        
        var config = Units.GetUnitConfig(RECORD_ID);
        unitName.text = Translate.Get(config.REAL_NAME);
        unitIntro.text = Translate.Get(config.REAL_NAME+ "_intro");
        
        TweenerCore<float, float, FloatOptions> Move(RectTransform targetRect, float endXPos)
        {
            float posX = targetRect.anchoredPosition.x;
            return DOTween.To(()=> posX, (value)=>posX = value, endXPos, emergeDuration).
                OnUpdate(
                    () =>
                    {
                        targetRect.anchoredPosition = new Vector2(posX, targetRect.anchoredPosition.y);
                    }
                ).SetEase(Ease.InSine);
        }

        _tweenerCores.Add(Move(unitImage.transform.GetComponent<RectTransform>(), unitImageEndPosX));
        _tweenerCores.Add(Move(bgImage.transform.GetComponent<RectTransform>(), bgEndPosX));
        _tweenerCores.Add(Move(unitName.transform.GetComponent<RectTransform>(), nameEndPosX));
    }
    
    public void LoadUnitImage()
    {
        ChangeUnitTheme(unitRecordIds.Random());
    }

    void OnDestroy()
    {
        foreach (var tweener in _tweenerCores)
        {
            if (tweener != null && tweener.IsActive())
                tweener.Kill();
        }
    }
}
