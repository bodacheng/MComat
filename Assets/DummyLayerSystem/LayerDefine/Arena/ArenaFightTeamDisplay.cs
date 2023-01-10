using System;
using UnityEngine;
using UnityEngine.UI;

// 该模块只用于竞技场front画面的玩家队伍显示和挑战敌人队伍显示
public class ArenaFightTeamDisplay : MonoBehaviour
{
    [SerializeField] Text displayName;
    [SerializeField] Text rank;
    [SerializeField] ArenaRankIcon rankIcon;
    [SerializeField] Text arenaPoint;
    [SerializeField] Text plusArenaPoint;
    [SerializeField] HeroIcon member1, member2, member3;
    [SerializeField] Button bigButton;

    void SetUpCommonInfo(LeaderboardInfo info, Action<UnitInfo> onClickUnitIcon = null)
    {
        displayName.text = info.PlayerLeaderboardEntry.DisplayName;
        rank.text = info.PlayerLeaderboardEntry.Position.ToString();
        arenaPoint.text =  info.PlayerLeaderboardEntry.StatValue.ToString();
        rankIcon.Set(info.PlayerLeaderboardEntry.StatValue);
        
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
            if (onClickUnitIcon != null)
                target.iconButton.SetListener(() => { onClickUnitIcon(unitInfo); });
        }
    }
    
    // 本函数唯一用途是竞技场的挑战玩家选择画面里每组敌人图标按钮的外观与功能加载
    public void AddFightToList(LeaderboardInfo info, LeaderboardInfo myInfo, Action<FightInfo> tryBeginStage)
    {
        SetUpCommonInfo(info);
        var stage = LeaderBoardInfoToFightInfo(info);
        
        bigButton.onClick.AddListener(()=> tryBeginStage(stage));
        
        plusArenaPoint.text = "+" +((info.PlayerLeaderboardEntry.StatValue - myInfo.PlayerLeaderboardEntry.StatValue) / 2);
        plusArenaPoint.gameObject.SetActive(true);
    }

    FightInfo LeaderBoardInfoToFightInfo(LeaderboardInfo info)
    {
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
        return stage;
    }
    
    public void ArenaRankingShow(LeaderboardInfo info, Action<UnitInfo> onClickUnitIcon)
    {
        SetUpCommonInfo(info, onClickUnitIcon);
        plusArenaPoint.gameObject.SetActive(false);
    }
}
