using System;
using System.Collections.Generic;
using mainMenu;
using NoSuchStudio.Common;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitBtnEffect : MonoBehaviour
{
    [SerializeField] private RectTransform[] nineSlots;
    
    private readonly List<string> _slotEffectNames = new()
    {
        "frontLayerUnitBtn/stoneEffect.prefab",
        "frontLayerUnitBtn/stoneEffect1.prefab",
        "frontLayerUnitBtn/stoneEffect2.prefab",
        "frontLayerUnitBtn/stoneEffect3.prefab",
        "frontLayerUnitBtn/stoneEffect4.prefab"
    };
    
    private readonly List<SlotJob> _slotJobs = new();
    
    void Start()
    {
        Shine();
    }

    void OnDestroy()
    {
        foreach (var slotJob in _slotJobs)
        {
            slotJob.Dispose();
        }
        _slotJobs.Clear();
    }

    void Shine()
    {
        foreach (var slot in nineSlots)
        {
            _slotJobs.Add(new SlotJob(_slotEffectNames, slot));
        }
    }
    
    class SlotJob
    {
        private IDisposable waitTask;
        private readonly List<string> _slotEffectNames;
        private ParticleSystem currentEffect;
        private RectTransform t;
        public SlotJob(List<string> _slotEffectNames, RectTransform t)
        {
            this._slotEffectNames = _slotEffectNames;
            this.t = t;
            SlotShine(t, null);
            Run();
        }
        
        void Run()
        {
            var randomSecond = Random.Range(2, 10);
            waitTask = Observable.Timer(TimeSpan.FromSeconds(randomSecond)).Subscribe(_ =>
            {
                SlotShine(t, () =>
                {
                    waitTask.Dispose();
                    Run();
                });
            }).AddTo(t.gameObject);
        }
        
        async void SlotShine(RectTransform t, Action after)
        {
            if (currentEffect != null)
                Destroy(currentEffect.gameObject);
            
            var slotName = _slotEffectNames.Random();
            currentEffect = await AddressablesLogic.LoadTOnObject<ParticleSystem>(slotName);
            if (t == null)
            {
                Destroy(currentEffect.gameObject);
                return;
            }
            currentEffect.transform.SetParent(t);
            currentEffect.transform.position = PosCal.GetWorldPos(PreScene.target.postProcessCamera, t.GetComponent<RectTransform>(), 20f);
            after?.Invoke();
        }
        
        public void Dispose()
        {
            waitTask.Dispose();
        }
    }
}