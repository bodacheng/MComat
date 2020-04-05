using UnityEngine;
using mainMenu;
using UnityEngine.UI;
using Api.Dto.Model;
using System.Collections.Generic;
using dataAccess;

public class StoneDeleteManger : MonoBehaviour
{
    public Text CurrentSelectedCount;
    public RectTransform SkillInfoT,SelectionInfoT,OperationsT,SelectionConfirmT;
    public SkillStonesBox SkillStonesBox;
    
    List<SkillStoneOfPlayerInfoModel> selected = new List<SkillStoneOfPlayerInfoModel>();

    public void EnterDeleteMode()
    {
        SkillStonesBox._Selected.SetActive(false);
        foreach (KeyValuePair<int, DragAndDropCell> KV in SkillStonesBox.CellsDictionary)
        {
            KV.Value._SelectMode = DragAndDropCell.SelectMode.multi;
            Button button = KV.Value.GetComponent<Button>();
            button.onClick.AddListener(delegate { SelectForDelete(KV.Value); });
        }
        
        SkillInfoT.gameObject.SetActive(false);
        SelectionInfoT.gameObject.SetActive(true);
        OperationsT.gameObject.SetActive(false);
        SelectionConfirmT.gameObject.SetActive(true);
    }
    
    public void ExitDeleteMode()
    {
        SkillInfoT.gameObject.SetActive(true);
        SelectionInfoT.gameObject.SetActive(false);
        OperationsT.gameObject.SetActive(true);
        SelectionConfirmT.gameObject.SetActive(false);
        CurrentSelectedCount.text = "";
        SkillStonesBox.GenerateCells(AccountSet.Instance._PlayerAccountInfo.Stoneboxsize,1);
    }
    
    public void RefreshSelectedRender()
    {
        foreach(KeyValuePair<int,DragAndDropCell> KV in SkillStonesBox.CellsDictionary)
        {
            DragAndDropCell cell = KV.Value;
            if (cell.GetItem() == null)
            {
                cell._selected.SetActive(false);
                continue;
            }
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(cell.GetItem().SkillStoneOfPlayerId);
            if (skillStoneOfPlayerInfoModel != null)
            {
                if (selected.Contains(skillStoneOfPlayerInfoModel))
                {
                    cell._selected.SetActive(true);
                }else{
                    cell._selected.SetActive(false);
                }
            }
        }
    }
    
    void SelectForDelete(DragAndDropCell cell)
    {
        if (cell.GetItem() != null)
        {
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(cell.GetItem().SkillStoneOfPlayerId);
            if (skillStoneOfPlayerInfoModel != null)
            {
                if (selected.Contains(skillStoneOfPlayerInfoModel))
                {
                    RemoveStoneForDelete(cell);
                }else{
                    SelectStoneForDelete(cell);
                }
            }
        }
    }
    
    void SelectStoneForDelete(DragAndDropCell cell)
    {
        if (cell.GetItem() != null)
        {
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(cell.GetItem().SkillStoneOfPlayerId);
            if (skillStoneOfPlayerInfoModel != null)
            {
                selected.Add(skillStoneOfPlayerInfoModel);
                CurrentSelectedCount.text = "选中" + selected.Count + "个技能石";
                cell._selected.SetActive(true);
            }
        }
    }
    
    void RemoveStoneForDelete(DragAndDropCell cell)
    {
        if (cell.GetItem() != null)
        {
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(cell.GetItem().SkillStoneOfPlayerId);
            if (skillStoneOfPlayerInfoModel != null)
            {
                selected.Remove(skillStoneOfPlayerInfoModel);
                CurrentSelectedCount.text = "选中" + selected.Count + "个技能石";
                cell._selected.SetActive(false);
            }
        }
    }
}
