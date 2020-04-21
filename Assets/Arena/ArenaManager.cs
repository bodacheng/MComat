using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using dataAccess;

// 其他模块很多东西应该给拿过来用。。
// 1.战斗图标生成 
// 如果五个列的话应该是五个点击触发按钮，点哪个就打哪个

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager target;
    
    public RectTransform ArenaCanvas;
    public HeroIcon member1, member2, member3;
    
    public HeroIcon FighterIcon;
    public ArenaFightTeamDisplay Fight1, Fight2, Fight3, Fight4;
    // 提供一个战斗列表供玩家选择
    List<StageScriptableObject> FightList = new List<StageScriptableObject>();

    public object ChangeHeroIconByMonsterOfPlayerId { get; private set; }

    void Awake()
    {
        target = this;
    }
    
    public IEnumerator ShowMyTeam()
    {
        string Pos1MonsterOfPlayerId = TeamSet.Instance.Default.GetPosMonsterOfPlayerId(0);
        string Pos2MonsterOfPlayerId = TeamSet.Instance.Default.GetPosMonsterOfPlayerId(1);
        string Pos3MonsterOfPlayerId = TeamSet.Instance.Default.GetPosMonsterOfPlayerId(2);

        yield return TeamEditManager.ChangeHeroIconByMonsterOfPlayerId(Pos1MonsterOfPlayerId,member1);
        yield return TeamEditManager.ChangeHeroIconByMonsterOfPlayerId(Pos2MonsterOfPlayerId,member2);
        yield return TeamEditManager.ChangeHeroIconByMonsterOfPlayerId(Pos3MonsterOfPlayerId,member3);
    }
    
    public StageScriptableObject RandomStage()
    {
        StageScriptableObject stage = ScriptableObject.CreateInstance<StageScriptableObject>();
        stage.localFight = StagesManager.RandomFight();
        stage.BattleGroundID = 2;
        stage.Team1Mode = TeamMode.rotation;
        stage.Team2Mode = TeamMode.rotation;
        stage._fightEventType = FightEventType.Arena;
        return stage;
    }
    
    public IEnumerator LoadFourChallenge()
    {
        FightList.Clear();
        FightList.Add(RandomStage());
        FightList.Add(RandomStage());
        FightList.Add(RandomStage());
        FightList.Add(RandomStage());
        
        for (int i = 0; i < 4; i++)
        {
            switch(i)
            {
                case 0:
                    Fight1.AddFightToList(FighterIcon,FightList[0]);
                break;
                case 1:
                    Fight2.AddFightToList(FighterIcon,FightList[1]);
                break;
                case 2:
                    Fight3.AddFightToList(FighterIcon,FightList[2]);
                break;
                case 3:
                    Fight4.AddFightToList(FighterIcon,FightList[3]);
                break;
            }
        }
        yield break;
    }
}
