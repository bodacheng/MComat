using System.Collections;
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
    bool gameOver = false;

    List<Team> deadTeam = new List<Team>();
    
    public bool ifGameOver()
    {
        return gameOver;
    }
    
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
        }
        winner = Team.none;
        gameOver = false;
    }
    
    public void TeamMemberDeathProcessing(IDictionary<Team, List<Data_Center>> fighters)
    {
        foreach (KeyValuePair<Team,List<Data_Center>> _KeyValuePair in fighters)
        {
            foreach (Data_Center _char in _KeyValuePair.Value)
            {
                if (!TeamDeadMemberDictionary[_KeyValuePair.Key].Contains(_char))
                {
                    if (_char.BO_Health._health <= 0)
                    {
                        _char.AIStateRunner.changeState("Death");//为什么要这样做呢。如果这样做的话。。。存在什么隐患，现在有没有啥问题
                        TeamDeadMemberDictionary[_KeyValuePair.Key].Add(_char);
                    }
                }
            }
            if (_KeyValuePair.Value.Count == TeamDeadMemberDictionary[_KeyValuePair.Key].Count)
            {
                if (!deadTeam.Contains(_KeyValuePair.Key))
                    deadTeam.Add(_KeyValuePair.Key);
            }
            if (wholeteamCount == deadTeam.Count + 1)
            {
                gameOver = true;
                List<Team> allteams =fighters.Keys.ToList();
                List<Team> _winner = allteams.Except(deadTeam).ToList();
                winner = _winner[0];
            }
        }
    }
}
