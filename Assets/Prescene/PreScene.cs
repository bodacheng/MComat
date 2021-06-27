using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UniRx;

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

        void Awake()
        {
            target = this;
            SingleThreadProcesser.backup = mainProcessRunner;
        }

        async void Start()
        {
            Screen.SetResolution(1920, 1080, true);
            AppSetting.Load();
            Time.timeScale = 1;
            FightGlobalSetting.scenestep = 0;

            UniTask uniTask = mainProcessRunner.RunAsQueued_UniTask(StartUp());
            await uniTask;

            BasicPhase();
            ToInitialPhase();
        }

        void BasicPhase()
        {
            LoadingCanvas.target.TurnOnProcessDescription(false);
            Application.targetFrameRate = 60;

            _SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
            _SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
            MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(false);
            UpperInfoBar.target.T.gameObject.SetActive(false);

            #region 主界面各大画面
            FrontPage frontPage = new FrontPage();
            TeamEditPage teamEditFront = new TeamEditPage();
            StonesPage skillStones = new StonesPage();
            StoneSell stoneSell = new StoneSell();
            StoneMerge stoneMerge = new StoneMerge();
            SelfFightPage selfFightFront = new SelfFightPage();
            QuestInfoPage questInfo = new QuestInfoPage();
            MonsterListPage memberDetail = new MonsterListPage();
            MonsterEditPage memberDetail_edit = new MonsterEditPage();
            SkillShowPage memberDetail_Skillshow = new SkillShowPage();
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
            ProcessesRunner.Main.Add(MainSceneStep.TeamEditFront, teamEditFront);
            ProcessesRunner.Main.Add(MainSceneStep.SkillStoneList, skillStones);
            ProcessesRunner.Main.Add(MainSceneStep.SkillStones_Sell, stoneSell);
            ProcessesRunner.Main.Add(MainSceneStep.StoneMerge, stoneMerge);
            ProcessesRunner.Main.Add(MainSceneStep.SelfFightFront, selfFightFront);
            ProcessesRunner.Main.Add(MainSceneStep.QuestInfo, questInfo);
            ProcessesRunner.Main.Add(MainSceneStep.MonsterList, memberDetail);
            ProcessesRunner.Main.Add(MainSceneStep.MemberDetail_edit, memberDetail_edit);
            ProcessesRunner.Main.Add(MainSceneStep.MemberDetail_show, memberDetail_Skillshow);
            ProcessesRunner.Main.Add(MainSceneStep.FrontPage, frontPage);
            ProcessesRunner.Main.Add(MainSceneStep.ArcadeFront, arcadeFrontProcess);
            ProcessesRunner.Main.Add(MainSceneStep.Arena, areanaProcess);
            ProcessesRunner.Main.Add(MainSceneStep.ShopTop, shopTop);
            ProcessesRunner.Main.Add(MainSceneStep.BoxOverLoadHelper, boxOverLoadFix);
            ProcessesRunner.Main.Add(MainSceneStep.BoxExpansion, stoneBoxExpansion);
            ProcessesRunner.Main.Add(MainSceneStep.MailBox, mailBox);
            ProcessesRunner.Main.Add(MainSceneStep.MailDetail, mailDetail);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaFront, gachaFront);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaAnim, gotchaAnim);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaResult, gachaResult);
            #endregion
        }

        IEnumerator StartUp()
        {
            UpperInfoBar.target.Refresh();
            HeroIcon.INIFrames();
            SkillStonesBox.target = _SkillStonesBox_NineSlot;
            _SkillStonesBox_NineSlot._SkillStoneBoxTabEffectsManager.StartUp();
            // 关卡按钮一次生成就可以
            yield return ArcadeManager.target.INIArcadeStageButtons();
            TheNineSlot.target.StartUp();
            _SelfFightManager.INITeamPosButtons();

            HurtObjectManager.ConstructDPool();
            if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
            {
                MemberDetail.target.SetMemberDetailFocusingChar("1");//确立focusing角色
                MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
            }
        }

        void ToInitialPhase()
        {
            if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
            {
                charSwitcher.gameObject.SetActive(true);
                trySwitchToStep(MainSceneStep.MemberDetail_edit, false);
            }
            else
            {
                charSwitcher.gameObject.SetActive(false);
                if (ReturnButtonManager.ReturnMissionList.Count > 0)
                {
                    ReturnButtonManager.AddFeatureToReturnButton();
                    //从战斗画面返回后，进入战斗前的菜单往上跳一节，指的是站前准备画面
                    ReturnButtonManager.POP();
                }
                else
                {
                    // 在以下的分歧之前，账户信息必须是最新，否则反应不到账户真实进度。
                    switch (Account._AccInfo.accountprogress)
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
            LoadingCanvas.target.ArrangeConfirmWindow(
                delegate {
                    FightLoad.Go(stage, true);
                }, "开打？");
        }

        public void BeginSkillTest_Rotatiom()
        {
            FightInfo stage = FightInfo.RandomSkillTestStage(TeamMode.rotation);
            FightLoad.Go(stage);
        }

        public void BeginSkillTest_Multi()
        {
            FightInfo stage = FightInfo.RandomSkillTestStage(TeamMode.multiraid);
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