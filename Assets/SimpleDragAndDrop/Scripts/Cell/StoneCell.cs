using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using mainMenu;
using DG.Tweening;

/// <summary>
/// Every item's cell must contain this script
/// </summary>
[RequireComponent(typeof(Image))]
public partial class StoneCell : MonoBehaviour, IDropHandler
{
    public enum CellPhase
    {
        SkillStoneBoxCell,
        NineSlotCell_full,
        NineSlotCell_empty,
        DeleteArea,
    }
    
    public enum SelectMode
    {
        single = 1,
        multi = 2
    }
    
    public SelectMode _SelectMode = SelectMode.single;
    
    [Tooltip("using Stone Character Icon")]
    public HeroIcon _charIcon;
    [Tooltip("选中框，用来确保有一个选中框选中这个格子的时候不会有其他选中框选中他。")]
    public GameObject _selected;
    [Tooltip("Functional type of this cell")]
    public CellPhase cellPhase = CellPhase.SkillStoneBoxCell;
    [Tooltip("Image of this cell")]
    public Image image;
    [Tooltip("Sprite color for empty cell")]
    public Color empty = new Color(); // Sprite color for empty cell
    [Tooltip("Sprite color for filled cell")]
    public Color full = new Color(); // Sprite color for filled cell
    
    SKStoneItem myDadItem;

    public SkillStoneSlot _SkillStoneSlot;
    SingleThreadProcesser _SingleThreadProcesser;
    
    void Awake()
    {
        _SingleThreadProcesser = transform.GetComponent<SingleThreadProcesser>();
        if (_SingleThreadProcesser == null)
            _SingleThreadProcesser = gameObject.AddComponent<SingleThreadProcesser>();
    }
   
    /// <summary>
    /// Put item into this cell.
    /// </summary>
    /// <param name="item">Item.</param>
    void PlaceItem(SKStoneItem item)
    {
        if (item != null)
        {
            DestroyItem(); // Remove current item from this cell
            myDadItem = null;
            StoneCell cell = item.GetComponentInParent<StoneCell>();
            item.transform.SetParent(transform, false);
            item.transform.localPosition = Vector3.zero;
            item.MakeRaycast(true);
            myDadItem = item;
        }
    }

    /// <summary>
    /// Put item into this cell.(Keep old item in that cell safe)
    /// </summary>
    /// <param name="item">Item.</param>
    void PlaceItemNotDestroyOldItemVersion(SKStoneItem item)
    {
        if (item != null)
        {
            UpdateMyItem();
            if (myDadItem != null)
                myDadItem.gameObject.transform.SetParent(SkillStonesBox.target.stonesTempContainer);
            myDadItem = null;
            StoneCell SourceCell = item.GetComponentInParent<StoneCell>();
            item.transform.SetParent(transform, false);
            item.transform.localScale = Vector3.one * 1.2f;
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
        myDadItem = GetComponentInChildren<SKStoneItem>();
        if (_SkillStoneSlot != null)
        {
            cellPhase = myDadItem != null ? CellPhase.NineSlotCell_full : CellPhase.NineSlotCell_empty;
        }
        switch(cellPhase)
        {
            case CellPhase.NineSlotCell_empty:
            case CellPhase.NineSlotCell_full:
                break;
            case CellPhase.SkillStoneBoxCell:
                if (gameObject.activeSelf)
                {
                    ShowUsingCharIcon(myDadItem,_charIcon);
                }
                break;
        }
    }
    
    void ShowUsingCharIcon(SKStoneItem dragAndDropItem, HeroIcon targetIcon)
    {
        _SingleThreadProcesser.Run(SkillStonesBox.target.ShowUsingChar(dragAndDropItem, targetIcon));
    }

    /// <summary>
    /// Get item from this cell
    /// </summary>
    /// <returns> Item </returns>
    public SKStoneItem GetItem()
	{
        //UpdateMyItem();
		return myDadItem;
	}

    /// <summary>
    /// Manualy add item into this cell
    /// </summary>
    /// <param name="newItem"> New item </param>
    public void AddItem(SKStoneItem newItem)
    {
        if (newItem != null)
        {
            TheNineSlot.SkillEditError valR = TheNineSlot.target.CheckEditBasedOnCurrent(newItem, this);
            if (valR != TheNineSlot.SkillEditError.Perfect)
            {
                TheNineSlot.target.ValiationWarn(valR, MemberDetail.target._focusing.monsterOfPlayerId);
                return;
            }
            
            newItem.gameObject.SetActive(true);
            PlaceItemNotDestroyOldItemVersion(newItem);//PlaceItem(newItem); 2018.10.9
            UpdateMyItem();
        }
    }

    /// <summary>
    /// Swap items between two cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    public void SwapItems(StoneCell firstCell, StoneCell secondCell)
    {
        if ((firstCell != null) && (secondCell != null))
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
    }
    
    public void RemoveItemWithOutDestroy()
    {
        SKStoneItem _DragAndDropItem = GetItem();
        if (_DragAndDropItem != null)
        {
            _DragAndDropItem.gameObject.SetActive(true);
            _DragAndDropItem.gameObject.transform.SetParent(ResourceKeeper.dontDestroyOnLoadParent);
        }
        UpdateMyItem();
    }
}
