using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;

public partial class SSLevelUpManager : MonoBehaviour
{
    [SerializeField] Button cancelBtn;
    [SerializeField] Button autoAdd;
    [SerializeField] Button confirmLevelUp;
    
    [Header("升级对象技能石参数")]
    [SerializeField] SkillStoneDetail focusingSSD;
    
    [Header("融合技能槽")]
    [SerializeField] StoneCell cell1;
    [SerializeField] StoneCell cell2;
    [SerializeField] StoneCell cell3;
    [SerializeField] StoneCell cell4;

    public void INI()
    {
        cancelBtn.onClick.AddListener(CloseLevelUpPage);
        autoAdd.onClick.AddListener(() =>
        {
            var info = Stones.Get(targetStoneID);
            AutoAddMaterials(info.skillId);
        });
        
        MaterialSlots = new List<StoneCell>
        {
            cell1,
            cell2,
            cell3,
            cell4
        };
        
        foreach (var cell in MaterialSlots)
        {
            cell.SetOnDropAction(OnDropAction);
        }
    }
    
    void OnDropAction(StoneCell source, StoneCell to)
    {
        if (SKStoneItem.dragging != null)
        {
            var item = SKStoneItem.draggedItem;
            if (item == null)
                return;
            var target = Stones.Get(targetStoneID);
            if (item.instanceId != targetStoneID && item._SkillConfig.RECORD_ID == target.skillId)
            {
                StoneCell.Install(source, to);
            }
        }
    }
    
    /// <summary>
    /// 技能石升级画面更新。
    /// </summary>
    public void RefreshSkillLevelUpModule()
    {
        confirmLevelUp.gameObject.SetActive(false);
        if (targetStoneID == null)
        {
            return;
        }
        var target = Stones.Get(targetStoneID);
        foreach (var slot in MaterialSlots)
        {
            if (slot.GetItem() == null)
                return; // 材料槽满的时候才可能弹出确认按钮
        }
        
        void Confirm()
        {
            var popupLayer = PopupLayer.Open(PreScene.target.T);
            popupLayer.ArrangeConfirmWindow(ConfirmSkillStoneLevelUp, "确实要升级技能石？");
        }
        confirmLevelUp.onClick.RemoveAllListeners();
        confirmLevelUp.onClick.AddListener(Confirm);
        confirmLevelUp.gameObject.SetActive(true);
    }
}