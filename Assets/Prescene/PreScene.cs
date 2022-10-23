using Cysharp.Threading.Tasks;
using UnityEngine;
using DummyLayerSystem;
using ModelView;
using UnityEngine.SceneManagement;

namespace mainMenu
{
    public class PreScene : MonoBehaviour
    {
        public static PreScene target;
        
        [Header("T")]
        public GameObject T;
        
        [Header("主进程处理器")]
        public SingleThreadProcesser mainProcessRunner;
        
        [Header("环境")]
        public CameraManager _CameraManager;
        
        [Header("UI 相机")] 
        public Camera FxCamera;
        
        [Header("Positions For Show")]
        public Transform MemDetailTargetPos;
        public Transform MemDetailWatchPos;
        
        [Header("Shader转换器")]
        public SwapAllModelShader _SwapAllModelShader;
        
        [Header("AudioSource")]
        public AudioSource audioSource;

        public RectTransform stonesTempContainer;
        
        public UnitInfo _focusing;
        
        //下面这个函数总是建立在monsterbox函数运行在前，而monsterbox会部署好所有展示用模
        public void SetFocusingUnit(string instanceID)
        {
            _focusing = dataAccess.Units.Get(instanceID);
            if (_focusing == null)
            {
                return;
            }
            
            var Ref = Units.GetUnitConfig(_focusing.r_id);
            if (Ref == null)
            {
                Debug.Log("No this unit:" + _focusing.r_id);
                return;
            }
            BackGroundPS.target.ChangeBGByElement(Ref.element);
        }
        
        void Awake()
        {
            target = this;
            SingleThreadProcesser.backup = mainProcessRunner;
        }

        void Start()
        {
            AnimationResourceLoader.Instance.Clear();
            DedicatedCameraConnector.ClearBackUpModels();
            AddressablesLogic.ReleaseAsyncOperationHandles();
            
            Screen.SetResolution(1920, 1080, true);
            UILayerLoader.Clear();
            UILayerLoader.SetHanger(T.transform);
            AppSetting.bgmSource = audioSource;
            AppSetting.Load();
            Time.timeScale = 1;
            FightGlobalSetting._sceneStep = 0;
            
            BasicPhase();
            ToInitialPhase();
            
            AddressablesLogic.Essentials().Forget();
        }
        
        public static void ReturnToLobby(string error)
        {
            PopupLayer.ArrangeConfirmWindow(PreScene.target.T, (() => { SceneManager.LoadScene(0);}), error);
        }
        
        void BasicPhase()
        {
            Application.targetFrameRate = 60;
            
            #region 主界面各大画面
            var settingPage = new SettingPage();
            var frontPage = new FrontPage();
            var teamEditFront = new TeamEditPage();
            var skillStones = new StonesPage();
            var stoneSell = new StoneSell();
            var selfFightFront = new SelfFightPage();
            var questInfo = new QuestInfoPage();
            var unitListPage = new UnitListPage();
            var memberDetail_edit = new SkillEditPage();
            var memberDetail_SkillShow = new SkillShowPage();
            var arcadeFrontPage = new ArcadeFrontPage();
            
            // Shop
            var shopTop = new ShopTop();
            var boxOverLoadFix = new BoxOverLoadFix();

            // Gotcha
            var gotchaFront = new GotchaFront();
            var gotchaResult = new GotchaResult();
            var arenaPage = new ArenaPage();
            
            // mail
            var mailBox = new MailBoxProcess();
            var mailDetail = new MailDetailProcess();
            
            ProcessesRunner.Main.Clear();
            ProcessesRunner.Main.Add(MainSceneStep.Setting, settingPage);
            ProcessesRunner.Main.Add(MainSceneStep.TeamEditFront, teamEditFront);
            ProcessesRunner.Main.Add(MainSceneStep.SkillStoneList, skillStones);
            ProcessesRunner.Main.Add(MainSceneStep.SkillStones_Sell, stoneSell);
            ProcessesRunner.Main.Add(MainSceneStep.SelfFightFront, selfFightFront);
            ProcessesRunner.Main.Add(MainSceneStep.QuestInfo, questInfo);
            ProcessesRunner.Main.Add(MainSceneStep.UnitList, unitListPage);
            ProcessesRunner.Main.Add(MainSceneStep.UnitSkillEdit, memberDetail_edit);
            ProcessesRunner.Main.Add(MainSceneStep.UnitSkillShow, memberDetail_SkillShow);
            ProcessesRunner.Main.Add(MainSceneStep.FrontPage, frontPage);
            ProcessesRunner.Main.Add(MainSceneStep.ArcadeFront, arcadeFrontPage);
            ProcessesRunner.Main.Add(MainSceneStep.Arena, arenaPage);
            ProcessesRunner.Main.Add(MainSceneStep.ShopTop, shopTop);
            ProcessesRunner.Main.Add(MainSceneStep.BoxOverLoadHelper, boxOverLoadFix);
            ProcessesRunner.Main.Add(MainSceneStep.MailBox, mailBox);
            ProcessesRunner.Main.Add(MainSceneStep.MailDetail, mailDetail);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaFront, gotchaFront);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaResult, gotchaResult);
            #endregion
        }
        
        void ToInitialPhase()
        {
            if (ReturnLayer.ReturnMissionList.Count > 0)
            {
                //ReturnLayer.AddFeatureToReturnButton();
                //从战斗画面返回后，进入战斗前的菜单往上跳一节，指的是站前准备画面
                ReturnLayer.POP();
            }
            else
            {
                trySwitchToStep(MainMenuNote.GoingTo, false);
            }
        }
        
        void Update()
        {
            ProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.Process();
        }

        public void AskIfLoadFight(FightInfo stage)
        {
            PopupLayer.ArrangeConfirmWindow(
                target.T,
                delegate {
                    FightLoad.Go(stage, true);
                }, "开打？");
        }

        public void BeginSkillTest_Rotation()
        {
            var stage = FightInfo.RandomSkillTestStage(TeamMode.rotation);
            stage.team1ID = PlayerAccountInfo.Me.PlayFabId;
            FightLoad.Go(stage);
        }
        
        public void BeginSkillTest_Multi()
        {
            var stage = FightInfo.RandomSkillTestStage(TeamMode.multiRaid);
            stage.team1ID = PlayerAccountInfo.Me.PlayFabId;
            FightLoad.Go(stage);
        }
        
        [EnumAction(typeof(MainSceneStep))]
        public void trySwitchToStep(MainSceneStep next_step, bool forward = true)
        {
            if (forward && ProcessesRunner.Main.currentProcess != null)
            {
                var returnToStep = ProcessesRunner.Main.currentProcess.Step;
                void returnToCurrent()
                {
                    trySwitchToStep(returnToStep, false);
                }
                ProcessesRunner.Main.ChangeProcess(next_step);
                ReturnLayer.PUSH(returnToCurrent);
            }
            else
            {
                ProcessesRunner.Main.ChangeProcess(next_step);
            }
        }

        public void trySwitchToStep<T>(MainSceneStep next_step, T t, bool forward)
        {
            if (forward && ProcessesRunner.Main.currentProcess != null)
            {
                var returnToStep = ProcessesRunner.Main.currentProcess.Step;
                void returnToCurrent()
                {
                    trySwitchToStep(returnToStep, false);
                }
                ProcessesRunner.Main.ChangeProcess(next_step, t);
                ReturnLayer.PUSH(returnToCurrent);
            }
            else
            {
                ProcessesRunner.Main.ChangeProcess(next_step, t);
            }
        }
    }
}