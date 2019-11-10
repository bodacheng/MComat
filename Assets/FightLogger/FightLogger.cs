using UniRx;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// 用于在每一局游戏里起记录数据的作用，包括胜利判断，都应该是由本模块来执行。
public class FightLogger : MonoBehaviour
{
    IDictionary<Team, List<Data_Center>> TeamDeadMemberDictionary = new Dictionary<Team, List<Data_Center>>();
    Team winner = Team.none;
    Data_Center finalSurviver;
    int wholeteamCount = 0;
    public ReactiveProperty<bool> gameOver{ get; set; } = new ReactiveProperty<bool>(false);

    public List<Team> deadTeam = new List<Team>();
        
    public Team getWinner()
    {
        return winner;
    }
    
    public void ReadyToLog(IDictionary<Team, List<Data_Center>> TeamMembers)
    {
        TeamDeadMemberDictionary.Clear();
        foreach (KeyValuePair<Team, List<Data_Center>> pair in TeamMembers)
        {
            TeamDeadMemberDictionary.Add(pair.Key,new List<Data_Center>());
            wholeteamCount += 1;
            foreach (Data_Center data_Center in pair.Value)
            {
                data_Center.IsDead.Where(isDead => isDead == true)
                .Subscribe(_ =>
                {
                    TeamDeadMemberDictionary[pair.Key].Add(data_Center);
                    if (pair.Value.Count == TeamDeadMemberDictionary[pair.Key].Count)
                    {
                        if (!deadTeam.Contains(pair.Key))
                            deadTeam.Add(pair.Key);
                    }
                    if (wholeteamCount == deadTeam.Count + 1)
                    {
                        gameOver.Value = true;
                        List<Team> allteams = TeamMembers.Keys.ToList();
                        List<Team> _winner = allteams.Except(deadTeam).ToList();
                        winner = _winner[0];
                    }
                });
            }
        }
        winner = Team.none;
        gameOver.Value = false;       
    }
}
