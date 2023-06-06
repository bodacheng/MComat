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
    [SerializeField] Button editTeamButton; // 根据进入战斗模式决定是否显示
    [SerializeField] GameObject teamEditIndicator;
    [SerializeField] Text teamEditIndicatorText;
    [SerializeField] FightModeSwitch fightModeSwitch;
    [SerializeField] FightBeginBtn beginFight;
    [SerializeField] Text team1OneWord;
    [SerializeField] Text team2OneWord;
    [SerializeField] Text arcadeStageNoText;
    [SerializeField] Button toArcadeFrontBtn;
    
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
        arcadeStageNoText.text = "Stage " + arcadeStageNo;
        toArcadeFrontBtn.gameObject.SetActive(true);
        toArcadeFrontBtn.onClick.AddListener(()=>toArcadeFront());
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
        else if (dataAccess.Units.Dic.Count > 0 && stage.FightMembers.HeroSets.GetValues().Count == 0)
        {
            teamEditIndicatorText.text = Translate.Get("MakeYourTeam");
            teamEditIndicator.SetActive(true);
        }
        else
        {
            teamEditIndicator.SetActive(false);
        }
        
        MemberInfosShow(stage.FightMembers.EnemySets.GetValues(), enemyTeamShowT, false);
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

    public void TutorialForceFightBegin()
    {
        editTeamButton.gameObject.SetActive(false);
    }
}