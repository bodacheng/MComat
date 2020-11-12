using UnityEngine;
using System.Collections;
using mainMenu;
using dataAccess;
using Api.Dto.Model;

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
    
    public void RefreshOpponent()
    {
        PreScene.target.mainProcessRunner.Run(target.LoadArena());
    }
    
    // 挑战玩家队伍机能加载（目前规定显示在画面上的挑战组一共四个。远程获取不到的情况下就本地生成）
    public IEnumerator LoadArena()
    {
        yield return myTeam.ShowMyTeam();
        if (Arena.rankOpponentsModel != null)
        {
            OneTeam OneTeam1 = Arena.rankOpponentsModel.strongTeam;
            OneTeam OneTeam2 = Arena.rankOpponentsModel.normalTeam1;
            OneTeam OneTeam3 = Arena.rankOpponentsModel.normalTeam2;
            OneTeam OneTeam4 = Arena.rankOpponentsModel.weakTeam;
            
            yield return Fight1.AddFightToList(StageScriptableObject.ArenaStage(OneTeam1.ToFightInfo()));
            yield return Fight2.AddFightToList(StageScriptableObject.ArenaStage(OneTeam2.ToFightInfo()));
            yield return Fight3.AddFightToList(StageScriptableObject.ArenaStage(OneTeam3.ToFightInfo()));
            yield return Fight4.AddFightToList(StageScriptableObject.ArenaStage(OneTeam4.ToFightInfo()));
        }else{
            yield return Fight1.AddFightToList(StageScriptableObject.RandomStage());
            yield return Fight2.AddFightToList(StageScriptableObject.RandomStage());
            yield return Fight3.AddFightToList(StageScriptableObject.RandomStage());
            yield return Fight4.AddFightToList(StageScriptableObject.RandomStage());
        }
    }
}