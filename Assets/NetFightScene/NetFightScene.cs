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
        public Canvas PreparingCanvas, FightCanvas , ScreensaverCanvas;
        
        #region before fight
        [Space(11)]
        [Header("PlayableDirector")]
        public PlayableDirector playableDirector;
        [Space(11)]
        [Header("CountDownText")]
        public Text CountDown;
        #endregion

        [Space(11)]
        [Header("战斗的最后一击时候的处理")]
        public Button NextLevelButton;
        
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
        
        [Space(11)]
        [Header("双方站位点_观看点")]
        public Transform WatchTeam1, WatchTeam2;
        
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
            BasicTryProcess basicTryProcess = new BasicTryProcess(this);
            
            switch(FightSceneNote.nextBattle._fightEventType)
            {
                case FightEventType.SkillTest:
                case FightEventType.Self:
                case FightEventType.Arena:
                case FightEventType.Test:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.CountDown, countDownProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightOver, fightOverProcess);
                    break;
                case FightEventType.Quest:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.StoryBeforeFight, storyProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.CountDown, countDownProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightOver, fightOverProcess);
                    break;
                case FightEventType.Screensaver:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    break;
            }
            FSceneProcessesRunner.Main.ArrangeProcessOrder();           
            
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
            HurtObjectManager.ConstructDPool();
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
            RealTimeGameProcessManager.target.FightTeam1.ModeStart();
            
            if (FightSceneNote.nextBattle._fightEventType == FightEventType.Test)
            {
                RealTimeGameProcessManager.target.FightTeam2.LetAllCharactersChangeToTestMode();
            }
            else
            {
                RealTimeGameProcessManager.target.FightTeam2.ModeStart();
            }
            
            switch (RealTimeGameProcessManager.playerTeam)
            {
                case Team.player1:
                    RealTimeGameProcessManager.target.SwitchToCMode(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values[0], false);
                    break;
                case Team.player2:
                    RealTimeGameProcessManager.target.SwitchToCMode(RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values[0], false);
                    break;
            }
            if (FightSceneNote.nextBattle._fightEventType == FightEventType.Screensaver)
                RealTimeGameProcessManager.target.ScreenSaverC(RealTimeGameProcessManager.playerTeam);
            else
                RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
        }
        
        public void SkillLog(List<Data_Center> player1, List<Data_Center> player2)
        {
            List<SingleFightLog> singleFightLogs = new List<SingleFightLog>();
            for (int i = 0; i < player1.Count; i++)
            {
                if (player1[i] != null)
                {
                    singleFightLogs.Add(player1[i]._MyBehaviorRunner.SingleFightLog);
                }
            }
            for (int i = 0; i < player2.Count; i++)
            {
                if (player2[i] != null)
                {
                    singleFightLogs.Add(player2[i]._MyBehaviorRunner.SingleFightLog);
                }
            }

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
        }
        
        // 这个函数应该包括一些更深层的考虑。
        public void ReturnToFront()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.None);
            
            //Position_Set_Executor.Instance.P_sets.Clear();
            List<Data_Center> player1 = RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values;
            List<string> dontdestroy = new List<string>();
            switch (FightSceneNote.nextBattle._fightEventType)
            {
                case FightEventType.Arena:
                case FightEventType.Quest:
                    for (int i = 0; i < player1.Count; i++)
                    {
                        if (player1[i] != null)
                        {
                            player1[i]._MyBehaviorRunner.ChangeState("Empty");
                            CharDataInfo charDataInfo = RealTimeGameProcessManager.target.FightTeam1.CharDataInfoRef[player1[i]];
                            if (charDataInfo != null)
                            {
                                dontdestroy.Add(charDataInfo.monsterOfPlayerId);
                            }
                        }
                    }
                break;
            }
            SkillLog(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values,RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values);
            RealTimeGameProcessManager.target.Clear();
            FSceneProcessesRunner.Main.Clear();
            MainMenuNote.goingtostep = MainSceneStep.FrontPage;
            HitBoxesProcesser.Instance.processingDecompositioners.Clear();
            SceneManager.LoadScene(1);
        }

        //本地系函数 
        public void LocalGameRestart()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
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
        
        // ArcadeNext
        public void CheckNextArcadeLevel()
        {
            if (FightSceneNote.nextBattle._fightEventType == FightEventType.Quest)
            {
                if (ArcadeManager.ArcadeStages.ContainsKey(FightSceneNote.nextBattle.LocalFightID + 1))
                {
                    NextLevelButton.onClick.RemoveAllListeners();
                    void LoadNextLevel()
                    {
                        FightSceneNote.nextBattle = ArcadeManager.ArcadeStages[FightSceneNote.nextBattle.LocalFightID + 1].stageConfig;
                        FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
                    }
                    NextLevelButton.onClick.AddListener(LoadNextLevel);
                    NextLevelButton.gameObject.SetActive(true);
                }else{
                    NextLevelButton.gameObject.SetActive(false);
                }
            }else{
                NextLevelButton.gameObject.SetActive(false);
            }
        }
    }
}