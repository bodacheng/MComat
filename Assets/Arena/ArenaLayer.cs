using System;
using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine.UI;

public class ArenaLayer : UILayer
{
    #region 玩家队伍
    [SerializeField] HeroIcon member1, member2, member3;
    [SerializeField] Button EditMyTeamBtn;
    [SerializeField] Text myScore;
    [SerializeField] Text myRank;
    [SerializeField] GameObject plsEditTeamIndicator;
    [SerializeField] GameObject bronzeIcon;
    [SerializeField] GameObject silverIcon;
    [SerializeField] GameObject goldIcon;
    #endregion
    
    [SerializeField] Button RefreshBtn;
    [SerializeField] RectTransform EnemiesT;
    [SerializeField] ArenaFightTeamDisplay ArenaFightTeamDisplayPrefab;
    
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
    
    LeaderboardInfo myLeaderboardInfo;
    
    private Action<bool> SetLoaded;
    private Action<string> ReturnToLobby;
    private Func<int, List<LeaderboardInfo>> GetOpponentAroundPoint;
    public void SetUp(Action<bool> SetLoaded, 
        Action<string> ReturnToLobby, 
        Func<int, List<LeaderboardInfo>> GetOpponentAroundPoint,
        Action openRanking)
    {
        this.SetLoaded = SetLoaded;
        this.ReturnToLobby = ReturnToLobby;
        this.GetOpponentAroundPoint = GetOpponentAroundPoint;
        
        rankingPageBtn.onClick.RemoveAllListeners();
        rankingPageBtn.onClick.AddListener(()=> openRanking());
    }

    void RefreshRankPointBar(int current)
    {
        rankPointBar.value = (float)current / (float)maxArenaPoint;
        
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
    
    public void RefreshOpponent()
    {
        var extraAwardLeft = 3 - PlayerAccountInfo.Me.ArenaCountToday;
        extraAwardLeft = Mathf.Clamp(extraAwardLeft, 0, extraAwardLeft);
        extraAwardLeftToday.text = "(" + extraAwardLeft + "/3)"; 
        RefreshRankIcon(PlayerAccountInfo.Me.currentRank);
        
        RefreshBtn.onClick.RemoveAllListeners();
        RefreshBtn.onClick.AddListener(RefreshOpponent);
        
        ProgressLayer.Loading(">");
        CloudScript.GetLeaderboardAroundUser(
            obj =>
            {
                var exceptSelf = new List<LeaderboardInfo>();
                foreach (var t in obj)
                {
                    if (t.PlayerLeaderboardEntry.PlayFabId != PlayerAccountInfo.Me.PlayFabId)
                    {
                        Debug.Log( "Opponent info loaded : " +t.PlayerLeaderboardEntry.PlayFabId);
                        exceptSelf.Add(t);
                    }
                    else
                    {
                        Debug.Log( "Self info loaded : " +t.PlayerLeaderboardEntry.PlayFabId);
                        myLeaderboardInfo = t;
                    }
                }
                if (myLeaderboardInfo != null)
                {
                    RefreshRankPointBar(myLeaderboardInfo.PlayerLeaderboardEntry.StatValue);
                    myScore.text = myLeaderboardInfo.PlayerLeaderboardEntry.StatValue.ToString();
                    myRank.text = "Rank :" + myLeaderboardInfo.PlayerLeaderboardEntry.Position;
                }
                else
                {
                    myScore.gameObject.SetActive(false);
                    myRank.gameObject.SetActive(false);
                }
                
                if (exceptSelf.Count < 3)
                {
                    var myPoint = (myLeaderboardInfo != null) ? myLeaderboardInfo.PlayerLeaderboardEntry.StatValue : 1000;
                    var list = this.GetOpponentAroundPoint(myPoint);
                    for (var i = 0; i < list.Count; i++)
                    {
                        exceptSelf.Add(list[i]);
                        if (exceptSelf.Count == 3)
                        {
                            break;
                        }
                    }
                }
                
                DisplayOpponents(exceptSelf);
                ProgressLayer.Close();
                SetLoaded.Invoke(true);
            },
            () =>
            {
                ProgressLayer.Close();
                ReturnToLobby.Invoke("network error");
            }
        );
    }
    
    // 挑战玩家队伍机能加载（目前规定显示在画面上的挑战组一共四个。远程获取不到的情况下就本地生成）
    void DisplayOpponents(List<LeaderboardInfo> leaderboards)
    {
        foreach (Transform c in EnemiesT)
        {
            Destroy(c.gameObject);
        }
        
        foreach (var t in leaderboards)
        {
            var o = Instantiate(ArenaFightTeamDisplayPrefab);
            o.AddFightToList(t);
            o.transform.SetParent(EnemiesT);
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
        EditMyTeamBtn.onClick.RemoveAllListeners();
        EditMyTeamBtn.onClick.AddListener(GoToTeamEdit);
    }
}