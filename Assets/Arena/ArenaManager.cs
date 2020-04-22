using UnityEngine;
using System.Collections;
using dataAccess;
using mainMenu;

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager target;
    
    public RectTransform ArenaCanvas;
    public ArenaFightTeamDisplay myTeam; // 玩家队伍显示
    public ArenaFightTeamDisplay Fight1, Fight2, Fight3, Fight4; // 挑战玩家队伍显示
    
    void Awake()
    {
        target = this;
    }
    
    // myTeam 机能加载
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
            PreScene.Instance.TeamEditor.SwitchTargetTeam(TeamSetGameMode.arena3V3);
            PreScene.Instance.trySwitchToStep(MainSceneStep.TeamEditFront,true);
        }
        myTeam.BigButton.onClick.RemoveAllListeners();
        myTeam.BigButton.onClick.AddListener(GoToTeamEdit);
    }
    
    // 挑战玩家队伍机能加载（目前规定显示在画面上的挑战组一共四个。远程获取不到的情况下就本地生成）
    public IEnumerator LoadFourChallenge()
    {
        yield return Fight1.AddFightToList(StageScriptableObject.RandomStage());
        yield return Fight2.AddFightToList(StageScriptableObject.RandomStage());
        yield return Fight3.AddFightToList(StageScriptableObject.RandomStage());
        yield return Fight4.AddFightToList(StageScriptableObject.RandomStage());
    }
}
