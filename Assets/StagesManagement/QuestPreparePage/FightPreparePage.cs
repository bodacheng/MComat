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
        
        public void StageMembersInfoShow(StageScriptableObject stage)
        {
            foreach (Transform _child in myTeamShowT)
            {
                Destroy(_child.gameObject);
            }
            foreach (Transform _child in enemyTeamShowT)
            {
                Destroy(_child.gameObject);
            }
            
            MemberInfosShow(stage.localFight.HeroSets.values, myTeamShowT);
            MemberInfosShow(stage.localFight.EnemySets.values, enemyTeamShowT);
        }
        
        public static List<HeroIcon> MemberInfosShow(List<CharDataInfo> HeroSets, RectTransform _ShowT)
        {
            List<HeroIcon> icons = new List<HeroIcon>();
            foreach(CharDataInfo oneMember in HeroSets)
            {
                icons.Add(HeroIcon.ArrangeHeroIconToT(target.FighterIcon, oneMember, _ShowT));
            }
            return icons;
        }
    }
}