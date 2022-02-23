using UnityEngine;
using dataAccess;
using DummyLayerSystem;
using UnityEngine.SceneManagement;

namespace mainMenu
{
    public class PreScene : MonoBehaviour
    {
        public static PreScene target;

        [Space(7)]
        [Header("T")]
        public GameObject T;

        [Space(7)]
        [Header("主进程处理器")]
        public SingleThreadProcesser mainProcessRunner;

        [Space(11)]
        [Header("主相机")]
        public CameraManager _CameraManager;
        
        [Space(11)] 
        [Header("FX 相机")] 
        public Camera FxCamera;
        
        [Space(7)]
        [Header("Positions For Show")]
        public Transform MemDetailTargetPos;
        public Transform MemDetailWatchPos;
        
        [Space(7)]
        [Header("Shader转换器")]
        public SwapAllModelShader _SwapAllModelShader;
        
        [Space(7)]
        [Header("AudioSource")]
        public AudioSource audioSource;

        public RectTransform stonesTempContainer;
        
        public UnitInfo _focusing;
        //下面这个函数总是建立在monsterbox函数运行在前，而monsterbox会部署好所有展示用模
        public void SetFocusingUnit(string localID)
        {
            _focusing = MyMonsters.Get(localID);
        }
        
        void Awake()
        {
            target = this;
            SingleThreadProcesser.backup = mainProcessRunner;
        }

        void Start()
        {
            Screen.SetResolution(1920, 1080, true);
            UILayerLoader.Clear();
            AppSetting.bgmSource = audioSource;
            AppSetting.Load();
            Time.timeScale = 1;
            FightGlobalSetting.scenestep = 0;
            
            StartUp();
            BasicPhase();
            ToInitialPhase();
        }
        
        public static void ReturnToLobby(string error)
        {
            PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
            popupLayer.ArrangeConfirmWindow((() => { SceneManager.LoadScene(1);}), error);
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
            var stoneMerge = new StoneMerge();
            var selfFightFront = new SelfFightPage();
            var questInfo = new QuestInfoPage();
            var memberDetail = new UnitListPage();
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
            ProcessesRunner.Main.Add(MainSceneStep.StoneMerge, stoneMerge);
            ProcessesRunner.Main.Add(MainSceneStep.SelfFightFront, selfFightFront);
            ProcessesRunner.Main.Add(MainSceneStep.QuestInfo, questInfo);
            ProcessesRunner.Main.Add(MainSceneStep.MonsterList, memberDetail);
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

        void StartUp()
        {
            HeroIcon.INIFrames();
            HurtObjectManager.ConstructDPool();
            if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
            {
                UnitOptionLayer unitOptionLayer = UnitOptionLayer.Open();
                SetFocusingUnit("1");//确立focusing角色
                unitOptionLayer.RefreshMemberDetailPageByFocusingChar();
            }
        }

        void ToInitialPhase()
        {
            if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
            {
                trySwitchToStep(MainSceneStep.UnitSkillEdit, false);
            }
            else
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
        }

        void Update()
        {
            ProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.ProcessNagare();
        }

        public void AskIfLoadFight(FightInfo stage)
        {
            PopupLayer popupLayer = PopupLayer.Open(PreScene.target.T);
            popupLayer.ArrangeConfirmWindow(
                delegate {
                    FightLoad.Go(stage, true);
                }, "开打？");
        }

        public void BeginSkillTest_Rotatiom()
        {
            FightInfo stage = FightInfo.RandomSkillTestStage(TeamMode.rotation);
            stage.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
            FightLoad.Go(stage);
        }

        public void BeginSkillTest_Multi()
        {
            FightInfo stage = FightInfo.RandomSkillTestStage(TeamMode.multiRaid);
            stage.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
            FightLoad.Go(stage);
        }

        [EnumAction(typeof(MainSceneStep))]
        public void trySwitchToStep(int next_step)
        {
            trySwitchToStep((MainSceneStep)next_step, true);
        }

        [EnumAction(typeof(MainSceneStep))]
        public void trySwitchToStep(MainSceneStep next_step, bool foward)
        {
            if (foward && ProcessesRunner.Main.currentProcess != null)
            {
                MainSceneStep returnToStep = ProcessesRunner.Main.currentProcess.Step;
                void returnTOCurrent()
                {
                    //Debug.Log("回到：" + returnToStep);
                    trySwitchToStep(returnToStep, false);
                }
                ReturnLayer.PUSH(returnTOCurrent);
            }
            ProcessesRunner.Main.ChangeProcess(next_step);
        }

        public void trySwitchToStep<T>(MainSceneStep next_step, T t, bool foward)
        {
            if (foward && ProcessesRunner.Main.currentProcess != null)
            {
                MainSceneStep returnToStep = ProcessesRunner.Main.currentProcess.Step;
                void returnTOCurrent()
                {
                    trySwitchToStep(returnToStep, false);
                }
                ReturnLayer.PUSH(returnTOCurrent);
            }
            ProcessesRunner.Main.ChangeProcess(next_step, t);
        }
    }
}