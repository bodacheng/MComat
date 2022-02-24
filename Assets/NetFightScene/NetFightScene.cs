using System.Collections;
using UnityEngine;
using UniRx;
using UnityEngine.Playables;
using System.Collections.Generic;
using DummyLayerSystem;

namespace FightScene
{
    public class NetFightScene : MonoBehaviour
    { 
        public RectTransform T;
        
        #region before fight
        [Header("PlayableDirector")]
        public PlayableDirector playableDirector;
        #endregion
        
        [Header("双方站位点")]
        public Transform[] Team1StandPoints, Team2StandPoints;//这个也是应该按模式区分，能改名字现在就改名字吧。免得以后乱
        
        [Header("FX")]
        public Camera fxCamera;
        
        // 主进程
        [Header("主进程处理器")]
        public SingleThreadProcesser mainProcessRunner;
        
        [Header("AudioSource")]
        public AudioSource audioSource;
        
        public static NetFightScene target;
        
        public ReactiveProperty<bool> LoadStageFinished { get; set; } = new ReactiveProperty<bool>(false);

        public static FightInfo Fight;

        void Awake()
        {
            target = this;
        }
        
        void Start()
        {
            UILayerLoader.Clear();
            PopupLayer.DarkOff(T.gameObject, 1, 0);
            
            //QualitySettings.vSyncCount = 1;
            Screen.SetResolution(1920, 1080, true);
            
            AppSetting.bgmSource = audioSource;
            AppSetting.Load();
            
            Application.targetFrameRate = 60;
            FightGlobalSetting.scenestep = 1;
            SingleThreadProcesser.backup = mainProcessRunner;
            Time.timeScale = 1f;
            //Position_Set_Executor.Instance.P_sets.Clear();
            var preparingProcess = new PreparingProcess();
            var fightingProcess = new FightingProcess();
            var countDownProcess = new CountDownProcess();
            var storyProcess = new StoryProcess();
            var fightResultAnim = new FightResultAnim();
            var fightOverProcess = new FightOverProcess();
            var basicTryProcess = new BasicTryProcess();
            
            switch(Fight.GetEventType())
            {
                case FightEventType.SkillTest:
                case FightEventType.Self:
                case FightEventType.Arena:
                case FightEventType.Test:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.CountDown, countDownProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightResultAnim, fightResultAnim);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightOver, fightOverProcess);
                    break;
                case FightEventType.Quest:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.StoryBeforeFight, storyProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.CountDown, countDownProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightResultAnim, fightResultAnim);
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
            RTFightManager.target.ModeStart();
            
            switch (RTFightManager.playerTeam)
            {
                case Team.player1:
                    RTFightManager.target.SwitchToCMode(RTFightManager.target.Team1Members.GetValues()[0], false);
                    break;
                case Team.player2:
                    RTFightManager.target.SwitchToCMode(RTFightManager.target.Team2Members.GetValues()[0], false);
                    break;
            }
            if (Fight.GetEventType() == FightEventType.Screensaver)
                RTFightManager.target.ScreenSaverC(RTFightManager.playerTeam);
        }
    }
}