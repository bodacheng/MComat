using System;
using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UniRx;
using UnityEngine.UI;

public class ArenaLayer : UILayer
{
    [SerializeField] private Text ticket;
    
    #region 玩家队伍
    [SerializeField] HeroIcon member1, member2, member3;
    [SerializeField] Button editMyTeamBtn;
    [SerializeField] Text myScore;
    [SerializeField] Text myRank;
    [SerializeField] GameObject plsEditTeamIndicator;
    [SerializeField] GameObject bronzeIcon;
    [SerializeField] GameObject silverIcon;
    [SerializeField] GameObject goldIcon;
    #endregion
    
    [SerializeField] Button refreshBtn;
    [SerializeField] RectTransform enemiesT;
    [SerializeField] ArenaFightTeamDisplay arenaFightTeamDisplayPrefab;
    
    #region reward indicator
    [SerializeField] Text extraAwardLeftToday;
    [SerializeField] Slider rankPointBar;
    [SerializeField] GameObject bronzeAwardGot;
    [SerializeField] GameObject silverAwardGot;
    [SerializeField] GameObject goldAwardGot;
    [SerializeField] Button questionBtn;
    #endregion
    
    [SerializeField] Button rankingPageBtn;
    
    private int maxArenaPoint = 1300;
    private int bronzePoint = 400;
    private int silverPoint = 800;
    private int goldPoint = 1200;
    
    public void SetUp(Action loadData, Action openRanking)
    {
        refreshBtn.onClick.RemoveAllListeners();
        refreshBtn.onClick.AddListener(()=> loadData());
        
        rankingPageBtn.onClick.RemoveAllListeners();
        rankingPageBtn.onClick.AddListener(()=> openRanking());
        Currencies.ArenaTicket.Subscribe(x=>
        {
            ticket.text = x.ToString();
        }).AddTo(this.gameObject);
    }

    public void SetMyArenaInfo(int rank, int extraAwardLeft)
    {
        RefreshRankIcon(rank);
        extraAwardLeftToday.text = "(" + extraAwardLeft + "/3)"; 
    }

    public void SetMyLeaderboardInfo(LeaderboardInfo _myLeaderboardInfo)
    {
        RefreshRankPointBar(_myLeaderboardInfo.PlayerLeaderboardEntry.StatValue);
        myScore.text = _myLeaderboardInfo.PlayerLeaderboardEntry.StatValue.ToString();
        myRank.text = "Rank :" + _myLeaderboardInfo.PlayerLeaderboardEntry.Position;
    }

    public void ShowEnemies(List<LeaderboardInfo> enemies)
    {
        DisplayOpponents(enemies);
    }

    void RefreshRankPointBar(int current)
    {
        rankPointBar.value = current / (float)maxArenaPoint;
        
        bronzeAwardGot.SetActive(current >= bronzePoint);
        silverAwardGot.SetActive(current >= silverPoint);
        goldAwardGot.SetActive(current >= goldPoint);
    }

    void RefreshRankIcon(int rank)
    {
        bronzeIcon.SetActive(rank == 0);
        silverIcon.SetActive(rank == 1);
        goldIcon.SetActive(rank == 2);
    }
    
    // 挑战玩家队伍机能加载（目前规定显示在画面上的挑战组一共四个。远程获取不到的情况下就本地生成）
    void DisplayOpponents(List<LeaderboardInfo> leaderboards)
    {
        foreach (Transform c in enemiesT)
        {
            Destroy(c.gameObject);
        }
        
        foreach (var t in leaderboards)
        {
            var o = Instantiate(arenaFightTeamDisplayPrefab);
            o.AddFightToList(t);
            o.transform.SetParent(enemiesT);
            o.transform.localPosition = Vector3.zero;
            o.transform.localScale = Vector3.one;
            o.gameObject.SetActive(true);
        }
    }
    
    public void ShowMyTeam()
    {
        var pos1InstanceID = TeamSet.Arena3V3.GetInstanceIdOnPos(0);
        var pos2InstanceID = TeamSet.Arena3V3.GetInstanceIdOnPos(1);
        var pos3InstanceID = TeamSet.Arena3V3.GetInstanceIdOnPos(2);
        
        var info1 = dataAccess.Units.Get(pos1InstanceID);
        var info2 = dataAccess.Units.Get(pos2InstanceID);
        var info3 = dataAccess.Units.Get(pos3InstanceID);
        
        plsEditTeamIndicator.SetActive(info1 == null || info2 == null || info3 == null);
        
        member1.ChangeIcon(info1);
        member2.ChangeIcon(info2);
        member3.ChangeIcon(info3);
        
        void GoToTeamEdit()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
        }
        editMyTeamBtn.onClick.RemoveAllListeners();
        editMyTeamBtn.onClick.AddListener(GoToTeamEdit);
    }
}