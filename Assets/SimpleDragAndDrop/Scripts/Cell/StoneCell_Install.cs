using UnityEngine;
using UnityEngine.EventSystems;
using dataAccess;
using mainMenu;

public partial class StoneCell : MonoBehaviour, IDropHandler
{
    // 将道具栏内的技能石正式拖入对应的技能槽
    public void Install(StoneCell cellInBox, SkillStoneSlot targetSlot)
    {
        SKStoneItem itemFromStoneBox = cellInBox.GetItem();
        if (itemFromStoneBox == null)
        {
            return;
        }
        targetSlot._DragAndDropCell.UpdateMyItem();
        switch(targetSlot._DragAndDropCell.cellPhase) //drag目标slot的phase
        {
            // 拖入空技能槽
            case CellPhase.NineSlotCell_empty:
                SVCenter.AddToSlotFromBox(cellInBox, targetSlot);
            break;
            
            // 拖入有技能石的技能槽 
            case CellPhase.NineSlotCell_full:
                SVCenter.Swap_BoxCell_SlotCell(cellInBox, targetSlot);                
            break;
        }
    }
}