using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System.Collections;
using dataAccess;

// 该模块只用于竞技场front画面的玩家队伍显示和挑战敌人队伍显示
public class ArenaFightTeamDisplay : MonoBehaviour
{
    public Text RanKInfo;
    public HeroIcon member1, member2, member3;
    public Button BigButton;
        
    // 本函数唯一用途是竞技场的挑战玩家选择画面里每组敌人图标按钮的外观与功能加载
    public IEnumerator AddFightToList(StageScriptableObject _SO)
    {
        // 竞技场模式下毫无考虑敌人“多组上场”的情况
        for (int index = 0; index < _SO.localFight.EnemySets._SerializableSets.Length; index++)
        {
            for (int index2 = 0; index2 < _SO.localFight.EnemySets._SerializableSets[index].value.Length; index2++)
            {
                int posNum = _SO.localFight.EnemySets._SerializableSets[index].value[index2]._Key2;
                CharDataInfo charDataInfo = _SO.localFight.EnemySets._SerializableSets[index].value[index2]._Value;
                HeroIcon target = null;
                switch(posNum)
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
                HeroIcon.ChangeHeroIconByMonsterID(charDataInfo.ResourceID, target);
            }
        }
        
        BigButton.onClick.RemoveAllListeners();
        void PrepareForIt()
        {
            QuestPreparePage.target.PreLoad(_SO, TeamSetGameMode.arena3V3);
            PreScene.Instance.trySwitchToStep(MainSceneStep.QuestInfo,true);
        }
        BigButton.onClick.AddListener(PrepareForIt);
        yield break;
    }
    
    // myTeam 机能加载
    public IEnumerator ShowMyTeam()
    {
        string Pos1MonsterOfPlayerId = TeamSet.Arena3V3.GetMonsterOfPlayerIdOnPos(0);
        string Pos2MonsterOfPlayerId = TeamSet.Arena3V3.GetMonsterOfPlayerIdOnPos(1);
        string Pos3MonsterOfPlayerId = TeamSet.Arena3V3.GetMonsterOfPlayerIdOnPos(2);
        
        yield return HeroIcon.ChangeHeroIconByMonsterOfPlayerId(Pos1MonsterOfPlayerId, member1);
        yield return HeroIcon.ChangeHeroIconByMonsterOfPlayerId(Pos2MonsterOfPlayerId, member2);
        yield return HeroIcon.ChangeHeroIconByMonsterOfPlayerId(Pos3MonsterOfPlayerId, member3);
        
        void GoToTeamEdit()
        {
            TeamSet.SwitchTargetTeam(TeamSetGameMode.arena3V3);
            PreScene.Instance.trySwitchToStep(MainSceneStep.TeamEditFront,true);
        }
        BigButton.onClick.RemoveAllListeners();
        BigButton.onClick.AddListener(GoToTeamEdit);
    }
}
