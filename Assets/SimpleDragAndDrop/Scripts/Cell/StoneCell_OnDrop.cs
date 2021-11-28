using System;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class StoneCell : MonoBehaviour, IDropHandler
{
    private Action<StoneCell, StoneCell> OnDropAction;
    
    public void SetOnDropAction(Action<StoneCell, StoneCell> action)
    {
        OnDropAction = action;
    }
    
    /// <summary>
    /// Item is dropped in this cell
    /// </summary>
    /// <param name="data"></param>
    public void OnDrop(PointerEventData data)
    {
        StoneCell sourceCell = SKStoneItem.sourceCell;
        if (SKStoneItem.icon != null)
        {
            SKStoneItem item = SKStoneItem.draggedItem;
            if (item == null)
                return;
            sourceCell = SKStoneItem.sourceCell;
            if (sourceCell == this)
                return;
            if (SKStoneItem.icon.activeSelf)
            {
                OnDropAction.Invoke(sourceCell, this);
            }
            sourceCell.UpdateMyItem();
        }
    }
}
