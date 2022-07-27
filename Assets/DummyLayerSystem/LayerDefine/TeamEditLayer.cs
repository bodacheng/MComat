using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;
using DummyLayerSystem;
using ModelView;
using UnityEngine.UI;

public class TeamEditLayer : UILayer
{
    public DedicatedCameraConnector _connector;
    
    public Button RemoveButton;
    public HeroIcon team1front, team1left, team1right;
    
    [Space(7)]
    [Header("选中框")]
    public GameObject selectedFrame;
    
    [Space(7)]
    [Header("选中角色的技能显示")]
    public NineForShow _nineForShow;
    [Space(7)]
    [Header("技能编辑按钮")]
    public Button SkillEditButton;
    
    int focusingPos = -1;
    readonly IDictionary<int, HeroIcon> teamBtnDic = new Dictionary<int, HeroIcon>();
    
    public static TeamEditLayer Open(string teamMode)
    {
        var returnValue = UILayerLoader.Load(PreScene.target.T,"TeamEditLayer") as TeamEditLayer;
        returnValue.INI(teamMode);
        return returnValue;
    }

    public static void Close()
    {
        UILayerLoader.Remove("TeamEditLayer");
    }
    
    void CancelSelect()
    {
        focusingPos = -1;
        HeroIcon.SelectedFeature(null, selectedFrame, 1f);
    }

    public void UnitIconClick(string instanceID, string teamMode)
    {
        UnitsLayer unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
        if (focusingPos != -1)
        {
            ChangeTeamPos(instanceID, focusingPos, teamMode);
            CancelSelect();
            unitsLayer.CancelSelect();
        }
        else
        {
            unitsLayer.Select(instanceID);
        }
        
        PreScene.target.SetFocusingUnit(instanceID);//确立focusing角色
        _nineForShow.ShowStones_Acc(instanceID);

        _connector.ShowMyModel(instanceID);
    }
    
    // 修改对象队伍编程
    void ChangeTeamPos(string instanceID, int targetPos, string teamMode)
    {
        List<PosKeySet.OneSet> returns = TeamSet.GetTargetSet(teamMode).SetPosMemByMonsterOfPlayerID(targetPos, instanceID);
        for (int i = 0; i < returns.Count; i++)
        {
            ChangeIconOnPos(returns[i].posNum, teamMode);
        }
    }

    // 纯渲染函数
    void ChangeIconOnPos(int posNum, string teamMode)
    {
        if (teamBtnDic.ContainsKey(posNum))
        {
            HeroIcon tar = teamBtnDic[posNum];
            string Pos_instanceID = TeamSet.GetTargetSet(teamMode).GetInstanceIdOnPos(posNum);
            HeroIcon.ChangeHeroIconByInstanceId(Pos_instanceID, tar);
        }
        else
        {
            Debug.Log("逻辑冗余？posNum:" + posNum);
        }
    }

    #region 初始化（显示目前队伍编辑，加载按钮功能）
    void INI(string teamMode)
    {
        teamBtnDic.Clear();
        teamBtnDic.Add(0, team1front);
        teamBtnDic.Add(1, team1left);
        teamBtnDic.Add(2, team1right);
        
        // 适配队伍编辑器各个位置初始头像
        ChangeIconOnPos(0, teamMode);
        ChangeIconOnPos(1, teamMode);
        ChangeIconOnPos(2, teamMode);
        
        void SkillEdit()
        {
            if (PreScene.target._focusing.id != null)
                PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillEdit, true);
        }
        SkillEditButton.onClick.AddListener(SkillEdit);
        
        void Remove()
        {
            ChangeTeamPos(null, focusingPos, teamMode);
            CancelSelect();
        }
        RemoveButton.onClick.AddListener(Remove);
        
        void SetPos(int posNum)
        {
            UnitsLayer unitsLayer = UILayerLoader.Get("UnitsLayer") as UnitsLayer;
            string selected_instanceID = unitsLayer.GetSelect();
            if (selected_instanceID != null)
            {
                Remove();
                unitsLayer.CancelSelect();
                CancelSelect();
                ChangeTeamPos(selected_instanceID, posNum, teamMode);
            }
            else
            {
                focusingPos = posNum;
                switch (focusingPos)
                {
                    case 0:
                        HeroIcon.SelectedFeature(team1front, selectedFrame, 1f);
                        break;
                    case 1:
                        HeroIcon.SelectedFeature(team1left, selectedFrame, 1f);
                        break;
                    case 2:
                        HeroIcon.SelectedFeature(team1right, selectedFrame, 1f);
                        break;
                    default:
                        HeroIcon.SelectedFeature(null, selectedFrame, 1f);
                        break;
                }
                
                string instanceID = TeamSet.GetTargetSet(teamMode).GetInstanceIdOnPos(focusingPos);
                PreScene.target.SetFocusingUnit(instanceID);//确立focusing角色
                _connector.ShowMyModel(instanceID);
                if (PreScene.target._focusing != null)
                    _nineForShow.ShowStones_Acc(PreScene.target._focusing.id);
                else
                {
                    // empty slot
                };
            }
        }
        
        team1front.iconButton.onClick.AddListener(() =>{SetPos(0);});
        team1left.iconButton.onClick.AddListener(() =>{SetPos(1);});
        team1right.iconButton.onClick.AddListener(() =>{SetPos(2);});
    }
    #endregion
}