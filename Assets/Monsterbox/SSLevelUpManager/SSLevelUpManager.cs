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
    [SerializeField] Text gdCount;
    
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
            var info = Stones.Get(targetInstanceID);
            AutoAddMaterials(info.SkillId);
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
            cell.SetOnDropAction(MSlotOnDropAction);
        }
    }
    
    void MSlotOnDropAction(StoneCell source, StoneCell to)
    {
        if (SKStoneItem.dragging != null)
        {
            var item = SKStoneItem.draggedItem;
            if (item == null)
                return;
            var target = Stones.Get(targetInstanceID);
            
            if (item.instanceId != targetInstanceID && item._SkillConfig.RECORD_ID == target.SkillId)
            {
                var m = Stones.Get(item.instanceId);
                if (m.Born == "true")
                {
                    PopupLayer.ArrangeWarnWindow(PreScene.target.T, "这个是被动技能，不能用作材料");
                    return;
                }
                if (m.UnitInstanceId != null)
                {
                    PopupLayer.ArrangeWarnWindow(PreScene.target.T,"有角色正在使用，不能用作材料");
                    return;
                }
                
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
        if (targetInstanceID == null)
        {
            return;
        }
        var target = Stones.Get(targetInstanceID);
        foreach (var slot in MaterialSlots)
        {
            if (slot.GetItem() == null)
                return; // 材料槽满的时候才可能弹出确认按钮
        }
        
        confirmLevelUp.gameObject.SetActive(true);
        int needGD = target.Level * 10 + 100;
        gdCount.text = needGD.ToString();
        if (Currencies.CoinCount < needGD)
        {
            confirmLevelUp.interactable = false;
            return; // 所需金币不够
        }
        confirmLevelUp.interactable = true;
        void Confirm()
        {
            PopupLayer.ArrangeConfirmWindow(
                PreScene.target.T,
                ()=>
                {
                    ConfirmSkillStoneLevelUp(x=> RefreshSkillLevelUpModule());
                }, 
                "确实要升级技能石？");
        }
        confirmLevelUp.onClick.RemoveAllListeners();
        confirmLevelUp.onClick.AddListener(Confirm);
    }
}