using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using dataAccess;
using TouchScript.Gestures;

/// <summary>
/// Every item's cell must contain this script
/// </summary>

[RequireComponent(typeof(Image))]
public partial class StoneCell : MonoBehaviour, IDropHandler
{
    public enum CellPhase
    {
        SkillStoneBoxCell,
        NineSlotCell,
        SKLevelUpMSlot,
        StoneMergeSlot
    }

    public LongPressGesture lpGesture;
    public PressGesture pGesture;
    public TapGesture tGesture;
    
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

    [Tooltip("Level")] 
    [SerializeField] Text level;
    
    SKStoneItem myDadItem;

    void ShowLevel()
    {
        if (level == null)
            return;
        if (myDadItem)
        {
            StoneOfPlayerInfo info = Stones.Get(myDadItem.instanceId);
            level.text = info.GetLevel().ToString();
        }
        else
        {
            level.text = string.Empty;
        }
    }

    public void ClearGestureFeature()
    {
        lpGesture.Clear();
        pGesture.Clear();
        tGesture.Clear();
    }
    
    /// <summary>
    /// Put item into this cell.(Keep old item in that cell safe)
    /// </summary>
    /// <param name="item">Item.</param>
    void PlaceItemNotDestroyOldItemVersion(SKStoneItem item)
    {
        item.gameObject.SetActive(true);
        RemoveToTemp();
        switch(cellPhase)
        {
            case CellPhase.NineSlotCell:
            case CellPhase.StoneMergeSlot:
            case CellPhase.SKLevelUpMSlot:
                item._using = true;
                break;
            default:
                item._using = false;
                break;
        }
        item.transform.SetParent(transform, false);
        item.transform.localScale = Vector3.one * 1.2f;
        item.transform.localPosition = Vector3.zero;
        item.MakeRaycast(true);
    }
    
    /// <summary>
    /// Updates my item
    /// </summary>
    public void UpdateMyItem()
    {
        myDadItem = GetComponentInChildren<SKStoneItem>();
        if (cellPhase == CellPhase.SkillStoneBoxCell)
        {
            if (gameObject.activeSelf)
            {
                ShowUsingChar(myDadItem, _charIcon);
            }
        }
        ShowLevel();
    }
    
    // Show Character icon using this SkillStone
    void ShowUsingChar(SKStoneItem Item, HeroIcon targetIcon)
    {
        if (Item == null || Item.instanceId == null)
        {
            targetIcon.gameObject.SetActive(false);
            return;
        }
        StoneOfPlayerInfo SSInfo = Stones.Get(Item.instanceId);
        if (SSInfo == null || SSInfo.inUsingMonsterOfPlayerId == null)
        {
            targetIcon.gameObject.SetActive(false);
            return;
        }
            
        UnitInfo _one = MyMonsters.Get(SSInfo.inUsingMonsterOfPlayerId);
        if (_one == null)
        {
            targetIcon.gameObject.SetActive(false);
            return;
        }
        targetIcon.gameObject.SetActive(true);
        UnitConfig unitConfig = Units.GetUnitConfig(_one.r_id);
        targetIcon.ChangeIcon(unitConfig == null ? null : MonsterIconDic.Get(unitConfig.RECORD_ID),
            unitConfig == null ? Zokusei.Null : unitConfig._zokusei);
    }
    
    /// <summary>
    /// Manualy add item into this cell
    /// </summary>
    /// <param name="newItem"> New item </param>
    public void AddItem(SKStoneItem newItem)
    {
        if (newItem != null)
        {
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
    
    /// <summary>
    /// Get item from this cell
    /// </summary>
    /// <returns> Item </returns>
    public SKStoneItem GetItem()
    {
        UpdateMyItem();
        return myDadItem;
    }
}
