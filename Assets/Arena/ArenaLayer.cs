using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using mainMenu;
using UnityEngine.UI;

public class ArenaLayer : UILayer
{
    #region 玩家队伍
    [SerializeField] HeroIcon member1, member2, member3;
    [SerializeField] Button EditMyTeamBtn;
    [SerializeField] Text myScore;
    [SerializeField] Text myRank;
    #endregion 
    
    [SerializeField] RectTransform EnemiesT;
    [SerializeField] ArenaFightTeamDisplay ArenaFightTeamDisplayPrefab;

    CloudScript.LeaderboardInfo myTeamLeaderboardInfo;
    
    public void RefreshOpponent()
    {
        CloudScript.GetLeaderboardAroundUser(
            (List<CloudScript.LeaderboardInfo> obj) =>
            {
                List<CloudScript.LeaderboardInfo> exceptSelf = new List<CloudScript.LeaderboardInfo>();
                
                for (int i = 0; i < obj.Count; i++)
                {
                    Debug.Log(i + ":" +obj[i].PlayerLeaderboardEntry.PlayFabId);
                    if (obj[i].PlayerLeaderboardEntry.PlayFabId != Account._AccInfo.playerID)
                    {
                        exceptSelf.Add(obj[i]);
                    }
                    else
                    {
                        myTeamLeaderboardInfo = obj[i];
                    }
                }
                LoadArena(exceptSelf);
                if (myTeamLeaderboardInfo != null)
                {
                    myScore.text = myTeamLeaderboardInfo.PlayerLeaderboardEntry.StatValue.ToString();
                    myRank.text = "Rank :" + myTeamLeaderboardInfo.PlayerLeaderboardEntry.Position;
                }
            },
            () =>
            {
                PreScene.ReturnToLobby("通讯错误");
            }
        );
    }
    
    // 挑战玩家队伍机能加载（目前规定显示在画面上的挑战组一共四个。远程获取不到的情况下就本地生成）
    void LoadArena(List<CloudScript.LeaderboardInfo> leaderboards)
    {
        ShowMyTeam();
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
    
    void ShowMyTeam()
    {
        string Pos1MonsterOfPlayerId = TeamSet.Arena3V3.GetMonsterOfPlayerIdOnPos(0);
        string Pos2MonsterOfPlayerId = TeamSet.Arena3V3.GetMonsterOfPlayerIdOnPos(1);
        string Pos3MonsterOfPlayerId = TeamSet.Arena3V3.GetMonsterOfPlayerIdOnPos(2);
        
        HeroIcon.ChangeHeroIconByInstanceId(Pos1MonsterOfPlayerId, member1);
        HeroIcon.ChangeHeroIconByInstanceId(Pos2MonsterOfPlayerId, member2);
        HeroIcon.ChangeHeroIconByInstanceId(Pos3MonsterOfPlayerId, member3);
        
        void GoToTeamEdit()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
        }
        EditMyTeamBtn.onClick.RemoveAllListeners();
        EditMyTeamBtn.onClick.AddListener(GoToTeamEdit);
    }
}