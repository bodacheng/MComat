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
            if (MemberDetail.target._focusing.monsterOfPlayerId != null)
                PreScene.target.trySwitchToStep(MainSceneStep.MemberDetail_edit, true);
        }
        SkillEditButton.onClick.AddListener(SkillEdit);
    }

    #region MonsterBoxIconFeature 必须在monsterbox生成所有角色头像之后执行
    public void AddHeroIconFeaturesToMonsterBox()
    {
        foreach (KeyValuePair<string, HeroIcon> keyValuePair in MonsterBox.mainMenuIcons)
        {
            AddHeroIconFeatureToMonsterBox(keyValuePair.Key,keyValuePair.Value.iconButton);
        }
    }

    void CancelSelect()
    {
        focusingPosNum = -1;
        HeroIcon.Seletedfeature(null, selectedFrame, 200f);
    }

    IEnumerator MonsterIconButton(string CharAccId)
    {
        if (focusingPosNum != -1)
        {
            yield return ChangeTeamPos(CharAccId, focusingPosNum);
            CancelSelect();
            MonsterBox.target.CancelSelect();
        }
        else
        {
            MonsterBox.target.Select(CharAccId);
        }

        yield return MemberDetail.target.SetMemberDetailFocusingChar(CharAccId);//确立focusing角色
        // mini nineslot show
        _nineForShow.ShowStones_Acc(CharAccId);
        yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
    }

    void AddHeroIconFeatureToMonsterBox(string CharAccId, Button targetButton)
    {
        void Trigger()
        {
            PreScene.target.mainProcessRunner.Run(MonsterIconButton(CharAccId));
        }
        targetButton.onClick.AddListener(Trigger);
    }
    #endregion
    
    // 修改对象队伍编程
    public IEnumerator ChangeTeamPos(string monsterlocalID,int targetPos)
    {
        List<PosNumWithLocalKey> returns = TeamSet.GetTargetSet().SetPosMemByMonsterOfPlayerID(targetPos, monsterlocalID);
        for (int i = 0; i < returns.Count;i++)
        {
            yield return ChangeIconOnPos(returns[i].posNum);
        }
    }
        
    // 纯渲染函数
    IEnumerator ChangeIconOnPos(int posNum)
    {
        if (teamButtonDic.ContainsKey(posNum))
        {
            HeroIcon tar = teamButtonDic[posNum];
            string PosMonsterOfPlayerId = TeamSet.GetTargetSet().GetMonsterOfPlayerIdOnPos(posNum);
            yield return HeroIcon.ChangeHeroIconByMonsterOfPlayerId(PosMonsterOfPlayerId, tar);
        }
        else
        {
            Debug.Log("逻辑冗余？posNum:" + posNum);
        }
    }

    #region 初始化（显示目前队伍编辑，加载按钮功能）
    public IEnumerator INITeamPosButtons()
    {
        teamButtonDic.Clear();
        teamButtonDic.Add(0, team1front);
        teamButtonDic.Add(1, team1left);
        teamButtonDic.Add(2, team1right);
        
        // 适配队伍编辑器各个位置初始头像
        yield return ChangeIconOnPos(0);
        yield return ChangeIconOnPos(1);
        yield return ChangeIconOnPos(2);
        
        RemoveButton.onClick.RemoveAllListeners();
        void Remove()
        {
            IEnumerator RemoveSelected()
            {
                yield return ChangeTeamPos(null, focusingPosNum);
                CancelSelect();
            }
            PreScene.target.mainProcessRunner.Run(RemoveSelected());
        }
        RemoveButton.onClick.AddListener(Remove);

        team1front.iconButton.onClick.RemoveAllListeners();

        IEnumerator setPos(int posNum)
        {
            if (MonsterBox.selectingAccID != null)
            {
                yield return ChangeTeamPos(MonsterBox.selectingAccID, posNum);
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
                yield return MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.GetTargetSet().GetMonsterOfPlayerIdOnPos(focusingPosNum));//确立focusing角色
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
                if (MemberDetail.target._focusing != null)
                    _nineForShow.ShowStones_Acc(MemberDetail.target._focusing.monsterOfPlayerId);
                else
                {
                    // empty slot
                };
            }
        }

        void pos1F()
        {
            PreScene.target.mainProcessRunner.Run(setPos(0));
        }
        team1front.iconButton.onClick.AddListener(pos1F);
        
        team1left.iconButton.onClick.RemoveAllListeners();
        void pos1L()
        {
            PreScene.target.mainProcessRunner.Run(setPos(1));
        }
        team1left.iconButton.onClick.AddListener(pos1L);
        
        team1right.iconButton.onClick.RemoveAllListeners();
        void pos1R()
        {
            PreScene.target.mainProcessRunner.Run(setPos(2));
        }
        team1right.iconButton.onClick.AddListener(pos1R);
    }
    #endregion
}