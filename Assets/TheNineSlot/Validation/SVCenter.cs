using UnityEngine;
using dataAccess;
using mainMenu;
using DG.Tweening;

public static class SVCenter
{
    public static void StoneRemoveFromSlotToCell(StoneCell sourceCell, StoneCell boxcell)
    {
        SKStoneItem stone = sourceCell.GetItem();
        if (stone != null && stone.Inherent)
        {
            Debug.Log("固有技能无法移出，返回");
            return;
        }
        TheNineSlot.SkillEditError valR = TheNineSlot.target.CheckEditBasedOnCurrent(null, sourceCell);
        if (valR != TheNineSlot.SkillEditError.Perfect)
        {
            TheNineSlot.target.ValiationWarn(valR, MemberDetail.target._focusing.monsterOfPlayerId);
            return;
        }
        
        // 如果把技能石从9宫格拖到技能背包的一个有石头的格子上，那么就直接把拖动中的技能石先从九宫格拔下来，接着让技能背包自动排序一下
        if (boxcell.GetItem() != null)
        {
            sourceCell._SkillStoneSlot.ReturnStoneToBox();
        }
        else
        {
            // 如果把技能石从9宫格拖到空技能背包格子上，那就让这个技能石在那个空格子上就可以。
            // 的确这个瞬间可能产生这个技能石所在位置和当前背包显示类型不一致问题，但如果是进行了一个背包自动排序的话，
            // 松手瞬间会有一个技能石“变图案”的错觉。
            boxcell.AddItem(stone);
        }
        TheNineSlot.target.NineSlotsStatusRefresh();
    }
    
    public static void AddToSlotFromBox(StoneCell cellInBox, SkillStoneSlot targetSlot)
    {
        SKStoneItem itemFromStoneBox = cellInBox.GetItem();
        if (itemFromStoneBox == null)
            return;
        if (!CheckIfOkAfterStoneRemove(itemFromStoneBox))
            return;
        TheNineSlot.SkillEditError valR = TheNineSlot.target.CheckEditBasedOnCurrent(itemFromStoneBox, targetSlot._DragAndDropCell);
        if (valR != TheNineSlot.SkillEditError.Perfect)
        {
            TheNineSlot.target.ValiationWarn(valR, MemberDetail.target._focusing.monsterOfPlayerId);
            return;
        }
        targetSlot._DragAndDropCell.AddItem(itemFromStoneBox);
        cellInBox.UpdateMyItem();
        TheNineSlot.target.NineSlotsStatusRefresh();
    }
    
    public static void Swap_BoxCell_SlotCell(StoneCell cellInBox, SkillStoneSlot targetSlot)
    {
        SKStoneItem itemFromStoneBox = cellInBox.GetItem();
        if (itemFromStoneBox == null)
            return;
        SKStoneItem stone = targetSlot._DragAndDropCell.GetItem();
        if (stone.Inherent)
        {
            Debug.Log("固有技能无法移出，返回");
            return;
        }
        
        if (!CheckIfOkAfterStoneRemove(itemFromStoneBox))
            return;
            
        TheNineSlot.SkillEditError valR2 = TheNineSlot.target.CheckEditBasedOnCurrent(itemFromStoneBox, targetSlot._DragAndDropCell);
        if (valR2 != TheNineSlot.SkillEditError.Perfect)
        {
            TheNineSlot.target.ValiationWarn(valR2, MemberDetail.target._focusing.monsterOfPlayerId);
            return;
        }
        SwapItems(cellInBox, targetSlot._DragAndDropCell);
        TheNineSlot.target.NineSlotsStatusRefresh();
    }
    
    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    static void SwapItems(StoneCell firstCell, StoneCell secondCell)
    {
        firstCell.UpdateMyItem();
        secondCell.UpdateMyItem();
        SKStoneItem firstItem = firstCell.GetItem();                // Get item from first cell
        SKStoneItem secondItem = secondCell.GetItem();              // Get item from second cell
        // Swap items
        if (firstItem != null)
        {
            //firstItem.transform.DOMove(secondCell.transform.position,1f);
            //firstItem.transform.localPosition = Vector3.zero;
            //firstItem.MakeRaycast(true);
            secondCell.AddItem(firstItem);
        }
        if (secondItem != null)
        {
            firstCell.AddItem(secondItem);
            secondItem.transform.position = secondCell.transform.position;
            secondItem.transform.DOMove(firstCell.transform.position,0.5f);
        }
    }
    
    // 尝试装载的技能石正被其他角色使用时候，对那个其他角色进行validation检验
    static bool CheckIfOkAfterStoneRemove(SKStoneItem itemFromStoneBox)
    {
        if (AccountCharsSet.CheckExist(MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId))
        {
            string monsterPlayerID = MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId;
            TheNineSlot.SkillEditError valR3 = TheNineSlot.target.CheckEditAfterOneStoneRemoved(monsterPlayerID, itemFromStoneBox._SkillConfig.RECORD_ID);
            if (valR3 != TheNineSlot.SkillEditError.Perfect)
            {
                TheNineSlot.target.ValiationWarn(valR3, monsterPlayerID);
                return false;
            }
        }
        return true;
    }
}
