using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    public partial class FightPreparePage : MonoBehaviour
    {
        [Space(7)]
        [Header("UI elements")]
        public Canvas QuestPreparePageCanvas;
        public Text QuestName;
        public HeroIcon FighterIcon;//多种属性框？
        public Button EnterQuest;
        public RectTransform myTeamShowT;
        public RectTransform enemyTeamShowT;
        public Button EditTeamButton; // 根据进入战斗模式决定是否显示
        public static FightPreparePage target;
        
        #region 待加载战斗信息
        StageScriptableObject ToBeLoad;
        TeamSetGameMode ToBeLoadMode;
        #endregion
        
        void Awake()
        {
            target = this;
        }
        
        public void PreLoad(StageScriptableObject stageScriptableObject, TeamSetGameMode teamSetGameMode)
        {
            ToBeLoad = stageScriptableObject;
            ToBeLoadMode = teamSetGameMode;
        }
        
        // 这个函数目前是固定使用“默认队伍配置”
        public IEnumerator GetReadyForStageASTeam()
        {
            QuestName.text = ToBeLoad.battleNameJPG;
            switch(ToBeLoadMode)
            {
                case TeamSetGameMode.arena3V3:
                    yield return Arena3V3();
                    break;
                case TeamSetGameMode.story:
                    yield return Arcade();
                    break;
                case TeamSetGameMode.SelfFight:
                    EditTeamButton.gameObject.SetActive(false);
                break;
            }
            
            target.QuestPreparePageCanvas.gameObject.SetActive(true);
            EnterQuest.gameObject.SetActive(false);
            StageMembersInfoShow(ToBeLoad);
            void Go()
            {
                CharsManager.target.PreventTheseMyModelsFromDestroying(ToBeLoad.GetTeam1EnterRingLocalIds(ToBeLoad.localFight));
                if (ToBeLoad.localFight.HeroSets.values.Count < 1 || ToBeLoad.localFight.EnemySets.values.Count < 1)
                {
                    LoadingCanvas.target.ArrangeWarnWindow("队伍人员不够。");
                    return;
                }
                PreScene.target.LoadFight(ToBeLoad);
            }
            EnterQuest.onClick.RemoveAllListeners();
            EnterQuest.onClick.AddListener(Go);
            EnterQuest.gameObject.SetActive(true);
        }
        
        void StageMembersInfoShow(StageScriptableObject stage)
        {
            foreach (Transform _child in myTeamShowT)
            {
                Destroy(_child.gameObject);
            }
            foreach (Transform _child in enemyTeamShowT)
            {
                Destroy(_child.gameObject);
            }
            foreach(CharDataInfo oneMember in stage.localFight.HeroSets.values)
            {
                HeroIcon.ArrangeHeroIconToT(FighterIcon,oneMember,myTeamShowT);
            }
            
            foreach(CharDataInfo oneMember in stage.localFight.EnemySets.values)
            {
                HeroIcon.ArrangeHeroIconToT(FighterIcon,oneMember,enemyTeamShowT);
            }
        }
    }
}