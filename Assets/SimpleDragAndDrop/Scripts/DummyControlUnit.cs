using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Example of control application for drag and drop events handle
/// </summary>
public class DummyControlUnit : MonoBehaviour
{
    public TheNineSlot _TheNineSlot;
    public SkillStonesBox _SkillStonesBox;

    //_SkillStoneSlot._SkillStonesBox.addUsingStoneInfo(_SkillStoneSlot._TheNineSlot.getUsingStonesId());

    /// <summary>
    /// Operate all drag and drop requests and events from children cells
    /// </summary>
    /// <param name="desc"> request or event descriptor </param>
    void OnSimpleDragAndDropEvent(DragAndDropCell.DropEventDescriptor desc)
    {
        // Get control unit of source cell
        DummyControlUnit sourceSheet = desc.sourceCell.GetComponentInParent<DummyControlUnit>();
        // Get control unit of destination cell
        DummyControlUnit destinationSheet = desc.destinationCell.GetComponentInParent<DummyControlUnit>();
        switch (desc.triggerType)                                               // What type event is?
        {
            case DragAndDropCell.TriggerType.DropRequest:                       // Request for item drag (note: do not destroy item on request)
                Debug.Log("Request " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
                break;
            case DragAndDropCell.TriggerType.DropEventEnd:                      // Drop event completed (successful or not)
                if (desc.permission == true)                                    // If drop successful (was permitted before)
                {
                    Debug.Log("Successful drop " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
                    List<int> skillStoneIDs = _TheNineSlot.getUsingStonesId();
                    _SkillStonesBox.arrangeSkillStonesToBox(_SkillStonesBox.getFocusingType(), _SkillStonesBox.getFocusingExType(),
                                                            _SkillStonesBox.closeCheckBox.isOn,
                                                            _SkillStonesBox.nearCheckBox.isOn,
                                                            _SkillStonesBox.farCheckBox.isOn,
                                                            _SkillStonesBox.outRangeCheckBox.isOn,
                                                            skillStoneIDs);
                    _TheNineSlot.SeliWholeNineAndTwo();
                    //  上面的这行SeliWholeNineAndTwo还有一个相当大的重点在于处理了石头上面的inbox参数
                    //这里有个巨大疑问。为什么skillStoneIDs的获取有问题而这里就没问题？
                }
                else                                                            // If drop unsuccessful (was denied before)
                {
                    Debug.Log("Denied drop " + desc.item.name + " from " + sourceSheet.name + " to " + destinationSheet.name);
                }
                break;
            case DragAndDropCell.TriggerType.ItemAdded:                         // New item is added from application
                //Debug.Log("Item " + desc.item.name + " added into " + destinationSheet.name);
                break;
            case DragAndDropCell.TriggerType.ItemWillBeDestroyed:               // Called before item be destructed (can not be canceled)
                Debug.Log("Item " + desc.item.name + " will be destroyed from " + sourceSheet.name);
                break;
            default:
                Debug.Log("Unknown drag and drop event");
                break;
        }
    }

    /// <summary>
    /// Add item in first free cell
    /// </summary>
    /// <param name="item"> new item </param>
    public void AddItemInFreeCell(DragAndDropItem item)
    {
        foreach (DragAndDropCell cell in GetComponentsInChildren<DragAndDropCell>())
        {
            if (cell != null)
            {
				if (cell.GetItem() == null)
                {
                    cell.AddItem(Instantiate(item.gameObject).GetComponent<DragAndDropItem>());
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Remove item from first not empty cell
    /// </summary>
    public void RemoveFirstItem()
    {
        foreach (DragAndDropCell cell in GetComponentsInChildren<DragAndDropCell>())
        {
            if (cell != null)
            {
				if (cell.GetItem() != null)
                {
                    cell.RemoveItem();
                    break;
                }
            }
        }
    }
}
