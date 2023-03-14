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
    [SerializeField] Button beginFight;
    [SerializeField] GameObject teamEditIndicator;
    
    public void SetFightBeginFeature(Action fightBegin)
    {
        beginFight.onClick.RemoveAllListeners();
        beginFight.onClick.AddListener(()=>fightBegin());
    }

    public void SetTeamEditFeature(Action teamEdit)
    {
        editTeamButton.onClick.RemoveAllListeners();
        editTeamButton.onClick.AddListener(()=>teamEdit());
    }
    
    public void ForcePressTeamEdit()
    {
        teamEditIndicator.gameObject.SetActive(true);
        beginFight.gameObject.SetActive(false);
        // 强制玩家点击EditTeamButton按钮，待制作
    }
        
    public void StageMembersInfoShow(FightInfo stage)
    {
        MemberInfosShow(stage.FightMembers.HeroSets.GetValues(), myTeamShowT, true);
        MemberInfosShow(stage.FightMembers.EnemySets.GetValues(), enemyTeamShowT, false);
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