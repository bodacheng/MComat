using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTextScaleManager
{
    private readonly Dictionary<Transform, Sequence> _dic = new Dictionary<Transform, Sequence>();

    public void AddNew(Transform target, Vector3 targetScale, Vector3 endScale, float halfDuration)
    {
        _dic.TryGetValue(target, out var sequence);
        if (sequence != null)
        {
            if (sequence.IsActive())
            {
                sequence.Kill();
            }
        }
        sequence = DOTween.Sequence().Append(target.DOScale(targetScale, halfDuration)).Append(target.DOScale(endScale, halfDuration));
        DicAdd<Transform, Sequence>.Add(_dic, target, sequence);
    }

    public void Clear()
    {
        foreach (var kv in _dic)
        {
            if (kv.Value != null)
            {
                if (kv.Value.IsActive())
                {
                    kv.Value.Kill();
                }
            }
        }
        _dic.Clear();
    }
}
