using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using mainMenu;
using UnityEngine;

public class StoneBtnEffect : MonoBehaviour
{
    [SerializeField] private RectTransform effectT;
    [SerializeField] string effectName;
    
    // Start is called before the first frame update
    void Start()
    {
        SlotShine(effectT);
    }

    async void SlotShine(RectTransform t)
    {
        var effect = await AddressablesLogic.LoadTOnObject<ParticleSystem>(effectName);
        if (t == null)
        {
            GameObject.Destroy(effect.gameObject);
            return;
        }
        await UniTask.DelayFrame(5);
        effect.transform.SetParent(t);
        effect.transform.position = 
            PosCal.GetWorldPos(PreScene.target.mainC, t.GetComponent<RectTransform>(), 20f);
    }
}
