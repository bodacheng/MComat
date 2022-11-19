using UnityEngine;
using UnityEngine.EventSystems;

public partial class SKStoneItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public async void Shine(Camera refC)
    {
        var worldPos = PosCal.GetWorldPos(refC, transform.GetComponent<RectTransform>(), 5f);
        var path = FightGlobalSetting.EffectPathDefine();
        var slotEffect = await AddressablesLogic.LoadTOnObject<ParticleSystem>("ButtonEffects/" + path + "/explosion2.prefab");
        slotEffect.gameObject.name = "stoneShine";
        slotEffect.gameObject.transform.position = worldPos;
        slotEffect.Play(true);
    }
    
    public static void SelectedRender(SKStoneItem item, GameObject _Selected)
    {
        if (item != null)
        {
            var cell = item.GetCell();
            StoneCell.SelectedRender(cell, _Selected);
        }
        else
        {
            StoneCell.SelectedRender(null, _Selected);
        }
    }
}