using UnityEngine;
using UnityEngine.EventSystems;

public partial class SKStoneItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
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