using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using mainMenu;

public enum SceneMode
{
    localDebug = 1,
    QuestFight = 3,
    MyPetsFight = 6,
}

public class NetFightScene : MonoBehaviour {

	public SceneMode _SceneMode = SceneMode.localDebug;

    [Space(11)]
    [Header("Canvas")]
    public Canvas PreparingCanvas,FightCanvas;
    
    [Space(11)]
    [Header("Basic Essentials")]
    public CameraManager _CameraManager;
	public CharsManager _CharSetManager;
    public DebugManager _DebugManager;

    [Space(11)]
    [Header("战斗前的演出")]
    public FightTalksRunner _FightTalksRunner;

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
    readonly FightSceneProcessesRunner fightSceneProcessesRunner = new FightSceneProcessesRunner();

    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        //QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        FightGlobalSetting.scenestep = 1; 
        mainProcessRunner.TriggerMainProcess(FightSceneStartUp());
    }

    private IEnumerator FightSceneStartUp()
    {
        Time.timeScale = 1f;
        //Position_Set_Executor.Instance.P_sets.Clear();
        _SceneMode = FightSceneModeManager.Instance.getSceneMode();
        if (this._SceneMode == SceneMode.QuestFight || this._SceneMode == SceneMode.MyPetsFight)
        {
            PreparingProcess preparingProcess = new PreparingProcess(this,fightSceneProcessesRunner);
            FightingProcess fightingProcess = new FightingProcess(this,fightSceneProcessesRunner);
            CountDownProcess countDownProcess = new CountDownProcess(this,fightSceneProcessesRunner);
            StoryProcess storyProcess = new StoryProcess(this,fightSceneProcessesRunner);
            FightOverProcess fightOverProcess = new FightOverProcess(this,fightSceneProcessesRunner);
            FightSummaryProcess fightSummaryProcess = new FightSummaryProcess(this,fightSceneProcessesRunner);
            
            BasicTryProcess basicTryProcess = new BasicTryProcess(this,fightSceneProcessesRunner);
            
            fightSceneProcessesRunner.AddNewProcess(SceneStep.Preparing, preparingProcess);
            fightSceneProcessesRunner.AddNewProcess(SceneStep.StoryBeforeFight, storyProcess);
            fightSceneProcessesRunner.AddNewProcess(SceneStep.CountDown, countDownProcess);
            fightSceneProcessesRunner.AddNewProcess(SceneStep.Fighting, fightingProcess);
            fightSceneProcessesRunner.AddNewProcess(SceneStep.FightOver, fightOverProcess);
            fightSceneProcessesRunner.AddNewProcess(SceneStep.FightSummary, fightSummaryProcess);
            fightSceneProcessesRunner.AddNewProcess(SceneStep.BasicTryTutorial,basicTryProcess);
        }
        if (this._SceneMode == SceneMode.localDebug)// 基本上用不到的xml 测试进程
        {
            OldDebugPreparingProcess oldDebugPreparingProcess = new OldDebugPreparingProcess(this,_DebugManager);
            OldDebugFightingProcess oldDebugFightingProcess = new OldDebugFightingProcess(this,_DebugManager);
            fightSceneProcessesRunner.AddNewProcess(SceneStep.Preparing,oldDebugPreparingProcess);
            fightSceneProcessesRunner.AddNewProcess(SceneStep.Fighting,oldDebugFightingProcess);
        }
        
        FightSceneProcessesRunner.ChangeProcess(SceneStep.Preparing);
        yield break;
    }

    void Update()
    {
        fightSceneProcessesRunner.LocalUpdate();
    }

    public IEnumerator LoadGame(StageScriptableObject stage)
    {
        switch (FightSceneNote.Instance.nextBattle.fightModeType)
        {
            case fightModeType.combat:
                _RealTimeGameProcessManager.FightTeam1.TeamMode = stage.Team1Mode;
                _RealTimeGameProcessManager.FightTeam2.TeamMode = stage.Team2Mode;
                _RealTimeGameProcessManager.FightTeam1.teamConfig = _RealTimeGameProcessManager.heroTeamConfig;
                _RealTimeGameProcessManager.FightTeam2.teamConfig = _RealTimeGameProcessManager.EnemyTeamConfig;
                yield return _RealTimeGameProcessManager.FightTeam1.Instantiate (stage.localFight.HeroSets);
                yield return _RealTimeGameProcessManager.FightTeam2.Instantiate (stage.localFight.EnemySets);
                _CharSetManager.ArrangeAllCharacterToPosition(_RealTimeGameProcessManager.FightTeam1.teamMembers, _RealTimeGameProcessManager.FightTeam2.teamMembers, Team1StandPoints, Team2StandPoints);
                break;
        }
        LoadStageFinished.Value = true;
    }
           
    // 本地系函数
    public void pressedStartButton()
    {
        switch (FightSceneNote.Instance.nextBattle.fightModeType)
        {
            case fightModeType.combat:
                _RealTimeGameProcessManager.FightTeam1.ModeStart();
                _RealTimeGameProcessManager.FightTeam2.ModeStart();
                switch (RealTimeGameProcessManager.playerTeam)
                {
                    case Team.player1:
                        _RealTimeGameProcessManager.SwitchToCMode(_RealTimeGameProcessManager.FightTeam1.teamMembers.values[0], false);
                        break;
                    case Team.player2:
                        _RealTimeGameProcessManager.SwitchToCMode(_RealTimeGameProcessManager.FightTeam2.teamMembers.values[0], false);
                        break;
                }
                break;
        }
    }
    
    // 这个函数应该包括一些更深层的考虑。
    public void returnToFront()
    {
        //Position_Set_Executor.Instance.P_sets.Clear();
        List<Data_Center> player1 = _RealTimeGameProcessManager.FightTeam1.teamMembers.values;
        List<string> dontdestroy = new List<string>();
        for (int i = 0; i < player1.Count; i++)
        {
            if (player1[i] != null)
            {
                player1[i].AIStateRunner.ChangeState("Empty");
                CharacterDataInfo characterDataInfo = _RealTimeGameProcessManager.FightTeam1.CharacterDataInfoReference[player1[i]];
                if (characterDataInfo != null)
                    dontdestroy.Add(characterDataInfo.monsterOfPlayerId);
            }
        }
        
        _CharSetManager.PreventTheseMyModelsFromDestroying(dontdestroy);
        _RealTimeGameProcessManager.Clear();
        FightSceneProcessesRunner.Clear();
        MainMenuNote.Instance.goingtostep = MainSceneStep.frontPage;
        SceneManager.LoadScene(1);
    }

	//本地系函数 
	public void LocalGameRestart()
	{
        FightSceneProcessesRunner.ChangeProcess(SceneStep.Preparing);
		SceneManager.LoadScene(FightSceneNote.Instance.nextBattle.BattleGroundID);
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
    
    public void passFightSummary()
    {
        FightSummaryProcess process = (FightSummaryProcess)fightSceneProcessesRunner.AccessCertainFightSceneProcessObject(SceneStep.FightSummary);
        process.enternext.Value = true;
    }
}

//void netFightDebugNAGARE()
//{
//  switch(this.step)
//  {
//      case SceneStep.CountDownEnter:
//          if (PhotonNetwork.isMasterClient) 
//          {
//          }
//      break;
//      case SceneStep.CountDown:

//          CountDown.text = "" + (1 + (int) (startTimestamp - PhotonNetwork.time));
//          if (PhotonNetwork.time >= startTimestamp)
//          {
//              //StartRace();
//          }
//      break;
//  }
//}

/// <summary>
/// 以下环节我们的用意是把之前那个生成角色的功能给嫁接到这个模块里。但要有一些改进。现在不需要由一个manger和好几个子角色生成器同步
/// 简单来说我们的经验是，不要有什么太远太复杂的状态量引用
/// </summary>

//

//  public void onTypeChanged()
//  {
//      _charDataBaseSet = _CharSetManager.getCharDataBaseSetByType(type.options[type.value].text);
//      if (_charDataBaseSet == null)
//      {
//          return;
//      }
//      List<string> resourceNames = _charDataBaseSet._CharResourceDataBase.getAllResourceNames();
//      foreach (string name in resourceNames)
//      {
//          Dropdown.OptionData m_NewData = new Dropdown.OptionData();
//          m_NewData.text = name;
//          prefab_num.options.Add(m_NewData);
//      }

//      List<string> AISeriesNames = _charDataBaseSet._AIDataBase.getALLAISeriesNames();
//      AISnum.ClearOptions();
//      foreach(string name in AISeriesNames)
//      {
//          Dropdown.OptionData m_NewData = new Dropdown.OptionData();
//          m_NewData.text = name;
//          AISnum.options.Add(m_NewData);
//      }
//AIlevelNum.text = "0";
//}

//public void NetFight_NAGARE()
//{
//  switch(this.step)
//  {
//      case SceneStep.Fighting:    
//      if (Icons.GetFocusingChar () != null) {
//          if (fucousingChar != Icons.GetFocusingChar ()) 
//               {
//                       fucousingChar.getRunner().playerMode = false;
//                   fucousingChar = Icons.GetFocusingChar ().gameObject.GetComponent<AI_DATA_CENTER>();
//          }
//      } else {
//          this.fucousingChar = null;
//      }

//      if (fucousingChar != null) {
//          if (_CameraManager.current_Camera_Mode_Num != Camera_Mode_Num.GodPlayerMode || !_CameraManager.current_Camera_Mode.targets.Contains(fucousingChar.transform)) 
//          {
//              _CameraManager.Assign_Camera (Camera_Mode_Num.GodPlayerMode, new List<Transform> () { fucousingChar.transform });
//          }
//      } else {
//          if (_CameraManager.current_Camera_Mode_Num != Camera_Mode_Num.GodMode) {
//              _CameraManager.Assign_Camera (Camera_Mode_Num.GodMode);
//          }
//      }

//      if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
//          if (fucousingChar != null) 
//          {
//                       if (fucousingChar.getRunner().playerMode)
//              {
//                  //mobile_input.SetActive(true);
//              }
//              else
//              {
//                  //mobile_input.SetActive(false);
//              }
//          }
//      }else{
//          //mobile_input.SetActive(false);
//      }
//      FightCanvas.SetActive(true);
//      FightOverCanvas.SetActive(false);
//      PreparingCanvas.SetActive(false);

//      if (fucousingChar != null) 
//      {
//                   if (fucousingChar.getRunner().playerMode)
//          {
//              if (_CameraManager.current_Camera_Mode_Num != Camera_Mode_Num.GodPlayerMode)
//              {
//                  _CameraManager.Assign_Camera(Camera_Mode_Num.GodPlayerMode, new List<Transform>() { fucousingChar.transform });
//              }
//          }else{
//              if (_CameraManager.current_Camera_Mode_Num != Camera_Mode_Num.GodMode)
//              {
//                  _CameraManager.Assign_Camera(Camera_Mode_Num.GodMode);
//              }
//          }

//          if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
//          {
//                       if (fucousingChar.getRunner().playerMode)
//              {
//                  //mobile_input.SetActive(true);
//              }
//              else
//              {
//                  //mobile_input.SetActive(false);
//              }
//          }
//          else
//          {
//              //mobile_input.SetActive(false);
//          }
//      }                   
//      if (_fightingSceneNetworkManager.getWinnerTag() != null)
//      {
//          step = SceneStep.FightOver;
//          this.arrangeMemberOfEveryTeam ();
//               List<Data_Center> winners = getTeamMembers (_fightingSceneNetworkManager.getWinnerTag());
//               foreach (Data_Center _one in winners)
//          {
//                       _one.getRunner().changeState("victory");//逻辑问题！！！！！！！！！！！！！！！
//          }
//      }
//      break;
//      case SceneStep.Preparing:
//      PreparingCanvas.SetActive (true);
//      FightCanvas.SetActive (false);
//      FightOverCanvas.SetActive (false);
//      if (_CameraManager.current_Camera_Mode_Num != Camera_Mode_Num.GodMode)
//          _CameraManager.Assign_Camera(Camera_Mode_Num.GodMode);
//      if (this.debugModePlayerPlacementStep == 1 || this.debugModePlayerPlacementStep == 2 || this.debugModePlayerPlacementStep == 3)
//      {
//          //placingCharacter ();
//      }

//      gameStartButton.gameObject.SetActive(false);
//      if (PhotonNetwork.inRoom)
//      {
//          if (this.checkIfEveryTeamHasMember())
//          {
//              netFightPreparedReadyButton.gameObject.SetActive(true);
//          }else{
//              netFightPreparedReadyButton.gameObject.SetActive(false);
//          }

//          if (this.checkIfEveryTeamHasMember() && this._fightingSceneNetworkManager.checkIfEveryPlayerIsReady())
//          {     
//              if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
//                  //mobile_input.SetActive(true);
//              }                   
//              this.changeMyPhotonViewOwnCharactersState("Defend");
//          }
//      }
//      break;
//  case SceneStep.FightOver:
//               LocalGameRestartButton.gameObject.SetActive (false);
//      if (this._fightingSceneNetworkManager.getWinnerTag () == this._CharSetManager.getNetModeMyPlayerTag ()) 
//      {
//                   List<Data_Center> winners = getMyMembers ();
//                   foreach (Data_Center onePersonOfTheWinnerTeam in winners) 
//          {
//                       if (onePersonOfTheWinnerTeam.getRunner().current_state_num != "Victory") {
//                           onePersonOfTheWinnerTeam.getRunner().changeState ("Victory");
//              }
//          }
//      }
//           returnToLobbyButton.gameObject.SetActive (true);
//      FightOverCanvas.SetActive(true);
//      PreparingCanvas.SetActive(false);
//      FightCanvas.SetActive(false);
//      break;
//  }
//}

//网络系函数
//  public void pressedReadyButton()
//  {
//this._playerNetInfo.ifFightReady = true;
//}

//// 网络系函数
//public void transferMyOwnedCharacterToOpponent()
//{
//  _fightingSceneNetworkManager.transferCharactersOwnerShip (getMyPhotonViewOwnCharacters(),PhotonNetwork.player.ID,_fightingSceneNetworkManager.getOpponentPlayerID(PhotonNetwork.player.ID));
//}

// 网络系函数
//public void changeMyPhotonViewOwnCharactersState(string num)
//{
//  List<GameObject> myPhotonViewOwnCharacters = getMyPhotonViewOwnCharacters ();
//  foreach (GameObject one in myPhotonViewOwnCharacters)
//  {
//      one.GetComponent<AIStateRunner>().changeState(num);
//  }
//}

// 网络系函数
//public List<GameObject> getMyPhotonViewOwnCharacters()
//{
//  List<GameObject> myPhotonViewOwnCharacters = new List<GameObject> ();
//  List<AIStateRunner> _charsOfTheFight = Transform.FindObjectsOfType<AIStateRunner>().ToList();
//  foreach (AIStateRunner _AIStateRunner in _charsOfTheFight)
//  {
//      if (_AIStateRunner.gameObject.GetComponent<PhotonView>().isMine)
//      {
//          myPhotonViewOwnCharacters.Add (_AIStateRunner.gameObject);
//      }
//  }
//  return myPhotonViewOwnCharacters;//
//}

// 网络系函数
//  public void returnToLobby()
//  {       
//      //Position_Set_Executor.Instance.P_sets.Clear();
//PhotonNetwork.LoadLevel("Connecting");
//}