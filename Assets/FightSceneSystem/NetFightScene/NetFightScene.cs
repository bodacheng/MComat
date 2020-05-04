using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;
using mainMenu;
using Soul;
using UnityEngine.Playables;

public class NetFightScene : MonoBehaviour {

    [Space(11)]
    [Header("Canvas")]
    public Canvas PreparingCanvas,FightCanvas;
    
    #region before fight
    [Space(11)]
    [Header("PlayableDirector")]
    public PlayableDirector playableDirector;
    [Space(11)]
    [Header("CountDownText")]
    public Text CountDown;
    #endregion

    [Space(11)]
    [Header("Basic Essentials")]
    public CameraManager _CameraManager;
	public CharsManager _CharSetManager;
    public DebugManager _DebugManager;

    [Space(11)]
    [Header("战斗的最后一击时候的处理")]
    public FightOverControl _FightOverControl;
    
    [Space(11)]
    [Header("战斗信息记录器")]
    public FightLogger fightLogger;

    [Space(11)]
    [Header("场地控制")]
    public BoundaryControllByGod _BoundaryControllByGod;

    [Space(11)]
    [Header("部分UI要素的管理模块")]
    public RealTimeGameProcessManager _RealTimeGameProcessManager;

    [Space(11)]
    public RectTransform PauseMenu;
    
    [Space(11)]
    [Header("双方站位点")]
    public Transform[] Team1StandPoints,Team2StandPoints;//这个也是应该按模式区分，能改名字现在就改名字吧。免得以后乱
       
    // 主进程
    [Space(7)]
    [Header("主进程处理器")]
    public SingleThreadProcesser mainProcessRunner;
    
    public ReactiveProperty<bool> LoadStageFinished{ get; set; } = new ReactiveProperty<bool>(false);
    readonly FightSceneProcessesRunner ProcessesRunner = new FightSceneProcessesRunner();

    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        //QualitySettings.vSyncCount = 1;
        Screen.SetResolution(1920, 1080, true);
        Application.targetFrameRate = 60;
        FightGlobalSetting.scenestep = 1; 
        mainProcessRunner.Run(FightSceneStartUp());
    }

    IEnumerator FightSceneStartUp()
    {
        Time.timeScale = 1f;
        //Position_Set_Executor.Instance.P_sets.Clear();
        PreparingProcess preparingProcess = new PreparingProcess(this, ProcessesRunner);
        FightingProcess fightingProcess = new FightingProcess(this, ProcessesRunner);
        CountDownProcess countDownProcess = new CountDownProcess(this, ProcessesRunner);
        StoryProcess storyProcess = new StoryProcess(this, ProcessesRunner);
        FightOverProcess fightOverProcess = new FightOverProcess(this, ProcessesRunner);
        FightSummaryProcess fightSummaryProcess = new FightSummaryProcess(this, ProcessesRunner);
        
        BasicTryProcess basicTryProcess = new BasicTryProcess(this, ProcessesRunner);
        
        ProcessesRunner.AddNewProcess(SceneStep.Preparing, preparingProcess);
        ProcessesRunner.AddNewProcess(SceneStep.StoryBeforeFight, storyProcess);
        ProcessesRunner.AddNewProcess(SceneStep.CountDown, countDownProcess);
        ProcessesRunner.AddNewProcess(SceneStep.Fighting, fightingProcess);
        ProcessesRunner.AddNewProcess(SceneStep.FightOver, fightOverProcess);
        ProcessesRunner.AddNewProcess(SceneStep.FightSummary, fightSummaryProcess);
        ProcessesRunner.AddNewProcess(SceneStep.BasicTryTutorial, basicTryProcess);
        
        FightSceneProcessesRunner.ChangeProcess(SceneStep.Preparing);
        yield break;
    }

    void Update()
    {
        ProcessesRunner.LocalUpdate();
    }

    public IEnumerator LoadGame(StageScriptableObject stage)
    {
        _RealTimeGameProcessManager.FightTeam1.TeamMode = stage.Team1Mode;        
        switch (_RealTimeGameProcessManager.FightTeam1.TeamMode)
        {
            case TeamMode.multiraid:
            _RealTimeGameProcessManager.FightTeam1 = _RealTimeGameProcessManager.FightTeam1_multi;
            break;
            case TeamMode.rotation:
            _RealTimeGameProcessManager.FightTeam1 = _RealTimeGameProcessManager.FightTeam1_rotation;
            break;
            case TeamMode.test:
            _RealTimeGameProcessManager.FightTeam1 = _RealTimeGameProcessManager.FightTeam1_multi;
            break;
        }
        
        _RealTimeGameProcessManager.FightTeam2.TeamMode = stage.Team2Mode;
        switch (_RealTimeGameProcessManager.FightTeam2.TeamMode)
        {
            case TeamMode.multiraid:
            _RealTimeGameProcessManager.FightTeam2 = _RealTimeGameProcessManager.FightTeam2_multi;
            break;
            case TeamMode.rotation:
            _RealTimeGameProcessManager.FightTeam2 = _RealTimeGameProcessManager.FightTeam2_rotation;
            break;
            case TeamMode.test:
            _RealTimeGameProcessManager.FightTeam2 = _RealTimeGameProcessManager.FightTeam2_multi;
            break;
        }

        _RealTimeGameProcessManager.FightTeam1.TeamStandPoints = Team1StandPoints;
        _RealTimeGameProcessManager.FightTeam2.TeamStandPoints = Team2StandPoints;
        
        _RealTimeGameProcessManager.FightTeam1.teamConfig = _RealTimeGameProcessManager.heroTeamConfig;
        _RealTimeGameProcessManager.FightTeam2.teamConfig = _RealTimeGameProcessManager.EnemyTeamConfig;
        
        yield return _RealTimeGameProcessManager.FightTeam1.Instantiate (stage.localFight.HeroSets,stage.team1_ExtraHP);
        yield return _RealTimeGameProcessManager.FightTeam2.Instantiate (stage.localFight.EnemySets,stage.team2_ExtraHP);
        
        _RealTimeGameProcessManager.FightTeam1.ArrangeAllTeamMembersToPosition(_RealTimeGameProcessManager.FightTeam1.TeamMembers);
        _RealTimeGameProcessManager.FightTeam2.ArrangeAllTeamMembersToPosition(_RealTimeGameProcessManager.FightTeam2.TeamMembers);
        
        switch (RealTimeGameProcessManager.playerTeam)
        {
            case Team.player1:
                _RealTimeGameProcessManager.SwitchToCMode(_RealTimeGameProcessManager.FightTeam1.TeamMembers.values[0],false);
                break;
            case Team.player2:
                _RealTimeGameProcessManager.SwitchToCMode(_RealTimeGameProcessManager.FightTeam2.TeamMembers.values[0],false);
                break;
        }

        LoadStageFinished.Value = true;
    }
           
    // 本地系函数
    public void PressedStartButton()
    {
        _RealTimeGameProcessManager.FightTeam1.ModeStart();
        _RealTimeGameProcessManager.FightTeam2.ModeStart();
        switch (RealTimeGameProcessManager.playerTeam)
        {
            case Team.player1:
                _RealTimeGameProcessManager.SwitchToCMode(_RealTimeGameProcessManager.FightTeam1.TeamMembers.values[0], false);
                break;
            case Team.player2:
                _RealTimeGameProcessManager.SwitchToCMode(_RealTimeGameProcessManager.FightTeam2.TeamMembers.values[0], false);
                break;
        }
        _RealTimeGameProcessManager.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
    }
    
    // 这个函数应该包括一些更深层的考虑。
    public void ReturnToFront()
    {
        //Position_Set_Executor.Instance.P_sets.Clear();
        List<Data_Center> player1 = _RealTimeGameProcessManager.FightTeam1.TeamMembers.values;
        List<string> dontdestroy = new List<string>();

        List<SingleFightLog> singleFightLogs = new List<SingleFightLog>();
        for (int i = 0; i < player1.Count; i++)
        {
            if (player1[i] != null)
            {
                singleFightLogs.Add(player1[i]._MyBehaviorRunner.SingleFightLog);
                player1[i]._MyBehaviorRunner.ChangeState("Empty");
                CharDataInfo characterDataInfo = _RealTimeGameProcessManager.FightTeam1.CharDataInfoRef[player1[i]];
                if (characterDataInfo != null)
                {
                    dontdestroy.Add(characterDataInfo.monsterOfPlayerId);
                }
            }
        }
        
        List<Data_Center> player2 = _RealTimeGameProcessManager.FightTeam2.TeamMembers.values;
        for (int i = 0; i < player2.Count; i++)
        {
            if (player2[i] != null)
            {
                singleFightLogs.Add(player2[i]._MyBehaviorRunner.SingleFightLog);
            }
        }
        
        _CharSetManager.PreventTheseMyModelsFromDestroying(dontdestroy);
        _RealTimeGameProcessManager.Clear();
        
        if (FightGlobalSetting.HitBoxLogger)
        {
            HitBoxLogTable.Instance.Load(HitBoxLogger.Instance.LoadCurrentToString());
            HitBoxLogger.Instance.LogSummit();
            for (int i = 0; i < singleFightLogs.Count; i++)
            {
                singleFightLogs[i].Summary();
            }
            HitBoxLogTable.Instance.SaveByCurrentRows_HitBoxLog(Application.persistentDataPath + "/HitBoxLog.csv",HitBoxLogger.Instance,singleFightLogs);
            for (int i = 0; i < singleFightLogs.Count; i++)
            {
                singleFightLogs[i].Clear();
            }
            HitBoxLogger.Instance.Clear();
        }

        FightSceneProcessesRunner.Clear();
        MainMenuNote.goingtostep = MainSceneStep.FrontPage;
        SceneManager.LoadScene(1);
    }
    
    //本地系函数 
    public void LocalGameRestart()
    {
        FightSceneProcessesRunner.ChangeProcess(SceneStep.Preparing);
    	SceneManager.LoadScene(FightSceneNote.nextBattle.BattleGroundID);
    }
    
    // 本地系函数 而且目前有逻辑问题
    public void ResumeScene()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
    // 本地系函数 而且目前有逻辑问题
    public void PauseScene()
    {
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void PassFightSummary()
    {
        FightSummaryProcess process = (FightSummaryProcess)ProcessesRunner.AccessCertainFightSceneProcessObject(SceneStep.FightSummary);
        process.enternext.Value = true;
    }
}