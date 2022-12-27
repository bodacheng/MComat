using System;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;
using DummyLayerSystem;
using ModelView;
using UnityEngine.UI;
using UniRx;

public class TeamEditLayer : UILayer
{
    [SerializeField] DedicatedCameraConnector connector;
    [SerializeField] Button removeButton;
    [SerializeField] HeroIcon team1Front, team1Left, team1Right;
    
    [Header("选中框")]
    [SerializeField] GameObject selectedFrame;
    
    [Header("选中角色的技能显示")]
    [SerializeField] NineForShow nineForShow;
    
    [Header("队伍保存")]
    [SerializeField] Button saveBtn;
    
    [Header("技能编辑按钮")]
    [SerializeField] Button skillEditButton;
    
    public ReactiveProperty<int> focusingPos = new(-1);
    readonly IDictionary<int, HeroIcon> _teamBtnDic = new Dictionary<int, HeroIcon>();
    private Func<string, bool> _teamLegal;
    private string _currentTeamMode;
    
    public void SetTeamLegalCheck(Func<string, bool> teamLegal)
    {
        this._teamLegal = teamLegal;
        SetConfirmBtnActive();
    }

    private void SetConfirmBtnActive()
    {
        saveBtn.interactable = _teamLegal(this._currentTeamMode);
    }
    
    /// <summary>
    /// unit box icon feature
    /// </summary>
    /// <param name="instanceID"></param>
    /// <param name="teamMode"></param>
    public void UnitIconClick(string instanceID, string teamMode)
    {
        var unitsLayer = UILayerLoader.Get<UnitsLayer>();
        if (unitsLayer == null) return;
        if (focusingPos.Value != -1)
        {
            ChangeTeamPos(instanceID, focusingPos.Value, teamMode);
            unitsLayer.Selected.Value = null;
        }
        else
        {
            unitsLayer.Selected.Value = instanceID;
        }
        
        PreScene.target.SetFocusingUnit(instanceID);
        nineForShow.ShowStones_Acc(instanceID);
        connector.ShowMyModel(instanceID);
    }
    
    /// <summary>
    /// Change target pos unit
    /// </summary>
    /// <param name="instanceID"></param>
    /// <param name="targetPos"></param>
    /// <param name="teamMode"></param>
    void ChangeTeamPos(string instanceID, int targetPos, string teamMode)
    {
        var unitInfo = dataAccess.Units.Get(instanceID);
        if (unitInfo != null && Stones.GetEquippingStones(instanceID).Count != 9)
        {
            focusingPos.Value = -1;
            Debug.Log("no enough skill");
            return;
        }
        
        var returns = TeamSet.GetTargetSet(teamMode).SetPosUnitByInstanceID(targetPos, instanceID);
        foreach (var t in returns)
        {
            ChangeIconOnPos(t.posNum, teamMode);
        }

        if (returns.Count > 0)
        {
            focusingPos.Value = -1;
        }

        SetConfirmBtnActive();
    }
    
    // 纯渲染函数
    void ChangeIconOnPos(int posNum, string teamMode)
    {
        if (_teamBtnDic.ContainsKey(posNum))
        {
            var icon = _teamBtnDic[posNum];
            var posInstanceID = TeamSet.GetTargetSet(teamMode).GetInstanceIdOnPos(posNum);
            var info = dataAccess.Units.Get(posInstanceID);
            icon.ChangeIcon(info);
            if (info != null && Stones.GetEquippingStones(posInstanceID).Count == 9)
            {
                icon.LightOn();
            }
            else
            {
                icon.Grey();
            }
        }
        else
        {
            Debug.Log("posNum not exists:" + posNum);
        }
    }
    
    public void Ini(string teamMode, Action save, Func<string, bool> teamLegal)
    {
        _currentTeamMode = teamMode;
        SetTeamLegalCheck(teamLegal);

        focusingPos.Subscribe((x) =>
        {
            var posInstanceID = TeamSet.GetTargetSet(teamMode).GetInstanceIdOnPos(x);
            removeButton.gameObject.SetActive(x != -1 && posInstanceID != null);
            switch (x)
            {
                case 0:
                    HeroIcon.SelectedFeature(team1Front, selectedFrame, 1f);
                    break;
                case 1:
                    HeroIcon.SelectedFeature(team1Left, selectedFrame, 1f);
                    break;
                case 2:
                    HeroIcon.SelectedFeature(team1Right, selectedFrame, 1f);
                    break;
                default:
                    HeroIcon.SelectedFeature(null, selectedFrame, 1f);
                    break;
            }
        }).AddTo(gameObject);
        
        _teamBtnDic.Clear();
        _teamBtnDic.Add(0, team1Front);
        _teamBtnDic.Add(1, team1Left);
        _teamBtnDic.Add(2, team1Right);
        
        // 适配队伍编辑器各个位置初始头像
        ChangeIconOnPos(0, teamMode);
        ChangeIconOnPos(1, teamMode);
        ChangeIconOnPos(2, teamMode);
        
        void SkillEdit()
        {
            if (PreScene.target.Focusing.id != null)
                PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillEdit);
        }
        skillEditButton.onClick.AddListener(SkillEdit);
        
        void Remove()
        {
            ChangeTeamPos(null, focusingPos.Value, teamMode);
        }
        removeButton.onClick.AddListener(Remove);
        
        void SetPos(int posNum)
        {
            var unitsLayer = UILayerLoader.Get<UnitsLayer>();
            if (unitsLayer == null) return;
            var selectedInstanceID = unitsLayer.Selected.Value;
            if (selectedInstanceID != null)
            {
                Remove();
                unitsLayer.Selected.Value = null;
                ChangeTeamPos(selectedInstanceID, posNum, teamMode);
            }
            else
            {
                focusingPos.Value = posNum;
                var instanceID = TeamSet.GetTargetSet(teamMode).GetInstanceIdOnPos(focusingPos.Value);
                PreScene.target.SetFocusingUnit(instanceID);
                connector.ShowMyModel(instanceID);
                if (PreScene.target.Focusing != null)
                    nineForShow.ShowStones_Acc(PreScene.target.Focusing.id);
                else
                {
                    // empty slot
                };
            }
        }
        
        team1Front.iconButton.onClick.AddListener(() => SetPos(0));
        team1Left.iconButton.onClick.AddListener(() => SetPos(1));
        team1Right.iconButton.onClick.AddListener(() => SetPos(2));
        
        saveBtn.onClick.AddListener(()=>save());
    }
}