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
                    case CellPhase.NineSlotCell:
                        switch (sourceCell.cellPhase)
                        {
                            case CellPhase.NineSlotCell:
                            case CellPhase.SkillStoneBoxCell:
                                Install(sourceCell, this);
                            break;
                        }
                    break;
                    case CellPhase.SkillStoneBoxCell:
                        switch (sourceCell.cellPhase)
                        {
                            case CellPhase.NineSlotCell:// 已装备石头的卸载
                                Install(sourceCell, this);
                                break;
                            case CellPhase.SKLevelUpMSlot:// 已装备石头的卸载                            
                                Install(sourceCell, this);
                                SSLevelUpManager.target.RefreshSkillLevelUpModule();
                            break;
                        }
                    break;
                    case CellPhase.SKLevelUpMSlot:
                        Install(sourceCell, this);
                        SSLevelUpManager.target.RefreshSkillLevelUpModule();
                    break;
                }
            }
            sourceCell.UpdateMyItem();
        }
    }
}
