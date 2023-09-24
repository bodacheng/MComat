using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class FightPrepareLayer : UILayer
{
    #region Gangbang
    [SerializeField] GangbangHeroIcon gangbangFighterIcon;
    [SerializeField] private Text team1WholeCount;
    [SerializeField] private Text team2WholeCount;
    private Func<int, string, int, bool, int> _setTeamUnitCount;
    private Func<int, string, int> _getTeamUnitCount;
    #endregion
    
    public void SetGangbangFeature(
        Action toGangbangFront, string gangbangStageNo, 
        Func<int, string ,int, bool, int> setTeamUnitCount, Func<int, string, int> getTeamUnitCount)
    {
        arcadeStageNoText.gameObject.SetActive(true);
        arcadeStageNoText.text = "Stage " + gangbangStageNo;
        toArcadeFrontBtn.gameObject.SetActive(PlayerAccountInfo.Me.tutorialProgress == "Finished");
        toArcadeFrontBtn.SetListener(toGangbangFront);
        
        var rewardDic = PlayFabReadClient.GangbangAwards;
        var reward = rewardDic[gangbangStageNo];
        rewardUI.ShowRewards(reward.d,reward.g);
        int.TryParse(gangbangStageNo, out var arcadeStageNoInt);
        rewardUI.AwardRender(PlayerAccountInfo.Me.gangbangProcess + 1 > arcadeStageNoInt);
        rewardUI.gameObject.SetActive(true);
        
        _setTeamUnitCount = (i, s, arg3 ,f) =>
        {
            var returnValue = setTeamUnitCount(i, s, arg3,f);
            if (i == 1)
            {
                team1WholeCount.text =  Translate.Get("WholeUnitCount")+ " "+  returnValue + "/" + CommonSetting.GangbangModeMaxUnitPerTeam;
            }
            else
            {
                team2WholeCount.text = Translate.Get("WholeUnitCount")+ " "+  returnValue + "/" + CommonSetting.GangbangModeMaxUnitPerTeam;
            }
            return setTeamUnitCount(i,s,arg3,f);
        };
        _getTeamUnitCount = getTeamUnitCount;
    }
    
    public void GangbangStageMembersInfoShow(GangbangInfo stage, string oneWordTeam1, string oneWordTeam2)
    {
        GangbangInfosShow(stage.FightMembers.HeroSets.GetValues(), myTeamShowT, true, 1);
        //teamEditIndicator.SetActive(stage.GetGroupWholeUnitCount(1) < CommonSetting.GangbangModeMaxUnitPerTeam);
        if (stage.FightMembers.HeroSets.GetValues().Count < 1)
        {
            teamEditIndicatorText.text = Translate.Get("HasExtraSeat");
            teamEditIndicator.SetActive(true);
        }
        else //if (dataAccess.Units.Dic.Count > 0 && stage.FightMembers.HeroSets.GetValues().Count == 0)
        {
            teamEditIndicatorText.text = string.Empty; // Translate.Get("MakeYourTeam");
            teamEditIndicator.SetActive(false);
        }
        GangbangInfosShow(stage.FightMembers.EnemySets.GetValues(), enemyTeamShowT, false, 2);
        team1OneWord.text = oneWordTeam1;
        team2OneWord.text = oneWordTeam2;
    }
    
    void GangbangInfosShow(List<UnitInfo> unitSets, RectTransform showT, bool withSkillCheck, int team)
    {
        foreach (Transform t in showT)
        {
            Destroy(t.gameObject);
        }
        
        int wholeTeamCount = 0;
        foreach(var unitInfo in unitSets)
        {
            wholeTeamCount += _getTeamUnitCount(team, unitInfo.id);
        }
        if (wholeTeamCount > CommonSetting.GangbangModeMaxUnitPerTeam)
        {
            foreach (var unitInfo in unitSets)
            {
                _setTeamUnitCount(team, unitInfo.id, CommonSetting.GangbangModeMaxUnitPerTeam / unitSets.Count, true);
            }
        }
        
        wholeTeamCount = 0;
        foreach(var unitInfo in unitSets)
        {
            GangbangHeroIcon.ArrangeGangbangHeroIconToParent(
                (x) => _setTeamUnitCount(team, unitInfo.id, x, false),
                ()=> _getTeamUnitCount(team, unitInfo.id),
                gangbangFighterIcon, unitInfo, showT, withSkillCheck, team == 1, unitIconSize);

            wholeTeamCount += _getTeamUnitCount(team, unitInfo.id);
        }
        
        if (team == 1)
        {
            team1WholeCount.text = Translate.Get("WholeUnitCount")+ ":"+  wholeTeamCount + "/" + CommonSetting.GangbangModeMaxUnitPerTeam;
        }
        else
        {
            team2WholeCount.text = Translate.Get("WholeUnitCount")+ ":"+  wholeTeamCount + "/" + CommonSetting.GangbangModeMaxUnitPerTeam;
        }
    }
}
