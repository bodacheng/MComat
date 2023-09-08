using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FightPrepareLayer : UILayer
{
    [SerializeField] HeroIcon fighterIcon;
    [SerializeField] RectTransform myTeamShowT;
    [SerializeField] RectTransform enemyTeamShowT;
    [SerializeField] float unitIconSize = 200;
    [SerializeField] BOButton editTeamButton; // 根据进入战斗模式决定是否显示
    [SerializeField] GameObject teamEditIndicator;
    [SerializeField] Text teamEditIndicatorText;
    [SerializeField] FightModeSwitch fightModeSwitch;
    [SerializeField] FightBeginBtn beginFight;
    [SerializeField] Text team1OneWord;
    [SerializeField] Text team2OneWord;
    [SerializeField] Text arcadeStageNoText;
    [SerializeField] RewardUI rewardUI;
    [SerializeField] Button toArcadeFrontBtn;

    #region Gangbang
    [SerializeField] GangbangHeroIcon gangbangFighterIcon;
    [SerializeField] private Text team1WholeCount;
    [SerializeField] private Text team2WholeCount;
    private Func<int, string, int, int> _setTeamUnitCount;
    private Func<int, string, int> _getTeamUnitCount;
    #endregion
    
    public void SetFightMode(int fightMode)
    {
        fightModeSwitch.Setup(fightMode, PlayerPrefs.GetInt("preferAdventureMode",  PlayerPrefs.GetInt("preferAdventureMode", 2)));
    }
    
    public TeamMode GetSetFightMode()
    {
        return fightModeSwitch.TeamMode;
    }
    
    public void SetFightBeginFeature(Action fightBegin)
    {
        beginFight.SetAction(fightBegin);
    }
    
    public void SetFightBeginEnableRender(bool canFight)
    {
        beginFight.Enable(canFight);
    }

    public void SetTeamEditFeature(Action teamEdit)
    {
        editTeamButton.onClick.RemoveAllListeners();
        editTeamButton.onClick.AddListener(()=> teamEdit());
    }

    public void SetArcadeFeature(Action toArcadeFront, string arcadeStageNo)
    {
        arcadeStageNoText.gameObject.SetActive(true);
        arcadeStageNoText.text = "Stage " + arcadeStageNo;
        toArcadeFrontBtn.gameObject.SetActive(PlayerAccountInfo.Me.tutorialProgress == "Finished");
        toArcadeFrontBtn.onClick.AddListener(()=> toArcadeFront());
        
        var rewardDic = PlayFabReadClient.StageAwards;
        var reward = rewardDic[arcadeStageNo];
        rewardUI.ShowRewards(reward.d,reward.g);
        int.TryParse(arcadeStageNo, out var arcadeStageNoInt);
        rewardUI.AwardRender(PlayerAccountInfo.Me.arcadeProcess + 1 > arcadeStageNoInt);
        rewardUI.gameObject.SetActive(true);
    }
    
    public void SetGangbangFeature(
        Action toArcadeFront, string arcadeStageNo, 
        Func<int, string ,int, int> setTeamUnitCount, Func<int, string, int> getTeamUnitCount)
    {
        arcadeStageNoText.gameObject.SetActive(true);
        arcadeStageNoText.text = "Stage " + arcadeStageNo;
        toArcadeFrontBtn.gameObject.SetActive(PlayerAccountInfo.Me.tutorialProgress == "Finished");
        toArcadeFrontBtn.onClick.AddListener(()=> toArcadeFront());
        rewardUI.gameObject.SetActive(false);
        
        _setTeamUnitCount = (i, s, arg3) =>
        {
            var returnValue = setTeamUnitCount(i, s, arg3);
            if (i == 1)
            {
                team1WholeCount.text = returnValue.ToString();
            }
            else
            {
                team2WholeCount.text = returnValue.ToString();
            }
            return setTeamUnitCount(i,s,arg3);
        };
        _getTeamUnitCount = getTeamUnitCount;
    }
    
    public void ForcePressTeamEdit()
    {
        teamEditIndicator.gameObject.SetActive(true);
        beginFight.gameObject.SetActive(false);
        // 强制玩家点击EditTeamButton按钮，待制作
    }
        
    public void StageMembersInfoShow(FightInfo stage, string oneWordTeam1, string oneWordTeam2)
    {
        MemberInfosShow(stage.FightMembers.HeroSets.GetValues(), myTeamShowT, true);
        if (dataAccess.Units.Dic.Count >= 3 && stage.FightMembers.HeroSets.GetValues().Count < 3)
        {
            teamEditIndicatorText.text = Translate.Get("HasExtraSeat");
            teamEditIndicator.SetActive(true);
        }
        else //if (dataAccess.Units.Dic.Count > 0 && stage.FightMembers.HeroSets.GetValues().Count == 0)
        {
            teamEditIndicatorText.text = Translate.Get("MakeYourTeam");
            teamEditIndicator.SetActive(true);
        }

        MemberInfosShow(stage.FightMembers.EnemySets.GetValues(), enemyTeamShowT, false);
        team1OneWord.text = oneWordTeam1;
        team2OneWord.text = oneWordTeam2;
    }
    
    public void GangbangStageMembersInfoShow(GangbangInfo stage, string oneWordTeam1, string oneWordTeam2)
    {
        GangbangInfosShow(stage.FightMembers.HeroSets.GetValues(), myTeamShowT, true, 1);
        if (dataAccess.Units.Dic.Count >= 3 && stage.FightMembers.HeroSets.GetValues().Count < 3)
        {
            teamEditIndicatorText.text = Translate.Get("HasExtraSeat");
            teamEditIndicator.SetActive(true);
        }
        else //if (dataAccess.Units.Dic.Count > 0 && stage.FightMembers.HeroSets.GetValues().Count == 0)
        {
            teamEditIndicatorText.text = Translate.Get("MakeYourTeam");
            teamEditIndicator.SetActive(true);
        }
        GangbangInfosShow(stage.FightMembers.EnemySets.GetValues(), enemyTeamShowT, false, 2);
        team1OneWord.text = oneWordTeam1;
        team2OneWord.text = oneWordTeam2;
    }
    
    List<HeroIcon> MemberInfosShow(List<UnitInfo> heroSets, RectTransform _showT, bool withSkillCheck)
    {
        foreach (Transform transform in _showT)
        {
            Destroy(transform.gameObject);
        }
        var icons = new List<HeroIcon>();
        foreach(var oneMember in heroSets)
        {
            var v = HeroIcon.ArrangeHeroIconToParent(fighterIcon, oneMember, _showT, unitIconSize, withSkillCheck);
            icons.Add(v);
        }
        return icons;
    }
    
    void GangbangInfosShow(List<UnitInfo> unitSets, RectTransform _showT, bool withSkillCheck, int team)
    {
        foreach (Transform transform in _showT)
        {
            Destroy(transform.gameObject);
        }

        int wholeTeamCount = 0;
        foreach(var oneMember in unitSets)
        {
            GangbangHeroIcon.ArrangeGangbangHeroIconToParent(
                (x) => _setTeamUnitCount(team, oneMember.id, x),
                ()=> _getTeamUnitCount(team, oneMember.id),
                gangbangFighterIcon, oneMember, _showT, unitIconSize, withSkillCheck, team == 1);

            wholeTeamCount += _getTeamUnitCount(team, oneMember.id);
        }

        if (wholeTeamCount > CommonSetting.GangbangModeMaxUnitPerTeam)
        {
            foreach (var oneMember in unitSets)
            {
                _setTeamUnitCount(team, oneMember.id, CommonSetting.GangbangModeMaxUnitPerTeam / unitSets.Count);
            }
        }
        
        if (team == 1)
        {
            team1WholeCount.text = wholeTeamCount.ToString();
        }
        else
        {
            team2WholeCount.text = wholeTeamCount.ToString();
        }
    }

    public void TutorialForceFightBegin()
    {
        editTeamButton.gameObject.SetActive(false);
    }
}