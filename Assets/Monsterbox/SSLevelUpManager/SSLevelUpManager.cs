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

    [SerializeField] StoneListLayer _stoneListLayer;

    public void INI()
    {
        cancelBtn.onClick.AddListener(CloseLevelUpPage);
        autoAdd.onClick.AddListener(() =>
        {
            var info = Stones.Get(_stoneListLayer.TargetStoneID);
            AutoAddMaterials(info.SkillId);
        });
        
        _materialSlots = new List<StoneCell>
        {
            cell1,
            cell2,
            cell3,
            cell4
        };
        
        foreach (var cell in _materialSlots)
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
            var target = Stones.Get(_stoneListLayer.TargetStoneID);
            if (item.instanceId != _stoneListLayer.TargetStoneID && item._SkillConfig.RECORD_ID == target.SkillId)
            {
                var m = Stones.Get(item.instanceId);
                if (m.Born == "true")
                {
                    PopupLayer.ArrangeWarnWindow("这个是被动技能，不能用作材料");
                    return;
                }
                if (m.UnitInstanceId != null)
                {
                    PopupLayer.ArrangeWarnWindow("このストーンは装備中です");
                    return;
                }
                
                StoneCell.Install(source, to);
            }
        }
    }
    
    /// <summary>
    /// 技能石升级画面更新。
    /// </summary>
    public void RefreshSkillLevelUpModule(string instanceId)
    {
        confirmLevelUp.gameObject.SetActive(false);
        if (instanceId == null)
        {
            return;
        }
        var target = Stones.Get(instanceId);
        foreach (var cell in _materialSlots)
        {
            cell.UpdateMyItem();
        }
        foreach (var slot in _materialSlots)
        {
            if (slot.GetItem() == null)
                return; // 材料槽满的时候才可能弹出确认按钮
        }
        
        confirmLevelUp.gameObject.SetActive(true);
        var needGD = target.Level * 10 + 100;
        gdCount.text = needGD.ToString();
        void Confirm()
        {
            if (Currencies.CoinCount < needGD)
            {
                PopupLayer.ArrangeWarnWindow("ゴールドが足りない");
                return;
            }
            
            PopupLayer.ArrangeConfirmWindow(
                ()=>
                {
                    LevelUpStone(instanceId, x =>
                    {
                        // 具体待定。但不应该是RefreshSkillLevelUpModule，这个在CloseLevelUpPage会跑一次才对
                    });
                }, 
                "技ストーンを強化しますか ?");
        }
        confirmLevelUp.onClick.RemoveAllListeners();
        confirmLevelUp.onClick.AddListener(Confirm);
    }
}