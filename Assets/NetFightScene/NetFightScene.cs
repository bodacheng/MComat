using UnityEngine;
using UniRx;
using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using ModelView;
using UnityEngine.SceneManagement;

namespace FightScene
{
    public class NetFightScene : MonoBehaviour
    {
        public RectTransform T;
        
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
        
        public ReactiveProperty<bool> LoadStageFinished { get; set; } = new(false);

        public static FightInfo Fight;

        void Awake()
        {
            target = this;
        }
        
        void Start()
        {
            AnimationResourceLoader.Instance.Clear();
            DedicatedCameraConnector.ClearBackUpModels();
            AddressablesLogic.ReleaseAsyncOperationHandles();
            HurtObjectManager.Clear();
            EffectsManager.Clear();
            
            UILayerLoader.Clear();
            UILayerLoader.SetHanger(T);
            
            HighLightLayer.DarkOff(Color.white, 0, true);
            Time.timeScale = 1;
            if (Fight == null)
            {
                return;
            }
            
            //QualitySettings.vSyncCount = 1;
            Screen.SetResolution(1920, 1080, true);
            
            AppSetting.bgmSource = audioSource;
            AppSetting.Load();
            Application.targetFrameRate = 60;
            FightGlobalSetting._sceneStep = 1;
            
            SingleThreadProcesser.backup = mainProcessRunner;
            //Position_Set_Executor.Instance.P_sets.Clear();
            var preparingProcess = new PreparingProcess();
            var countDownProcess = new CountDownProcess();
            var fightingProcess = new FightingProcess();
            var fightResultAnim = new FightResultAnim();
            var fightOverProcess = new FightOverProcess();
            var basicTryProcess = new BasicTryProcess();
            
            FSceneProcessesRunner.Main.Clear();
            switch(Fight.EventType)
            {
                case FightEventType.SkillTest:
                case FightEventType.Self:
                case FightEventType.Arena:
                case FightEventType.Quest:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
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
        }
        
        void Update()
        {
            FSceneProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.Process();
        }
        
        public void ReturnToFront()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.None);
            var data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RTFightManager.target.team1.TeamMembers.GetValues());
            data_Centers.AddRange(RTFightManager.target.team2.TeamMembers.GetValues());
            RTFightManager.target.ClearUnitData();
            RTFightManager.target.ClearUnits();
            FightLogger.value.WatchMissionsAbandon();
            FSceneProcessesRunner.Main.Clear();
            MainMenuNote.GoingTo = MainSceneStep.FrontPage;
            HitBoxesProcesser.Instance.Clear();
            SingleAssignmentDisposableCleaner.Clear();
            SceneManager.LoadScene(1);
        }
    }
}