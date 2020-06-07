using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using dataAccess;
using mainMenu;

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
            sourceCell = SKStoneItem.sourceCell;

            // If icon inactive do not need to drop item into cell
            if (SKStoneItem.icon.activeSelf == true)
            {
                if (sourceCell == this)
                    return;
                    
                if (item != null)
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
                                case CellPhase.NineSlotCell_full:// 已装备石头的卸载功能。
                                    SVCenter.StoneRemoveFromSlotToCell(sourceCell, this, item);
                                break;
                            }
                        break;
                        case CellPhase.DeleteArea:
                            switch (sourceCell.cellPhase)
                            {
                                case CellPhase.SkillStoneBoxCell:
                                    UnityEngine.Events.UnityAction SkillstoneDeleteConfirm = () =>
                                    {
                                        TheNineSlot.target.mainProcessRunner.Run(MySkillStonesReader.RemoveTheseStonesFromLocalDic(new List<string>{ GetItem().SkillStoneOfPlayerId}));
                                        UpdateMyItem();
                                    };
                                    UnityEngine.Events.UnityAction SkillstoneDeleteCancel = () =>
                                    {
                                        TheNineSlot.target.mainProcessRunner.Run(SkillStonesBox.target.ArrangeSkillStonesToBox());
                                    };
                                    LoadingCanvas.target.ArrangeValiationWindow(SkillstoneDeleteConfirm, SkillstoneDeleteCancel, "确实要删除技能石头：" + GetItem()._SkillConfig.REAL_NAME + "?");
                                    break;
                            }
                        break;
                    }
                }
            }
        }
    }
}
