using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.UI;

public class FightBeginBtn : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BOButton btn;
    [SerializeField] List<Image> texts;
    [SerializeField] private float duration;
    
    private float titleAnimFactor = 0;
    private TweenerCore<float, float, FloatOptions> titleTween;

    private void OnEnable()
    {
        titleAnimFactor = 0;
        SetTexts(titleAnimFactor);
    }

    public void SetAction(Action action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(action.Invoke);
    }
    
    public void Enable(bool on, bool guide = false)
    {
        btn.interactable = on;
        //animator.SetBool("On", on);

        float endValue;
        if (on)
        {
            endValue = 1;
        }
        else
        {
            endValue = 0;
        }
        
        titleTween?.Kill();
        titleTween = DOTween.To(() => titleAnimFactor, (x) => titleAnimFactor = x, endValue, duration)
            .OnUpdate(
                () =>
                {
                    SetTexts(titleAnimFactor);
                }
            );
    }

    void SetTexts(float value)
    {
        texts.ToList().ForEach(x =>
        {
            x.material.SetFloat("_Animation_Factor", value);
        });
    }
    
    private void OnDestroy()
    {
        titleTween?.Kill();
    }
}

