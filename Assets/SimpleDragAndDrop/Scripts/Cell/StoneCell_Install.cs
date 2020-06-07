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
            break;
            
            // 拖入有技能石的技能槽 
            case CellPhase.NineSlotCell_full:
                SKStoneItem stone = targetSlot._DragAndDropCell.GetItem();
                if (stone.Inherent)
                {
                    Debug.Log("固有技能无法移出，返回");
                    return;
                }
                
                if (!CheckIfOkAfterStoneRemove(itemFromStoneBox))
                    return;
                    
                // 对正尝试编辑技能的角色进行validation检验
                TheNineSlot.SkillEditError valR2 = TheNineSlot.target.CheckEditBasedOnCurrent(itemFromStoneBox, targetSlot._DragAndDropCell);
                if (valR2 != TheNineSlot.SkillEditError.Perfect)
                {
                    TheNineSlot.target.ValiationWarn(valR2, MemberDetail.target._focusing.monsterOfPlayerId);
                    return;
                }
                SwapItems(cellInBox, targetSlot._DragAndDropCell);
            break;
        }
        TheNineSlot.target.NineSlotsStatusRefresh();
    }
    
    // 尝试装载的技能石正被其他角色使用时候，对那个其他角色进行validation检验
    bool CheckIfOkAfterStoneRemove(SKStoneItem itemFromStoneBox)
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