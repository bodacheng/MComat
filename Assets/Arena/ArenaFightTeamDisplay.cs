using System;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;

// 该模块只用于竞技场front画面的玩家队伍显示和挑战敌人队伍显示
public class ArenaFightTeamDisplay : MonoBehaviour
{
    public Text displayName;
    public Text rank;
    public Text arenaPoint;
    public HeroIcon member1, member2, member3;
    public Button BigButton;
        
    // 本函数唯一用途是竞技场的挑战玩家选择画面里每组敌人图标按钮的外观与功能加载
    public void AddFightToList(LeaderboardInfo info, Action<FightInfo> tryBeginStage)
    {
        displayName.text = info.PlayerLeaderboardEntry.DisplayName;
        rank.text = info.PlayerLeaderboardEntry.Position.ToString();
        arenaPoint.text =  info.PlayerLeaderboardEntry.StatValue.ToString();
        
        // 竞技场模式下毫无考虑敌人“多组上场”的情况
        for (var index = 0; index < info.Team.Length; index++)
        {
            var posNum = info.Team[index].key2;
            var unitInfo = info.Team[index].value;
            HeroIcon target = null;
            switch (posNum)
            {
                case 0:
                    target = member1;
                    break;
                case 1:
                    target = member2;
                    break;
                case 2:
                    target = member3;
                    break;
            }
            target.ChangeIcon(unitInfo);
        }
        
        var fightMembers = new FightMembers
        {
            EnemySets =
            {
                _SerializableSets = info.Team
            }
        };
        fightMembers.EnemySets.ConvertSerializableArrayToDictionary();
        var stage = FightInfo.ArenaStage(fightMembers);
        stage.Team2ID = info.PlayerLeaderboardEntry.PlayFabId;
        stage.EventType = FightEventType.Arena;
        stage.Team2ArenaPoint = info.PlayerLeaderboardEntry.StatValue;
        
        BigButton.onClick.RemoveAllListeners();
        BigButton.onClick.AddListener(()=> tryBeginStage(stage));
    }
    
    public void ArenaRankingShow(LeaderboardInfo info, Action<UnitInfo> onClickUnitIcon)
    {
        displayName.text = info.PlayerLeaderboardEntry.DisplayName;
        rank.text = info.PlayerLeaderboardEntry.Position.ToString();
        arenaPoint.text =  info.PlayerLeaderboardEntry.StatValue.ToString();
        
        var fightMembers = new FightMembers
        {
            EnemySets =
            {
                _SerializableSets = info.Team
            }
        };
        fightMembers.EnemySets.ConvertSerializableArrayToDictionary();
        
        for (var index = 0; index < info.Team.Length; index++)
        {
            var posNum = info.Team[index].key2;
            var unitInfo = info.Team[index].value;
            HeroIcon target = null;
            switch (posNum)
            {
                case 0:
                    target = member1;
                    break;
                case 1:
                    target = member2;
                    break;
                case 2:
                    target = member3;
                    break;
            }
            target.ChangeIcon(unitInfo);
            target.iconButton.SetListener(() => { onClickUnitIcon(unitInfo); });
        }
    }
}
