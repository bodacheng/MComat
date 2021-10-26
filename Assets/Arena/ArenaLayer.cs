using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using mainMenu;

public class ArenaLayer : UILayer
{
    public RectTransform ArenaCanvas;
    public ArenaFightTeamDisplay myTeam; // 玩家队伍显示
    
    public RectTransform EnemiesT;
    public ArenaFightTeamDisplay ArenaFightTeamDisplayPrefab;
    
    public void RefreshOpponent()
    {
        //PreScene.target.mainProcessRunner.RunAsQueued(target.LoadArena());
        CloudScript.GetLeaderboardAroundUser(
            (List<CloudScript.LeaderboardInfo> obj) =>
            {
                List<CloudScript.LeaderboardInfo> exceptSelf = new List<CloudScript.LeaderboardInfo>();
                for (int i = 0; i < obj.Count; i++)
                {
                    if (obj[i].PlayerLeaderboardEntry.PlayFabId != Account._AccInfo.playerID)
                    {
                        exceptSelf.Add(obj[i]);
                    }
                }
                LoadArena(exceptSelf);
                ArenaCanvas.gameObject.SetActive(true);
            } ,
            () =>
            {
                PreScene.ReturnToLobby("通讯错误");
            }
        );
    }
    
    // 挑战玩家队伍机能加载（目前规定显示在画面上的挑战组一共四个。远程获取不到的情况下就本地生成）
    public void LoadArena(List<CloudScript.LeaderboardInfo> leaderboards)
    {
        myTeam.ShowMyTeam();
        foreach (Transform c in EnemiesT)
        {
            Destroy(c.gameObject);
        }
        for (int i = 0; i < leaderboards.Count; i++)
        {
            ArenaFightTeamDisplay o = Instantiate(ArenaFightTeamDisplayPrefab);
            o.AddFightToList(leaderboards[i]);
            o.transform.SetParent(EnemiesT);
            o.transform.localPosition = Vector3.zero;
            o.transform.localScale = Vector3.one;
        }
    }
}