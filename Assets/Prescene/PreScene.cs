using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    public class PreScene : MonoBehaviour
    {
        public static PreScene target;
        
        [Space(7)]
        [Header("主进程处理器")]
        public SingleThreadProcesser mainProcessRunner;
        
        [Space(11)]
        [Header("Essentials")]
        public CameraManager _CameraManager;
        public Text UserID;
        public Text accountDiamondCoin;
        public Text accountIntelliCoin;
        
        [Space(11)]
        [Header("TeamEdit")]
        public TeamEditManager TeamEditor;

        [Space(11)]
        [Header("技能展示器模式切换角色按钮")]
        public Button charSwitcher;
        
        [Space(11)]
        [Header("modelShower")]
        public ModelShower _modelShower;
        
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
        
        //preparingscene应该就是只有这些画布
        [Space(7)]
        [Header("Canvas")]
        public Canvas MainMenuCanvas;
        
        [Space(7)]
        [Header("若干子画面的总RectTransfrom")]
        public RectTransform MainMenuBottonsT;
        public RectTransform ArcadeTeamEditT;
        
        void Awake()
        {
            target = this;
            SingleThreadProcesser.backup = mainProcessRunner;
        }
        
        void Start()
        {
            //_stagesManager.loadAndRefresh();
            Time.timeScale = 1;
            FightGlobalSetting.scenestep = 0;
            mainProcessRunner.Run(StartUpProcess());
            Screen.SetResolution(1920,1080,true);
        }

        // 这个应该是和热更新进程完全分开了。
        // 我们把场景加载时候需要进行加载的一些东西给总结到了一起
        // 这里面的东西都是应该针对本地模式或联网模式来搞分歧处理。
        // 这里面有的是读数据库，有的是和本地资源的循环有关，
        // 应该有更好精致的机理来回避重复计算。
        public IEnumerator StartUpProcess()
        {
            switch (AccountSet._AccInfo.accountprogress)
            {
                case PlayerAccountProgressStep.Freedom:
                    yield return AccountCharsSet.Load_List();
                    yield return MySkillStonesReader.LoadAMySkillstones(Setting.Language);
                break;
                case PlayerAccountProgressStep.justCreated:
                break;
                case PlayerAccountProgressStep.Tutorial:
                    yield return AccountCharsSet.LoadTutorial();
                    yield return MySkillStonesReader.LoadTutorial();
                break;
            }
            yield return AccountSet.LoadCustomerInfo(); // 缺response判断
            yield return TeamSet.LoadTeamSet(TeamSetGameMode.story);
            yield return TeamSet.LoadTeamSet(TeamSetGameMode.arena3V3);
        
            LoadingCanvas.target.DarkOff(0.5f);
            Application.targetFrameRate = 60;
            
            _SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
            _SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
            TheNineSlot.target.gameObject.SetActive(false);
            MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(false);
            
            TeamEditFront teamEditFront = new TeamEditFront();
            SkillStones skillStones = new SkillStones();
            StoneSell stoneSell = new StoneSell();
            SelfFightFront selfFightFront = new SelfFightFront();
            QuestInfo questInfo = new QuestInfo();
            MemberDetailProcess memberDetail = new MemberDetailProcess();
            MemberDetail_edit memberDetail_edit = new MemberDetail_edit();
            MemberDetail_skillshow memberDetail_Skillshow = new MemberDetail_skillshow();
            TopPage frontPage = new TopPage();
            ArcadeFrontProcess arcadeFrontProcess = new ArcadeFrontProcess();
            
            // 关卡按钮一次生成就可以
            yield return ArcadeManager.target.INIArcadeStageButtons();
            
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
            MailBox mailBox = new MailBox();
            MailDetail mailDetail = new MailDetail();
            
            ProcessesRunner.Main.Clear();
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.TeamEditFront, teamEditFront);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.SkillStones, skillStones);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.SkillStones_Sell, stoneSell);
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
            
            LoadingCanvas.target.TurnOnProcessDescription(true);
            LoadingCanvas.target.NowProcess("正在读取账户信息", 0);
            
            Setting.target.LoadProgrameSettingFromAccount();
            UserID.text = AccountSet._AccInfo.PlayerName; //SystemInfo.deviceUniqueIdentifier;
            accountDiamondCoin.text = AccountSet._AccInfo.diamondCount.ToString();
            accountIntelliCoin.text = AccountSet._AccInfo.coinCount.ToString();
            LoadingCanvas.target.TurnOnProcessDescription(false);
            
            HeroIcon.INIFrames();
            
            LoadingCanvas.target.NowProcess("正在启动技能石头背包", 0.6f);
            SkillStonesBox.target = _SkillStonesBox_NineSlot;
            yield return _SkillStonesBox_NineSlot._SkillStoneBoxTabEffectsManager.StartUp();
            yield return _SkillStonesBox_NineSlot.StartUp(AccountSet._AccInfo.Stoneboxsize);
            yield return _SkillStonesBox_Show.StartUp(AccountSet._AccInfo.Stoneboxsize);
            LoadingCanvas.target.NowProcess("正在加载技能编辑器", 0.7f);
            yield return (TheNineSlot.target.StartUp());
            
            yield return _SelfFightManager.INITeamPosButtons();
            
            yield return MonsterBox.DisplayMonsterIcons();//这个进程会先找到所有角色的头像。
            LoadingCanvas.target.LightUp();
            
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
            HurtObjectManager.ConstructDPool();
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
    }
}