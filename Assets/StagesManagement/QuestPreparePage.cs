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

        public static QuestPreparePage target;

        void Awake()
        {
            target = this;
        }

        // 队伍编辑按钮功能 放在按钮上。
        public void EditTeamButtonBehaviour()
        {
            PreScene.Instance.trySwitchToStep(MainSceneStep.TeamEditFront, true);
        }
                
        // 这个函数目前是固定使用“默认队伍配置”
        public IEnumerator LoadStageByScriptThenGetReadyForIt(StageScriptableObject _SO)
        {
            QuestName.text = _SO.battleNameJPG;
            _SO.LoadLocalFightFromScript(_SO.Script);
            IEnumerator getPlayerOne = TeamSet.Instance.MyTeamByEntryLimit(_SO.EntryMemberNum, TeamSet.Instance.Default);
            yield return getPlayerOne;
            if (getPlayerOne.Current == null)
            {
                Debug.Log("获取我方人员错误");
                yield break;
            }
            if (_SO.localFight == null)
            {
                                Debug.Log("あれ？");
                yield break;
            }
            _SO.localFight.HeroSets = (MultiDictionary<int, int, CharDataInfo>)getPlayerOne.Current;
            if (_SO.localFight.HeroSets == null)
            {
                Debug.Log("严重错误。get不到队员"); yield break;
            }
            mainProcessRunner.Run(GetReadyToBattle(_SO));
            yield break;
        }
        
        //这个函数只考虑了队员的加载。。。
        public IEnumerator GetReadyToBattle(StageScriptableObject stage)
        {
            target.QuestPreparePageCanvas.gameObject.SetActive(true);
            EnterQuest.gameObject.SetActive(false);
            LoadingCanvas.target.DarkOff(1f);
            StageMembersInfoShow(stage);
            EnterQuest.onClick.RemoveAllListeners();
            void Go()
            {
                PreScene.Instance.AskIfLoadFight(stage);
            }
            EnterQuest.onClick.AddListener(Go);
            EnterQuest.gameObject.SetActive(true);
            PreScene.Instance.trySwitchToStep(MainSceneStep.QuestInfo, true);
            LoadingCanvas.target.LightUp();
            yield break;
        }
        
        //
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