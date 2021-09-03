using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;
using UnityEngine.UI;

public class TeamEditManager : MonoBehaviour
{
    public Button StartToTeamEditButton;
    public Button RemoveButton;
    public HeroIcon team1front, team1left, team1right; // 可能允许部分为null。。。来对应不同人数制的队伍编辑
    
    [Space(7)]
    [Header("选中框")]
    public GameObject selectedFrame;
    
    [Space(7)]
    [Header("选中角色的技能显示")]
    public NineForShow _nineForShow;
    [Space(7)]
    [Header("技能编辑按钮")]
    public Button SkillEditButton;

    public int focusingPosNum = -1;
    readonly IDictionary<int, HeroIcon> teamButtonDic = new Dictionary<int, HeroIcon>();

    void Awake()
    {
        // edit按钮功能加载
        SkillEditButton.onClick.RemoveAllListeners();
        void SkillEdit()
        {
            if (MemberDetail.target._focusing.id != null)
                PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillEdit, true);
        }
        SkillEditButton.onClick.AddListener(SkillEdit);
    }

    #region MonsterBoxIconFeature 必须在monsterbox生成所有角色头像之后执行
    public void AddHeroIconFeaturesToMonsterBox(string teamMode)
    {
        foreach (KeyValuePair<string, HeroIcon> keyValuePair in MonsterBox.mainMenuIcons)
        {
            AddHeroIconFeatureToMonsterBox(keyValuePair.Key, teamMode, keyValuePair.Value.iconButton);
        }
    }

    void CancelSelect()
    {
        focusingPosNum = -1;
        HeroIcon.Seletedfeature(null, selectedFrame, 200f);
    }

    void MonsterIconButton(string CharAccId, string teammode)
    {
        if (focusingPosNum != -1)
        {
            ChangeTeamPos(CharAccId, focusingPosNum, teammode);
            CancelSelect();
            MonsterBox.target.CancelSelect();
        }
        else
        {
            MonsterBox.target.Select(CharAccId);
        }

        MemberDetail.target.SetMemberDetailFocusingChar(CharAccId);//确立focusing角色
        // mini nineslot show
        _nineForShow.ShowStones_Acc(CharAccId);
        MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
    }

    void AddHeroIconFeatureToMonsterBox(string CharAccId, string teammode, Button targetButton)
    {
        void Trigger()
        {
            MonsterIconButton(CharAccId, teammode);
        }
        targetButton.onClick.AddListener(Trigger);
    }
    #endregion
    
    // 修改对象队伍编程
    public void ChangeTeamPos(string instanceID, int targetPos, string teammode)
    {
        List<PosKeySet.OneSet> returns = TeamSet.GetTargetSet(teammode).SetPosMemByMonsterOfPlayerID(targetPos, instanceID);
        for (int i = 0; i < returns.Count;i++)
        {
            ChangeIconOnPos(returns[i].posNum, teammode);
        }
    }

    // 纯渲染函数
    void ChangeIconOnPos(int posNum, string teammode)
    {
        if (teamButtonDic.ContainsKey(posNum))
        {
            HeroIcon tar = teamButtonDic[posNum];
            string PosMonsterOfPlayerId = TeamSet.GetTargetSet(teammode).GetMonsterOfPlayerIdOnPos(posNum);
            HeroIcon.ChangeHeroIconByInstanceId(PosMonsterOfPlayerId, tar);
        }
        else
        {
            Debug.Log("逻辑冗余？posNum:" + posNum);
        }
    }

    #region 初始化（显示目前队伍编辑，加载按钮功能）
    public void INITeamPosButtons(string teammode)
    {
        teamButtonDic.Clear();
        teamButtonDic.Add(0, team1front);
        teamButtonDic.Add(1, team1left);
        teamButtonDic.Add(2, team1right);
        
        // 适配队伍编辑器各个位置初始头像
        ChangeIconOnPos(0, teammode);
        ChangeIconOnPos(1, teammode);
        ChangeIconOnPos(2, teammode);
        
        RemoveButton.onClick.RemoveAllListeners();
        void Remove()
        {
            ChangeTeamPos(null, focusingPosNum, teammode);
            CancelSelect();
        }
        RemoveButton.onClick.AddListener(Remove);

        
        void SetPos(int posNum)
        {
            if (MonsterBox.selectingAccID != null)
            {
                Remove();
                MonsterBox.target.CancelSelect();
                CancelSelect();
            }
            else
            {
                focusingPosNum = posNum;
                switch (focusingPosNum)
                {
                    case 0:
                        HeroIcon.Seletedfeature(team1front, selectedFrame, 200f);
                        break;
                    case 1:
                        HeroIcon.Seletedfeature(team1left, selectedFrame, 200f);
                        break;
                    case 2:
                        HeroIcon.Seletedfeature(team1right, selectedFrame, 200f);
                        break;
                    default:
                        HeroIcon.Seletedfeature(null, selectedFrame, 200f);
                        break;
                }
                MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.GetTargetSet(teammode).GetMonsterOfPlayerIdOnPos(focusingPosNum));//确立focusing角色
                MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
                if (MemberDetail.target._focusing != null)
                    _nineForShow.ShowStones_Acc(MemberDetail.target._focusing.id);
                else
                {
                    // empty slot
                };
            }
        }

        team1front.iconButton.onClick.RemoveAllListeners();
        void pos1F()
        {
            SetPos(0);
        }
        team1front.iconButton.onClick.AddListener(pos1F);
        
        team1left.iconButton.onClick.RemoveAllListeners();
        void pos1L()
        {
            SetPos(1);
        }
        team1left.iconButton.onClick.AddListener(pos1L);
        
        team1right.iconButton.onClick.RemoveAllListeners();
        void pos1R()
        {
            SetPos(2);
        }
        team1right.iconButton.onClick.AddListener(pos1R);
    }
    #endregion
}