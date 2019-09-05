using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using Api.Dto.Model;
using dataAccess;

public class TeamEditManager : MonoBehaviour
{
    public preparingScene _preparingScene;
    public RectTransform UIT;
    public charIcon team1back, team1front, team1left, team1right;
    
    [Space(7)]
    [Header("选中框")]
    public GameObject selectedFrame;
    
    public static PosNum focusingPosNum = PosNum.none;
    private IDictionary<int, charIcon> team1ButtonDic = new Dictionary<int, charIcon>();
    
    public void OpenButtons(bool _on)
    {
        UIT.gameObject.SetActive(_on);
    }

    public IEnumerator monsterIConButton(string monsterlocalID,PosNum targetPos)
    {
        IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(monsterlocalID);
        yield return getchar;
        GetMonsterOfPlayerDetailModel _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
        if (_one == null)
        {
            Debug.Log("角色存档问题。localid："+ monsterlocalID);
            yield break;
        }
        List<PosNumWithLocalKey> returns = TeamSet.Instance.storyModeTeamSet.setPosMemInfoByLocalIDConservationMode(targetPos,monsterlocalID);
        for (int i = 0; i < returns.Count;i++)
            yield return changeIconOnPos(returns[i].posNum);
        yield return TeamSet.Instance.saveTeamSet(TeamSetGameMode.story);//也就是说只要对队伍进行了一次编辑，立刻保存阵容信息。
    }
    
    private IEnumerator changeIconOnPos(PosNum posNum)
    {
        if (posNum == PosNum.none)
        {
            Debug.Log("请检查changeIconOnPos函数执行顺序");
            yield break;
        }
        charIcon tar = null;
        if (team1ButtonDic.ContainsKey((int)posNum))
        {
            team1ButtonDic.TryGetValue((int)posNum,out tar);
        }else{
            Debug.Log("错误的位置值："+posNum);
            yield break;
        }        
        if (tar == null)
        {
            Debug.Log("严重错误");
            yield break;
        }

        string PositionMonsterOfPlayerId = TeamSet.Instance.storyModeTeamSet.getPositionMonsterOfPlayerId(posNum);
        if (PositionMonsterOfPlayerId != null)
        {
            GetMonsterOfPlayerDetailModel _one;
            CharacterResourceInfo characterResourceInfo = null;
            IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(PositionMonsterOfPlayerId);
            yield return getchar;
            if (getchar.Current == null)
                yield break;
            _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
            characterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(int.Parse(_one.monsterId));
            tar.changeIcon(characterResourceInfo == null ? null: monsterIconsDic.Instance.getMonsterIconSyn(characterResourceInfo.monsterId),
                characterResourceInfo == null ? zokusei.Null : characterResourceInfo._zokusei);
        }else{
            tar.changeIcon(null,zokusei.Null);
        }
        yield break;
    }
    
    public IEnumerator INITeamPosButtons()
    {
        if (team1ButtonDic == null)
        {
            team1ButtonDic = new Dictionary<int, charIcon>();
            Debug.Log("team1ButtonDic = new Dictionary<int, charIcon>();");
        }
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
        yield return changeIconOnPos(PosNum.back);
        yield return changeIconOnPos(PosNum.left);
        yield return changeIconOnPos(PosNum.front);
        yield return changeIconOnPos(PosNum.right);
        
        team1back.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos1B = () => {
            IEnumerator setPosB ()
            {
                TeamEditManager.focusingPosNum = 0;
                yield return _preparingScene._MemberDetail.SetMemberDetailSystemFocusingCharacter(TeamSet.instance.storyModeTeamSet.getPositionMonsterOfPlayerId(TeamEditManager.focusingPosNum));//确立focusing角色
                charIcon.Seletedfeature(team1back,selectedFrame);
                yield return _preparingScene._MemberDetail.refreshMemberDetailGamenSystemBaseOnFocusingChar();
                yield break;
            };
            _preparingScene.triggerMainProcess(setPosB ());
        };
        team1back.iconButton.onClick.AddListener(pos1B);

        team1left.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos1L = () => {
            IEnumerator setPosL ()
            {
                TeamEditManager.focusingPosNum = (PosNum)1;
                 yield return _preparingScene._MemberDetail.SetMemberDetailSystemFocusingCharacter(TeamSet.instance.storyModeTeamSet.getPositionMonsterOfPlayerId(TeamEditManager.focusingPosNum));//确立focusing角色
                charIcon.Seletedfeature(team1left,selectedFrame);
                yield return _preparingScene._MemberDetail.refreshMemberDetailGamenSystemBaseOnFocusingChar();
                yield break;
            }
            _preparingScene.triggerMainProcess(setPosL());
        };
        team1left.iconButton.onClick.AddListener(pos1L);

        team1front.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos1F = () => {
            IEnumerator setPosF ()
            {
                TeamEditManager.focusingPosNum = (PosNum)2;
                 yield return _preparingScene._MemberDetail.SetMemberDetailSystemFocusingCharacter(TeamSet.instance.storyModeTeamSet.getPositionMonsterOfPlayerId(TeamEditManager.focusingPosNum));//确立focusing角色
                charIcon.Seletedfeature(team1front,selectedFrame);
                yield return _preparingScene._MemberDetail.refreshMemberDetailGamenSystemBaseOnFocusingChar();
                yield break;
            }
            _preparingScene.triggerMainProcess(setPosF());
        };
        team1front.iconButton.onClick.AddListener(pos1F);

        team1right.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos1R = () => {
            IEnumerator setPosR()
            {
                TeamEditManager.focusingPosNum = (PosNum)3;
                 yield return _preparingScene._MemberDetail.SetMemberDetailSystemFocusingCharacter(TeamSet.instance.storyModeTeamSet.getPositionMonsterOfPlayerId(TeamEditManager.focusingPosNum));//确立focusing角色
                charIcon.Seletedfeature(team1right,selectedFrame);
                yield return _preparingScene._MemberDetail.refreshMemberDetailGamenSystemBaseOnFocusingChar();
                yield break;
            }
            _preparingScene.triggerMainProcess(setPosR());
        };
        team1right.iconButton.onClick.AddListener(pos1R);
        yield break;
    }
}
