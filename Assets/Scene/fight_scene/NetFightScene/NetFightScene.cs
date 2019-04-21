using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public enum SceneMode : int
{
	localDebug = 1,
	QuestFight = 3,
    MyPetsFight = 6,
	netFight = 2,
	netDebugFight = 4,
}

public partial class NetFightScene : MonoBehaviour {

	public SceneMode _SceneMode = SceneMode.localDebug;

    [Space(11)]
    [Header("Canvas")]
    public Canvas PreparingCanvas,FightCanvas;

    [Space(11)]
    [Header("Basic Essentials")]
    public CameraManager _CameraManager;//这个东西其实其他模块如果也拥有对其操作权的话，倒不会产生多大的问题
	public CharsManager _CharSetManager;//0610 charsetmanger临时扮演数据库的作用
    public DebugManager _DebugManager;

    [Space(11)]
    [Header("战斗前的演出")]
    public FightTalksRunner _FightTalksRunner;

    [Space(11)]
    [Header("战斗的最后一击时候的处理")]
    public FightOverControl _FightOverControl;

    [Space(11)]
    [Header("场地控制")]
    public BoundaryControllByGod _BoundaryControllByGod;

    [Space(11)]
    [Header("部分UI要素的管理模块")]
    public jueSeLiebiao Icons;

    [Space(11)]
    public Transform BattleGroundTransform;
	public Transform[] Team1StandPoints,Team2StandPoints;//这个也是应该按模式区分，能改名字现在就改名字吧。免得以后乱
    public RectTransform PauseMenu;

    private IEnumerator fightSceneProcess;
    private bool loadStageFinished = false;

    //大状态机
    private PreparingProcess preparingProcess;
    private StoryProcess storyProcess;
    private CountDownProcess countDownProcess;
    private FightingProcess fightingProcess;
    private FightOverProcess fightOverProcess;

    // 基本上用不到的xml 测试进程
    private OldDebugPreparingProcess oldDebugPreparingProcess;
    private OldDebugFightingProcess oldDebugFightingProcess;

    public void RunFightSceneProcess(IEnumerator enumerator)
    {
        if (fightSceneProcess != null)
            StopCoroutine(fightSceneProcess);
        fightSceneProcess = enumerator;
        StartCoroutine(fightSceneProcess);
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;

        //Position_Set_Executor.Instance.P_sets.Clear();
        _SceneMode = FightSceneModeManager.Instance.getSceneMode();
        _CameraManager.Assign_Camera(Camera_Mode_Num.GodMode);

        SceneProcessDictionary = new Dictionary<SceneStep, NagareProcess>();
        if (this._SceneMode == SceneMode.QuestFight || this._SceneMode == SceneMode.MyPetsFight)
        {
            preparingProcess = new PreparingProcess(this);
            fightingProcess = new FightingProcess(this);
            countDownProcess = new CountDownProcess(this);
            storyProcess = new StoryProcess(this);
            fightOverProcess = new FightOverProcess(this);
    
            SceneProcessDictionary.Add(SceneStep.Preparing, preparingProcess);
            SceneProcessDictionary.Add(SceneStep.StoryBeforeFight, storyProcess);
            SceneProcessDictionary.Add(SceneStep.CountDown, countDownProcess);
            SceneProcessDictionary.Add(SceneStep.Fighting, fightingProcess);
            SceneProcessDictionary.Add(SceneStep.FightOver, fightOverProcess);
        }
        if (this._SceneMode == SceneMode.localDebug)
        {
            oldDebugPreparingProcess = new OldDebugPreparingProcess(this,_DebugManager);
            oldDebugFightingProcess = new OldDebugFightingProcess(this,_DebugManager);

            SceneProcessDictionary.Add(SceneStep.Preparing,oldDebugPreparingProcess);
            SceneProcessDictionary.Add(SceneStep.Fighting,oldDebugFightingProcess);
        }

        switch (this._SceneMode)
        {
            case SceneMode.netFight:
                //if (PhotonNetwork.isMasterClient) 
                //{
                //  this._fightingSceneNetworkManager = PhotonNetwork.InstantiateSceneObject ("fightingSceneNetworkManager",new Vector3(0,0,0),new Quaternion(0,0,0,0),0,null).GetComponent<fightingSceneNetworkManager>();//resource文件夹下必须有这么个东西存在。
                //}
                //if (this._playerNetInfo == null)
                //{
                //  this._playerNetInfo = PhotonNetwork.Instantiate("playerNetInfo",new Vector3(),new Quaternion(),0).GetComponent<playerNetInfo>();
                //  this._playerNetInfo.playerID = PhotonNetwork.player.ID;
                //  _CharSetManager.setNetModeMyPlayerTag ("Player"+ this._playerNetInfo.playerID);
                //  this._playerNetInfo.playerTag = "Player" + this._playerNetInfo.playerID;
                //}
                break;

            case SceneMode.localDebug:
                changeProcess(SceneStep.Preparing);
                break;
            case SceneMode.MyPetsFight:
                changeProcess(SceneStep.Preparing);
                break;
            case SceneMode.QuestFight:
                changeProcess(SceneStep.Preparing);
                break;
        }
    }
    
    public bool ifLoadStageFinished()
    {
        return loadStageFinished;
    }
    public void resetLoadStageFinishedFlag()
    {
        loadStageFinished = false;
    }

    public IEnumerator loadGame(Stage stage,bool isAgainstOtherEnemy)
    {
        _CharSetManager.TeamMembers.Clear();//下面两个环节会在角色生成阶段就生成新的TeamMembers
        _BoundaryControllByGod.battleRingCenter = Vector3.zero;// 这个逻辑有一定问题，不一定对不对？
        Debug.Log("这里："+isAgainstOtherEnemy);
        yield return (LoadATeam(stage._LocalFight.team1members, myModelPool.Instance.ModelDicBasedOnPlayerLocalID, Team.player1));
        yield return (LoadATeam(stage._LocalFight.Enemies, isAgainstOtherEnemy ? myModelPool.Instance.ModelDicBasedOnEnemiesLocalID : myModelPool.Instance.ModelDicBasedOnPlayerLocalID, Team.player2));

        //但是上面两个环节的完成不能等同于处理的成功，应该是他们的全流程结束后做一个报错统计，没有错的话才正式开始游戏。
        //关于一场战斗下来每个角色应该是有多少HP，这个事情我们不走宏观调配路线
        //而是把角色的Hp也好，回蓝机制也好作为敌人和自方的属性。
        _CharSetManager.step3IniForALLPlayers();
        Debug.Log("这里1：");
        _CharSetManager.ArrangeAllCharacterToPosition(stage._LocalFight,
                                                      Team1StandPoints,
                                                      Team2StandPoints,
                                                       myModelPool.Instance.ModelDicBasedOnPlayerLocalID,
                                                      isAgainstOtherEnemy ?
                                                      myModelPool.Instance.ModelDicBasedOnEnemiesLocalID : myModelPool.Instance.ModelDicBasedOnPlayerLocalID,
                                                      stage._LocalFight._team1positionLocalCharKeySet, stage._LocalFight._team2positionLocalCharKeySet);
        Debug.Log("这里2：");
        Icons.instantiateCharsIconsAndFloatHPBar (_CharSetManager.getTeamMembers(Team.player1),Icons.Team1Container);
        Icons.instantiateCharsIconsAndFloatHPBar (_CharSetManager.getTeamMembers(Team.player2),Icons.Team2Container);
        loadStageFinished = true;
    }

    private int PoolObjectReparentStep = 0;
	// Update is called once per frame
	void Update () {
		switch (this._SceneMode)
		{
			case SceneMode.netFight:
				//if (PhotonNetwork.isMasterClient) {
				//	//Position_Set_Executor.Instance.Update();
				//	//有些东西只需要一个客户端去执行，有些东西只需要同步状态。这就是p2p模式下这些东西的逻辑。
				//} else {
				//	if (this._fightingSceneNetworkManager == null)
				//		this._fightingSceneNetworkManager = Transform.FindObjectOfType<fightingSceneNetworkManager> ();
				//}
				//if (this._playerNetInfo == null) {
				//	this._playerNetInfo = PhotonNetwork.Instantiate ("playerNetInfo", new Vector3 (), new Quaternion (), 0).GetComponent<playerNetInfo> ();
				//	this._playerNetInfo.playerID = PhotonNetwork.player.ID;
				//	_CharSetManager.setNetModeMyPlayerTag ("Player" + this._playerNetInfo.playerID);
				//}
				//if (_fightingSceneNetworkManager) {
				//	step = _fightingSceneNetworkManager.step;
				//}
				//NetFight_NAGARE ();

			break;
			case SceneMode.localDebug:
                ProcessNagare();

                if (PoolObjectReparentStep == 10)
                {
                    defaultPools.Instance.ReparentPooledObjects(false);
                    PoolObjectReparentStep = 0;
                }

			break;
            case SceneMode.MyPetsFight:
                ProcessNagare();
                if (PoolObjectReparentStep == 10)
                {
                    //我们不得不设计出这个机制的原因是Unity没法让物体在disable的同时重设parent。暂时没想出别的办法解决。
                    // you can't change the parent of an object in the same frame as you set it active or inactive due to the way the system works (all children also are affected).
                    defaultPools.Instance.ReparentPooledObjects(false);
                    PoolObjectReparentStep = 0;
                }
                break;
            case SceneMode.QuestFight:
                ProcessNagare();
                if (PoolObjectReparentStep == 10)
                {
                    //我们不得不设计出这个机制的原因是Unity没法让物体在disable的同时重设parent。暂时没想出别的办法解决。
                    // you can't change the parent of an object in the same frame as you set it active or inactive due to the way the system works (all children also are affected).
                    defaultPools.Instance.ReparentPooledObjects(false);
                    PoolObjectReparentStep = 0;
                }
                break;
        }
        PoolObjectReparentStep++;
    }

	void OnGUI()
	{
		if (GUI.Button(new Rect(20, 20, 25, 25), "R"))
		{
            returnToFront();
        }

        //if (step == SceneStep.Preparing && (this._SceneMode == SceneMode.MyPetsFight || this._SceneMode == SceneMode.QuestFight))
        //{
        //    GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + 50, 150, 30), "loading");
        //}
	}

    public void letAllCharactersStartOff()
    {
        foreach (KeyValuePair<Team, List<Data_Center>> _KeyValuePair in _CharSetManager.TeamMembers)
        {
            foreach (Data_Center _char in _KeyValuePair.Value)
            {
                Debug.Log("正让以下角色开跑：" + _KeyValuePair.Key + "的"+ _char.gameObject.name);
                _char.setIfShowMode(false);
                _char.getRunner().StartToGo();
            }
        }
    }

    public void returnToFront()
    {
        //Position_Set_Executor.Instance.P_sets.Clear();
        List<Data_Center> player1 = _CharSetManager.getTeamMembers(Team.player1);
        List<int> dontdestroy = new List<int>();
        for (int i = 0; i < player1.Count; i++)
        {
            if (player1[i] != null)
            {
                player1[i].setIfShowMode(true);
                player1[i].animator.SetBool("Grounded", true);
                player1[i].animator.SetFloat("speed", 0f);
                player1[i].getRunner().changeState("Empty");
                dontdestroy.Add(player1[i]._CharacterDataInfo.localID);
            }
        }
        _CharSetManager.preventTheseMyModelsFromDestroying(dontdestroy);
        _CharSetManager.TeamMembers.Clear();
        defaultPools.Instance.ReparentPooledObjects(true);
        Icons.Clear();
        SceneManager.LoadScene(1);
    }

	// 本地系函数
	public void pressedStartButton()
	{
        _BoundaryControllByGod.AllMembers = _CharSetManager.TeamMembers;
        foreach (KeyValuePair<Team,List<Data_Center>> _set in _CharSetManager.TeamMembers)
		{
            foreach (Data_Center _char in _set.Value)
			{
                _char.Sensor.TeamMembers = _CharSetManager.TeamMembers;
            }
		}

        Icons.Team2Container.gameObject.SetActive(false);

        letAllCharactersStartOff();
        Icons.SetFocusingChar(_CharSetManager.getTeamMembers(Team.player1)[0]);
	}

	//本地系函数 
	public void LocalGameRestart()
	{
        changeProcess(SceneStep.Preparing);
		SceneManager.LoadScene(GoingToLoadFight.Instance.nextBattle._LocalFight.BattleGroundID);
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