using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using dataAccess;
using UnityEngine.UI;

// 一个TeamEditManager应该只能应用到一组队伍编辑上。
public class TeamEditManager : MonoBehaviour
{
    public Button RemoveButton;
    public HeroIcon team1back, team1front, team1left, team1right; // 可能允许部分为null。。。来对应不同人数制的队伍编辑

    [Space(7)]
    [Header("对象队伍")]
    public TeamSetGameMode TeamSetGameMode;
    
    [Space(7)]
    [Header("选中框")]
    public GameObject selectedFrame;
    
    public static int focusingPosNum = -1;
    readonly IDictionary<int, HeroIcon> teamButtonDic = new Dictionary<int, HeroIcon>();

    PosKeySet target;

    void Awake()
    {
        switch(TeamSetGameMode)
        {
            case TeamSetGameMode.story:
                target = TeamSet.Instance.Default;
                break;
            case TeamSetGameMode.arena3V3:
                target = TeamSet.Instance.Arena3V3;
                break;
        }
    }

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
            yield return ChangeTeamPos(MemberDetail.target.focusingCharDataInfo.monsterOfPlayerId, focusingPosNum);
            yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
        }
        void Trigger()
        {
            PreScene.Instance.mainProcessRunner.Run(MonsterIconButton());
        }
        targetButton.onClick.AddListener(Trigger);
    }
    #endregion
    
    // 实际修改队伍编辑存档
    public IEnumerator ChangeTeamPos(string monsterlocalID,int targetPos)
    {
        List<PosNumWithLocalKey> returns = target.SetPosMemInfoByLocalIDConservationMode(targetPos, monsterlocalID);
        for (int i = 0; i < returns.Count;i++)
        {
            yield return ChangeIconOnPos(returns[i].posNum);
        }
        yield return TeamSet.Instance.SaveTeamSet(TeamSetGameMode);//也就是说只要对队伍进行了一次编辑，立刻保存阵容信息。
    }
    
    public static IEnumerator ChangeHeroIconByMonsterOfPlayerId(string PosMonsterOfPlayerId, HeroIcon Icon)
    {
        if (PosMonsterOfPlayerId != null)
        {
            IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo(PosMonsterOfPlayerId);
            yield return getchar;
            if (getchar.Current == null)
                yield break;
            GetMonsterOfPlayerDetailModel _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(_one.monsterId);
            Icon.ChangeIcon(charConfig == null ? null : MonsterIconDic.Instance.GetMonsterIconSyn(charConfig.RECORD_ID), charConfig == null ? Zokusei.Null : charConfig._zokusei);
        }
        else
        {
            Icon.ChangeIcon(null, Zokusei.Null);
        }
    }
    
    // 纯渲染函数
    IEnumerator ChangeIconOnPos(int posNum)
    {
        HeroIcon tar = teamButtonDic[posNum];
        string PosMonsterOfPlayerId = target.GetPosMonsterOfPlayerId(posNum);
        yield return ChangeHeroIconByMonsterOfPlayerId(PosMonsterOfPlayerId, tar);
    }

    #region 初始化（显示目前队伍编辑，加载按钮功能）
    public IEnumerator INITeamPosButtons()
    {
        teamButtonDic.Clear();
        if (team1back == null || team1left == null || team1front == null || team1right == null) 
        {
            Debug.Log("队伍编辑器按钮没适配？？");
        }
        teamButtonDic.Add(0, team1back);
        teamButtonDic.Add(1, team1left);
        teamButtonDic.Add(2, team1front);
        teamButtonDic.Add(3, team1right);
        
        Debug.Log("开始适配队伍编辑器各个位置初始头像");
        yield return ChangeIconOnPos(0);
        yield return ChangeIconOnPos(1);
        yield return ChangeIconOnPos(2);
        yield return ChangeIconOnPos(3);
        
        RemoveButton.onClick.RemoveAllListeners();
        void Remove()
        {
            IEnumerator RemoveSelected()
            {
                yield return ChangeTeamPos(null, focusingPosNum);
            }
            PreScene.Instance.mainProcessRunner.Run(RemoveSelected());
        }
        RemoveButton.onClick.AddListener(Remove);
        
        team1back.iconButton.onClick.RemoveAllListeners();
        void pos1B()
        {
            IEnumerator setPosB()
            {
                focusingPosNum = 0;
                yield return MemberDetail.target.SetMemberDetailFocusingChar(target.GetPosMonsterOfPlayerId(focusingPosNum));//确立focusing角色
                HeroIcon.Seletedfeature(team1back, selectedFrame,200f);
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
            PreScene.Instance.mainProcessRunner.Run(setPosB());
        }
        team1back.iconButton.onClick.AddListener(pos1B);

        team1left.iconButton.onClick.RemoveAllListeners();
        void pos1L()
        {
            IEnumerator setPosL()
            {
                focusingPosNum = 1;
                yield return MemberDetail.target.SetMemberDetailFocusingChar(target.GetPosMonsterOfPlayerId(focusingPosNum));//确立focusing角色
                HeroIcon.Seletedfeature(team1left, selectedFrame,200f);
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
            PreScene.Instance.mainProcessRunner.Run(setPosL());
        }
        team1left.iconButton.onClick.AddListener(pos1L);

        team1front.iconButton.onClick.RemoveAllListeners();
        void pos1F()
        {
            IEnumerator setPosF()
            {
                focusingPosNum = 2;
                yield return MemberDetail.target.SetMemberDetailFocusingChar(target.GetPosMonsterOfPlayerId(focusingPosNum));//确立focusing角色
                HeroIcon.Seletedfeature(team1front, selectedFrame,200f);
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
            PreScene.Instance.mainProcessRunner.Run(setPosF());
        }
        team1front.iconButton.onClick.AddListener(pos1F);
        
        team1right.iconButton.onClick.RemoveAllListeners();
        void pos1R()
        {
            IEnumerator setPosR()
            {
                focusingPosNum = 3;
                yield return MemberDetail.target.SetMemberDetailFocusingChar(target.GetPosMonsterOfPlayerId(TeamEditManager.focusingPosNum));//确立focusing角色
                HeroIcon.Seletedfeature(team1right, selectedFrame,200f);
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
            PreScene.Instance.mainProcessRunner.Run(setPosR());
        }
        team1right.iconButton.onClick.AddListener(pos1R);
    }
    #endregion
}
