using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public partial class SSLevelUpManager : MonoBehaviour
{
    [SerializeField] private Button cancelBtn;
    [SerializeField] Button confirmLevelUp;
    
    [Header("目前各种参数显示")]
    [SerializeField] Slider expValue;
    [SerializeField] Text StoneTargetLevel;
    [SerializeField] Text CurrentExpToNextLevel;
    
    [Header("升级对象技能石参数")]
    [SerializeField] SkillStoneDetail focusingSSD;
    
    [Header("融合技能槽")]
    [SerializeField] StoneCell cell1;
    [SerializeField] StoneCell cell2;
    [SerializeField] StoneCell cell3;
    [SerializeField] StoneCell cell4;
    [SerializeField] StoneCell cell5;
    
    public void INI()
    {
        cancelBtn.onClick.AddListener(CloseLevelUpPage);
        //target = this;
        MaterialSlots = new List<StoneCell>
        {
            cell1,
            cell2,
            cell3,
            cell4,
            cell5
        };
        
        foreach (var cell in MaterialSlots)
        {
            cell.SetOnDropAction(OnDropAction);
        }
    }
    
    int targetExp;
    int TargetExp
    {
        set
        {
            LevelExpConfig.Current before = LevelExpConfig.GetCurrentInfo(targetExp);
            LevelExpConfig.Current after = LevelExpConfig.GetCurrentInfo(value);
            DOTween.To(() => DataForShow, x => DataForShow = x, value, 0.6f);
            targetExp = value;
        }
    }
    
    int dataForShow;
    float DataForShow
    {
        get => dataForShow;
        set
        {
            LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo((int)value);
            expValue.value = (float)current.expRemain / (float)(current.expRemain + current.expToNextLevel);
            if (expValue.value >= 1)
                expValue.value = 0;
            StoneTargetLevel.text = "Level:" + current.currentLevel.ToString();
            StoneTargetLevel.color = CalCurrentExpFromMaterials() > 0 ? new Color(0, 1, 1) : new Color(1, 1, 1);
            CurrentExpToNextLevel.text = "( " + (expValue.value * 100).ToString() + "% )";
            dataForShow = (int)value;
        }
    }
    
    void OnDropAction(StoneCell source, StoneCell to)
    {
        if (SKStoneItem.dragging != null)
        {
            SKStoneItem item = SKStoneItem.draggedItem;
            if (item == null)
                return;
            StoneOfPlayerInfo target = Stones.Get(targetStoneID);
            Debug.Log(targetStoneID + ":"+ target);
            if (item.instanceId != targetStoneID && item._SkillConfig.RECORD_ID == target.skillId)
            {
                StoneCell.Install(source, to);
            }
        }
    }
    
    // 清除显示
    void Clear()
    {
        StoneTargetLevel.text = "";
        CurrentExpToNextLevel.text = "";
        if (expValue != null)
        {
            expValue.value = 0;
            expValue.gameObject.SetActive(false);
        }
        confirmLevelUp.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// 技能石升级画面更新。每调整一次目标等级画面都要随之更新
    /// </summary>
    public void RefreshSkillLevelUpModule()
    {
        if (targetStoneID == null)
        {
            Clear();
            return;
        }
        
        StoneOfPlayerInfo target = Stones.Get(targetStoneID);
        TargetExp = CalCurrentExpFromMaterials() + target.EXP;
        
        if (CalCurrentExpFromMaterials() > 0)
        {
            void Confirm()
            {
                PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
                popupLayer.ArrangeConfirmWindow(ConfirmSkillStoneLevelUp, "确实要升级技能石？");
            }
            confirmLevelUp.onClick.RemoveAllListeners();
            confirmLevelUp.onClick.AddListener(Confirm);
            confirmLevelUp.gameObject.SetActive(true);
        }else{
            confirmLevelUp.gameObject.SetActive(false);
        }
    }
}