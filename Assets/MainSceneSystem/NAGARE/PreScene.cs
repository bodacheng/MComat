using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using dataAccess;
using Api.Dto.Model;
using Skill;

namespace mainMenu
{
    public class PreScene : MonoBehaviour
    {
        public static PreScene Instance;

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
        [Header("CustomGUISkin")]
        public GUISkin CustomGUISkin;
                        
        [Space(7)]
        [Header("自我战斗管理模块")]
        public SelfFightManager _SelfFightManager;
        
        [Space(7)]
        [Header("抽奖管理模块")]
        public gotchaManager _gotchaManager;
        
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
        
        void Awake()
        {
            Instance = this;
        }
        
        void Start()
        {
            //_stagesManager.loadAndRefresh();
            Time.timeScale = 1;
            FightGlobalSetting.scenestep = 0;
            mainProcessRunner.TriggerMainProcess(StartUpProcess());
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
            yield return null; // 这一行的目的是为了让整个项目那些靠start（）里进行初始化工作的模块顺利完成初始化后在开始下面的各种加载 

            _SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
            TheNineSlot.Instance.gameObject.SetActive(false);
            _MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
            MainMenuCanvas.gameObject.SetActive(false);

            LoadingCanvas.target.DarkOff(0.5f);
            LoadingCanvas.target.TurnOnProcessDescription(true);
            LoadingCanvas.target.NowProcess("正在读取账户信息", 0);

            //SceneProcessDictionary
            TeamEditFront teamEditFront = new TeamEditFront();
            SkillStones skillStones = new SkillStones();
            SelfFightFront selfFightFront = new SelfFightFront();
            QuestInfo questInfo = new QuestInfo();
            MemberDetailProcess memberDetail = new MemberDetailProcess();
            MemberDetail_edit memberDetail_edit = new MemberDetail_edit();
            MemberDetail_skillshow memberDetail_Skillshow = new MemberDetail_skillshow();
            frontPage frontPage = new frontPage();
            ArcadeFrontProcess arcadeFrontProcess = new ArcadeFrontProcess(ArcadeManager.Instance.ButtonsContainer);
            Tutorial_skillEdit tutorial_SkillEdit = new Tutorial_skillEdit();
            GotchaProcess gotchaProcess = new GotchaProcess();

            ProcessesRunner.Instance.Clear();
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.TeamEditFront, teamEditFront);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.SkillStones, skillStones);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.SelfFightFront, selfFightFront);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.QuestInfo, questInfo);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.MemberDetail, memberDetail);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.MemberDetail_edit, memberDetail_edit);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.MemberDetail_show, memberDetail_Skillshow);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.frontPage, frontPage);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.ArcadeFront, arcadeFrontProcess);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.Tutorial_skillEdit,tutorial_SkillEdit);
            ProcessesRunner.Instance.AddNewProcess(MainSceneStep.Gotcha,gotchaProcess);

            yield return AccountSet.Instance.LoadCustomerInfo();
            accountDiamondCoin.text = AccountSet.Instance._PlayerAccountInfo.Diamond.ToString();
            accountIntelliCoin.text = AccountSet.Instance._PlayerAccountInfo.Coin.ToString();
            LoadingCanvas.target.TurnOnProcessDescription(false);
            yield return _modelShower.StartUpProcess();
            
            HeroIcon.IniFrames();
            LoadingCanvas.target.NowProcess("正在启动技能石头背包", 0.6f);
            yield return (_SkillStonesBox.StartUp(AccountSet.Instance._PlayerAccountInfo.Stoneboxsize));
            LoadingCanvas.target.NowProcess("正在加载技能编辑器", 0.7f);
            yield return (TheNineSlot.Instance.StartUp());
            
            // 在以下的分歧之前，账户信息必须是最新，否则反应不到账户真实进度。
            switch (AccountSet.Instance._PlayerAccountInfo.accountprogress)
            {
                case PlayerAccountProgressStep.Freedom:
                    // 账户信息。。如果账户信息没有能读取成功的话那接下来的账户拥有财产等等都不应该继续尝试读取。
                    // 在正式版本当中读取账户信息应该就是获取token的过程。那么。。。说白了如果用户信息都没能获取那程序的初始化工作应该一点也不需要再进行了才对。
                    // 那这样的话势必我需要来看接下来这个请求工作的返回值。
                    IEnumerator localMyChractersProcess = AccountCharsSet.Instance.LoadMyOwnedAccountCharacterInfoList();
                    yield return (localMyChractersProcess);
                    //上面这些都缺response判断
                    yield return TeamSet.Instance.LoadTeamSet(TeamSetGameMode.story);
                    yield return MonsterBox.DisplayMonsterIcons();//这个进程会先找到所有角色的头像。
                    IEnumerator loadMyStonesProcess = MySkillStonesReader.Instance.LoadMySkillStones();
                    yield return loadMyStonesProcess;
                    yield return TeamEditManager.Instance.INITeamPosButtons();
                    trySwitchToStep(MainMenuNote.Instance.goingtostep, false);
                    break;
                case PlayerAccountProgressStep.justCreated:
                    trySwitchToStep(MainSceneStep.Tutorial_skillEdit, false);
                    break;
                case PlayerAccountProgressStep.Tutorial:
                    trySwitchToStep(MainSceneStep.Tutorial_skillEdit,false);
                    break;
            }
            LoadingCanvas.target.LightUp();
        }

        void Update()
        {
            ProcessesRunner.Instance.ProcessNagare();
        }
        
        public void AskIfLoadFight(SceneMode sceneMode, StageScriptableObject stage)
        {
            LoadingCanvas.target.ArrangeValiationWindow(delegate { LoadFight(sceneMode, stage); }, "开打？");
        }

        public void LoadFight(SceneMode sceneMode, StageScriptableObject stage)//6.29 这个环节可能要进一步研究。进入战斗场景要做的事情安说很多，包括loadscene什么的，而这些都应该在这里进行。
        {
            FightSceneNote.Instance.nextBattle = stage;
            _CharSetManager.PreventTheseMyModelsFromDestroying(FightSceneNote.Instance.nextBattle.GetTeam1EnterRingLocalIds(FightSceneNote.Instance.nextBattle.localFight));
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
            if (foward && ProcessesRunner.Instance.currentProcess != null)
            {
                MainSceneStep returnToStep = ProcessesRunner.Instance.currentProcess.thisProcessStep;
                void returnTOCurrent()
                {
                    trySwitchToStep(returnToStep, false);
                }
                _ReturnButtonManager.PUSH(returnTOCurrent);
            }

            ProcessesRunner.Instance.ChangeProcess(next_step);
        }

        //看起来这个函数不应该在这个模块里，但其中的各种操作和整个mainmenu的乱七八糟东西相关性实在太多了，所以姑且放在这
        public IEnumerator MonsterIconButton(string localId)
        {
            switch (ProcessesRunner.Instance.currentProcess.thisProcessStep)
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
                    TeamEditFront process = (TeamEditFront)ProcessesRunner.Instance.AccessCertainMainSceneProcessObject(MainSceneStep.TeamEditFront);
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
                    mainProcessRunner.TriggerMainProcess(AccountCharsSet.Instance.LocalSaveDataGetAllCharacters());
                }
                if (GUI.Button(new Rect(0, 50, 100, 50), "All stones"))
                {
                    if (Directory.Exists(Application.persistentDataPath) && File.Exists(Application.persistentDataPath + "/MySkillStones.json"))
                    {
                        File.Delete(Application.persistentDataPath + "/MySkillStones.json");
                    }
                    IEnumerator GetAllStones()
                    {
                        yield return SkillConfigTable.Instance.LoadAllSkillConfigs();
                        int i = 1;
                        foreach (KeyValuePair<string, SkillConfig> _pair in SkillConfigTable.Instance.SkillConfigDicForReference)
                        {
                            Debug.Log("尝试于本地存档追加石：" + _pair.Value.REAL_NAME);
                            var skillStoneOfPlayerInfoModel = new SkillStoneOfPlayerInfoModel
                            {
                                skillStoneOfPlayerId = string.Format("{0:D20}", i),
                                skillId = _pair.Value.RECORD_ID,
                                level = 1.ToString()
                            };
                            yield return SkillStonesBox.Instance.GenerateOneStone(skillStoneOfPlayerInfoModel);
                            i++;
                        }
                        MySkillStonesReader.Instance.OverrideMySkillStoneInfosOnLocalFile(MySkillStonesReader.mySkillStonesDataDic.Values.ToList());
                        yield return SkillStonesBox.Instance.ArrangeSkillStonesToBox();
                    }
                    mainProcessRunner.TriggerMainProcess(GetAllStones());
                }
            }
        }
    }
}