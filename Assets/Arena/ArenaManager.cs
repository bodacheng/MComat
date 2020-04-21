using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using dataAccess;
using mainMenu;

// 其他模块很多东西应该给拿过来用。。
// 1.战斗图标生成 
// 如果五个列的话应该是五个点击触发按钮，点哪个就打哪个

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager target;
    
    public RectTransform ArenaCanvas;
    public ArenaFightTeamDisplay myTeam;
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
        string Pos1MonsterOfPlayerId = TeamSet.Instance.Arena3V3.GetPosMonsterOfPlayerId(0);
        string Pos2MonsterOfPlayerId = TeamSet.Instance.Arena3V3.GetPosMonsterOfPlayerId(1);
        string Pos3MonsterOfPlayerId = TeamSet.Instance.Arena3V3.GetPosMonsterOfPlayerId(2);
        
        yield return HeroIcon.ChangeHeroIconByMonsterOfPlayerId(Pos1MonsterOfPlayerId,myTeam.member1);
        yield return HeroIcon.ChangeHeroIconByMonsterOfPlayerId(Pos2MonsterOfPlayerId,myTeam.member2);
        yield return HeroIcon.ChangeHeroIconByMonsterOfPlayerId(Pos3MonsterOfPlayerId,myTeam.member3);
        
        void GoToTeamEdit()
        {
            PreScene.Instance.arcadeTeamManager.SwitchTargetTeam(TeamSetGameMode.arena3V3);
            PreScene.Instance.trySwitchToStep(MainSceneStep.TeamEditFront,true);
        }
        myTeam.PrepareFightButton.onClick.RemoveAllListeners();
        myTeam.PrepareFightButton.onClick.AddListener(GoToTeamEdit);
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
                    yield return Fight1.AddFightToList(FightList[0]);
                break;
                case 1:
                    yield return Fight2.AddFightToList(FightList[1]);
                break;
                case 2:
                    yield return Fight3.AddFightToList(FightList[2]);
                break;
                case 3:
                    yield return Fight4.AddFightToList(FightList[3]);
                break;
            }
        }
    }
}
