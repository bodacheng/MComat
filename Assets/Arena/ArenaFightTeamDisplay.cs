using UnityEngine;
using UnityEngine.UI;
using mainMenu;

// 该模块只用于竞技场front画面的玩家队伍显示和挑战敌人队伍显示
public class ArenaFightTeamDisplay : MonoBehaviour
{
    public Text displayName;
    public Text rank;
    public HeroIcon member1, member2, member3;
    public Button BigButton;
        
    // 本函数唯一用途是竞技场的挑战玩家选择画面里每组敌人图标按钮的外观与功能加载
    public void AddFightToList(CloudScript.LeaderboardInfo LInfo)
    {
        displayName.text = LInfo.PlayerLeaderboardEntry.DisplayName;
        rank.text = LInfo.PlayerLeaderboardEntry.Position.ToString();
        
        // 竞技场模式下毫无考虑敌人“多组上场”的情况
        for (var index = 0; index < LInfo.Team.Length; index++)
        {
            var posNum = LInfo.Team[index].key2;
            var unitInfo = LInfo.Team[index].value;
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
                _SerializableSets = LInfo.Team
            }
        };
        fightMembers.EnemySets.ConvertSerializableArrayToDictionary();
        var stage = FightInfo.ArenaStage(fightMembers);
        stage.team2ID = LInfo.PlayerLeaderboardEntry.PlayFabId;
        stage.EventType = FightEventType.Arena;
        stage.Team2ArenaPoint = LInfo.PlayerLeaderboardEntry.StatValue;
        
        BigButton.onClick.RemoveAllListeners();
        void PrepareForIt()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, stage, true);
        }
        BigButton.onClick.AddListener(PrepareForIt);
    }
}
