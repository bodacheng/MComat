using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    public class QuestPreparePage : MonoBehaviour
    {
        public SingleThreadProcesser mainProcessRunner;

        [Space(7)]
        [Header("UI elements")]
        public Canvas QuestPreparePageCanvas;
        public Text QuestName;
        public HeroIcon FighterIcon;//多种属性框？
        public Button EnterQuest;
        public RectTransform myTeamShowT;
        public RectTransform enemyTeamShowT;
        public Button EditTeamButton; // 根据进入战斗模式决定是否显示
        public static QuestPreparePage target;

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
            PosKeySet set = null;
            switch(ToBeLoadMode)
            {
                case TeamSetGameMode.arena3V3:
                    void GoToTeamEdit_Arena()
                    {
                        TeamSet.SwitchTargetTeam(TeamSetGameMode.arena3V3);
                        PreScene.Instance.trySwitchToStep(MainSceneStep.TeamEditFront,true);
                    }
                    EditTeamButton.gameObject.SetActive(true);
                    EditTeamButton.onClick.RemoveAllListeners();
                    EditTeamButton.onClick.AddListener(GoToTeamEdit_Arena);
                    set = TeamSet.Arena3V3;
                    IEnumerator getArenaTeam = TeamSet.MyTeamByEntryLimit(ToBeLoad.EntryMemberNum, set);
                    yield return getArenaTeam;
                    if (getArenaTeam.Current == null)
                    {
                        Debug.Log("获取我方人员错误");
                        yield break;
                    }
                    ToBeLoad.localFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)getArenaTeam.Current;
                    break;
                case TeamSetGameMode.story:
                    void GoToTeamEdit_Arcade()
                    {
                        TeamSet.SwitchTargetTeam(TeamSetGameMode.story);
                        PreScene.Instance.trySwitchToStep(MainSceneStep.TeamEditFront,true);
                    }
                    EditTeamButton.gameObject.SetActive(true);
                    EditTeamButton.onClick.RemoveAllListeners();
                    EditTeamButton.onClick.AddListener(GoToTeamEdit_Arcade);
                    set = TeamSet.Default;
                    IEnumerator getDefaultTeamSet = TeamSet.MyTeamByEntryLimit(ToBeLoad.EntryMemberNum, set);
                    yield return getDefaultTeamSet;
                    if (getDefaultTeamSet.Current == null)
                    {
                        Debug.Log("获取我方人员错误");
                        yield break;
                    }
                    ToBeLoad.localFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)getDefaultTeamSet.Current;
                    break;
                case TeamSetGameMode.selfFight:
                    EditTeamButton.gameObject.SetActive(false);
                break;
            }
            
            target.QuestPreparePageCanvas.gameObject.SetActive(true);
            EnterQuest.gameObject.SetActive(false);
            StageMembersInfoShow(ToBeLoad);
            EnterQuest.onClick.RemoveAllListeners();
            void Go()
            {
                PreScene.Instance.AskIfLoadFight(ToBeLoad);
            }
            EnterQuest.onClick.AddListener(Go);
            EnterQuest.gameObject.SetActive(true);
            yield break;
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