using UnityEngine;
using mainMenu;
using UnityEngine.UI;
using Api.Dto.Model;
using System.Collections.Generic;
using dataAccess;

public class StoneDeleteManger : MonoBehaviour
{
    public Text CurrentSelectedCount;
    public RectTransform SkillInfoT, SelectionInfoT, SelectionConfirmT;
    public SkillStonesBox SkillStonesBox;
    public Button EnterDeleteModeButton;
    readonly List<SkillStoneOfPlayerInfoModel> selectedForDelete = new List<SkillStoneOfPlayerInfoModel>();

    public static StoneDeleteManger target;
    
    void Awake()
    {
        target = this;
    }

    public void EnterDeleteMode()
    {
        SkillStonesBox._Selected.SetActive(false);
        foreach (KeyValuePair<int, StoneCell> KV in SkillStonesBox.CellsDictionary)
        {
            KV.Value._SelectMode = StoneCell.SelectMode.multi;
            Button button = KV.Value.GetComponent<Button>();
            button.onClick.AddListener(delegate { SelectForDelete(KV.Value); });
        }
        
        SkillInfoT.gameObject.SetActive(false);
        SelectionInfoT.gameObject.SetActive(true);
        SelectionConfirmT.gameObject.SetActive(true);
        EnterDeleteModeButton.gameObject.SetActive(false);
    }

    public void ExitDeleteMode()
    {
        SkillInfoT.gameObject.SetActive(true);
        SelectionInfoT.gameObject.SetActive(false);
        EnterDeleteModeButton.gameObject.SetActive(true);
        SelectionConfirmT.gameObject.SetActive(false);
        CurrentSelectedCount.text = "";
        SkillStonesBox.GenerateCells(AccountSet._AccInfo.Stoneboxsize, 1);
    }
    
    // 按钮函数
    public void ClearSelect()
    {
        selectedForDelete.Clear();
        RefreshSelectedRender();
    }
    
    // 显示正选择中的技能石
    public void RefreshSelectedRender()
    {
        foreach(KeyValuePair<int, StoneCell> KV in SkillStonesBox.CellsDictionary)
        {
            StoneCell cell = KV.Value;
            if (cell.GetItem() == null)
            {
                cell._selected.SetActive(false);
                continue;
            }
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Get(cell.GetItem().SkillStoneOfPlayerId);
            if (skillStoneOfPlayerInfoModel != null)
            {
                if (selectedForDelete.Contains(skillStoneOfPlayerInfoModel))
                {
                    cell._selected.SetActive(true);
                }else{
                    cell._selected.SetActive(false);
                }
            }
        }
    }
    
    // 在集体删除技能石多选模式下单击技能石格。未选中时点击为选中，选中时点击则取消
    void SelectForDelete(StoneCell cell)
    {
        if (cell.GetItem() != null)
        {
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Get(cell.GetItem().SkillStoneOfPlayerId);
            if (skillStoneOfPlayerInfoModel != null)
            {
                if (selectedForDelete.Contains(skillStoneOfPlayerInfoModel))
                {
                    RemoveStoneForDelete(cell);
                }else{
                    SelectStoneForDelete(cell);
                }
            }
        }
    }
    
    // 选择以集体删除
    void SelectStoneForDelete(StoneCell cell)
    {
        if (cell.GetItem() != null)
        {
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Get(cell.GetItem().SkillStoneOfPlayerId);
            if (skillStoneOfPlayerInfoModel != null)
            {
                selectedForDelete.Add(skillStoneOfPlayerInfoModel);
                CurrentSelectedCount.text = "选中" + selectedForDelete.Count + "个技能石";
                cell._selected.SetActive(true);
            }
        }
    }
    
    // 取消选择
    void RemoveStoneForDelete(StoneCell cell)
    {
        if (cell.GetItem() != null)
        {
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Get(cell.GetItem().SkillStoneOfPlayerId);
            if (skillStoneOfPlayerInfoModel != null)
            {
                selectedForDelete.Remove(skillStoneOfPlayerInfoModel);
                CurrentSelectedCount.text = "选中" + selectedForDelete.Count + "个技能石";
                cell._selected.SetActive(false);
            }
        }
    }
}