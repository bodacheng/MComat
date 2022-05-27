using UnityEngine;
using UnityEngine.UI;
using mainMenu;

// 该模块只用于竞技场front画面的玩家队伍显示和挑战敌人队伍显示
public class ArenaFightTeamDisplay : MonoBehaviour
{
    public Text displayName;
    public Text score;
    public HeroIcon member1, member2, member3;
    public Button BigButton;
        
    // 本函数唯一用途是竞技场的挑战玩家选择画面里每组敌人图标按钮的外观与功能加载
    public void AddFightToList(CloudScript.LeaderboardInfo LInfo)
    {
        displayName.text = LInfo.PlayerLeaderboardEntry.DisplayName;
        score.text = LInfo.PlayerLeaderboardEntry.StatValue.ToString();
        
        // 竞技场模式下毫无考虑敌人“多组上场”的情况
        for (int index = 0; index < LInfo.Team.Length; index++)
        {
            int posNum = LInfo.Team[index].key2;
            UnitInfo unitInfo = LInfo.Team[index].value;
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
            HeroIcon.ChangeHeroIconByRid(unitInfo.r_id, target);
        }
        
        FightMembers fightMembers = new FightMembers
        {
            EnemySets =
            {
                _SerializableSets = LInfo.Team
            }
        };
        fightMembers.EnemySets.ConvertSerializableArrayToDictionary();
        FightInfo stage = FightInfo.ArenaStage(fightMembers);
        stage.team2ID = LInfo.PlayerLeaderboardEntry.PlayFabId;
        stage.SetEventType(FightEventType.Arena);
        
        BigButton.onClick.RemoveAllListeners();
        void PrepareForIt()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, stage, true);
        }
        BigButton.onClick.AddListener(PrepareForIt);
    }
}
