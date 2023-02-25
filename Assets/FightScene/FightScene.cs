using UnityEngine;
using UniRx;
using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using ModelView;
using UnityEngine.SceneManagement;

namespace FightScene
{
    public class FightScene : MonoBehaviour
    {
        public RectTransform T;
        
        [Header("双方站位点")]
        public Transform[] Team1StandPoints, Team2StandPoints;//这个也是应该按模式区分，能改名字现在就改名字吧。免得以后乱
        
        [Header("FX")]
        public Camera fxCamera;
        
        [Header("AudioSource")]
        public AudioSource audioSource;
        
        public static FightScene target;
        
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
            
            //HighLightLayer.DarkOff(Color.white, 0, true);
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
            
            //Position_Set_Executor.Instance.P_sets.Clear();
            var preparingProcess = new PreparingProcess();
            var countDownProcess = new CountDownProcess();
            var fightingProcess = new FightingProcess();
            var fightResultAnim = new FightResultAnim();
            var fightOverProcess = new FightOverProcess();
            
            FSceneProcessesRunner.Main.Clear();
            switch(Fight.EventType)
            {
                case FightEventType.Arena:
                case FightEventType.Quest:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.CountDown, countDownProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightResultAnim, fightResultAnim);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightOver, fightOverProcess);
                    break;
                case FightEventType.SkillTest:
                case FightEventType.Self:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
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
        }
        
        void Update()
        {
            FSceneProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.Process();
        }
        
        public void ReturnToFront()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.None);
            var dataCenters = new List<Data_Center>();
            dataCenters.AddRange(RTFightManager.Target.team1.teamMembers.GetValues());
            dataCenters.AddRange(RTFightManager.Target.team2.teamMembers.GetValues());
            RTFightManager.Target.ClearUnitData();
            RTFightManager.Target.ClearUnits();
            FightLogger.value.WatchMissionsAbandon();
            FSceneProcessesRunner.Main.Clear();
            MainMenuNote.GoingTo = MainSceneStep.FrontPage;
            HitBoxesProcesser.Instance.Clear();
            SingleAssignmentDisposableCleaner.Clear();
            SceneManager.LoadScene(1);
        }
    }
}