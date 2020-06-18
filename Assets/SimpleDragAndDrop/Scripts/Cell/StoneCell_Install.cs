using UnityEngine;
using UnityEngine.EventSystems;
using mainMenu;

public partial class StoneCell : MonoBehaviour, IDropHandler
{
    public void Install(StoneCell from, StoneCell to)
    {
        switch(to.cellPhase)
        {
            case CellPhase.NineSlotCell:
                switch(from.cellPhase)
                {
                    case CellPhase.SkillStoneBoxCell:
                    case CellPhase.NineSlotCell:
                        if (to.myDadItem == null)
                        {
                            SVCenter.MoveItemFromTo(from, to);
                        }else{
                            SVCenter.SwapItemFromTo(from, to);
                        }
                    break;
                }
            break;
            case CellPhase.Casual:
                if (to.myDadItem == null)
                    SVCenter.MoveItemFromTo(from, to);
                else
                    SVCenter.SwapItemFromTo(from, to);
            break;
            case CellPhase.SkillStoneBoxCell:
                switch(from.cellPhase)
                {
                    case CellPhase.NineSlotCell:
                    case CellPhase.Casual:
                        SVCenter.StoneRemoveFromSlotToCell(from, to);
                    break;
                }
            break;
        }
    }
    
    public void ReturnStoneToBox()
    {
        UpdateMyItem();
        SKStoneItem _DragAndDropItem = GetItem();
        if (_DragAndDropItem)
        {
            if (_DragAndDropItem._SkillConfig.SP_LEVEL == SkillStonesBox.target.GetFocusingExType())//如果尝试归还背包的技能石必杀等级与显示中的一致，则找个当前的空格给放进去就可以。
            {
                StoneCell dragAndDropCell = SkillStonesBox.target.GetFirstEmptyCell();
                if (dragAndDropCell != null)
                {
                    dragAndDropCell.AddItem(_DragAndDropItem);
                }
                else
                {
                    _DragAndDropItem.gameObject.transform.SetParent(SkillStonesBox.target.stonesTempContainer);
                }
            }
            else{
                _DragAndDropItem.gameObject.transform.SetParent(SkillStonesBox.target.stonesTempContainer);//如果尝试归还背包的技能石必杀等级与显示中的不一致，则直接使其非显示。
            }
        }
        UpdateMyItem();
    }
}