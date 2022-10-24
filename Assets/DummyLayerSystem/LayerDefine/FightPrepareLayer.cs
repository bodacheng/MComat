using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class FightPrepareLayer : UILayer
{
    public Text QuestName;
    [SerializeField] HeroIcon FighterIcon;
    [SerializeField] RectTransform myTeamShowT;
    [SerializeField] RectTransform enemyTeamShowT;
    public Button EditTeamButton; // 根据进入战斗模式决定是否显示
    public Button BeginFight;

    [SerializeField] GameObject teamEditIndicator;
    public void ForcePressTeamEdit()
    {
        teamEditIndicator.gameObject.SetActive(true);
        BeginFight.gameObject.SetActive(false);
        // 强制玩家点击EditTeamButton按钮，待制作
    }
        
    public void StageMembersInfoShow(FightInfo stage)
    {
        MemberInfosShow(stage.FightMembers.HeroSets.GetValues(), myTeamShowT).Forget();
        MemberInfosShow(stage.FightMembers.EnemySets.GetValues(), enemyTeamShowT).Forget();
    }
        
    async UniTask<List<HeroIcon>> MemberInfosShow(List<UnitInfo> HeroSets, RectTransform _ShowT)
    {
        foreach (Transform transform in _ShowT)
        {
            Destroy(transform.gameObject);
        }
        List<HeroIcon> icons = new List<HeroIcon>();
        foreach(UnitInfo oneMember in HeroSets)
        {
            var v = await HeroIcon.ArrangeHeroIconToT(FighterIcon, oneMember, _ShowT);
            icons.Add(v);
        }
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].iconButton.targetGraphic.raycastTarget = true;
        }
        return icons;
    }
}