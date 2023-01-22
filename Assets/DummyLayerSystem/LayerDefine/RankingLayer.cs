using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ModelView;
using UnityEngine;
using UnityEngine.UI;

public class RankingLayer : UILayer
{
    [SerializeField] VerticalLayoutGroup enemiesT;
    [SerializeField] ArenaFightTeamDisplay myArenaFightTeamDisplay;
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
        
        miniNineForShow.AddOnClickToSlots((RECORD_ID) =>
        {
            var skillConfig = SkillConfigTable.GetSkillConfig(RECORD_ID);
            _cameraConnector.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
        });
        
        _cameraConnector.ShowModel(unitInfo.r_id).Forget();
    }

    public void SetMyLeaderboardInfo(LeaderboardInfo myTeamInfo)
    {
        myArenaFightTeamDisplay.ArenaRankingShow(myTeamInfo, OnClickUnitIcon);
    }
    
    public void DisplayOpponents(List<LeaderboardInfo> leaderboards)
    {
        float rectHeight = 0;
        foreach (var t in leaderboards)
        {
            var o = Instantiate(arenaFightTeamDisplayPrefab);
            o.ArenaRankingShow(t, OnClickUnitIcon);
            o.transform.SetParent(enemiesT.transform);
            o.transform.localPosition = Vector3.zero;
            o.transform.localScale = Vector3.one;
            o.gameObject.SetActive(true);
            
            rectHeight += o.GetComponent<RectTransform>().rect.height + enemiesT.spacing;
        }
        enemiesT.GetComponent<RectTransform>().sizeDelta = 
            new Vector2(enemiesT.GetComponent<RectTransform>().sizeDelta.x, rectHeight);
    }
}
