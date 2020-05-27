using UnityEngine;
using System.Collections;
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

    public void RefreshOpponent()
    {
        PreScene.target.mainProcessRunner.Run(ArenaManager.target.LoadArena());
    }

    // 挑战玩家队伍机能加载（目前规定显示在画面上的挑战组一共四个。远程获取不到的情况下就本地生成）
    public IEnumerator LoadArena()
    {
        yield return myTeam.ShowMyTeam();
        yield return Fight1.AddFightToList(StageScriptableObject.RandomStage());
        yield return Fight2.AddFightToList(StageScriptableObject.RandomStage());
        yield return Fight3.AddFightToList(StageScriptableObject.RandomStage());
        yield return Fight4.AddFightToList(StageScriptableObject.RandomStage());
    }
}