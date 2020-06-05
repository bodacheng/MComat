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

    public int focusingPosNum = -1;
    readonly IDictionary<int, HeroIcon> teamButtonDic = new Dictionary<int, HeroIcon>();
        
    #region MonsterBoxIconFeature 必须在monsterbox生成所有角色头像之后执行
    public void AddHeroIconFeaturesToMonsterBox()
    {
        foreach (KeyValuePair<string, HeroIcon> keyValuePair in MonsterBox.mainMenuIcons)
        {
            AddHeroIconFeatureToMonsterBox(keyValuePair.Key,keyValuePair.Value.iconButton);
        }
    }
    
    void AddHeroIconFeatureToMonsterBox(string CharRecordId, Button targetButton)
    {
        IEnumerator MonsterIconButton()
        {
            yield return MemberDetail.target.SetMemberDetailFocusingChar(CharRecordId);//确立focusing角色
            yield return ChangeTeamPos(MemberDetail.target._focusing.monsterOfPlayerId, focusingPosNum);
            yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
        }
        void Trigger()
        {
            PreScene.target.mainProcessRunner.Run(MonsterIconButton());
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
            }
            PreScene.target.mainProcessRunner.Run(RemoveSelected());
        }
        RemoveButton.onClick.AddListener(Remove);

        team1front.iconButton.onClick.RemoveAllListeners();
        void pos1F()
        {
            IEnumerator setPosF()
            {
                focusingPosNum = 0;
                yield return MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.GetTargetSet().GetMonsterOfPlayerIdOnPos(focusingPosNum));//确立focusing角色
                HeroIcon.Seletedfeature(team1front, selectedFrame, 200f);
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
            PreScene.target.mainProcessRunner.Run(setPosF());
        }
        team1front.iconButton.onClick.AddListener(pos1F);
        
        team1left.iconButton.onClick.RemoveAllListeners();
        void pos1L()
        {
            IEnumerator setPosL()
            {
                focusingPosNum = 1;
                yield return MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.GetTargetSet().GetMonsterOfPlayerIdOnPos(focusingPosNum));//确立focusing角色
                HeroIcon.Seletedfeature(team1left, selectedFrame, 200f);
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
            PreScene.target.mainProcessRunner.Run(setPosL());
        }
        team1left.iconButton.onClick.AddListener(pos1L);
        
        team1right.iconButton.onClick.RemoveAllListeners();
        void pos1R()
        {
            IEnumerator setPosR()
            {
                focusingPosNum = 2;
                yield return MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.GetTargetSet().GetMonsterOfPlayerIdOnPos(focusingPosNum));//确立focusing角色
                HeroIcon.Seletedfeature(team1right, selectedFrame, 200f);
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
            PreScene.target.mainProcessRunner.Run(setPosR());
        }
        team1right.iconButton.onClick.AddListener(pos1R);
    }
    #endregion
}