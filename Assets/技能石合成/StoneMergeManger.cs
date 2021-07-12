using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using mainMenu;
using Api.Dto.Form;
using System.Collections;
using dataAccess;
using Api.Common;
using Api.Dto.Model;

public class StoneMergeManger : MonoBehaviour
{
    [Space(7)]
    [Header("对应画布")]
    public Canvas _Canvas;
    
    [Space(7)]
    [Header("融合技能槽")]
    public StoneCell cell1;
    public StoneCell cell2;
    public StoneCell cell3;
    public StoneCell cell4;
    public StoneCell cell5;
    List<StoneCell> MaterialSlots;
    public static StoneMergeManger target;

    void Awake()
    {
        target = this;
        MaterialSlots = new List<StoneCell>
        {
            cell1,
            cell2,
            cell3,
            cell4,
            cell5
        };

        AddMSlotBehaviour(cell1);
        AddMSlotBehaviour(cell2);
        AddMSlotBehaviour(cell3);
        AddMSlotBehaviour(cell4);
        AddMSlotBehaviour(cell5);
    }

    #region 材料槽功能加载
    public void AddMSlotBehaviour(StoneCell cell)
    {
        Button button = cell.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(delegate { StoneCell.SeletedRender(cell, SkillStonesBox._Selected); });
        }
    }
    #endregion

    #region 素材的添加与移除
    public void AddMaterial(StoneCell skillboxcell)
    {
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            MaterialSlots[i].UpdateMyItem();
            SKStoneItem Material = skillboxcell.GetItem();
            if (MaterialSlots[i].GetItem() == null && Material != null)
            {
                StoneCell.Install(skillboxcell, MaterialSlots[i]);
                break;
            }
        }
    }

    public void ReturnAllMaterialsToBox()
    {
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            MaterialSlots[i].UpdateMyItem();
            if (MaterialSlots[i].GetItem() != null)
            {
                MaterialSlots[i].ReturnStoneToBox();
            }
        }
    }
    #endregion

    #region Merger Process
    public void Confirm()
    {
        void run()
        {
            //
        }
        LoadingCanvas.target.ArrangeConfirmWindow(run, "确实要融合技能石？");
    }
    #endregion
}
