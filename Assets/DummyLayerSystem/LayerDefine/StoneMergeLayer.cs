using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using mainMenu;
using UnityEngine.EventSystems;

public class StoneMergeLayer : UILayer
{
    public SkillStonesBox stoneBox;
    public Camera fxCamera;
    public SkillStoneDetail _skillStoneDetail;
    
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
    void AddMSlotBehaviour(StoneCell _Cell)
    {
        Button button = _Cell.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(delegate { StoneCell.SeletedRender(_Cell, SkillStonesBox._Selected); });
        }
        _Cell.SetOnDropAction(OnDropAction);
    }
    #endregion

    #region 素材的添加与移除
    void AddMaterial(StoneCell @from)
    {
        for (int i = 0; i < MaterialSlots.Count; i++)
        {
            MaterialSlots[i].UpdateMyItem();
            SKStoneItem Material = @from.GetItem();
            if (MaterialSlots[i].GetItem() == null && Material != null)
            {
                StoneCell.Install(@from, MaterialSlots[i]);
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
                stoneBox.ReturnStoneToBox(MaterialSlots[i].GetItem());
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
    
    public void CellFeature_MergeMode(StoneCell _Cell)
    {
        StoneMergeLayer stoneMergeLayer = Open();
        
        void buttonFeature(object sender, System.EventArgs e)
        {
            StoneCell.SeletedRender(_Cell, SkillStonesBox._Selected);
        }

        void doubleClick(object sender, System.EventArgs e)
        {
            stoneMergeLayer.AddMaterial(_Cell);
        }
        _Cell.pGesture.Pressed += buttonFeature;
        _Cell.tGesture.Tapped += doubleClick;
        
        _Cell.SetOnDropAction(OnDropAction);
    }
    
    void OnDropAction(StoneCell source, StoneCell to)
    {
        StoneCell.Install(source, to);
    }
}
