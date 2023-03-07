using System;
using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using PlayFab.ClientModels;
using UnityEngine;

public class ArenaPage : MSceneProcess
{
    ArenaLayer arenaLayer;
    
    public ArenaPage()
    {
        Step = MainSceneStep.Arena;
    }
    
    void ItemsLoadFinished(bool value)
    {
        missionWatcher.Finish("itemsLoadFinished", value);
    }
    
    void ArenaTFinished(bool value)
    {
        missionWatcher.Finish("arenaTFinished", value);
    }
    
    void LeaderBoardFinished(bool value)
    {
        missionWatcher.Finish("leaderBoardFinished", value);
    }
    
    void GotServerTime(bool value)
    {
        missionWatcher.Finish("gotServerTime", value);
    }

    bool CheckNewSeason(Action onClickRankResetLayer)
    {
        int lastSeasonPoint = PlayerPrefs.GetInt("arenapoint");
        if (lastSeasonPoint > PlayerAccountInfo.Me.arenaPoint)
        {
            var arenaNewSeason = UILayerLoader.Load<ArenaNewSeason>();
            arenaNewSeason.Setup(lastSeasonPoint, PlayerAccountInfo.Me.arenaPoint, () =>
                {
                    UILayerLoader.Remove<ArenaNewSeason>();
                    onClickRankResetLayer.Invoke();
                }
            );
            PlayerPrefs.SetInt("arenapoint", PlayerAccountInfo.Me.arenaPoint);
            return true;
        }
        PlayerPrefs.SetInt("arenapoint", PlayerAccountInfo.Me.arenaPoint);
        return false;
    }
    
    void EnterProcess()
    {
        arenaLayer = UILayerLoader.Load<ArenaLayer>();
        arenaLayer.SetUp(
            LoadLeaderboardInfos,
            () =>
            {
                PreScene.target.trySwitchToStep(MainSceneStep.Ranking);
            },
            PlusPoint,
            PrepareForIt
        );
        
        missionWatcher = new MissionWatcher(
            new List<string>
            {
                "itemsLoadFinished", "leaderBoardFinished", "gotServerTime" // "arenaTFinished"
            },
            () =>
            {
                arenaLayer.SetupArenaTicket();
            },
            PreScene.ReturnToLobby
        );
        
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        //PlayFabReadClient.LoadTeamSet("arena", ArenaTFinished);
        PlayFabReadClient.GetServerTime(
            (x) =>
            {
                // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
                int daysUntilTuesday = ((int) DayOfWeek.Monday - (int) x.DayOfWeek + 7) % 7;
                DateTime nextMonday = x.AddDays(daysUntilTuesday);
                nextMonday = new DateTime(nextMonday.Year, nextMonday.Month, nextMonday.Day, 0, 0, 0, 0, nextMonday.Kind);
                Debug.Log("next monday:"+ nextMonday);
                GotServerTime(true);
                
                arenaLayer.SetSeasonCountDown(nextMonday);
            },
            ()=>{ GotServerTime(false); }
        );
        
        if (PlayerAccountInfo.Me.arenaPoint != -1)
        {
            LoadLeaderboardInfos();
        }
        else
        {
            // 说明玩家的防御队伍没有登陆，因为arenaPoint是首次登陆防御队伍时候顺便登陆的
            // 强制玩家登陆防御队伍
            arenaLayer.ShowMyTeamByLeaderInfo(null);
            LeaderBoardFinished(true);
            SetLoaded(true);
        }
    }
    
    void PrepareForIt(FightInfo stage)
    {
        if (Currencies.ArenaTicket.Value > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, stage, true);
        }
        else
        {
            PopupLayer.ArrangeWarnWindow(Translate.Get("NoArenaEnoughTicket"));
        }
    }
    
    public override void ProcessEnter()
    {
        var showRankReset = CheckNewSeason(EnterProcess);
        if (!showRankReset)
            EnterProcess();
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<ArenaNewSeason>();
        UILayerLoader.Remove<ArenaLayer>();
    }
    
    public LeaderboardInfo MyLeaderboardInfo => _myLeaderboardInfo;
    private LeaderboardInfo _myLeaderboardInfo;
    void LoadLeaderboardInfos()
    {
        CloudScript.GetLeaderboardAroundUser(
            leaderboardInfos =>
            {
                if (leaderboardInfos == null || leaderboardInfos.Count == 0)
                {
                    arenaLayer.ShowMyTeamByLeaderInfo(null);
                    LeaderBoardFinished(true);
                    SetLoaded(true);
                    return;
                }
                
                var exceptSelf = new List<LeaderboardInfo>();
                foreach (var leaderboardInfo in leaderboardInfos)
                {
                    if (leaderboardInfo.PlayerLeaderboardEntry.PlayFabId != PlayerAccountInfo.Me.PlayFabId)
                    {
                        Debug.Log( "Opponent info loaded : " +leaderboardInfo.PlayerLeaderboardEntry.PlayFabId);
                        var info = exceptSelf.Find(x=> x.PlayerLeaderboardEntry.PlayFabId == leaderboardInfo.PlayerLeaderboardEntry.PlayFabId);
                        if (info == null)
                            exceptSelf.Add(leaderboardInfo);
                    }
                    else
                    {
                        Debug.Log( "Self info loaded : " +leaderboardInfo.PlayerLeaderboardEntry.PlayFabId);
                        _myLeaderboardInfo = leaderboardInfo;
                    }
                }
                arenaLayer.ShowMyTeamByLeaderInfo(_myLeaderboardInfo);
                arenaLayer.DisplayOpponents(exceptSelf, _myLeaderboardInfo);
                LeaderBoardFinished(true);
                SetLoaded(true);
            },
            () =>
            {
                LeaderBoardFinished(false);
            }
        );
    }
    
    int PlusPoint(PlayerLeaderboardEntry myInfo, PlayerLeaderboardEntry opponentInfo)
    {
        if (opponentInfo.Position - myInfo.Position >= 50)
        {
            return Mathf.Clamp(opponentInfo.StatValue - myInfo.StatValue, 10, 20);
        }
        else
        {
            return Mathf.Clamp(opponentInfo.StatValue - myInfo.StatValue, 5, 10);
        }
    }
}