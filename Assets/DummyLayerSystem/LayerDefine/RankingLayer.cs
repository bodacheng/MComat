using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ModelView;
using UnityEngine;

public class RankingLayer : UILayer
{
    [SerializeField] RectTransform EnemiesT;
    [SerializeField] ArenaFightTeamDisplay arenaFightTeamDisplayPrefab;
    [SerializeField] DedicatedCameraConnector _cameraConnector;
    [SerializeField] NineForShow miniNineForShow;
    
    void OnClickUnitIcon(UnitInfo unitInfo)
    {
        var set = unitInfo.set;
        miniNineForShow.ShowStones(
            set.a1, set.a2, set.a3,
            set.b1, set.b2, set.b3,
            set.c1, set.c2, set.c3
        ).Forget();

        _cameraConnector._ShowModel(unitInfo.r_id).Forget();
    }
    
    public void DisplayOpponents(List<LeaderboardInfo> leaderboards)
    {
        foreach (Transform c in EnemiesT)
        {
            Destroy(c.gameObject);
        }
        
        foreach (var t in leaderboards)
        {
            var o = Instantiate(arenaFightTeamDisplayPrefab);
            o.ArenaRankingShow(t, OnClickUnitIcon);
            o.transform.SetParent(EnemiesT);
            o.transform.localPosition = Vector3.zero;
            o.transform.localScale = Vector3.one;
            o.gameObject.SetActive(true);
        }
    }
}
