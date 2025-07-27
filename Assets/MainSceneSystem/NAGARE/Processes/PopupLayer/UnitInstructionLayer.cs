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
    [SerializeField] private SizeAdjustBySpriteSize bgImage;
    [SerializeField] private Text gameTipTitle;
    [SerializeField] private Text gameTip;
    
    private readonly List<TweenerCore<float, float, FloatOptions>> _tweenerCores = new List<TweenerCore<float, float, FloatOptions>>();
    
    async void ChangeUnitTheme(string RECORD_ID)
    {
        var config = Units.GetUnitConfig(RECORD_ID);
        if (config == null)
            return;

        var sprite = await AddressablesLogic.LoadT<Sprite>("unit_bg/" + RECORD_ID);
        if (sprite == null )
        {
            return;
        }
        
        if (bgImage == null)
        {
            return;
        }
        
        bgImage.Image.sprite = sprite;
        
        bgImage.Image.color = Color.black; // Start from transparent
        bgImage.Image.DOColor(Color.white, 0.2f).SetEase(Ease.Linear);
        bgImage.AdjustSize();
        
        var tip = Translate.GetRandomGameTip();

        gameTipTitle.text = tip[0];
        gameTip.text = tip[1];
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
