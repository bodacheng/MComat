using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using UnityEngine;

public class ArenaPage : MSceneProcess
{
    ArenaLayer arenaLayer;
    readonly ArenaDummiesTable _table = new ();
    
    public ArenaPage()
    {
        Step = MainSceneStep.Arena;
    }
    
    void ArenaTFinished(bool value)
    {
        missionWatcher.Finish("arenaTFinished", value);
    }
    
    void LeaderBoardFinished(bool value)
    {
        missionWatcher.Finish("leaderBoardFinished", value);
    }
    
    void EnterProcess()
    {
        arenaLayer = UILayerLoader.Load<ArenaLayer>();
        arenaLayer.SetUp(
            LoadLeaderboardInfos,
            () =>
            {
                PreScene.target.trySwitchToStep(MainSceneStep.Ranking);
            });
        
        arenaLayer.SetMyArenaInfo(Currencies.ArenaTicket.Value);
        missionWatcher = new MissionWatcher(
            new List<string>
            {
                "arenaTFinished", "leaderBoardFinished"
            },
            () =>
            {
                arenaLayer.ShowMyTeam();
            }, PreScene.ReturnToLobby);
        
        PlayFabReadClient.LoadTeamSet("arena", ArenaTFinished);
        
        if (PlayerAccountInfo.Me.arenaPoint != -1)
        {
            LoadLeaderboardInfos();
        }
        else
        {
            // 说明玩家的防御队伍没有登陆，因为arenaPoint是首次登陆防御队伍时候顺便登陆的
            // 强制玩家登陆防御队伍
            SetLoaded(true);
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
    
    private LeaderboardInfo _myLeaderboardInfo;
    void LoadLeaderboardInfos()
    {
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
                        _myLeaderboardInfo = t;
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
                
                arenaLayer.ShowEnemies(exceptSelf);
                LeaderBoardFinished(true);
                SetLoaded(true);
            },
            () =>
            {
                LeaderBoardFinished(false);
            }
        );
    }
}