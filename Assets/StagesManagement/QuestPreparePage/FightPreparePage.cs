using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace mainMenu
{
    public class FightPreparePage : MonoBehaviour
    {
        [Space(7)]
        [Header("UI elements")]
        public Canvas QuestPreparePageCanvas;
        public Text QuestName;
        public HeroIcon FighterIcon;
        public RectTransform myTeamShowT;
        public RectTransform enemyTeamShowT;
        public Button EditTeamButton; // 根据进入战斗模式决定是否显示
        public Button BeginFight;
        public static FightPreparePage target;
              
        void Awake()
        {
            target = this;
        }
        
        public void StageMembersInfoShow(FightInfo stage)
        {            
            MemberInfosShow(stage.localFight.HeroSets.values, myTeamShowT);
            MemberInfosShow(stage.localFight.EnemySets.values, enemyTeamShowT);
        }
        
        public static List<HeroIcon> MemberInfosShow(List<CharDataInfo> HeroSets, RectTransform _ShowT)
        {
            foreach (Transform transform in _ShowT)
            {
                Destroy(transform.gameObject);
            }
            List<HeroIcon> icons = new List<HeroIcon>();
            foreach(CharDataInfo oneMember in HeroSets)
            {
                icons.Add(HeroIcon.ArrangeHeroIconToT(target.FighterIcon, oneMember, _ShowT));
            }
            for (int i = 0; i < icons.Count; i++)
            {
                icons[i].iconButton.targetGraphic.raycastTarget = true;
            }
            return icons;
        }
    }
}