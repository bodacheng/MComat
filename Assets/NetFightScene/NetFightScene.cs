using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;
using mainMenu;
using Soul;
using UnityEngine.Playables;

namespace FightScene
{
    public class NetFightScene : MonoBehaviour
    {
        [Space(11)]
        [Header("Canvas")]
        public Canvas PreparingCanvas, FightCanvas;

        #region before fight
        [Space(11)]
        [Header("PlayableDirector")]
        public PlayableDirector playableDirector;
        [Space(11)]
        [Header("CountDownText")]
        public Text CountDown;
        #endregion

        [Space(11)]
        [Header("Basic Essentials")]
        public CameraManager _CameraManager;
        public CharsManager _CharSetManager;

        [Space(11)]
        [Header("战斗的最后一击时候的处理")]
        public FightOverControl _FightOverControl;

        [Space(11)]
        [Header("战斗信息记录器")]
        public FightLogger fightLogger;

        [Space(11)]
        [Header("场地控制")]
        public BoundaryControllByGod _BoundaryControllByGod;

        [Space(11)]
        public RectTransform PauseMenu;

        [Space(11)]
        [Header("双方站位点")]
        public Transform[] Team1StandPoints, Team2StandPoints;//这个也是应该按模式区分，能改名字现在就改名字吧。免得以后乱

        // 主进程
        [Space(7)]
        [Header("主进程处理器")]
        public SingleThreadProcesser mainProcessRunner;

        public static NetFightScene target;

        public ReactiveProperty<bool> LoadStageFinished { get; set; } = new ReactiveProperty<bool>(false);

        void Awake()
        {
            target = this;
            Screen.SetResolution(1920, 1080, true);
        }

        void Start()
        {
            //QualitySettings.vSyncCount = 1;
            Screen.SetResolution(1920, 1080, true);
            Application.targetFrameRate = 60;
            FightGlobalSetting.scenestep = 1;
            mainProcessRunner.Run(FightSceneStartUp());
        }

        IEnumerator FightSceneStartUp()
        {
            Time.timeScale = 1f;
            //Position_Set_Executor.Instance.P_sets.Clear();
            PreparingProcess preparingProcess = new PreparingProcess(this);
            FightingProcess fightingProcess = new FightingProcess(this);
            CountDownProcess countDownProcess = new CountDownProcess(this);
            StoryProcess storyProcess = new StoryProcess(this);
            FightOverProcess fightOverProcess = new FightOverProcess(this);
            FightSummaryProcess fightSummaryProcess = new FightSummaryProcess(this);

            BasicTryProcess basicTryProcess = new BasicTryProcess(this);

            FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
            FSceneProcessesRunner.Main.AddNewProcess(SceneStep.StoryBeforeFight, storyProcess);
            FSceneProcessesRunner.Main.AddNewProcess(SceneStep.CountDown, countDownProcess);
            FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
            FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightOver, fightOverProcess);
            FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightSummary, fightSummaryProcess);
            FSceneProcessesRunner.Main.AddNewProcess(SceneStep.BasicTryTutorial, basicTryProcess);
            
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
            yield break;
        }

        void Update()
        {
            FSceneProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.ProcessNagare();
        }

        // 本地系函数
        public void PressedStartButton()
        {
            if (RealTimeGameProcessManager.target.FightTeam1.TeamMode == TeamMode.test)
                RealTimeGameProcessManager.target.FightTeam1.LetAllCharactersChangeToTestMode();
            else
                RealTimeGameProcessManager.target.FightTeam1.ModeStart();
                
            if (RealTimeGameProcessManager.target.FightTeam2.TeamMode == TeamMode.test)
                RealTimeGameProcessManager.target.FightTeam2.LetAllCharactersChangeToTestMode();
            else
                RealTimeGameProcessManager.target.FightTeam2.ModeStart();
                
            switch (RealTimeGameProcessManager.playerTeam)
            {
                case Team.player1:
                    RealTimeGameProcessManager.target.SwitchToCMode(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values[0], false);
                    break;
                case Team.player2:
                    RealTimeGameProcessManager.target.SwitchToCMode(RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values[0], false);
                    break;
            }
            RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
        }

        // 这个函数应该包括一些更深层的考虑。
        public void ReturnToFront()
        {
            //Position_Set_Executor.Instance.P_sets.Clear();
            List<Data_Center> player1 = RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values;
            List<string> dontdestroy = new List<string>();

            List<SingleFightLog> singleFightLogs = new List<SingleFightLog>();
            for (int i = 0; i < player1.Count; i++)
            {
                if (player1[i] != null)
                {
                    singleFightLogs.Add(player1[i]._MyBehaviorRunner.SingleFightLog);
                    player1[i]._MyBehaviorRunner.ChangeState("Empty");
                    CharDataInfo characterDataInfo = RealTimeGameProcessManager.target.FightTeam1.CharDataInfoRef[player1[i]];
                    if (characterDataInfo != null)
                    {
                        dontdestroy.Add(characterDataInfo.monsterOfPlayerId);
                    }
                }
            }

            List<Data_Center> player2 = RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values;
            for (int i = 0; i < player2.Count; i++)
            {
                if (player2[i] != null)
                {
                    singleFightLogs.Add(player2[i]._MyBehaviorRunner.SingleFightLog);
                }
            }

            _CharSetManager.PreventTheseMyModelsFromDestroying(dontdestroy);
            RealTimeGameProcessManager.target.Clear();

            if (FightGlobalSetting.HitBoxLogger)
            {
                HitBoxLogTable.Instance.Load(HitBoxLogger.Instance.LoadCurrentToString());
                HitBoxLogger.Instance.LogSummit();
                for (int i = 0; i < singleFightLogs.Count; i++)
                {
                    singleFightLogs[i].Summary();
                }
                HitBoxLogTable.Instance.SaveByCurrentRows_HitBoxLog(Application.persistentDataPath + "/HitBoxLog.csv", HitBoxLogger.Instance, singleFightLogs);
                for (int i = 0; i < singleFightLogs.Count; i++)
                {
                    singleFightLogs[i].Clear();
                }
                HitBoxLogger.Instance.Clear();
            }

            FSceneProcessesRunner.Main.Clear();
            MainMenuNote.goingtostep = MainSceneStep.FrontPage;
            SceneManager.LoadScene(1);
        }

        //本地系函数 
        public void LocalGameRestart()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
            SceneManager.LoadScene(FightSceneNote.nextBattle.BattleGroundID);
        }

        // 本地系函数 而且目前有逻辑问题
        public void ResumeScene()
        {
            PauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        // 本地系函数 而且目前有逻辑问题
        public void PauseScene()
        {
            PauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void PassFightSummary()
        {
            FightSummaryProcess process = (FightSummaryProcess)FSceneProcessesRunner.Main.GetProcess(SceneStep.FightSummary);
            process.enternext.Value = true;
        }
    }
}