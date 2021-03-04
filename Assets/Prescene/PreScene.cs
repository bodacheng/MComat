using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Cysharp.Threading.Tasks;

namespace mainMenu
{
    public class PreScene : MonoBehaviour
    {
        public static PreScene target;
        
        [Space(7)]
        [Header("主进程处理器")]
        public SingleThreadProcesser mainProcessRunner;
        
        [Space(11)]
        [Header("主相机")]
        public CameraManager _CameraManager;
        
        [Space(11)]
        [Header("TeamEdit")]
        public TeamEditManager TeamEditor;
        
        [Space(11)]
        [Header("技能展示器模式切换角色按钮")]
        public Button charSwitcher;
        
        [Space(11)]
        [Header("SkillStonesBox")]
        public SkillStonesBox _SkillStonesBox_NineSlot;
        
        [Space(11)]
        [Header("SkillStonesBox 技能石单独画面")]
        public SkillStonesBox _SkillStonesBox_Show;
        
        [Space(7)]
        [Header("Shader转换器")]
        public SwapAllModelShader _SwapAllModelShader;
        
        [Space(7)]
        [Header("CustomGUISkin")]
        public GUISkin CustomGUISkin;
        
        [Space(7)]
        [Header("自我战斗管理模块")]
        public SelfFightManager _SelfFightManager;
        
        [Space(7)]
        [Header("Canvas")]
        public Canvas MainMenuCanvas;
        
        [Space(7)]
        [Header("若干子画面的总RectTransfrom")]
        public RectTransform MainMenuBottonsT;
        public RectTransform ArcadeTeamEditT;

        static bool DataRead = false;
        
        void Awake()
        {
            target = this;
            SingleThreadProcesser.backup = mainProcessRunner;
        }
        
        void Start()
        {
            Time.timeScale = 1;
            FightGlobalSetting.scenestep = 0;

            if (!DataRead)
                mainProcessRunner.Run(DataLoad().ToUniTask());
            mainProcessRunner.Run(StartUpProcess().ToUniTask());
            Screen.SetResolution(1920, 1080, true);
        }

        IEnumerator DataLoad()
        {
            switch (AccountSet._AccInfo.accountprogress)
            {
                case PlayerAccountProgressStep.Freedom:
                    yield return AccountCharsSet.Load_List();
                    yield return MySkillStones.LoadAMySkillstones(Setting.Language);
                break;
                case PlayerAccountProgressStep.justCreated:
                break;
                case PlayerAccountProgressStep.Tutorial:
                    yield return AccountCharsSet.LoadTutorial();
                    yield return MySkillStones.LoadTutorial();
                break;
            }
            yield return AccountSet.LoadCustomerInfo(); // 缺response判断
            yield return TeamSet.LoadTeamSet(TeamSetGameMode.story);
            yield return TeamSet.LoadTeamSet(TeamSetGameMode.arena3V3);
            
            DataRead = true;
        }

        public IEnumerator StartUpProcess()
        {
            Application.targetFrameRate = 60;
            
            _SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
            _SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
            MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(false);
            UpperInfoBar.target.T.gameObject.SetActive(false);
            
            LoadingCanvas.target.TurnOnProcessDescription(true);
            LoadingCanvas.target.NowProcess("Reading Account", 0);

            #region 主界面各大画面
            TopPage frontPage = new TopPage();
            TeamEditFront teamEditFront = new TeamEditFront();
            SkillStonesList skillStones = new SkillStonesList();
            StoneSell stoneSell = new StoneSell();
            StoneMerge stoneMerge = new StoneMerge();
            SelfFightFront selfFightFront = new SelfFightFront();
            QuestInfo questInfo = new QuestInfo();
            MemberDetailProcess memberDetail = new MemberDetailProcess();
            MemberDetail_edit memberDetail_edit = new MemberDetail_edit();
            MemberDetail_skillshow memberDetail_Skillshow = new MemberDetail_skillshow();
            ArcadeFrontProcess arcadeFrontProcess = new ArcadeFrontProcess();
            
            // Shop
            ShopTop shopTop = new ShopTop();
            BoxOverLoadFix boxOverLoadFix = new BoxOverLoadFix();
            StoneBoxExpansion stoneBoxExpansion = new StoneBoxExpansion();
            
            // Gotcha
            GachaFront gachaFront = new GachaFront();
            GachaAnim gotchaAnim = new GachaAnim();
            GachaResult gachaResult = new GachaResult();
            ArenaProcess areanaProcess = new ArenaProcess();
            
            // mail
            MailBoxProcess mailBox = new MailBoxProcess();
            MailDetailProcess mailDetail = new MailDetailProcess();
            
            ProcessesRunner.Main.Clear();
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.TeamEditFront, teamEditFront);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.SkillStoneList, skillStones);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.SkillStones_Sell, stoneSell);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.StoneMerge, stoneMerge);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.SelfFightFront, selfFightFront);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.QuestInfo, questInfo);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.MemberDetail, memberDetail);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.MemberDetail_edit, memberDetail_edit);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.MemberDetail_show, memberDetail_Skillshow);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.FrontPage, frontPage);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.ArcadeFront, arcadeFrontProcess);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.Arena, areanaProcess);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.ShopTop, shopTop);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.BoxOverLoadHelper, boxOverLoadFix);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.BoxExpansion, stoneBoxExpansion);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.MailBox, mailBox);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.MailDetail, mailDetail);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.GotchaFront, gachaFront);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.GotchaAnim, gotchaAnim);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.GotchaResult, gachaResult);
            #endregion
            
            // 关卡按钮一次生成就可以
            yield return ArcadeManager.target.INIArcadeStageButtons();
            
            UpperInfoBar.target.Refresh();
            HeroIcon.INIFrames();
            
            LoadingCanvas.target.NowProcess("Opening item Box", 0.2f);
            SkillStonesBox.target = _SkillStonesBox_NineSlot;
            yield return _SkillStonesBox_NineSlot._SkillStoneBoxTabEffectsManager.StartUp();
            LoadingCanvas.target.NowProcess("Opening item Box", 0.4f);
            yield return _SkillStonesBox_NineSlot.StartUp();
            LoadingCanvas.target.NowProcess("Opening item Box", 0.5f);
            yield return _SkillStonesBox_Show.StartUp();
            LoadingCanvas.target.NowProcess("Opening item Box", 0.6f);
            yield return (TheNineSlot.target.StartUp());
            LoadingCanvas.target.NowProcess("Opening item Box", 0.7f);
            yield return _SelfFightManager.INITeamPosButtons();
            yield return MonsterBox.DisplayMonsterIcons(true);//这个进程会先找到所有角色的头像。
            
            HurtObjectManager.ConstructDPool();
            
            if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
            {
                charSwitcher.gameObject.SetActive(true);
                yield return MemberDetail.target.SetMemberDetailFocusingChar("1");//确立focusing角色
                yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
                trySwitchToStep(MainSceneStep.MemberDetail_edit, false);
            }else{
                charSwitcher.gameObject.SetActive(false);
                if (ReturnButtonManager.ReturnMissionList.Count > 0)
                {
                    ReturnButtonManager.AddFeatureToReturnButton();
                    //从战斗画面返回后，进入战斗前的菜单往上跳一节，指的是站前准备画面
                    ReturnButtonManager.POP();
                }else{
                    // 在以下的分歧之前，账户信息必须是最新，否则反应不到账户真实进度。
                    switch (AccountSet._AccInfo.accountprogress)
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
            LoadingCanvas.target.NowProcess("Finished", 1f);
            LoadingCanvas.target.TurnOnProcessDescription(false);
        }
        
        void Update()
        {
            ProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.ProcessNagare();
        }
        
        public void AskIfLoadFight(StageScriptableObject stage)
        {
            LoadingCanvas.target.ArrangeConfirmWindow(
                delegate {
                    FightLoad.Go(stage); 
                }, "开打？");
        }
        
        public void BeginSkillTest_Rotatiom()
        {
            StageScriptableObject stage = StageScriptableObject.RandomSkillTestStage(TeamMode.rotation);
            FightLoad.Go(stage);
        }
        
        public void BeginSkillTest_Multi()
        {
            StageScriptableObject stage = StageScriptableObject.RandomSkillTestStage(TeamMode.multiraid);
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
                    trySwitchToStep(returnToStep, false);
                }
                ReturnButtonManager.PUSH(returnTOCurrent);
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
                ReturnButtonManager.PUSH(returnTOCurrent);
            }
            ProcessesRunner.Main.ChangeProcess(next_step, t);
        }
    }
}