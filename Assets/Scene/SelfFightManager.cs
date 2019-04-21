using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfFightManager : MonoBehaviour {

    public preparingScene _preparingScene;

    public Button FightStartBUtton;
    public charIcon team1back, team1front, team1left, team1right;
    public charIcon team2back, team2front, team2left, team2right;

    public QuestPreparePage _QuestPreparePage;

    private IDictionary<int, charIcon> team1ButtonDic = new Dictionary<int, charIcon>();
    private IDictionary<int, charIcon> team2ButtonDic = new Dictionary<int, charIcon>();

    private LocalFight _selfFight;
    private List<CharacterDataInfo> team1 = new List<CharacterDataInfo>();
    private List<CharacterDataInfo> team2 = new List<CharacterDataInfo>();
    private Team focusingTeam; //team1或者是team2
    private int focusingPosition; // 0到3
    private charIcon focusingPosButton;
    private List<int> inFightLocalMemberIDs = new List<int>();

    public void clear()
    {
        foreach(KeyValuePair<int,charIcon> keyValuePair in team1ButtonDic)
        {
            keyValuePair.Value.changeIcon(null,zokusei.Null);
        }
        foreach (KeyValuePair<int, charIcon> keyValuePair in team2ButtonDic)
        {
            keyValuePair.Value.changeIcon(null,zokusei.Null);
        }
        team1.Clear();
        team2.Clear();
    }

    public void FightStart()
    {
        _preparingScene._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);

        PosNumWithLocalKey[] team1posmems = _selfFight._team1positionLocalCharKeySet.PosNumsWithLocalKeys;
        PosNumWithLocalKey[] team2posmems = _selfFight._team2positionLocalCharKeySet.PosNumsWithLocalKeys;

        team1.Clear();
        for (int i = 0; i < team1posmems.Length;i++)
        {
            if (team1posmems[i].LocalID != -1)
                team1.Add(AccountCharsSet.getTheCharacterOfMine(team1posmems[i].LocalID));
        }
        team2.Clear();
        for (int i = 0; i < team2posmems.Length; i++)
        {
            if (team2posmems[i].LocalID != -1)
                team2.Add(AccountCharsSet.getTheCharacterOfMine(team2posmems[i].LocalID));
        }

        _selfFight.team1members = team1.ToArray();
        _selfFight.Enemies = team2.ToArray();

        //foreach(CharacterDataInfo _one in _selfFight.team1members)
        //{
        //    _selfFight._team1positionLocalCharKeySet.PosNumsWithLocalKeys[_one.localID] = new PosNumWithLocalKey(_one.localID, _one.localID);
        //}
        //foreach (CharacterDataInfo _one in _selfFight.Enemies)
        //{
        //    _selfFight._team2positionLocalCharKeySet.PosNumsWithLocalKeys[_one.localID] = new PosNumWithLocalKey(_one.localID, _one.localID);
        //}

        Stage stage = new Stage();
        _selfFight.BattleGroundID = 2;
        stage._LocalFight = _selfFight;
        _preparingScene.triggerMainProcess(_QuestPreparePage.getReadyToBattle(stage,SceneMode.MyPetsFight));
    }

    public void monsterIConButton(int localID)
    {
        CharacterDataInfo _one = AccountCharsSet.getTheCharacterOfMine(localID);
        CharacterResourceInfo characterResourceInfo = CharsManager.getCharacterResourceInfo(_one.resource_num);
        if (_one == null)
        {
            Debug.Log("角色存档问题。localid："+ localID);
            return;
        }
        if (this.focusingTeam == Team.none || this.focusingPosition < 0)
            return;//也就是说还没点击目标适配位置
        
        //CharacterDataInfo _clone = _one.DeepCopy();
        switch (this.focusingTeam)
        {
            case Team.player1:
                team1ButtonDic.TryGetValue(this.focusingPosition,out focusingPosButton);
                //team1.Add(_one);
                _selfFight._team1positionLocalCharKeySet.PosNumsWithLocalKeys[this.focusingPosition].LocalID = _one.localID;
                break;
            case Team.player2:
                team2ButtonDic.TryGetValue(this.focusingPosition, out focusingPosButton);
                //team2.Add(_one);
                _selfFight._team2positionLocalCharKeySet.PosNumsWithLocalKeys[this.focusingPosition].LocalID = _one.localID;
                break;
        }
        focusingPosButton.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(_one.resource_num),characterResourceInfo._zokusei);
    }

    public void deployCancel()
    {
        if (this.focusingTeam == Team.none || this.focusingPosition < 0)
            return;//也就是说还没点击目标适配位置

        CharacterDataInfo _toCancel = null;
        switch (this.focusingTeam)
        {
            case Team.player1:
                team1ButtonDic.TryGetValue(this.focusingPosition, out focusingPosButton);
                foreach (CharacterDataInfo _one in team1)
                {
                    if (_one.localID == this.focusingPosition)
                        _toCancel = _one;
                }
                if (_toCancel != null)
                    team1.Remove(_toCancel);
                break;
            case Team.player2:
                team2ButtonDic.TryGetValue(this.focusingPosition, out focusingPosButton);
                foreach (CharacterDataInfo _one in team2)
                {
                    if (_one.localID == this.focusingPosition)
                        _toCancel = _one;
                }
                if (_toCancel != null)
                    team2.Remove(_toCancel);
                break;
        }
        focusingPosButton.changeIcon(null,zokusei.Null);
    }

    public void INITeamPosButtons()
    {
        _selfFight = new LocalFight();

        team1ButtonDic.Clear();
        team2ButtonDic.Clear();

        team1ButtonDic.Add(0, team1back);
        team1ButtonDic.Add(1, team1left);
        team1ButtonDic.Add(2, team1front);
        team1ButtonDic.Add(3, team1right);

        team2ButtonDic.Add(0, team2back);
        team2ButtonDic.Add(1, team2left);
        team2ButtonDic.Add(2, team2front);
        team2ButtonDic.Add(3, team2right);

        team1back.changeIcon(null,zokusei.Null);
        team1left.changeIcon(null,zokusei.Null);
        team1front.changeIcon(null,zokusei.Null);
        team1right.changeIcon(null,zokusei.Null);
        
        team2back.changeIcon(null,zokusei.Null);
        team2left.changeIcon(null,zokusei.Null);
        team2front.changeIcon(null,zokusei.Null);
        team2right.changeIcon(null,zokusei.Null);

        team1back.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos1B = () => {
            OneTeamPosButtonBehaviour(Team.player1,0);
        };
        team1back.iconButton.onClick.AddListener(pos1B);

        team1left.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos1L = () => {
            OneTeamPosButtonBehaviour(Team.player1, 1);
        };
        team1left.iconButton.onClick.AddListener(pos1L);

        team1front.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos1F = () => {
            OneTeamPosButtonBehaviour(Team.player1, 2);
        };
        team1front.iconButton.onClick.AddListener(pos1F);

        team1right.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos1R = () => {
            OneTeamPosButtonBehaviour(Team.player1, 3);
        };
        team1right.iconButton.onClick.AddListener(pos1R);

        ////////////////////////////////////////////

        team2back.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos2B = () => {
            OneTeamPosButtonBehaviour(Team.player2, 0);
        };
        team2back.iconButton.onClick.AddListener(pos2B);

        team2left.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos2L = () => {
            OneTeamPosButtonBehaviour(Team.player2, 1);
        };
        team2left.iconButton.onClick.AddListener(pos2L);

        team2front.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos2F = () => {
            OneTeamPosButtonBehaviour(Team.player2, 2);
        };
        team2front.iconButton.onClick.AddListener(pos2F);

        team2right.iconButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction pos2R = () => {
            OneTeamPosButtonBehaviour(Team.player2, 3);
        };
        team2right.iconButton.onClick.AddListener(pos2R);

        ////////////
        /// 
        FightStartBUtton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction AskStartFight = () => {
            FightStart();
        };
        FightStartBUtton.onClick.AddListener(AskStartFight);
    }

    void OneTeamPosButtonBehaviour(Team team,int pos)
    {
        this.focusingTeam = team;
        this.focusingPosition = pos;
    }
}
