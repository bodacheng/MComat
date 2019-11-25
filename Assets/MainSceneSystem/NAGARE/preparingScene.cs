using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public class preparingScene : MonoBehaviour
    {
        public static preparingScene Instance;
    
        [Space(7)]
        [Header("主进程处理器")]
        public SingleThreadProcesser mainProcessRunner;
            
        [Header("ReturnButtonManager")]
        public ReturnButtonManager _ReturnButtonManager;

        [Space(11)]
        [Header("Essentials")]
        public CameraManager _CameraManager;
        public CharsManager _CharSetManager;
        public Text accountDiamondCoin;
        public Text accountIntelliCoin;
      
        [Space(11)]
        [Header("modelShower")]
        public ModelShower _modelShower;

        [Space(11)]
        [Header("SkillStonesBox")]
        public SkillStonesBox _SkillStonesBox;

        [Space(7)]
        [Header("Shader转换器")]
        public SwapAllModelShader _SwapAllModelShader;

        [Space(7)]
        [Header("MemberDetail")]
        public MemberDetail _MemberDetail;
        
        [Space(7)]
        [Header("QuestPreparePage")]
        public QuestPreparePage _QuestPreparePage;

        [Space(7)]
        [Header("CustomGUISkin")]
        public GUISkin CustomGUISkin;

        [Space(7)]
        [Header("MonsterBox")]
        public MonsterBox _MonsterBox;

        [Space(7)]
        [Header("九宫槽管理器")]
        public TheNineSlot TheNineSlot;

        [Space(7)]
        [Header("队伍编辑器")]
        public TeamEditManager _TeamEditManager;

        [Space(7)]
        [Header("章节编辑器")]
        public ChaptersManager _ChaptersManager;
        
        [Space(7)]
        [Header("自我战斗管理模块")]
        public SelfFightManager _SelfFightManager;
        
        [Space(7)]
        [Header("抽奖管理模块")]
        public gotchaManager _gotchaManager;
        
        [Space(7)]
        [Header("LoadingProcess")]
        public LoadingCanvas _LoadingCanvas;

        //preparingscene应该就是只有这些画布
        [Space(7)]
        [Header("Canvas")]
        public Canvas MainMenuCanvas;

        [Space(7)]
        [Header("若干子画面的总RectTransfrom")]
        public RectTransform QuestInfoT;
        public RectTransform JiNengRongLian_selectT;
        public RectTransform RonglianConfirmGAMENT;
        public RectTransform FightModeChooseT;
        
        public RectTransform SeasonsT;
        public RectTransform AllSeasonsGamensT;
        public RectTransform SelfFightUIT;
        
        public ProcessesRunner processesRunner;

        void Awake()
        {
            Instance = this;
            Screen.SetResolution(1920, 1080, true);
        }
        
        void Start()
        {
            //_stagesManager.loadAndRefresh();
            Time.timeScale = 1;
            FightGlobalSetting.scenestep = 0;
            mainProcessRunner.triggerMainProcess(StartUpProcess());
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
            //QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;

            _SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
            TheNineSlot.NineSlotT.gameObject.SetActive(false);
            _MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
            _MemberDetail._LevelManager.turnOnUI(false);
            MainMenuCanvas.gameObject.SetActive(false);

            _LoadingCanvas.Loading_Canvas.gameObject.SetActive(true);
            _LoadingCanvas.turnOnProcessDescription(true);
            _LoadingCanvas.nowProcess("正在读取账户信息", 0);

            //SceneProcessDictionary
            TeamEditFront teamEditFront = new TeamEditFront(this);
            SkillStones skillStones = new SkillStones(this);
            SelfFightFront selfFightFront = new SelfFightFront(this);
            SeasonsGamen seasonsGamen = new SeasonsGamen(this, SeasonsT);
            ShowOneSeasonChapters _ShowOneSeasonChapters = new ShowOneSeasonChapters(this, AllSeasonsGamensT);
            QuestInfo questInfo = new QuestInfo(this, QuestInfoT);
            MemberDetailProcess memberDetail = new MemberDetailProcess(this);
            MemberDetail_edit memberDetail_edit = new MemberDetail_edit(this);
            MemberDetail_skillshow memberDetail_Skillshow = new MemberDetail_skillshow(this);
            frontPage frontPage = new frontPage(this);
            ChapterProcess chapterProcess = new ChapterProcess(this, _ChaptersManager.ChapterInfoT);
            Tutorial_skillEdit tutorial_SkillEdit = new Tutorial_skillEdit(this);
            GotchaProcess gotchaProcess = new GotchaProcess(this);

            processesRunner = new ProcessesRunner();
            processesRunner.AddNewProcess(MainSceneStep.TeamEditFront, teamEditFront);
            processesRunner.AddNewProcess(MainSceneStep.SkillStones, skillStones);
            processesRunner.AddNewProcess(MainSceneStep.SelfFightFront, selfFightFront);
            processesRunner.AddNewProcess(MainSceneStep.ChaptersOfOneSeason, _ShowOneSeasonChapters);
            processesRunner.AddNewProcess(MainSceneStep.SeasonsGamen, seasonsGamen);
            processesRunner.AddNewProcess(MainSceneStep.QuestInfo, questInfo);
            processesRunner.AddNewProcess(MainSceneStep.MemberDetail, memberDetail);
            processesRunner.AddNewProcess(MainSceneStep.MemberDetail_edit, memberDetail_edit);
            processesRunner.AddNewProcess(MainSceneStep.MemberDetail_show, memberDetail_Skillshow);
            processesRunner.AddNewProcess(MainSceneStep.frontPage, frontPage);
            processesRunner.AddNewProcess(MainSceneStep.Chapter, chapterProcess);
            processesRunner.AddNewProcess(MainSceneStep.Tutorial_skillEdit,tutorial_SkillEdit);
            processesRunner.AddNewProcess(MainSceneStep.Gotcha,gotchaProcess);

            charIcon.iniFrames();
            _LoadingCanvas.nowProcess("正在启动技能石头背包", 0.6f);
            yield return (_SkillStonesBox.StartUp());
            _LoadingCanvas.nowProcess("正在加载技能编辑器", 0.7f);
            yield return (TheNineSlot.startUp());

            yield return AccountSet.Instance.loadCustomerInfo();
            accountDiamondCoin.text = AccountSet.Instance._PlayerAccountInfo.Diamond.ToString();
            accountIntelliCoin.text = AccountSet.Instance._PlayerAccountInfo.Coin.ToString();
            _LoadingCanvas.turnOnProcessDescription(false);
            yield return _modelShower.StartUpProcess();
            
            // 在以下的分歧之前，账户信息必须是最新，否则反应不到账户真实进度。
            switch (AccountSet.Instance._PlayerAccountInfo.accountprogress)
            {
                case playerAccountProgressStep.Freedom:
                    // 账户信息。。如果账户信息没有能读取成功的话那接下来的账户拥有财产等等都不应该继续尝试读取。
                    // 在正式版本当中读取账户信息应该就是获取token的过程。那么。。。说白了如果用户信息都没能获取那程序的初始化工作应该一点也不需要再进行了才对。
                    // 那这样的话势必我需要来看接下来这个请求工作的返回值。
                    IEnumerator localMyChractersProcess = AccountCharsSet.Instance.LoadMyOwnedAccountCharacterInfoList();
                    yield return (localMyChractersProcess);
                    //上面这些都缺response判断
                    yield return TeamSet.Instance.LoadTeamSet(TeamSetGameMode.story);
                    yield return MonsterBox.MonsterIconsGenerate();//这个进程会先找到所有角色的头像。
                    IEnumerator loadMyStonesProcess = MySkillStonesReader.Instance.LoadMySkillStones();
                    yield return loadMyStonesProcess;
                    yield return _TeamEditManager.INITeamPosButtons();
                    trySwitchToStep(MainMenuNote.Instance.goingtostep, false);
                    break;
                case playerAccountProgressStep.justCreated:
                    trySwitchToStep(MainSceneStep.Tutorial_skillEdit, false);
                    break;
                case playerAccountProgressStep.Tutorial:
                    trySwitchToStep(MainSceneStep.Tutorial_skillEdit,false);
                    break;
            }
            _LoadingCanvas.LightUp();
        }

        void Update()
        {
            processesRunner.ProcessNagare();
        }

        public void AskIfLoadFight(SceneMode sceneMode, StageScriptableObject stage)
        {
            _LoadingCanvas.arrangeValiationWindow(delegate { LoadFight(sceneMode, stage); }, "开打？");
        }

        public void LoadFight(SceneMode sceneMode, StageScriptableObject stage)//6.29 这个环节可能要进一步研究。进入战斗场景要做的事情安说很多，包括loadscene什么的，而这些都应该在这里进行。
        {
            FightSceneNote.Instance.nextBattle = stage;
            _CharSetManager.PreventTheseMyModelsFromDestroying(FightSceneNote.Instance.nextBattle.getTeam1EnterRingLocalIds(FightSceneNote.Instance.nextBattle.localFight));
            FightSceneModeManager.Instance.setSceneMode(sceneMode);
            SceneManager.LoadScene(FightSceneNote.Instance.nextBattle.BattleGroundID);
        }

        [EnumAction(typeof(MainSceneStep))]
        public void trySwitchToStep(int next_step)
        {
            trySwitchToStep((MainSceneStep)next_step, true);
        }

        [EnumAction(typeof(MainSceneStep))]
        public void trySwitchToStep(MainSceneStep next_step, bool foward)//这个是试图进入某个step。另一个是根据一些东西的选择情况来在某个step内对GUI进行刷新。两个都需要。
        {
            if (foward && processesRunner.currentProcess != null)
            {
                MainSceneStep returnToStep = processesRunner.currentProcess.thisProcessStep;
                void returnTOCurrent()
                {
                    trySwitchToStep(returnToStep, false);
                }
                _ReturnButtonManager.PUSH(returnTOCurrent);
            }

            processesRunner.changeProcess(next_step);
        }

        //看起来这个函数不应该在这个模块里，但其中的各种操作和整个mainmenu的乱七八糟东西相关性实在太多了，所以姑且放在这
        public IEnumerator MonsterIconButton(string localId)
        {
            Debug.Log("于monsterbox点下了如下localid的头像：" + localId + ",Scenestep:" + processesRunner.currentProcess.thisProcessStep);
            switch (processesRunner.currentProcess.thisProcessStep)
            {
                case MainSceneStep.SelfFightFront:
                    yield return _SelfFightManager.MonsterIConButton(localId);
                    break;
                case MainSceneStep.MemberDetail:
                    yield return _MemberDetail.SetMemberDetailSystemFocusingCharacter(localId);//确立focusing角色
                    yield return _MemberDetail.RefreshMemberDetailGamenSystemBaseOnFocusingChar();
                    break;
                case MainSceneStep.TeamEditFront:
                     yield return _MemberDetail.SetMemberDetailSystemFocusingCharacter(localId);//确立focusing角色
                    TeamEditFront process = (TeamEditFront)processesRunner.accessCertainMainSceneProcessObject(MainSceneStep.TeamEditFront);
                    yield return process.TeamEditMonsterDetailMonsterIconBehaviour();
                    yield return _MemberDetail.RefreshMemberDetailGamenSystemBaseOnFocusingChar();
                    break;
            }
            //_MonsterBox.adjustAllIconsSize(localId);
            yield break;
        }

        void OnGUI()
        {
            if (AccountSet.Instance._playerinfoReferenceMode == playerinfoReferenceMode.localTestSaveData)
            {
                if (GUI.Button(new Rect(0, 0, 100, 50), "All Characters"))
                {
                    mainProcessRunner.triggerMainProcess(AccountCharsSet.Instance.LocalSaveDataGetAllCharacters());
                }
                if (GUI.Button(new Rect(0, 50, 100, 50), "All stones"))
                {
                    IEnumerator getAllStones()
                    {
                        yield return SkillConfigTable.Instance.loadAllSkillConfigs();
                        List<SkillStoneOfPlayerInfoModel> mystones = new List<SkillStoneOfPlayerInfoModel>();
                        int i = 1;
                        foreach (KeyValuePair<string, SkillConfig> _pair in SkillConfigTable.Instance.SkillConfigDicForReference)
                        {
                            Debug.Log("尝试于本地存档追加石：" + _pair.Value.REAL_NAME);
                            var skillStoneOfPlayerInfoModel = new SkillStoneOfPlayerInfoModel
                            {
                                skillStoneOfPlayerId = String.Format("{0:D20}", i),
                                skillId = _pair.Value.RECORD_ID
                            };
                            mystones.Add(skillStoneOfPlayerInfoModel);
                            i++;
                        }
                        MySkillStonesReader.Instance.OverrideMySkillStoneInfosOnLocalFile(mystones);
                    }
                    mainProcessRunner.triggerMainProcess(getAllStones());
                }
            }
        }
    }
}