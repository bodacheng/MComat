using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using dataAccess;
using mainMenu;
using DG.Tweening;

/// <summary>
/// Every item's cell must contain this script
/// </summary>
[RequireComponent(typeof(Image))]
public class DragAndDropCell : MonoBehaviour, IDropHandler
{
    public enum CellPhase
    {
        SkillStoneBoxCell,
        NineSlotCell_full,
        NineSlotCell_empty,
        DeleteArea,
    }

    [Tooltip("using Stone Character Icon")]
    public charIcon _charIcon;
	[Tooltip("Functional type of this cell")]
    public CellPhase cellPhase = CellPhase.SkillStoneBoxCell;
    [Tooltip("Image of this cell")]
    public Image image;
	[Tooltip("Sprite color for empty cell")]
    public Color empty = new Color();                                       // Sprite color for empty cell
	[Tooltip("Sprite color for filled cell")]
    public Color full = new Color();                                        // Sprite color for filled cell
	[Tooltip("This cell has unlimited amount of items")]
    public bool unlimitedSource;                                    // Item from this cell will be cloned on drag start

    DragAndDropItem myDadItem;										// Item of this DaD cell

    // 自定义成员
    public SkillStoneSlot _SkillStoneSlot;//这个看起来比较古怪，目的是和这个cell对应的SkillStoneSlot形成一个互相链接。只针对9宫设置画面，和SkillStoneBox无关。

    public void DragStoneFromSKillStoneBoxToNineSlot(DragAndDropCell cellInSkillStoneBox, SkillStoneSlot targetSlot)
    {
        DragAndDropItem itemFromStoneBox = cellInSkillStoneBox.GetItem();
        if (itemFromStoneBox == null)
        {
            return;
        }

        switch(targetSlot._DragAndDropCell.cellPhase)//drag目标slot的phase
        {
            case CellPhase.NineSlotCell_empty:
                if (AccountCharsSet.CheckIfContainsAccountCharsSetKey(MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId))
                {
                    string monsterID = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId;
                    if (TheNineSlot.Instance.CheckNineSlotPointsAfterOneStoneRemoved(monsterID, itemFromStoneBox._SkillConfigOfSkillStone.RECORD_ID) < 0)
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
                if (AccountCharsSet.CheckIfContainsAccountCharsSetKey(MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId))
                {
                    string monsterID = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(itemFromStoneBox.SkillStoneOfPlayerId).inUsingMonsterOfPlayerId;
                    if (TheNineSlot.Instance.CheckNineSlotPointsAfterOneStoneRemoved(monsterID, itemFromStoneBox._SkillConfigOfSkillStone.RECORD_ID) < 0)
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
    
    /// <summary>
    /// Item is dropped in this cell
    /// </summary>
    /// <param name="data"></param>
    public void OnDrop(PointerEventData data)
    {
        DragAndDropItem item;
        DragAndDropCell sourceCell = DragAndDropItem.sourceCell;
        if (DragAndDropItem.icon != null)
        {
            item = DragAndDropItem.draggedItem;
            sourceCell = DragAndDropItem.sourceCell;

            // If icon inactive do not need to drop item into cell
            if (DragAndDropItem.icon.activeSelf == true)
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
                                case CellPhase.NineSlotCell_full://从box把一个石头拖到9宫中同被新石头所覆盖的格子上
                                    SwapItems(sourceCell,this);
                                    break;
                                case CellPhase.NineSlotCell_empty://add模式下，从box把一个石头拖到9宫中同被新石头所覆盖的格子上
                                    break;
                                case CellPhase.SkillStoneBoxCell:
                                    DragStoneFromSKillStoneBoxToNineSlot(sourceCell,_SkillStoneSlot);
                                    break;
                            }
                            break;
                        case CellPhase.NineSlotCell_empty:
                            switch (sourceCell.cellPhase)
                            {
                                case CellPhase.NineSlotCell_full://add模式下，从box把一个石头拖到9宫中同被新石头所覆盖的格子上
                                    AddItem(item);
                                break;
                                case CellPhase.SkillStoneBoxCell:
                                    DragStoneFromSKillStoneBoxToNineSlot(sourceCell, _SkillStoneSlot);
                                break;
                                case CellPhase.NineSlotCell_empty:
                                break;
                            }
                            break;
                        case CellPhase.SkillStoneBoxCell:
                            switch (sourceCell.cellPhase)
                            {
                                case CellPhase.NineSlotCell_full:// 已装备石头的卸载功能。
                                    if (!TheNineSlot.Instance.RefreshWholePointBasedOnCurrentNineSlots(null, sourceCell))
                                    {
                                        Debug.Log("Validation错误，不执行操作，返回");
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
                                        TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(MySkillStonesReader.RemoveTheseStonesFromLocalDic(new List<string>{ GetItem().SkillStoneOfPlayerId}));
                                        UpdateMyItem();
                                    };
                                    UnityEngine.Events.UnityAction SkillstoneDeleteCancel = () =>
                                    {
                                        TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(SkillStonesBox.Instance.ArrangeSkillStonesToBox());
                                    };
                                    LoadingCanvas.target.ArrangeValiationWindow(SkillstoneDeleteConfirm, SkillstoneDeleteCancel, "确实要删除技能石头：" + GetItem()._SkillConfigOfSkillStone.REAL_NAME + "?");
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
            TheNineSlot.Instance.NineSlotsStatusRefresh();
        }
    }

    /// <summary>
    /// Put item into this cell.
    /// </summary>
    /// <param name="item">Item.</param>
    void PlaceItem(DragAndDropItem item)
    {
        if (item != null)
        {
            DestroyItem();                                              // Remove current item from this cell
            myDadItem = null;
            DragAndDropCell cell = item.GetComponentInParent<DragAndDropCell>();
            if (cell != null)//那么这个cell也就是source cell
            {
                if (cell.unlimitedSource == true)
                {
                    string itemName = item.name;
                    item = Instantiate(item);                               // Clone item from source cell
                    item.name = itemName;
                }
            }
            item.transform.SetParent(transform, false);
            item.transform.localPosition = Vector3.zero;
            item.MakeRaycast(true);
            myDadItem = item;
        }
    }

    void PlaceItem(DragAndDropItem item, Color color)
    {
        if (item != null)
        {
            DestroyItem();                                              // Remove current item from this cell
            myDadItem = null;
            DragAndDropCell cell = item.GetComponentInParent<DragAndDropCell>();
            Debug.Log("应该换颜色了");
            item.GetComponent<Image>().color = color;
            if (cell != null)
            {
                if (cell.unlimitedSource == true)
                {
                    string itemName = item.name;
                    item = Instantiate(item);                               // Clone item from source cell
                    item.name = itemName;
                }
            }
            item.transform.SetParent(transform, false);
            item.transform.localPosition = Vector3.zero;
            item.MakeRaycast(true);
            myDadItem = item;
        }
    }

    ////////// haku ///////////
    void PlaceItemNotDestroyOldItemVersion(DragAndDropItem item)
    {
        if (item != null)
        {
            UpdateMyItem();
            if (myDadItem != null)
                myDadItem.gameObject.transform.SetParent(SkillStonesBox.Instance.stonesTempContainer);
            myDadItem = null;
            // 以下功能本游戏用不上。即所谓的无限道具格
            DragAndDropCell SourceCell = item.GetComponentInParent<DragAndDropCell>();
            if (SourceCell != null)
            {
                if (SourceCell.unlimitedSource == true)
                {
                    string itemName = item.name;
                    item = Instantiate(item);// Clone item from source cell
                    item.name = itemName;
                }
            }
            item.transform.SetParent(transform, false);
            item.transform.localScale = Vector3.one * 0.7f;
            item.transform.localPosition = Vector3.zero;
            item.MakeRaycast(true);
            myDadItem = item;
        }
    }

    /// <summary>
    /// Destroy item in this cell
    /// </summary>
    void DestroyItem()
    {
        UpdateMyItem();
        if (myDadItem != null)
        {
            if (myDadItem != null)
            {
                Destroy(myDadItem.gameObject);
            }
        }
        myDadItem = null;
        UpdateMyItem();
    }

    /// <summary>
    /// Updates my item
    /// </summary>
    public void UpdateMyItem()
    {
        myDadItem = GetComponentInChildren<DragAndDropItem>();
        if (cellPhase == CellPhase.SkillStoneBoxCell)
        {
            if (gameObject.activeSelf)
            {
                if (myDadItem != null && myDadItem.SkillStoneOfPlayerId != null)
                {
                    _charIcon.gameObject.SetActive(true);
                    SkillStonesBox.Instance.ShowUsingCharIcon(myDadItem.SkillStoneOfPlayerId,_charIcon);
                }else{
                    _charIcon.gameObject.SetActive(false);
                }
            }
        }
    }

	/// <summary>
	/// Get item from this cell
	/// </summary>
	/// <returns> Item </returns>
	public DragAndDropItem GetItem()
	{
        //UpdateMyItem();
		return myDadItem;
	}

    /// <summary>
    /// Manualy add item into this cell
    /// </summary>
    /// <param name="newItem"> New item </param>
    public void AddItem(DragAndDropItem newItem)
    {
        if (newItem != null)
        {
            newItem.gameObject.SetActive(true);
            PlaceItemNotDestroyOldItemVersion(newItem);//PlaceItem(newItem); 2018.10.9
            UpdateMyItem();
        }
    }

    public void RemoveItemWithOutDestroy()
    {
        DragAndDropItem _DragAndDropItem = GetItem();
        if (_DragAndDropItem != null)
        {
            _DragAndDropItem.gameObject.SetActive(false);
            _DragAndDropItem.gameObject.transform.parent = null;
        }
        UpdateMyItem();
    }

    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    public void SwapItems(DragAndDropCell firstCell, DragAndDropCell secondCell)
	{
		if ((firstCell != null) && (secondCell != null))
		{
            firstCell.UpdateMyItem();
            secondCell.UpdateMyItem();
			DragAndDropItem firstItem = firstCell.GetItem();                // Get item from first cell
			DragAndDropItem secondItem = secondCell.GetItem();              // Get item from second cell
			// Swap items
			if (firstItem != null)
			{
                firstItem.transform.SetParent(secondCell.transform);
                //firstItem.transform.DOMove(secondCell.transform.position,1f);
				firstItem.transform.localPosition = Vector3.zero;
				firstItem.MakeRaycast(true);
			}
			if (secondItem != null)
			{
                secondItem.transform.SetParent(firstCell.transform);
                secondItem.transform.DOMove(firstCell.transform.position,0.5f);
				//secondItem.transform.localPosition = Vector3.zero;
				secondItem.MakeRaycast(true);
			}
			// Update states
			firstCell.UpdateMyItem();
			secondCell.UpdateMyItem();
		}
	}
}
