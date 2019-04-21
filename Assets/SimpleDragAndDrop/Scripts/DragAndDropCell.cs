using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Every item's cell must contain this script
/// </summary>
[RequireComponent(typeof(Image))]
public class DragAndDropCell : MonoBehaviour, IDropHandler
{
    public enum CellType                                                    // Cell types
    {
        Swap,                                                               // Items will be swapped between any cells
        DropOnly,                                                           // Item will be dropped into cell
        DragOnly                                                            // Item will be dragged from this cell
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
    public CellType cellType = CellType.Swap;                               // Special type of this cell
	[Tooltip("Sprite color for empty cell")]
    public Color empty = new Color();                                       // Sprite color for empty cell
	[Tooltip("Sprite color for filled cell")]
    public Color full = new Color();                                        // Sprite color for filled cell
	[Tooltip("This cell has unlimited amount of items")]
    public bool unlimitedSource = false;                                    // Item from this cell will be cloned on drag start

	private DragAndDropItem myDadItem;										// Item of this DaD cell

    // 自定义成员
    public SkillStoneSlot _SkillStoneSlot;//这个看起来比较古怪，目的是和这个cell对应的SkillStoneSlot形成一个互相链接。只针对9宫设置画面，和SkillStoneBox无关。
    // 自定义成员
    public bool isDeleteArea = false;

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
			if (myDadItem == item)                                         	// If item dragged from this cell
            {
                // Check cell's type
                switch (cellType)
                {
                    case CellType.DropOnly:
                        DragAndDropItem.icon.SetActive(false);              // Item can not be dragged. Hide icon
                        break;
                }
            }
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

    /// <summary>
    /// Item is dropped in this cell
    /// </summary>
    /// <param name="data"></param>
    public void OnDrop(PointerEventData data)//这个函数的很早阶段就应该进行关于9宫技能配置的valiadation，一旦拖入的石头不符合标准，不允许进行任何操作才是。
    {
        Debug.Log("拖拽操作step2");
        DragAndDropItem item;
        DragAndDropCell sourceCell = DragAndDropItem.sourceCell;
        if (DragAndDropItem.icon != null)
        {
            item = DragAndDropItem.draggedItem;
            sourceCell = DragAndDropItem.sourceCell;

            //由于存在一些技能编辑过程中的禁忌，我们应该有一个拖入技能石检查模块，一旦有错，要直接从这个拖动插件的位置处理环节直接把操作给断下
            if (DragAndDropItem.icon.activeSelf == true)                    // If icon inactive do not need to drop item into cell
            {
                if (sourceCell == this)
                {
                    return;
                }
                if ((item != null) && (sourceCell != this))
                {
                    //这里就应该进行valiadation，因为如果出了问题还不断下来，那么底下的流程就会牵扯到各种的数值更新
                    if (this._SkillStoneSlot != null && item._SkillConfigOfSkillStone != null)
                    {
                        item.ifDropedOnNineSlot = true;//服务于技能石添加撤销
                        if (this._SkillStoneSlot._TheNineSlot != null)
                        {
                            List<int> nineskillids = this._SkillStoneSlot._TheNineSlot.getCurrentNineSlotAllSkillIds();
                            
                            if (this == this._SkillStoneSlot._TheNineSlot.A1DragAndDropCell)
                            {
                                nineskillids[0] = item._SkillConfigOfSkillStone.id;
                            }
                            if (this == this._SkillStoneSlot._TheNineSlot.A2DragAndDropCell)
                            {
                                nineskillids[1] = item._SkillConfigOfSkillStone.id;
                            }
                            if (this == this._SkillStoneSlot._TheNineSlot.A3DragAndDropCell)
                            {
                                nineskillids[2] = item._SkillConfigOfSkillStone.id;
                            }
                            if (this == this._SkillStoneSlot._TheNineSlot.B1DragAndDropCell)
                            {
                                nineskillids[3] = item._SkillConfigOfSkillStone.id;
                            }
                            if (this == this._SkillStoneSlot._TheNineSlot.B2DragAndDropCell)
                            {
                                nineskillids[4] = item._SkillConfigOfSkillStone.id;
                            }
                            if (this == this._SkillStoneSlot._TheNineSlot.B3DragAndDropCell)
                            {
                                nineskillids[5] = item._SkillConfigOfSkillStone.id;
                            }
                            if (this == this._SkillStoneSlot._TheNineSlot.C1DragAndDropCell)
                            {
                                nineskillids[6] = item._SkillConfigOfSkillStone.id;
                            }
                            if (this == this._SkillStoneSlot._TheNineSlot.C2DragAndDropCell)
                            {
                                nineskillids[7] = item._SkillConfigOfSkillStone.id;
                            }
                            if (this == this._SkillStoneSlot._TheNineSlot.C3DragAndDropCell)
                            {
                                nineskillids[8] = item._SkillConfigOfSkillStone.id;
                            }
                            
                            int wholepint = MySkillStonesReader.skillsetValidation(nineskillids[0],nineskillids[1],nineskillids[2],
                                                                                    nineskillids[3],nineskillids[4],nineskillids[5],
                                                                                    nineskillids[6],nineskillids[7],nineskillids[8]);
                            if (wholepint < 0)
                            {
                                Debug.Log("Validation错误，不执行操作，返回");
                                return;
                            }
                        }
                    }

                    DropEventDescriptor desc = new DropEventDescriptor();
                    switch (cellType)                                       // Check this cell's type
                    {
                        case CellType.Swap:                                 // Item in destination cell can be swapped
							UpdateMyItem();
                            switch (sourceCell.cellType)
                            {
                                case CellType.Swap:                         // swap和swap对转只能是两种情况：要么是编辑模式，要么是add模式中新石头之间的互转
                                    // Fill event descriptor
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
                                            DropEventDescriptor descAutoswap = new DropEventDescriptor();
											descAutoswap.item = myDadItem;
                                            descAutoswap.sourceCell = this;
                                            descAutoswap.destinationCell = sourceCell;                                                            
                                            SendRequest(descAutoswap);                      // Send drop request
                                            StartCoroutine(NotifyOnDragEnd(descAutoswap));  // Send notification after drop will be finished
                                            if (descAutoswap.permission == true)            // If drop permitted by application
                                            {
                                                SwapItems(sourceCell, this);                // Swap items between cells
                                            }
                                            else
                                            {
                                                //我们回头会再看看这个环节，现在看PlaceItemNotDestroyOldItemVersion是没必要的，是我们对这个环节误会了。
                                                // 其实这个地方真正删除了的是手上拖动的那个图标而已
                                                PlaceItemNotDestroyOldItemVersion(item);            // Delete old item and place dropped item into this cell
                                                //PlaceItemNotDestroyOldItemVersion(item);
                                            }
                                        }
                                        else
                                        {
                                            PlaceItemNotDestroyOldItemVersion(item);
                                            //PlaceItemNotDestroyOldItemVersion(item);                // Place dropped item into this empty cell
                                        }
                                    }
                                    break;
                                default:                                    // Source是default目标是Swap，我们系统中最应该小心的一个环节。
                                                                            // 有以下情况：1.add模式下，从box把一个石头拖到9宫中同被新石头所覆盖的格子上
                                                                            // 2.add模式下，手指拖“原”技能石拖向9宫中的新技能石
                                                                            // 3.edit模式下，某bug发生导致石头box没关闭，将box中的石头拖向9宫中有石头或没石头的任何格子上。
                                    // Fill event descriptor
                                    desc.item = item;
                                    desc.sourceCell = sourceCell;
                                    desc.destinationCell = this;                                 
                                    SendRequest(desc);                      // Send drop request
                                    StartCoroutine(NotifyOnDragEnd(desc));  // Send notification after drop will be finished
                                    if (desc.permission == true)            // If drop permitted by application
                                    {
                                        PlaceItemNotDestroyOldItemVersion(item);// Place dropped item into this cell  该函数是自己写的，它要么会摧毁愿技能图标，要么单纯关闭拥有技能石头
                                    }
                                    break;
                            }
                            break;
                        case CellType.DropOnly:                             // Item only can be dropped into destination cell
                            // Fill event descriptor
                            desc.item = item;
                            desc.sourceCell = sourceCell;
                            desc.destinationCell = this;
                            Debug.Log("拖拽操作step3");
                            UpdateMyItem();
                            SendRequest(desc);                              // Send drop request
                            StartCoroutine(NotifyOnDragEnd(desc));          // Send notification after drop will be finished
                            if (desc.permission == true)                    // If drop permitted by application
                            {
                                PlaceItemNotDestroyOldItemVersion(item);                            // Place dropped item in this cell
                                //这个也就是我们把技能石头从SkillBox拖到九宫格上的那个瞬间。
                                //PlaceItemNotDestroyOldItemVersion(item);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            if (item != null)
            {
                if (item.GetComponentInParent<DragAndDropCell>() == null)   // If item have no cell after drop
                {
                    //Destroy(item.gameObject);                               // Destroy it
                }
            }
        }
        
        ////
        if (sourceCell == null || sourceCell.cellType == CellType.DropOnly)
        {
            Debug.Log("按理说不应该经过这里");
            return;
        }
        UpdateMyItem();
        UpdateBackgroundState();
        sourceCell.UpdateMyItem();
        sourceCell.UpdateBackgroundState();
        ////

        if (isDeleteArea)
        {
            if (this._SkillStoneSlot != null && this._SkillStoneSlot._TheNineSlot != null && this._SkillStoneSlot._TheNineSlot._preparingScene != null && this._SkillStoneSlot._SkillStonesBox != null)
            {
                UnityEngine.Events.UnityAction SkillstoneDeleteConfirm = () =>
                {
                    this._SkillStoneSlot._SkillStonesBox.deleteTheseStonesLocal(new List<DragAndDropItem>{ GetItem()});
                    DestroyItem();
                    Debug.Log("删除了一个技能石头。" + GetItem()._SkillConfigOfSkillStone.keyName + " 正式版本这个技能石头的删除操作需要进一步review ");
                };
                UnityEngine.Events.UnityAction SkillstoneDeleteCancel = () =>
                {
                    this._SkillStoneSlot._SkillStonesBox.arrangeSkillStonesToBox(
                                this._SkillStoneSlot._SkillStonesBox.getFocusingType(),
                                this._SkillStoneSlot._SkillStonesBox.getFocusingExType(),
                                this._SkillStoneSlot._SkillStonesBox.closeCheckBox.isOn,
                                this._SkillStoneSlot._SkillStonesBox.nearCheckBox.isOn,
                                this._SkillStoneSlot._SkillStonesBox.farCheckBox.isOn,
                                this._SkillStoneSlot._SkillStonesBox.outRangeCheckBox.isOn,
                                this._SkillStoneSlot._TheNineSlot.getUsingStonesId());
                };
                this._SkillStoneSlot._TheNineSlot._preparingScene._LoadingCanvas.arrangeValiationWindow(SkillstoneDeleteConfirm, SkillstoneDeleteCancel, 
                                                                                                        "确实要删除技能石头：" + GetItem()._SkillConfigOfSkillStone.keyName + "?");
            }
        }

        return;
    }

	/// <summary>
	/// Put item into this cell.
	/// </summary>
	/// <param name="item">Item.</param>
	private void PlaceItem(DragAndDropItem item)
	{
		if (item != null)
		{
			DestroyItem();                                            	// Remove current item from this cell
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

    private void PlaceItem(DragAndDropItem item,Color color)
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
    private void PlaceItemNotDestroyOldItemVersion(DragAndDropItem item)
    {
        if (item != null)
        {
            UpdateMyItem();
            if (myDadItem != null)
            {
                if (myDadItem.myskillstone_localid != -1)
                {
                    myDadItem.gameObject.transform.SetParent(null);
                    myDadItem.gameObject.SetActive(false);
                }else{
                    Destroy(myDadItem.gameObject);
                }
            }
            myDadItem = null;
            UpdateBackgroundState();

            DragAndDropCell cell = item.GetComponentInParent<DragAndDropCell>();
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
            item.transform.localScale = Vector3.one * 0.6f;
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
            DropEventDescriptor desc = new DropEventDescriptor();
            // Fill event descriptor
            desc.triggerType = TriggerType.ItemWillBeDestroyed;
			desc.item = myDadItem;
            desc.sourceCell = this;
            desc.destinationCell = this;
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
	/// <param name="condition"> true - filled, false - empty </param>
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
            DropEventDescriptor desc = new DropEventDescriptor();
            // Fill event descriptor
            desc.triggerType = TriggerType.ItemAdded;
            desc.item = newItem;
            desc.sourceCell = this;
            desc.destinationCell = this;
            //UpdateMyItem();
            SendNotification(desc);
        }
    }

    /// <summary>
    /// Manualy delete item from this cell
    /// </summary>
    public void RemoveItem()
    {
        DestroyItem();
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
				firstItem.transform.SetParent(secondCell.transform, false);
				firstItem.transform.localPosition = Vector3.zero;
				firstItem.MakeRaycast(true);
			}
			if (secondItem != null)
			{
				secondItem.transform.SetParent(firstCell.transform, false);
				secondItem.transform.localPosition = Vector3.zero;
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
