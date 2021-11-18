using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using mainMenu;
using UnityEngine.EventSystems;

public class StoneMergeLayer : UILayer
{
    public SkillStonesBox stoneBox;
    public Camera fxCamera;
    
    [Space(7)]
    [Header("融合技能槽")]
    public StoneCell cell1;
    public StoneCell cell2;
    public StoneCell cell3;
    public StoneCell cell4;
    public StoneCell cell5;
    
    List<StoneCell> MaterialSlots;
    
    public static StoneMergeLayer Open()
    {
        return UILayerLoader.Load(PreScene.target.T,"StoneMergeLayer") as StoneMergeLayer;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("StoneMergeLayer");
    }
    
    void Awake()
    {
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
        PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
        popupLayer.ArrangeConfirmWindow(run, "确实要融合技能石？");
    }
    #endregion

    private float lastclicktime;
    public void CellButtonBeheviour_StoneMergeMode(StoneCell _SkillStoneCell)
    {
        StoneMergeLayer stoneMergeLayer = StoneMergeLayer.Open();
        Button button = _SkillStoneCell.GetComponent<Button>();
        if (button != null)
        {
            EventTrigger trigger = button.GetComponent<EventTrigger>();
            trigger.triggers.Clear();
                
            void buttonFeature()
            {
                if (Time.time - lastclicktime < 0.25f) // double click
                {
                    stoneMergeLayer.AddMaterial(_SkillStoneCell);
                }
                lastclicktime = Time.time;
            }
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(buttonFeature);
            button.onClick.AddListener(delegate { StoneCell.SeletedRender(_SkillStoneCell, SkillStonesBox._Selected); });
            stoneMergeLayer.AddMSlotBehaviour(_SkillStoneCell);
        }
    }
}
