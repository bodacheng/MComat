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
            PreScene.target.mainProcessRunner.RunAsQueued(SubmitMergeRequest());
        }
        LoadingCanvas.target.ArrangeConfirmWindow(run, "确实要融合技能石？");
    }
    
    public IEnumerator SubmitMergeRequest()
    {
        SkillStoneMergeForm skillStoneLevelUpForm = new SkillStoneMergeForm();
        
        SKStoneItem item1 = cell1.GetItem();
        SKStoneItem item2 = cell2.GetItem();
        SKStoneItem item3 = cell3.GetItem();
        SKStoneItem item4 = cell4.GetItem();
        SKStoneItem item5 = cell5.GetItem();
        
        skillStoneLevelUpForm.M1Stone = item1 != null ? item1.equipingId : null;
        skillStoneLevelUpForm.M2Stone = item2 != null ? item2.equipingId : null;
        skillStoneLevelUpForm.M3Stone = item3 != null ? item3.equipingId : null;
        skillStoneLevelUpForm.M4Stone = item4 != null ? item4.equipingId : null;
        skillStoneLevelUpForm.M5Stone = item5 != null ? item5.equipingId : null;
                
        yield return Merge(skillStoneLevelUpForm,
             model =>
             {
                if (skillStoneLevelUpForm.M1Stone != null)
                    MySkillStones.RemoveStoneLocal(skillStoneLevelUpForm.M1Stone);
                if (skillStoneLevelUpForm.M2Stone != null)
                    MySkillStones.RemoveStoneLocal(skillStoneLevelUpForm.M2Stone);
                if (skillStoneLevelUpForm.M3Stone != null)
                    MySkillStones.RemoveStoneLocal(skillStoneLevelUpForm.M3Stone);
                if (skillStoneLevelUpForm.M4Stone != null)
                    MySkillStones.RemoveStoneLocal(skillStoneLevelUpForm.M4Stone);
                if (skillStoneLevelUpForm.M5Stone != null)
                    MySkillStones.RemoveStoneLocal(skillStoneLevelUpForm.M5Stone);
                    
                MySkillStones.Add(model.stone);
             },
             model => {
             
             }
             , Setting.Language
        );
    }
    
    // 技能石升级
    // 该操作仍余留一个很大的问题：选择的技能石为装备中的情况。如何避免点数失衡
    public IEnumerator Merge(SkillStoneMergeForm form, SuccessDelegate<GetMergedStoneModel> success, FailDelegate<GetMergedStoneModel> fail, ApiLanguage apiLanguage)
    {
        switch (Account.ReferenceMode)
        {
            case PlayerInfoRefMode.localTestSaveData:
                bool succeed = false;
                if (succeed)
                {
                    success(null);
                }else{
                    fail(null);
                }
            break;
            case PlayerInfoRefMode.remoteTestPlayer:
                yield return ApiCaller.Instance.Post<GetMergedStoneModel, SkillStoneMergeForm>("目前地址未定", form, ApiCaller.Instance.getHeader(apiLanguage), 
                    model => {
                        success(model.data);
                    },
                    model => {
                        fail(model.data);
                    }
                );
            break;
            case PlayerInfoRefMode.formalVersion:
            break;
        }
        yield break;
    }
    #endregion
}
