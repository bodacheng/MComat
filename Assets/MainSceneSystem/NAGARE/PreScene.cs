using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        public CharsManager _CharSetManager;
        public Text accountDiamondCoin;
        public Text accountIntelliCoin;
        
        [Space(11)]
        [Header("TeamEdit")]
        public TeamEditManager TeamEditor;
        
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
        public RectTransform JiNengRongLian_selectT;
        public RectTransform RonglianConfirmGAMENT;
        public RectTransform SelfFightUIT;
        public RectTransform ArcadeTeamEditT;
        
        void Awake()
        {
            target = this;
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
        // 我们应该对这里面的所有进程做进一步分析，然后来细致的安排这个进程运行的时机
        // 现在Start()里只有场景相关的一些轻量级画面相关处理。
        // 围绕这个进程画面应该有相应配合
        public IEnumerator StartUpProcess()
        {
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
            Tutorial_skillEdit tutorial_SkillEdit = new Tutorial_skillEdit();
            
            // Shop
            ShopTop shopTop = new ShopTop();
            BoxOverLoadFix boxOverLoadFix = new BoxOverLoadFix();
            StoneBoxExpansion stoneBoxExpansion = new StoneBoxExpansion();
            
            // Gotcha
            GachaFront gachaFront = new GachaFront();
            GachaAnim gotchaAnim = new GachaAnim();
            GachaResult gachaResult = new GachaResult();
            
            ArenaProcess areanaProcess = new ArenaProcess();
            
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
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.Tutorial_skillEdit, tutorial_SkillEdit);
            
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.ShopTop, shopTop);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.BoxOverLoadHelper, boxOverLoadFix);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.BoxExpansion, stoneBoxExpansion);
            
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.GotchaFront, gachaFront);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.GotchaAnim, gotchaAnim);
            ProcessesRunner.Main.AddNewProcess(MainSceneStep.GotchaResult, gachaResult);
            
            LoadingCanvas.target.TurnOnProcessDescription(true);
            LoadingCanvas.target.NowProcess("正在读取账户信息", 0);
            
            yield return AccountSet.LoadCustomerInfo(); // 缺response判断
            Setting.target.LoadProgrameSettingFromAccount();
            accountDiamondCoin.text = AccountSet._AccInfo.Diamond.ToString();
            accountIntelliCoin.text = AccountSet._AccInfo.Coin.ToString();
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
            
            IEnumerator localMyChractersProcess = AccountCharsSet.LoadAll();
            yield return localMyChractersProcess;
            // 缺response判断
            yield return TeamSet.LoadTeamSet(TeamSetGameMode.story);
            yield return TeamSet.LoadTeamSet(TeamSetGameMode.arena3V3);
            yield return MonsterBox.DisplayMonsterIcons();//这个进程会先找到所有角色的头像。
            IEnumerator loadMyStonesProcess = MySkillStonesReader.LoadAll();
            yield return loadMyStonesProcess;
            
            LoadingCanvas.target.LightUp();
            
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
                        GoToMemberDetail goToMemberDetail = new GoToMemberDetail();
                        OpenSkillEdit openSkillEdit = new OpenSkillEdit();
                        
                        SkillEditA1Try skillEditA1Try = new SkillEditA1Try();
                        SkillEditA2Try skillEditA2Try = new SkillEditA2Try();
                        SkillEditA3Try skillEditA3Try = new SkillEditA3Try();
                        SkillEditTry_A1Filled skillEditTry_A1Filled = new SkillEditTry_A1Filled();
                        SkillEditTry_A2Filled skillEditTry_A2Filled = new SkillEditTry_A2Filled();
                        SkillEditTry_A3Filled skillEditTry_A3Filled = new SkillEditTry_A3Filled();
                        ALineConfirm aLineConfirm = new ALineConfirm();
                        ReturnOne returnOne = new ReturnOne();
                        
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.GoToMemberDetail, goToMemberDetail);
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.OpenSkillEdit, openSkillEdit);
                        
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A1Selected, skillEditA1Try);
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A2Selected, skillEditA2Try);
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A3Selected, skillEditA3Try);
                        
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A1Filled, skillEditTry_A1Filled);
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A2Filled, skillEditTry_A2Filled);
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.SkillEditTry_A3Filled, skillEditTry_A3Filled);
                        
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.ALineConfirm, aLineConfirm);
                        ProcessesRunner.Tutorial.AddNewProcess(MainSceneStep.TutorialReturn, returnOne);
                        
                        trySwitchToStep(MainMenuNote.goingtostep, false);
                        ProcessesRunner.Tutorial.ChangeProcess(MainSceneStep.GoToMemberDetail);
                    break;
                }
            }
        }

        void Update()
        {
            ProcessesRunner.Main.ProcessNagare();
            ProcessesRunner.Tutorial.ProcessNagare();
        }
        
        public void AskIfLoadFight(StageScriptableObject stage)
        {
            LoadingCanvas.target.ArrangeValiationWindow(delegate { LoadFight(stage); }, "开打？");
        }
        
        public void LoadFight(StageScriptableObject stage)//6.29 这个环节可能要进一步研究。进入战斗场景要做的事情安说很多，包括loadscene什么的，而这些都应该在这里进行。
        {
            FightSceneNote.nextBattle = stage;
            _CharSetManager.PreventTheseMyModelsFromDestroying(FightSceneNote.nextBattle.GetTeam1EnterRingLocalIds(FightSceneNote.nextBattle.localFight));
            SkillStonesBox.PreventCellsFromDestroy();
            MySkillStonesReader.PreventStonesFromDestroy();
            SceneManager.LoadScene(FightSceneNote.nextBattle.BattleGroundID);
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
        
        //void OnGUI()
        //{
        //    if (AccountSet.Instance._playerinfoReferenceMode == playerinfoReferenceMode.localTestSaveData)
        //    {
        //        if (GUI.Button(new Rect(0, 0, 100, 50), "All Characters"))
        //        {
        //            mainProcessRunner.Run(AccountCharsSet.Instance.LocalSaveDataGetAllCharacters());
        //        }
        //        if (GUI.Button(new Rect(0, 50, 100, 50), "All stones"))
        //        {
        //            if (Directory.Exists(Application.persistentDataPath) && File.Exists(Application.persistentDataPath + "/MySkillStones.json"))
        //            {
        //                File.Delete(Application.persistentDataPath + "/MySkillStones.json");
        //            }
        //            IEnumerator GetAllStones()
        //            {
        //                yield return SkillConfigTable.Instance.LoadAllSkillConfigs();
        //                int i = 1;
        //                foreach (KeyValuePair<string, SkillConfig> _pair in SkillConfigTable.Instance.SkillConfigRefDic)
        //                {
        //                    Debug.Log("尝试于本地存档追加石：" + _pair.Value.REAL_NAME);
        //                    var skillStoneOfPlayerInfoModel = new SkillStoneOfPlayerInfoModel
        //                    {
        //                        skillStoneOfPlayerId = string.Format("{0:D20}", i),
        //                        skillId = _pair.Value.RECORD_ID,
        //                        level = 1.ToString()
        //                    };
        //                    yield return SkillStonesBox.GenerateOneStone(skillStoneOfPlayerInfoModel);
        //                    i++;
        //                }
        //                MySkillStonesReader.Instance.OverrideMySkillStoneInfosOnLocalFile(MySkillStonesReader.mySkillStonesDataDic.Values.ToList());
        //                yield return SkillStonesBox.target.ArrangeSkillStonesToBox();
        //            }
        //            mainProcessRunner.Run(GetAllStones());
        //        }
        //    }
        //}
    }
}