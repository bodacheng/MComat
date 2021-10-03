using UniRx;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 用于在每一局游戏里起记录数据的作用，包括胜利判断，都应该是由本模块来执行。
public class FightLogger
{
    public ReactiveProperty<bool> GameOver{ get; set; } = new ReactiveProperty<bool>(false);
    readonly IDictionary<Team, List<Data_Center>> TeamDeadMemberDic = new Dictionary<Team, List<Data_Center>>();
    readonly List<Team> deadTeam = new List<Team>();
    readonly List<SingleAssignmentDisposable> WatchPlayerers = new List<SingleAssignmentDisposable>();
    Team winnerTeam = Team.none;
    int wholeteamCount;
    
    Dictionary<Team, string> IdDicRef = new Dictionary<Team, string>();
    
    public string GetWinnerId()
    {
        return IdDicRef[winnerTeam];
    }
    
    public Team GetWinnerTeam()
    {
        return winnerTeam;
    }
    
    public void WatchMissionsAbandon()
    {
        deadTeam.Clear();
        for (int i = 0; i < WatchPlayerers.Count; i++)
        {
            if (!WatchPlayerers[i].IsDisposed)
            {
                WatchPlayerers[i].Dispose();
            }
        }
        WatchPlayerers.Clear();
        GameOver.Value = false;
    }
    
    public void ReadyToLog(IDictionary<TeamConfig, List<Data_Center>> TeamMembers)
    {
        wholeteamCount = 0;
        TeamDeadMemberDic.Clear();
        deadTeam.Clear();
        GameOver.Value = false;
        winnerTeam = Team.none;
        IdDicRef.Clear();
        
        foreach (KeyValuePair<TeamConfig, List<Data_Center>> pair in TeamMembers)
        {
            IdDicRef.Add(pair.Key.myTeam, pair.Key.playID);
            Debug.Log(pair.Key.myTeam+ " 我" + pair.Key.playID);
            
            TeamDeadMemberDic.Add(pair.Key.myTeam, new List<Data_Center>());
            wholeteamCount += 1;
            foreach (Data_Center data_Center in pair.Value)
            {
                var disposable = new SingleAssignmentDisposable();
                disposable.Disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (data_Center.IsDead.Value == true)
                    {
                        TeamDeadMemberDic[pair.Key.myTeam].Add(data_Center);
                        if (pair.Value.Count == TeamDeadMemberDic[pair.Key.myTeam].Count)
                        {
                            if (!deadTeam.Contains(pair.Key.myTeam))
                                deadTeam.Add(pair.Key.myTeam);
                        }
                        if (wholeteamCount == deadTeam.Count + 1) // 胜负已决
                        {
                            GameOver.Value = true;
                            List<Team> allteams = TeamMembers.Keys.ToList().Select(x => x.myTeam).ToList();
                            List<Team> _winner = allteams.Except(deadTeam).ToList();
                            winnerTeam = _winner[0];
                        }
                        disposable.Dispose();
                    }
                });
                WatchPlayerers.Add(disposable);
            }
        }
    }
}