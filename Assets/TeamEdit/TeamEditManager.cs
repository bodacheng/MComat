using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using dataAccess;
using UnityEngine.UI;

public class TeamEditManager : MonoBehaviour
{
    public Canvas _Canvas;
    public Button RemoveButton;
    public HeroIcon team1back, team1front, team1left, team1right;
    
    [Space(7)]
    [Header("选中框")]
    public GameObject selectedFrame;
    
    public static int focusingPosNum = -1;
    readonly IDictionary<int, HeroIcon> team1ButtonDic = new Dictionary<int, HeroIcon>();

    public static TeamEditManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public IEnumerator MonsterIConButton(string monsterlocalID,int targetPos)
    {
        List<PosNumWithLocalKey> returns = TeamSet.Instance.Default.SetPosMemInfoByLocalIDConservationMode(targetPos, monsterlocalID);
        for (int i = 0; i < returns.Count;i++)
        {
            yield return ChangeIconOnPos(returns[i].posNum);
        }
        yield return TeamSet.Instance.SaveTeamSet(TeamSetGameMode.story);//也就是说只要对队伍进行了一次编辑，立刻保存阵容信息。
    }

    IEnumerator ChangeIconOnPos(int posNum)
    {
        if (posNum == -1)
        {
            Debug.Log("请检查changeIconOnPos函数执行顺序");
            yield break;
        }
        HeroIcon tar = null;
        if (team1ButtonDic.ContainsKey(posNum))
        {
            team1ButtonDic.TryGetValue(posNum, out tar);
        }
        else
        {
            Debug.Log("错误的位置值：" + posNum);
            yield break;
        }
        if (tar == null)
        {
            Debug.Log("严重错误");
            yield break;
        }

        string PositionMonsterOfPlayerId = TeamSet.Instance.Default.GetPositionMonsterOfPlayerId(posNum);
        if (PositionMonsterOfPlayerId != null)
        {
            GetMonsterOfPlayerDetailModel _one;
            CharConfig characterResourceInfo = null;
            IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo(PositionMonsterOfPlayerId);
            yield return getchar;
            if (getchar.Current == null)
                yield break;
            _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
            characterResourceInfo = MonstersConfigTable.GetCharConfig(_one.monsterId);
            tar.ChangeIcon(characterResourceInfo == null ? null : MonsterIconDic.Instance.GetMonsterIconSyn(characterResourceInfo.RECORD_ID),
                characterResourceInfo == null ? Zokusei.Null : characterResourceInfo._zokusei);
        }
        else
        {
            tar.ChangeIcon(null, Zokusei.Null);
        }
        yield break;
    }

    public IEnumerator INITeamPosButtons()
    {
        team1ButtonDic.Clear();
        if (team1back == null || team1left == null || team1front == null || team1right == null) 
        {
            Debug.Log("队伍编辑器按钮没适配？？");
        }
        team1ButtonDic.Add(0, team1back);
        team1ButtonDic.Add(1, team1left);
        team1ButtonDic.Add(2, team1front);
        team1ButtonDic.Add(3, team1right);

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
                yield return Instance.MonsterIConButton(null,TeamEditManager.focusingPosNum);
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
                yield return MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.Instance.Default.GetPositionMonsterOfPlayerId(focusingPosNum));//确立focusing角色
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
                yield return MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.Instance.Default.GetPositionMonsterOfPlayerId(focusingPosNum));//确立focusing角色
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
                yield return MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.Instance.Default.GetPositionMonsterOfPlayerId(focusingPosNum));//确立focusing角色
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
                yield return MemberDetail.target.SetMemberDetailFocusingChar(TeamSet.Instance.Default.GetPositionMonsterOfPlayerId(TeamEditManager.focusingPosNum));//确立focusing角色
                HeroIcon.Seletedfeature(team1right, selectedFrame,200f);
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
            PreScene.Instance.mainProcessRunner.Run(setPosR());
        }
        team1right.iconButton.onClick.AddListener(pos1R);
    }
}
