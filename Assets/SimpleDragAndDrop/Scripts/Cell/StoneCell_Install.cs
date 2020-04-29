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
        targetSlot._DragAndDropCell.UpdateMyItem();
        switch(targetSlot._DragAndDropCell.cellPhase)//drag目标slot的phase
        {
            case CellPhase.NineSlotCell_empty:
                if (AccountCharsSet.CheckExist(MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId))
                {
                    string monsterOfID = MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId;
                    TheNineSlot.SkillEditError valR3 = TheNineSlot.Instance.CheckEditBasedOnSaveDataAfterOneStoneRemoved(monsterOfID, itemFromStoneBox._SkillConfig.RECORD_ID);
                    if (valR3 != TheNineSlot.SkillEditError.Perfect)
                    {
                        TheNineSlot.Instance.ValiationWarn(valR3, monsterOfID);
                        return;
                    }
                }
                TheNineSlot.SkillEditError valR = TheNineSlot.Instance.CheckEditBasedOnCurrent(itemFromStoneBox, targetSlot._DragAndDropCell);
                if (valR != TheNineSlot.SkillEditError.Perfect)
                {
                    TheNineSlot.Instance.ValiationWarn(valR, MemberDetail.target.focusingCharDataInfo.monsterOfPlayerId);
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
                    string monsterPlayerID = MySkillStonesReader.Get(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId;
                    TheNineSlot.SkillEditError valR3 = TheNineSlot.Instance.CheckEditBasedOnSaveDataAfterOneStoneRemoved(monsterPlayerID, itemFromStoneBox._SkillConfig.RECORD_ID);
                    if (valR3 != TheNineSlot.SkillEditError.Perfect)
                    {
                        TheNineSlot.Instance.ValiationWarn(valR3, monsterPlayerID);
                        return;
                    }
                }
                TheNineSlot.SkillEditError valR2 = TheNineSlot.Instance.CheckEditBasedOnCurrent(itemFromStoneBox, targetSlot._DragAndDropCell);
                if (valR2 != TheNineSlot.SkillEditError.Perfect)
                {
                    TheNineSlot.Instance.ValiationWarn(valR2, MemberDetail.target.focusingCharDataInfo.monsterOfPlayerId);
                    return;
                }
                SwapItems(cellInSkillStoneBox, targetSlot._DragAndDropCell);
            break;
        }
        TheNineSlot.Instance.NineSlotsStatusRefresh();
    }
}