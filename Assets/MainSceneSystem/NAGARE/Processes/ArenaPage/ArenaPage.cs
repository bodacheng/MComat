using System;
using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using PlayFab.ClientModels;
using UnityEngine;

public partial class ArenaPage : MSceneProcess
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
    
    void LeaderBoardFinished(bool value)
    {
        missionWatcher.Finish("leaderBoardFinished", value);
    }
    
    void CheckSeasonRankAndEnter(Action onClickRankResetLayer)
    {
        int lastSeasonPoint = PlayerPrefs.GetInt("arenapoint");
        if (lastSeasonPoint > PlayerAccountInfo.Me.arenaPoint)
        {
            var arenaNewSeason = UILayerLoader.Load<ArenaNewSeason>();
            arenaNewSeason.Setup(lastSeasonPoint, PlayerAccountInfo.Me.arenaPoint, 
                () =>
                {
                    UILayerLoader.Remove<ArenaNewSeason>();
                    onClickRankResetLayer.Invoke();
                }
            );
            PlayerPrefs.SetInt("arenapoint", PlayerAccountInfo.Me.arenaPoint);
        }
        PlayerPrefs.SetInt("arenapoint", PlayerAccountInfo.Me.arenaPoint);
        onClickRankResetLayer.Invoke();
    }
    
    void EnterProcess()
    {
        BackGroundPS.target.ChangeBGByElement(Element.redMagic);
        missionWatcher = new MissionWatcher(
            new List<string>
            {
                "itemsLoadFinished", "leaderBoardFinished"
            },
            () =>
            {
                SetLoaded(true);
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
                arenaLayer.SetupArenaTicket();
                arenaLayer.SetSeasonCountDown(timeUntilSettlement);
                arenaLayer.ShowMyTeamByLeaderInfo(_myLeaderboardInfo);
                arenaLayer.DisplayOpponents(opponents, _myLeaderboardInfo);
                ReturnLayer.MoveBack();
            },
            ()=>
            {
                SetLoaded(true);
                PreScene.ReturnToLobby();
            });
        
        LoadLeaderboardInfos();
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
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
        DayOfWeek settlementDay = DayOfWeek.Sunday;
        TimeSpan settlementTime = new TimeSpan(15, 0, 0); // 设置竞技场结算时间为UTC时间每周日的 15:00:00，即日本时间每周日晚上12点
        
        //DayOfWeek settlementDay = DayOfWeek.Monday;
        //TimeSpan settlementTime = new TimeSpan(7, 10, 0); // 设置竞技场结算时间为UTC时间每周日的 15:00:00，即日本时间每周日晚上12点
        GetServerTime(settlementDay, settlementTime);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<ArenaNewSeason>();
        UILayerLoader.Remove<ArenaLayer>();
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