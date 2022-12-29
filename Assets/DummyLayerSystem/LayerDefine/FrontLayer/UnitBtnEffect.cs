using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using mainMenu;
using NoSuchStudio.Common;
using UnityEngine;

public class UnitBtnEffect : MonoBehaviour
{
    [SerializeField] private RectTransform[] nineSlots;

    private readonly List<string> _slotEffectNames = new List<string>()
    {
        "frontLayerUnitBtn/stoneEffect.prefab",
        "frontLayerUnitBtn/stoneEffect1.prefab",
        "frontLayerUnitBtn/stoneEffect2.prefab",
        "frontLayerUnitBtn/stoneEffect3.prefab",
        "frontLayerUnitBtn/stoneEffect4.prefab"
    };
    
    void Start()
    {
        Shine();
    }
    
    void Shine()
    {
        foreach (var slot in nineSlots)
        {
            SlotShine(slot);
        }
    }
    
    async void SlotShine(RectTransform t)
    {
        var slotName = _slotEffectNames.Random();
        var effect = await AddressablesLogic.LoadTOnObject<ParticleSystem>(slotName);
        if (t == null)
        {
            GameObject.Destroy(effect.gameObject);
            return;
        }
        await UniTask.DelayFrame(5);
        effect.transform.SetParent(t);
        effect.transform.position = 
            PosCal.GetWorldPos(PreScene.target.postProcessCamera, t.GetComponent<RectTransform>(), 20f);
    }
}