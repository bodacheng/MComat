using System.Collections.Generic;
using mainMenu;
using NoSuchStudio.Common;
using UnityEngine;

public class UnitBtnEffect : MonoBehaviour
{
    [SerializeField] private RectTransform[] nineSlots;

    private List<string> slotEffectNames = new List<string>()
    {
        "SlotEffects/ex1",
        "SlotEffects/ex2",
        "SlotEffects/ex3"
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
        var slotName = slotEffectNames.Random();
        var effect = await AddressablesLogic.LoadTOnObject<ParticleSystem>(slotName);
        if (t == null)
        {
            GameObject.Destroy(effect.gameObject);
            return;
        }
        effect.transform.SetParent(t);
        effect.transform.position = 
            PosCal.GetWorldPos(PreScene.target.mainC, 
                PosCal.ConvertAnchorPos(t.GetComponent<RectTransform>().anchoredPosition, Vector2.one, Vector2.zero )
                , 20f);
    }
}