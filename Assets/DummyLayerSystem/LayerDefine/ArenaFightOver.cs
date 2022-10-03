using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class ArenaFightOver : UILayer
{
    [SerializeField] private Button returnBtn;
    [SerializeField] private ArenaRankIcon arenaRankIcon;
    [SerializeField] private TextMeshProUGUI arenaPoint;
    [SerializeField] private TextMeshProUGUI awardCurrency;

    private int arenaPointValue;
    private TweenerCore<int, int, NoOptions> _tweenerCore;
    
    public void Initialise(Action R, int oldPoint = 0, int currentPoint = -1, int awardCurrency = 0)
    {
        returnBtn.onClick.AddListener(R.Invoke);
        if (currentPoint != -1)
            arenaRankIcon.Set(currentPoint);
        else
        {
            arenaRankIcon.gameObject.SetActive(false);
        }

        arenaPointValue = oldPoint;
        _tweenerCore = DOTween.To(
            () => arenaPointValue,          // 何を対象にするのか
            num => arenaPointValue = num,   // 値の更新
            currentPoint,                  // 最終的な値
            5.0f                  // アニメーション時間
        ).OnUpdate(()=> arenaPoint.text = arenaPointValue.ToString());
        
        this.awardCurrency.gameObject.SetActive(awardCurrency > 0);
        this.awardCurrency.text = awardCurrency.ToString();
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        if (_tweenerCore != null)
            _tweenerCore.Kill();
    }
}