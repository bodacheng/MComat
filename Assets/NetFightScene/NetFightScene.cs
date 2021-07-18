using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Playables;
using System.Collections.Generic;

namespace FightScene
{
    public class NetFightScene : MonoBehaviour
    {
        [Space(11)] public RectTransform T;
        
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
        [Header("战斗信息记录器")]
        public FightLogger fightLogger;
        
        [Space(11)]
        [Header("场地控制")]
        public BoundaryControllByGod _BoundaryControllByGod;
        
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

        public static FightInfo Fight;

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
            SingleThreadProcesser.backup = mainProcessRunner;
            Time.timeScale = 1f;
            //Position_Set_Executor.Instance.P_sets.Clear();
            PreparingProcess preparingProcess = new PreparingProcess();
            FightingProcess fightingProcess = new FightingProcess();
            CountDownProcess countDownProcess = new CountDownProcess();
            StoryProcess storyProcess = new StoryProcess();
            FightOverProcess fightOverProcess = new FightOverProcess();
            BasicTryProcess basicTryProcess = new BasicTryProcess();
            
            switch(Fight.GetEventType())
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
            
            if (Fight.GetEventType() == FightEventType.Test)
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
                    RealTimeGameProcessManager.target.SwitchToCMode(RealTimeGameProcessManager.target.FightTeam1.TeamMembers.GetValues()[0], false);
                    break;
                case Team.player2:
                    RealTimeGameProcessManager.target.SwitchToCMode(RealTimeGameProcessManager.target.FightTeam2.TeamMembers.GetValues()[0], false);
                    break;
            }
            if (Fight.GetEventType() == FightEventType.Screensaver)
                RealTimeGameProcessManager.target.ScreenSaverC(RealTimeGameProcessManager.playerTeam);
        }
        
        public IEnumerator SKillTestReload()
        {
            int i = 0;
            foreach (KeyValuePair<Data_Center,CharDataInfo> keyValuePair in RealTimeGameProcessManager.target.FightTeam1.CharDataInfoRef)
            {
                switch(i)
                {
                    case 0:
                    case 1:
                    case 2:
                        keyValuePair.Value.set = SkillSet.RandomSkillSet("human", null, 1, false);
                        break;
                    case 3:
                        keyValuePair.Value.set = SkillSet.RandomSkillSet("human", null, 1, false);
                        break;
                }
                
                CharConfig _CharConfig = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(keyValuePair.Value.r_id));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value.set, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
                i++;
            }
            i = 0;
            foreach (KeyValuePair<Data_Center,CharDataInfo> keyValuePair in RealTimeGameProcessManager.target.FightTeam2.CharDataInfoRef)
            {
                switch(i)
                {
                    case 0:
                    case 1:
                    case 2:
                        keyValuePair.Value.set = SkillSet.RandomSkillSet("human", null, 1, false);
                        break;
                    case 3:
                        keyValuePair.Value.set = SkillSet.RandomSkillSet("human", null, 1, false);
                        break;
                }
                
                CharConfig _CharConfig = MonstersConfigTable.RowToCharConfigInfo(MonstersConfigTable.Find_RECORD_ID(keyValuePair.Value.r_id));
                yield return keyValuePair.Key.Step2Initialize(_CharConfig.TYPE, keyValuePair.Value.set, _CharConfig._zokusei, _CharConfig.SPECIAL_ZOKUSEI);
                i++;
            }
            FightOverControl.target.LocalGameRestart();
        }
    }
}