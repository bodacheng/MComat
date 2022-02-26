using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DummyLayerSystem;

namespace mainMenu
{
    public class FightPrepareLayer : UILayer
    {
        public Text QuestName;
        public HeroIcon FighterIcon;
        public RectTransform myTeamShowT;
        public RectTransform enemyTeamShowT;
        public Button EditTeamButton; // 根据进入战斗模式决定是否显示
        public Button BeginFight;
        
        static FightPrepareLayer Get()
        {
            var l = UILayerLoader.Get("FightPrepareLayer");
            FightPrepareLayer returnValue = null;
            if (l != null)
            {
                returnValue = l as FightPrepareLayer;
            }
            return returnValue;
        }
        
        public static FightPrepareLayer Open()
        {
            var returnValue = Get();
            if (returnValue != null)
            {
                return returnValue;
            }
            returnValue = UILayerLoader.Load(PreScene.target.T,"FightPrepareLayer") as FightPrepareLayer;
            return returnValue;
        }
        
        public void StageMembersInfoShow(FightInfo stage)
        {
            MemberInfosShow(stage.fightMembers.HeroSets.GetValues(), myTeamShowT);
            MemberInfosShow(stage.fightMembers.EnemySets.GetValues(), enemyTeamShowT);
        }
        
        List<HeroIcon> MemberInfosShow(List<UnitInfo> HeroSets, RectTransform _ShowT)
        {
            foreach (Transform transform in _ShowT)
            {
                Destroy(transform.gameObject);
            }
            List<HeroIcon> icons = new List<HeroIcon>();
            foreach(UnitInfo oneMember in HeroSets)
            {
                icons.Add(HeroIcon.ArrangeHeroIconToT(FighterIcon, oneMember, _ShowT));
            }
            for (int i = 0; i < icons.Count; i++)
            {
                icons[i].iconButton.targetGraphic.raycastTarget = true;
            }
            return icons;
        }
    }
}