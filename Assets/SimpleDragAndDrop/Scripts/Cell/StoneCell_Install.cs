using UnityEngine;
using UnityEngine.EventSystems;
using dataAccess;
using mainMenu;

public partial class StoneCell : MonoBehaviour, IDropHandler
{
    public void Install(StoneCell cellInSkillStoneBox, SkillStoneSlot targetSlot)
    {
        SKStoneItem itemFromStoneBox = cellInSkillStoneBox.GetItem();
        if (itemFromStoneBox == null)
        {
            return;
        }
        
        switch(targetSlot._DragAndDropCell.cellPhase)//drag目标slot的phase
        {
            case CellPhase.NineSlotCell_empty:
                if (AccountCharsSet.CheckExist(MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId))
                {
                    string monsterID = MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId;
                    if (TheNineSlot.Instance.CheckNineSlotPointsAfterOneStoneRemoved(monsterID, itemFromStoneBox._SkillConfig.RECORD_ID) < 0)
                    {
                        Debug.Log("其他角色卸载此技能石会导致点数失衡，不予操作");
                        return;
                    }
                }
                if (!TheNineSlot.Instance.RefreshWholePointBasedOnCurrentNineSlots(itemFromStoneBox, targetSlot._DragAndDropCell))
                {
                    Debug.Log("Validation错误，不执行操作，返回");
                    return;
                }
                targetSlot._DragAndDropCell.AddItem(itemFromStoneBox);
                cellInSkillStoneBox.UpdateMyItem();
            break;
            case CellPhase.NineSlotCell_full:
                SKStoneItem stone = targetSlot._DragAndDropCell.GetItem();
                if (stone.Inherent)
                {
                    Debug.Log("固有技能无法移出，返回");
                    return;
                }
                
                if (AccountCharsSet.CheckExist(MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId))
                {
                    string monsterID = MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId;
                    if (TheNineSlot.Instance.CheckNineSlotPointsAfterOneStoneRemoved(monsterID, itemFromStoneBox._SkillConfig.RECORD_ID) < 0)
                    {
                        Debug.Log("其他角色卸载此技能石会导致点数失衡，不予操作");
                        return;
                    }
                }
                if (!TheNineSlot.Instance.RefreshWholePointBasedOnCurrentNineSlots(itemFromStoneBox, targetSlot._DragAndDropCell))
                {
                    Debug.Log("Validation错误，不执行操作，返回");
                    return;
                }
                SwapItems(cellInSkillStoneBox, targetSlot._DragAndDropCell);
            break;
        }
        TheNineSlot.Instance.NineSlotsStatusRefresh();
    }
}