using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using PlayFab.ClientModels;
using UnityEngine;

public class ArenaPage : MSceneProcess
{
    ArenaLayer arenaLayer;
    readonly ArenaDummiesTable _table = new ();
    
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

    void CheckNewSeason()
    {
        int lastSeasonPoint = PlayerPrefs.GetInt("arenapoint");
        if (lastSeasonPoint > PlayerAccountInfo.Me.arenaPoint)
        {
            var arenaNewSeason = UILayerLoader.Load<ArenaNewSeason>();
            arenaNewSeason.Setup(lastSeasonPoint, PlayerAccountInfo.Me.arenaPoint,
                UILayerLoader.Remove<ArenaNewSeason>);
        }
        PlayerPrefs.SetInt("arenapoint", PlayerAccountInfo.Me.arenaPoint);
    }
    
    void EnterProcess()
    {
        CheckNewSeason();
        
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
                "itemsLoadFinished", "arenaTFinished", "leaderBoardFinished", 
            },
            () =>
            {
                arenaLayer.SetupArenaTicket();
                arenaLayer.ShowMyTeam();
            },
            PreScene.ReturnToLobby
        );
        
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        PlayFabReadClient.LoadTeamSet("arena", ArenaTFinished);
        
        if (PlayerAccountInfo.Me.arenaPoint != -1)
        {
            LoadLeaderboardInfos();
        }
        else
        {
            // 说明玩家的防御队伍没有登陆，因为arenaPoint是首次登陆防御队伍时候顺便登陆的
            // 强制玩家登陆防御队伍
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
        _table.Load();
        EnterProcess();
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<ArenaLayer>();
    }
    
    public LeaderboardInfo MyLeaderboardInfo => _myLeaderboardInfo;
    private LeaderboardInfo _myLeaderboardInfo;
    void LoadLeaderboardInfos()
    {
        CloudScript.GetLeaderboardAroundUser(
            leaderboardInfos =>
            {
                var exceptSelf = new List<LeaderboardInfo>();
                foreach (var leaderboardInfo in leaderboardInfos)
                {
                    if (leaderboardInfo.PlayerLeaderboardEntry.PlayFabId != PlayerAccountInfo.Me.PlayFabId)
                    {
                        Debug.Log( "Opponent info loaded : " +leaderboardInfo.PlayerLeaderboardEntry.PlayFabId);
                        exceptSelf.Add(leaderboardInfo);
                    }
                    else
                    {
                        Debug.Log( "Self info loaded : " +leaderboardInfo.PlayerLeaderboardEntry.PlayFabId);
                        _myLeaderboardInfo = leaderboardInfo;
                    }
                }
                if (_myLeaderboardInfo != null)
                {
                    arenaLayer.SetMyLeaderboardInfo(_myLeaderboardInfo);
                }
                
                if (exceptSelf.Count < 3)
                {
                    var myPoint = (_myLeaderboardInfo != null) ? _myLeaderboardInfo.PlayerLeaderboardEntry.StatValue : 1000;
                    var list = _table.GetDummiesAroundPoint(myPoint);
                    for (var i = 0; i < list.Count; i++)
                    {
                        exceptSelf.Add(list[i]);
                        if (exceptSelf.Count == 3)
                        {
                            break;
                        }
                    }
                }
                
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