using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Drag and Drop item.
/// </summary>
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static bool dragDisabled = false;										// Drag start global disable

	public static DragAndDropItem draggedItem;                                      // Item that is dragged now
	public static GameObject icon;                                                  // Icon of dragged item
	public static DragAndDropCell sourceCell;                                       // From this cell dragged item is

	public delegate void DragEvent(DragAndDropItem item);
	public static event DragEvent OnItemDragStartEvent;                             // Drag start event
	public static event DragEvent OnItemDragEndEvent;                               // Drag end event

	private static Canvas canvas;                                                   // Canvas for item drag operation
	private static string canvasName = "DragAndDropCanvas";                   		// Name of canvas
	private static int canvasSortOrder = 100;										// Sort order for canvas

    //自定义item属性
    public SkillConfig _SkillConfigOfSkillStone;

    // 用来记忆他们在SKillStoneBox里的位置。从而来保证他们可以返回盒子里正确位置。
    // 这个量其实就相当于拥有技能石头的本地id，但是，这个本地id并不是像玩家拥有怪物的localdi那样相对固定，
    // 这个id仅仅是技能石头盒子每次依据检索条件展示所有石头的过程中临时给加的，因此实际的相关财产操作
    // 必须要保证逻辑的正确性，这些机制只是服务于显示
    public int myskillstone_localid = -1;

    public bool inBox;//myskillstone_localid能看出石头是新拖入的还是角色已经有的，但inBox用来区分操作中的未绑定到角色身上的技能石是在盒子里还是在9宫格里
    public bool ifDropedOnNineSlot;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
	{
		if (canvas == null)
		{
			GameObject canvasObj = new GameObject(canvasName);
			canvas = canvasObj.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = canvasSortOrder;
		}
	}

	/// <summary>
	/// This item started to drag.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
        ifDropedOnNineSlot = false;
		if (dragDisabled == false)
		{
			sourceCell = GetCell();                       							// Remember source cell
			draggedItem = this;                                             		// Set as dragged item
			// Create item's icon
			icon = new GameObject();
			icon.transform.SetParent(canvas.transform);
			icon.name = "Icon";
			Image myImage = GetComponent<Image>();
			myImage.raycastTarget = false;                                        	// Disable icon's raycast for correct drop handling
			Image iconImage = icon.AddComponent<Image>();
			iconImage.raycastTarget = false;
			iconImage.sprite = myImage.sprite;
            iconImage.color = myImage.color;
			RectTransform iconRect = icon.GetComponent<RectTransform>();
			// Set icon's dimensions
			RectTransform myRect = GetComponent<RectTransform>();
			iconRect.pivot = new Vector2(0.5f, 0.5f);
			iconRect.anchorMin = new Vector2(0.5f, 0.5f);
			iconRect.anchorMax = new Vector2(0.5f, 0.5f);
			iconRect.sizeDelta = new Vector2(myRect.rect.width, myRect.rect.height);
			if (OnItemDragStartEvent != null)
			{
				OnItemDragStartEvent(this);                                			// Notify all items about drag start for raycast disabling
			}
		}
        Debug.Log("拖拽操作step1");
	}

	/// <summary>
	/// Every frame on this item drag.
	/// </summary>
	/// <param name="data"></param>
	public void OnDrag(PointerEventData data)
	{
		if (icon != null)
		{
			icon.transform.position = Input.mousePosition;                          // Item's icon follows to cursor in screen pixels
		}
	}

    /// <summary>
    /// This item is dropped.
    /// </summary>
    /// <param name="eventData"></param>
    /// //这个环节里很多操作看起来和DummyControlUnit里的.DropEventEnd很像，不要被迷惑，真正处理适配技能石功能的主要是DummyControlUnit那边，
    /// 这个环节处理的是把石头从9宫拖出来扔到空白区域的情况。
    /// 这个空白区域应该覆盖技能石头盒子，因为玩家想撤销添加技能操作的时候会本能的把石头向盒子方向移动。
    public void OnEndDrag(PointerEventData eventData)
    {
		ResetConditions();
        if (!ifDropedOnNineSlot && !inBox)//这个Box指的是道具盒子，这个环节是针对从9宫拉出石头并扔到了空白区域。//
        {
            DragAndDropCell tempSourceCell = GetCell();
            if (tempSourceCell != null)
            {
                if (tempSourceCell._SkillStoneSlot != null)//说明在9宫格里
                {
                    if (this.myskillstone_localid != -1)//说明是放进9宫格还没确定的新石头
                    {
                        if (tempSourceCell._SkillStoneSlot._SkillStonesBox != null && tempSourceCell._SkillStoneSlot._TheNineSlot != null)
                        {
                            List<int> inNineTwo = tempSourceCell._SkillStoneSlot._TheNineSlot.getUsingStonesId();
                            if (inNineTwo.Contains(this.myskillstone_localid))
                            {
                                Debug.Log("石头回到背包,石头本地id："+this.myskillstone_localid);
                                inNineTwo.Remove(this.myskillstone_localid);//这个恐怕是肯定会跑
                            }
                            tempSourceCell._SkillStoneSlot._SkillStonesBox.arrangeSkillStonesToBox(
                                tempSourceCell._SkillStoneSlot._SkillStonesBox.getFocusingType(),
                                tempSourceCell._SkillStoneSlot._SkillStonesBox.getFocusingExType(),
                                tempSourceCell._SkillStoneSlot._SkillStonesBox.closeCheckBox.isOn,
                                tempSourceCell._SkillStoneSlot._SkillStonesBox.nearCheckBox.isOn,
                                tempSourceCell._SkillStoneSlot._SkillStonesBox.farCheckBox.isOn,
                                tempSourceCell._SkillStoneSlot._SkillStonesBox.outRangeCheckBox.isOn,
                                inNineTwo);
                            tempSourceCell._SkillStoneSlot._TheNineSlot.SeliWholeNineAndTwo();
                        }
                    }
                }
            }
        }
    }

	/// <summary>
	/// Resets all temporary conditions.
	/// </summary>
	private void ResetConditions()
	{
		if (icon != null)
		{
			Destroy(icon);                                                          // Destroy icon on item drop
		}
		if (OnItemDragEndEvent != null)
		{
			OnItemDragEndEvent(this);                                       		// Notify all cells about item drag end
		}
		draggedItem = null;
		icon = null;
		sourceCell = null;
	}

	/// <summary>
	/// Enable item's raycast.
	/// </summary>
	/// <param name="condition"> true - enable, false - disable </param>
	public void MakeRaycast(bool condition)
	{
		Image image = GetComponent<Image>();
		if (image != null)
		{
			image.raycastTarget = condition;
		}
	}

	/// <summary>
	/// Gets DaD cell which contains this item.
	/// </summary>
	/// <returns>The cell.</returns>
	public DragAndDropCell GetCell()
	{
		return GetComponentInParent<DragAndDropCell>();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		ResetConditions();
	}
}
