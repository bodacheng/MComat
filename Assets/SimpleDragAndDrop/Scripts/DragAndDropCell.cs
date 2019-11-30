using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
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

    public enum TriggerType                                                 // Types of drag and drop events
    {
        DropRequest,                                                        // Request for item drop from one cell to another
        DropEventEnd,                                                       // Drop event completed
        ItemAdded,                                                          // Item manualy added into cell
        ItemWillBeDestroyed                                                 // Called just before item will be destroyed
    }

    public class DropEventDescriptor                                        // Info about item's drop event
    {
        public TriggerType triggerType;                                     // Type of drag and drop trigger
        public DragAndDropCell sourceCell;                                  // From this cell item was dragged
        public DragAndDropCell destinationCell;                             // Into this cell item was dropped
        public DragAndDropItem item;                                        // Dropped item
        public bool permission;                                             // Decision need to be made on request
    }
    
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

	private DragAndDropItem myDadItem;										// Item of this DaD cell

    // 自定义成员
    public SkillStoneSlot _SkillStoneSlot;//这个看起来比较古怪，目的是和这个cell对应的SkillStoneSlot形成一个互相链接。只针对9宫设置画面，和SkillStoneBox无关。

    void OnEnable()
    {
        DragAndDropItem.OnItemDragStartEvent += OnAnyItemDragStart;         // Handle any item drag start
        DragAndDropItem.OnItemDragEndEvent += OnAnyItemDragEnd;             // Handle any item drag end
		UpdateMyItem();
		UpdateBackgroundState();
    }

    void OnDisable()
    {
        DragAndDropItem.OnItemDragStartEvent -= OnAnyItemDragStart;
        DragAndDropItem.OnItemDragEndEvent -= OnAnyItemDragEnd;
        StopAllCoroutines();                                                // Stop all coroutines if there is any
    }

    /// <summary>
    /// On any item drag start need to disable all items raycast for correct drop operation
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragStart(DragAndDropItem item)
    {
		UpdateMyItem();
		if (myDadItem != null)
        {
			myDadItem.MakeRaycast(false);                                  	// Disable item's raycast for correct drop handling
        }
    }

    /// <summary>
    /// On any item drag end enable all items raycast
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragEnd(DragAndDropItem item)
    {
		UpdateMyItem();
		if (myDadItem != null)
        {
			myDadItem.MakeRaycast(true);                                  	// Enable item's raycast
        }
		UpdateBackgroundState();
    }

    void OnDropEvent_Swap(DropEventDescriptor desc,DragAndDropItem item, DragAndDropCell sourceCell)
    {
        desc.item = item;
        desc.sourceCell = sourceCell;
        desc.destinationCell = this;
        SendRequest(desc);                      // Send drop request
        StartCoroutine(NotifyOnDragEnd(desc));  // Send notification after drop will be finished
        if (desc.permission == true)            // If drop permitted by application
        {
            if (myDadItem != null)            // If destination cell has item
            {
                // Fill event descriptor
                DropEventDescriptor descAutoswap = new DropEventDescriptor
                {
                    item = myDadItem,
                    sourceCell = this,
                    destinationCell = sourceCell
                };
                SendRequest(descAutoswap);                      // Send drop request
                StartCoroutine(NotifyOnDragEnd(descAutoswap));  // Send notification after drop will be finished
                if (descAutoswap.permission == true)            // If drop permitted by application
                    SwapItems(sourceCell, this);                // Swap items between cells
                else
                {
                    Debug.Log("交换行为未被允许，请检查道具逻辑:"+item);
                }
                    
            }
            else 
            {
                Debug.Log("交换行为未被允许，请检查道具逻辑:"+item);
            }
        }
    }
    
    void OnDropEvent_Override(DropEventDescriptor desc,DragAndDropItem item, DragAndDropCell sourceCell)
    {
        desc.item = item;
        desc.sourceCell = sourceCell;
        desc.destinationCell = this;
        UpdateMyItem();
        SendRequest(desc);                              // Send drop request
        StartCoroutine(NotifyOnDragEnd(desc));          // Send notification after drop will be finished
        if (desc.permission == true)                    // If drop permitted by application
        {
            PlaceItemNotDestroyOldItemVersion(item);// Place dropped item in this cell
        }
    }
    
    /// <summary>
    /// Item is dropped in this cell
    /// </summary>
    /// <param name="data"></param>
    //  这个函数的很早阶段就应该进行关于9宫技能配置的valiadation，一旦拖入的石头不符合标准，不允许进行任何操作才是。
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
                    DropEventDescriptor desc = new DropEventDescriptor();
                    switch (cellPhase)//自身phase
                    {
                        case CellPhase.NineSlotCell_full:
                            switch (sourceCell.cellPhase)
                            {
                                case CellPhase.NineSlotCell_full://从box把一个石头拖到9宫中同被新石头所覆盖的格子上
                                    OnDropEvent_Swap(desc,item,sourceCell);
                                    break;
                                case CellPhase.NineSlotCell_empty://add模式下，从box把一个石头拖到9宫中同被新石头所覆盖的格子上
                                    break;
                                case CellPhase.SkillStoneBoxCell:
                                    if (AccountCharsSet.CheckifContainsAccountCharsSetKey(MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(item.localID).inUsingMonsterOfPlayerId))
                                    {
                                        Debug.Log("其他玩家正在使用的石头不可拖入");
                                        return;
                                    }
                                    if (!RefreshWholePoint(item))
                                    {
                                        Debug.Log("Validation错误，不执行操作，返回");
                                        return;
                                    }
                                    OnDropEvent_Swap(desc,item,sourceCell);
                                    break;
                            }
                            break;
                        case CellPhase.NineSlotCell_empty:
                            switch (sourceCell.cellPhase)
                            {
                                case CellPhase.NineSlotCell_full://add模式下，从box把一个石头拖到9宫中同被新石头所覆盖的格子上
                                    OnDropEvent_Override(desc,item,sourceCell);
                                    break;
                                case CellPhase.SkillStoneBoxCell:
                                    if (AccountCharsSet.CheckifContainsAccountCharsSetKey(MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(item.localID).inUsingMonsterOfPlayerId))
                                    {
                                        Debug.Log("其他玩家正在使用的石头不可拖入");
                                        return;
                                    }
                                    if (!RefreshWholePoint(item))
                                    {
                                        Debug.Log("Validation错误，不执行操作，返回");
                                        return;
                                    }
                                    OnDropEvent_Override(desc,item,sourceCell);
                                    break;
                                case CellPhase.NineSlotCell_empty:
                                    break;
                            }
                            break;
                        case CellPhase.SkillStoneBoxCell:
                            switch (sourceCell.cellPhase)
                            {
                                case CellPhase.NineSlotCell_full:// 已装备石头的卸载功能。 
                                    if (this.GetItem()==null)
                                    {
                                        OnDropEvent_Override(desc,item,sourceCell);
                                    }else{
                                        sourceCell._SkillStoneSlot.ReturnStoneToBox();
                                    }
                                break;
                                case CellPhase.NineSlotCell_empty:
                                    OnDropEvent_Override(desc,item,sourceCell);
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
                                        TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(MySkillStonesReader.RemoveTheseStonesFromLocalDic(new List<string>{ GetItem().localID}));
                                        UpdateMyItem();
                                    };
                                    UnityEngine.Events.UnityAction SkillstoneDeleteCancel = () =>
                                    {
                                        TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(SkillStonesBox.Instance.ArrangeSkillStonesToBox());
                                    };
                                    LoadingCanvas.target.ArrangeValiationWindow(SkillstoneDeleteConfirm, SkillstoneDeleteCancel, 
                                        "确实要删除技能石头：" + GetItem()._SkillConfigOfSkillStone.REAL_NAME + "?");
                                    break;
                            }
                            break;
                    }
                }
            }
        }
        
        if (sourceCell == null)
        {
            Debug.Log("按理说不应该经过这里");
            return;
        }
                      
        UpdateMyItem();
        UpdateBackgroundState();
        sourceCell.UpdateMyItem();
        sourceCell.UpdateBackgroundState();
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
        UpdateBackgroundState();
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
        UpdateBackgroundState();
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
            UpdateBackgroundState();

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
            //////////////////////////////

            item.transform.SetParent(transform, false);
            item.transform.localScale = Vector3.one * 0.7f;
            item.transform.localPosition = Vector3.zero;
            item.MakeRaycast(true);
            myDadItem = item;
        }
        UpdateBackgroundState();
    }

    /// <summary>
    /// Destroy item in this cell
    /// </summary>
    private void DestroyItem()
    {
		UpdateMyItem();
		if (myDadItem != null)
        {
            DropEventDescriptor desc = new DropEventDescriptor
            {
                // Fill event descriptor
                triggerType = TriggerType.ItemWillBeDestroyed,
                item = myDadItem,
                sourceCell = this,
                destinationCell = this
            };
            SendNotification(desc);                                         // Notify application about item destruction
			if (myDadItem != null)
			{
				Destroy(myDadItem.gameObject);
			}
        }
		myDadItem = null;
		UpdateBackgroundState();
        UpdateMyItem();
    }
    
    bool RefreshWholePoint(DragAndDropItem item)
    {
        //这里就应该进行valiadation，因为如果出了问题还不断下来，那么底下的流程就会牵扯到各种的数值更新
        if (this._SkillStoneSlot != null && item._SkillConfigOfSkillStone != null)
        {
            if (TheNineSlot.Instance != null)
            {
                List<string> nineskillids = TheNineSlot.Instance.GetCurrentNineSlotAllSkillIds();
                
                if (this == TheNineSlot.Instance.A1DragAndDropCell)
                {
                    nineskillids[0] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                if (this == TheNineSlot.Instance.A2DragAndDropCell)
                {
                    nineskillids[1] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                if (this == TheNineSlot.Instance.A3DragAndDropCell)
                {
                    nineskillids[2] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                if (this == TheNineSlot.Instance.B1DragAndDropCell)
                {
                    nineskillids[3] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                if (this == TheNineSlot.Instance.B2DragAndDropCell)
                {
                    nineskillids[4] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                if (this == TheNineSlot.Instance.B3DragAndDropCell)
                {
                    nineskillids[5] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                if (this == TheNineSlot.Instance.C1DragAndDropCell)
                {
                    nineskillids[6] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                if (this == TheNineSlot.Instance.C2DragAndDropCell)
                {
                    nineskillids[7] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                if (this == TheNineSlot.Instance.C3DragAndDropCell)
                {
                    nineskillids[8] = item._SkillConfigOfSkillStone.RECORD_ID;
                }
                
                int wholepint = MySkillStonesReader.SkillSetValidation(nineskillids[0],nineskillids[1],nineskillids[2],
                                                                        nineskillids[3],nineskillids[4],nineskillids[5],
                                                                        nineskillids[6],nineskillids[7],nineskillids[8]);
                if (wholepint < 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Send drag and drop information to application
    /// </summary>
    /// <param name="desc"> drag and drop event descriptor </param>
    private void SendNotification(DropEventDescriptor desc)
    {
        if (desc != null)
        {
            // Send message with DragAndDrop info to parents GameObjects
            gameObject.SendMessageUpwards("OnSimpleDragAndDropEvent", desc, SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// Send drag and drop request to application
    /// </summary>
    /// <param name="desc"> drag and drop event descriptor </param>
    /// <returns> result from desc.permission </returns>
    private bool SendRequest(DropEventDescriptor desc)
    {
        bool result = false;
        if (desc != null)
        {
            desc.triggerType = TriggerType.DropRequest;
            desc.permission = true;
            SendNotification(desc);
            result = desc.permission;
        }
        return result;
    }

    /// <summary>
    /// Wait for event end and send notification to application
    /// </summary>
    /// <param name="desc"> drag and drop event descriptor </param>
    /// <returns></returns>
    private IEnumerator NotifyOnDragEnd(DropEventDescriptor desc)
    {
        // Wait end of drag operation
        while (DragAndDropItem.draggedItem != null)
        {
            yield return new WaitForEndOfFrame();
        }
        desc.triggerType = TriggerType.DropEventEnd;
        SendNotification(desc);
    }

	/// <summary>
	/// Change cell's sprite color on item put/remove.
	/// </summary>
    /// 这个就是改个颜色，因为我们的特殊需求我们不用它的逻辑了。
	public void UpdateBackgroundState()
	{
		//Image bg = GetComponent<Image>();
		//if (bg != null)
		//{
		//	bg.color = myDadItem != null ? full : empty;
		//}
	}

	/// <summary>
	/// Updates my item
	/// </summary>
	public void UpdateMyItem()
	{
        myDadItem = GetComponentInChildren<DragAndDropItem>();
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
            DropEventDescriptor desc = new DropEventDescriptor
            {
                // Fill event descriptor
                triggerType = TriggerType.ItemAdded,
                item = newItem,
                sourceCell = this,
                destinationCell = this
            };
            //UpdateMyItem();
            SendNotification(desc);
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
			firstCell.UpdateBackgroundState();
			secondCell.UpdateBackgroundState();
		}
	}
}
