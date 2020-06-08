using UnityEngine;
using UnityEngine.EventSystems;

public partial class StoneCell : MonoBehaviour, IDropHandler
{
    public void Install(StoneCell cellInBox, SkillStoneSlot targetSlot)
    {
        switch(targetSlot._DragAndDropCell.cellPhase)
        {
            case CellPhase.NineSlotCell_empty:
                SVCenter.AddToSlotFromBox(cellInBox, targetSlot);
            break;
            case CellPhase.NineSlotCell_full:
                SVCenter.Swap_BoxCell_SlotCell(cellInBox, targetSlot);
            break;
        }
    }
}