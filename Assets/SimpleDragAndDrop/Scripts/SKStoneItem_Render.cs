using UnityEngine;
using UnityEngine.EventSystems;

public partial class SKStoneItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void LevelUpShow(float beforeexp, float afterexp)
    {
        int beforeLevel = 0;
        int aferlevel = 1;
        if (aferlevel > beforeLevel)
        {
            //OverlayCanvasController.instance.ShowCombatText(gameObject, CombatTextType.LevelUp, aferlevel);
        }
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