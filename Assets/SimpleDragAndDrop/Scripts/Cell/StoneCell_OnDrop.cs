using UnityEngine;
using UnityEngine.EventSystems;

public partial class StoneCell : MonoBehaviour, IDropHandler
{
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
            // If icon inactive do not need to drop item into cell
            if (SKStoneItem.icon.activeSelf == true)
            {
                switch (cellPhase)//自身phase
                {
                    case CellPhase.NineSlotCell_full:
                        switch (sourceCell.cellPhase)
                        {
                            case CellPhase.NineSlotCell_full:
                                SwapItems(sourceCell, this);
                            break;
                            case CellPhase.SkillStoneBoxCell:
                                Install(sourceCell, _SkillStoneSlot);
                            break;
                        }
                    break;
                    case CellPhase.NineSlotCell_empty:
                        switch (sourceCell.cellPhase)
                        {
                            case CellPhase.NineSlotCell_full:
                                AddItem(item);
                            break;
                            case CellPhase.SkillStoneBoxCell:
                                Install(sourceCell, _SkillStoneSlot);
                            break;
                        }
                    break;
                    case CellPhase.SkillStoneBoxCell:
                        switch (sourceCell.cellPhase)
                        {
                            case CellPhase.NineSlotCell_full:// 已装备石头的卸载
                                SVCenter.StoneRemoveFromSlotToCell(sourceCell, this);
                            break;
                        }
                    break;
                    case CellPhase.Casual:
                        AddItem(item);
                    break;
                }
            }
            sourceCell.UpdateMyItem();
        }
    }
}
