using UnityEngine;
using dataAccess;
using DummyLayerSystem;
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
        public void SetFocusingUnit(string localID)
        {
            _focusing = MyMonsters.Get(localID);
            if (_focusing == null)
            {
                Debug.Log("玩家目前一个角色也没？？？？");
                return;
            }
            
            var Ref = Units.GetUnitConfig(_focusing.r_id);
            if (Ref == null)
            {
                Debug.Log("No this monster:" + _focusing.r_id);
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
            AddressablesLogic.ReleaseAsyncOperationHandles();
            
            Screen.SetResolution(1920, 1080, true);
            UILayerLoader.Clear();
            AppSetting.bgmSource = audioSource;
            AppSetting.Load();
            Time.timeScale = 1;
            FightGlobalSetting.scenestep = 0;
            
            StartUp();
            BasicPhase();
            ToInitialPhase();

            AddressablesLogic.Essentials();
        }
        
        public static void ReturnToLobby(string error)
        {
            PopupLayer.Close();
            var popupLayer = PopupLayer.Open(PreScene.target.T);
            popupLayer.ArrangeConfirmWindow((() => { SceneManager.LoadScene(0);}), error);
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
            var memberDetail_edit = new MonsterEditPage();
            var memberDetail_SkillShow = new SkillShowPage();
            var arcadeFrontPage = new ArcadeFrontPage();
            
            // Shop
            var shopTop = new ShopTop();
            var boxOverLoadFix = new BoxOverLoadFix();
            var stoneBoxExpansion = new StoneBoxExpansion();

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
            ProcessesRunner.Main.Add(MainSceneStep.MonsterList, unitListPage);
            ProcessesRunner.Main.Add(MainSceneStep.UnitSkillEdit, memberDetail_edit);
            ProcessesRunner.Main.Add(MainSceneStep.UnitSkillShow, memberDetail_SkillShow);
            ProcessesRunner.Main.Add(MainSceneStep.FrontPage, frontPage);
            ProcessesRunner.Main.Add(MainSceneStep.ArcadeFront, arcadeFrontPage);
            ProcessesRunner.Main.Add(MainSceneStep.Arena, arenaPage);
            ProcessesRunner.Main.Add(MainSceneStep.ShopTop, shopTop);
            ProcessesRunner.Main.Add(MainSceneStep.BoxOverLoadHelper, boxOverLoadFix);
            ProcessesRunner.Main.Add(MainSceneStep.BoxExpansion, stoneBoxExpansion);
            ProcessesRunner.Main.Add(MainSceneStep.MailBox, mailBox);
            ProcessesRunner.Main.Add(MainSceneStep.MailDetail, mailDetail);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaFront, gotchaFront);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaResult, gotchaResult);
            #endregion
        }

        async void StartUp()
        {
            await HeroIcon.IniFrames();
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
                // 在以下的分歧之前，账户信息必须是最新，否则反应不到账户真实进度。
                switch (PlayerAccountInfo.Me.progress)
                {
                    case PlayerAccountProgressStep.Freedom:
                        trySwitchToStep(MainMenuNote.goingtostep, false);
                        break;
                    case PlayerAccountProgressStep.justCreated:
                        break;
                    case PlayerAccountProgressStep.Tutorial:
                        TutorialRunner.Main.GenerateTutorial();
                        trySwitchToStep(MainMenuNote.goingtostep, false);
                        TutorialRunner.Main.StartToMove();
                        break;
                }
            }
        }

        void Update()
        {
            ProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.ProcessNagare();
        }

        public void AskIfLoadFight(FightInfo stage)
        {
            var popupLayer = PopupLayer.Open(PreScene.target.T);
            popupLayer.ArrangeConfirmWindow(
                delegate {
                    FightLoad.Go(stage, true);
                }, "开打？");
        }

        public void BeginSkillTest_Rotation()
        {
            var stage = FightInfo.RandomSkillTestStage(TeamMode.rotation);
            stage.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
            FightLoad.Go(stage);
        }
        
        public void BeginSkillTest_Multi()
        {
            var stage = FightInfo.RandomSkillTestStage(TeamMode.multiRaid);
            stage.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
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
                    //Debug.Log("回到：" + returnToStep);
                    trySwitchToStep(returnToStep, false);
                }
                ReturnLayer.PUSH(returnToCurrent);
            }
            Debug.Log("迁移 ：" + next_step + " ，"+ forward);
            ProcessesRunner.Main.ChangeProcess(next_step);
        }

        public void trySwitchToStep<T>(MainSceneStep next_step, T t, bool forward)
        {
            if (forward && ProcessesRunner.Main.currentProcess != null)
            {
                var returnToStep = ProcessesRunner.Main.currentProcess.Step;
                void returnTOCurrent()
                {
                    trySwitchToStep(returnToStep, false);
                }
                ReturnLayer.PUSH(returnTOCurrent);
            }
            Debug.Log("迁移 ：" + next_step + " ，"+ forward);
            ProcessesRunner.Main.ChangeProcess(next_step, t);
        }
    }
}