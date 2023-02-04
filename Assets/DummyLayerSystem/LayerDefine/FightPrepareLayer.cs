using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FightPrepareLayer : UILayer
{
    [SerializeField] HeroIcon fighterIcon;
    [SerializeField] RectTransform myTeamShowT;
    [SerializeField] RectTransform enemyTeamShowT;
    public Button editTeamButton; // 根据进入战斗模式决定是否显示
    public Button beginFight;

    [SerializeField] GameObject teamEditIndicator;
    public void ForcePressTeamEdit()
    {
        teamEditIndicator.gameObject.SetActive(true);
        beginFight.gameObject.SetActive(false);
        // 强制玩家点击EditTeamButton按钮，待制作
    }
        
    public void StageMembersInfoShow(FightInfo stage)
    {
        MemberInfosShow(stage.FightMembers.HeroSets.GetValues(), myTeamShowT);
        MemberInfosShow(stage.FightMembers.EnemySets.GetValues(), enemyTeamShowT);
    }
    List<HeroIcon> MemberInfosShow(List<UnitInfo> HeroSets, RectTransform _ShowT)
    {
        foreach (Transform transform in _ShowT)
        {
            Destroy(transform.gameObject);
        }
        var icons = new List<HeroIcon>();
        foreach(var oneMember in HeroSets)
        {
            var v = HeroIcon.ArrangeHeroIconToT(fighterIcon, oneMember, _ShowT);
            icons.Add(v);
        }
        return icons;
    }

    public void TutorialForceFightBegin()
    {
        editTeamButton.gameObject.SetActive(false);
    }
}