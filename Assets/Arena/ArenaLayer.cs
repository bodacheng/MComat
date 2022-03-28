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
    
    [SerializeField] Button RefreshBtn;
    [SerializeField] RectTransform EnemiesT;
    [SerializeField] ArenaFightTeamDisplay ArenaFightTeamDisplayPrefab;

    CloudScript.LeaderboardInfo myLeaderboardInfo;
    readonly ArenaDummiesTable table = new ArenaDummiesTable();
    
    void Awake()
    {
        RefreshBtn.onClick.AddListener(RefreshOpponent);
    }

    void Start()
    {
        table.Load();
    }

    public void RefreshOpponent()
    {
        PopupLayer.Loading(">", PreScene.target.T);
        View(false);
        CloudScript.GetLeaderboardAroundUser(
            obj =>
            {
                var exceptSelf = new List<CloudScript.LeaderboardInfo>();
                foreach (var t in obj)
                {
                    Debug.Log( "读取到以下玩家信息 : " +t.PlayerLeaderboardEntry.PlayFabId);
                    if (t.PlayerLeaderboardEntry.PlayFabId != PlayerAccountInfo.Me.PlayFabUsername)
                    {
                        exceptSelf.Add(t);
                    }
                    else
                    {
                        myLeaderboardInfo = t;
                    }
                }
                if (myLeaderboardInfo != null)
                {
                    myScore.text = myLeaderboardInfo.PlayerLeaderboardEntry.StatValue.ToString();
                    myRank.text = "Rank :" + myLeaderboardInfo.PlayerLeaderboardEntry.Position;
                }
                else
                {
                    myScore.gameObject.SetActive(false);
                    myRank.gameObject.SetActive(false);
                }
                
                ShowMyTeam();
                
                if (exceptSelf.Count < 3)
                {
                    var myPoint = (myLeaderboardInfo != null) ? myLeaderboardInfo.PlayerLeaderboardEntry.StatValue : 1000;
                    var list = table.GetOpponentAroundPoint(myPoint);
                    for (var i = 0; i < list.Count; i++)
                    {
                        exceptSelf.Add(list[i]);
                        if (exceptSelf.Count == 3)
                        {
                            break;
                        }
                    }
                }
                
                LoadArena(exceptSelf);
                View(true);
                PopupLayer.Close();
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
        foreach (Transform c in EnemiesT)
        {
            Destroy(c.gameObject);
        }
        
        foreach (var t in leaderboards)
        {
            var o = Instantiate(ArenaFightTeamDisplayPrefab);
            o.AddFightToList(t);
            o.transform.SetParent(EnemiesT);
            o.transform.localPosition = Vector3.zero;
            o.transform.localScale = Vector3.one;
        }
    }
    
    void ShowMyTeam()
    {
        string Pos1InstanceID = TeamSet.Arena3V3.GetInstanceIdOnPos(0);
        string Pos2InstanceID = TeamSet.Arena3V3.GetInstanceIdOnPos(1);
        string Pos3InstanceID = TeamSet.Arena3V3.GetInstanceIdOnPos(2);
        
        HeroIcon.ChangeHeroIconByInstanceId(Pos1InstanceID, member1);
        HeroIcon.ChangeHeroIconByInstanceId(Pos2InstanceID, member2);
        HeroIcon.ChangeHeroIconByInstanceId(Pos3InstanceID, member3);
        
        void GoToTeamEdit()
        {
            PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
        }
        EditMyTeamBtn.onClick.RemoveAllListeners();
        EditMyTeamBtn.onClick.AddListener(GoToTeamEdit);
    }

    void View(bool on)
    {
        member1.gameObject.SetActive(on);
        member2.gameObject.SetActive(on);
        member3.gameObject.SetActive(on);
    }
}