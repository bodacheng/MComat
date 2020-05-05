using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using dataAccess;

namespace mainMenu
{
    public class PreScene : MonoBehaviour
    {
        public static PreScene Instance;
        
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
            Instance = this;
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
            SelfFightFront selfFightFront = new SelfFightFront();
            QuestInfo questInfo = new QuestInfo();
            MemberDetailProcess memberDetail = new MemberDetailProcess();
            MemberDetail_edit memberDetail_edit = new MemberDetail_edit();
            MemberDetail_skillshow memberDetail_Skillshow = new MemberDetail_skillshow();
            TopPage frontPage = new TopPage();
            ArcadeFrontProcess arcadeFrontProcess = new ArcadeFrontProcess();
            Tutorial_skillEdit tutorial_SkillEdit = new Tutorial_skillEdit();
            
            GachaFront gachaFront = new GachaFront();
            GachaAnim gotchaAnim = new GachaAnim();
            GachaResult gachaResult = new GachaResult();
            
            ArenaProcess areanaProcess = new ArenaProcess();
            
            ProcessesRunner.Instance.Clear();
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.TeamEditFront, teamEditFront);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.SkillStones, skillStones);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.SelfFightFront, selfFightFront);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.QuestInfo, questInfo);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.MemberDetail, memberDetail);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.MemberDetail_edit, memberDetail_edit);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.MemberDetail_show, memberDetail_Skillshow);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.FrontPage, frontPage);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.ArcadeFront, arcadeFrontProcess);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.Arena, areanaProcess);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.Tutorial_skillEdit, tutorial_SkillEdit);
            
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.GotchaFront, gachaFront);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.GotchaAnim, gotchaAnim);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.GotchaResult, gachaResult);
            
            LoadingCanvas.target.TurnOnProcessDescription(true);
            LoadingCanvas.target.NowProcess("正在读取账户信息", 0);
            
            yield return AccountSet.Instance.LoadCustomerInfo();
            Setting.target.LoadProgrameSettingFromAccount();
            accountDiamondCoin.text = AccountSet.Instance._PlayerAccountInfo.Diamond.ToString();
            accountIntelliCoin.text = AccountSet.Instance._PlayerAccountInfo.Coin.ToString();
            LoadingCanvas.target.TurnOnProcessDescription(false);

            HeroIcon.INIFrames();

            LoadingCanvas.target.NowProcess("正在启动技能石头背包", 0.6f);
            SkillStonesBox.target = _SkillStonesBox_NineSlot;
            yield return _SkillStonesBox_NineSlot._SkillStoneBoxTabEffectsManager.StartUp();
            yield return _SkillStonesBox_NineSlot.StartUp(AccountSet.Instance._PlayerAccountInfo.Stoneboxsize);
            yield return _SkillStonesBox_Show.StartUp(AccountSet.Instance._PlayerAccountInfo.Stoneboxsize);
            LoadingCanvas.target.NowProcess("正在加载技能编辑器", 0.7f);
            yield return (TheNineSlot.target.StartUp());
            
            yield return _SelfFightManager.INITeamPosButtons();
            
            // 在以下的分歧之前，账户信息必须是最新，否则反应不到账户真实进度。
            switch (AccountSet.Instance._PlayerAccountInfo.accountprogress)
            {
                case PlayerAccountProgressStep.Freedom:
                    // 账户信息。。如果账户信息没有能读取成功的话那接下来的账户拥有财产等等都不应该继续尝试读取。
                    // 在正式版本当中读取账户信息应该就是获取token的过程。那么。。。说白了如果用户信息都没能获取那程序的初始化工作应该一点也不需要再进行了才对。
                    // 那这样的话势必我需要来看接下来这个请求工作的返回值。
                    IEnumerator localMyChractersProcess = AccountCharsSet.LoadAll();
                    yield return (localMyChractersProcess);
                    //上面这些都缺response判断
                    yield return TeamSet.LoadTeamSet(TeamSetGameMode.story);
                    yield return TeamSet.LoadTeamSet(TeamSetGameMode.arena3V3);
                    yield return MonsterBox.DisplayMonsterIcons();//这个进程会先找到所有角色的头像。
                    IEnumerator loadMyStonesProcess = MySkillStonesReader.LoadAll();
                    yield return loadMyStonesProcess;
                break;
                case PlayerAccountProgressStep.justCreated:
                break;
                case PlayerAccountProgressStep.Tutorial:
                break;
            }
            LoadingCanvas.target.LightUp();
            
            if (ReturnButtonManager.ReturnMissionList.Count > 0)
            {
                ReturnButtonManager.AddFeatureToReturnButton();
                //从战斗画面返回后，进入战斗前的菜单往上跳一节，指的是站前准备画面
                ReturnButtonManager.POP();
            }else{
                // 在以下的分歧之前，账户信息必须是最新，否则反应不到账户真实进度。
                switch (AccountSet.Instance._PlayerAccountInfo.accountprogress)
                {
                    case PlayerAccountProgressStep.Freedom:
                        trySwitchToStep(MainMenuNote.goingtostep, false);
                    break;
                    case PlayerAccountProgressStep.justCreated:
                        trySwitchToStep(MainSceneStep.Tutorial_skillEdit, false);
                    break;
                    case PlayerAccountProgressStep.Tutorial:
                        trySwitchToStep(MainSceneStep.Tutorial_skillEdit,false);
                    break;
                }
            }            
        }

        void Update()
        {
            ProcessesRunner.Instance.ProcessNagare();
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
        public void trySwitchToStep(MainSceneStep next_step, bool foward)//这个是试图进入某个step。另一个是根据一些东西的选择情况来在某个step内对GUI进行刷新。两个都需要。
        {
            if (foward && ProcessesRunner.Instance.currentProcess != null)
            {
                MainSceneStep returnToStep = ProcessesRunner.Instance.currentProcess.thisProcessStep;
                void returnTOCurrent()
                {
                    trySwitchToStep(returnToStep, false);
                }
                ReturnButtonManager.PUSH(returnTOCurrent);
            }
            ProcessesRunner.Instance.ChangeProcess(next_step);
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