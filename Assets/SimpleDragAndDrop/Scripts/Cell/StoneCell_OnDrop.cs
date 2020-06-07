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
        SKStoneItem item;
        StoneCell sourceCell = SKStoneItem.sourceCell;
        if (SKStoneItem.icon != null)
        {
            item = SKStoneItem.draggedItem;
            sourceCell = SKStoneItem.sourceCell;
            
            // If icon inactive do not need to drop item into cell
            if (SKStoneItem.icon.activeSelf == true)
            {
                if (sourceCell == this)
                    return;

                if ((item != null) && (sourceCell != this))
                {
                    switch (cellPhase)//自身phase
                    {
                        case CellPhase.NineSlotCell_full:
                            switch (sourceCell.cellPhase)
                            {
                                case CellPhase.NineSlotCell_full:
                                    SwapItems(sourceCell, this);
                                break;
                                case CellPhase.NineSlotCell_empty:
                                break;
                                case CellPhase.SkillStoneBoxCell:
                                    Install(sourceCell,_SkillStoneSlot);
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
                                case CellPhase.NineSlotCell_empty:
                                break;
                            }
                        break;
                        case CellPhase.SkillStoneBoxCell:
                            switch (sourceCell.cellPhase)
                            {
                                case CellPhase.NineSlotCell_full:// 已装备石头的卸载功能。
                                    SKStoneItem stone = sourceCell.GetItem();
                                    if (stone != null && stone.Inherent)
                                    {
                                        Debug.Log("固有技能无法移出，返回");
                                        return;
                                    }
                                    TheNineSlot.SkillEditError valR = TheNineSlot.target.CheckEditBasedOnCurrent();
                                    if (valR != TheNineSlot.SkillEditError.Perfect)
                                    {
                                        TheNineSlot.target.ValiationWarn(valR, MemberDetail.target._focusing.monsterOfPlayerId);
                                        return;
                                    }
                                    if (GetItem() != null) // 如果把技能石从9宫格拖到技能背包的一个有石头的格子上，那么就直接把拖动中的技能石先从九宫格拔下来，接着让技能背包自动排序一下
                                    {
                                        sourceCell._SkillStoneSlot.ReturnStoneToBox();
                                    }
                                    else
                                    {
                                        // 如果把技能石从9宫格拖到空技能背包格子上，那就让这个技能石在那个空格子上就可以。
                                        // 的确这个瞬间可能产生这个技能石所在位置和当前背包显示类型不一致问题，但如果是进行了一个背包自动排序的话，
                                        // 松手瞬间会有一个技能石“变图案”的错觉。
                                        AddItem(item);
                                    }
                                    break;
                                case CellPhase.NineSlotCell_empty:
                                break;
                                case CellPhase.SkillStoneBoxCell:
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
        
        UpdateMyItem();
        if (sourceCell == null)
        {
            Debug.Log("按理说不应该经过这里");
            return;
        }
        sourceCell.UpdateMyItem();
        if (_SkillStoneSlot != null)
        {
            TheNineSlot.target.NineSlotsStatusRefresh();
        }
    }
}
