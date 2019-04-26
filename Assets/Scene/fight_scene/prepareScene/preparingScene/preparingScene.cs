using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class preparingScene : MonoBehaviour {
    
    [Header("ReturnButtonManager")]
    public ReturnButtonManager _ReturnButtonManager;

    [Header("Team Member Positions For Show")]
	public Transform Member0StandPoint;
    public Transform Member1StandPoint;
    public Transform Member2StandPoint;
    public Transform Member3StandPoint;
	public Transform TeamEditWatchPoint;

    IDictionary<PosNum,Transform> myShowCharPositionDic = new Dictionary<PosNum,Transform>();
    public Transform getPosTransform(PosNum num)
    {
        return myShowCharPositionDic[num];
    }

    [Space(11)]
    [Header("Essentials")]
	public CameraManager _CameraManager;
	public CharsManager _CharSetManager;
	public Text accountIntelliCoin;
    
    [Header("fxcamera")]
    public Camera fxCamera;

    [Space(11)]
    [Header("SkillStonesBox")]
    public SkillStonesBox _SkillStonesBox;

    [Space(7)]
    [Header("Shader转换器")]
    public SwapAllModelShader _SwapAllModelShader;

    [Space(7)]
    [Header("RongLianGamen")]
    public RongLianGamen _RongLianGamen;

    [Space(7)]
    [Header("MemberDetail")]
    public MemberDetail _MemberDetail;
    
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
    [Header("自我战斗管理模块")]
    public SelfFightManager _SelfFightManager;

    [Space(7)]
    [Header("LoadingProcess")]
    public LoadingCanvas _LoadingCanvas;

    //preparingscene应该就是只有这些画布
    [Space(7)]
    [Header("Canvas")]
    public Canvas MainMenuCanvas;
    public Canvas SkillEditCanvas;

    //preparingscene应该就是只有这些画布
    [Space(7)]
    [Header("Seasons Ts")]
    public ProjectStagesManger _ProjectStagesManger;

    [Space(7)]
    [Header("若干子画面的总RectTransfrom")]
    public RectTransform QuestInfoT;
    public RectTransform MemberSelectT;
    public RectTransform MemberT_show;
    public RectTransform JiNengRongLian_selectT;
    public RectTransform RonglianConfirmGAMENT;
    public RectTransform FrontT;
    public RectTransform ChapterInfoT;
    public RectTransform FightModeChooseT;
    public RectTransform SeasonsT;
    public RectTransform AllSeasonsGamensT;
    public RectTransform SelfFightUIT;

    private IDictionary<PosNum, prepareSceneCharShowSet> onSetCharShows = new Dictionary<PosNum, prepareSceneCharShowSet>();
    public CharacterDataInfo getCurrentOnSetCharInfoByPosNum(PosNum num)
    {
        if (onSetCharShows[num] != null)
            return onSetCharShows[num]._CharacterDataInfo;
        else
            return null;
    }
    
    private int currentBattleEntryNum = 4;
    public void setBattleEntryNum(int _currentBattleEntryNum)
    {
        this.currentBattleEntryNum = _currentBattleEntryNum;
    }
    
    // 主进程
    private IEnumerator MenuProcess;
    private bool processEnded = false;
    private float processTime = 0;
    private void setProcessStartEnd(bool a)
    {
        processEnded = a;
    }
    public void triggerMainProcess(IEnumerator _process)
    {
        StartCoroutine(this.MainProcess(_process));
    }
    private IEnumerator giveProcessStartEndFlag(IEnumerator _process)
    {
        setProcessStartEnd(false);
        yield return _process;
        setProcessStartEnd(true);
    }
    private IEnumerator MainProcess(IEnumerator _process)//这个函数是供外界调用的。
    {
        if (MenuProcess != null)
        {
            while (!processEnded)
            {
                processTime += 0.01f;
                if (processTime > 5f)
                {
                    Debug.Log("进程超时.");
                    StopCoroutine(MenuProcess);
                    break;
                }
                yield return null;
            };
        }
        processTime = 0;
        MenuProcess = giveProcessStartEndFlag(_process);
        yield return MenuProcess;
    }
    
    // 表现类进程
    private IEnumerator runningPresentationProcess;
    private bool PresentationProcessEnded = false;
    private float PresentationProcessTime = 0;
    private void setPresentationProcessStartEnd(bool a)
    {
        PresentationProcessEnded = a;
    }
    public void triggerPresentationProcess(IEnumerator _process)
    {
        StartCoroutine(this.PresentationProcess(_process));
    }
    private IEnumerator givePresentationProcessStartEndFlag(IEnumerator _process)
    {
        setPresentationProcessStartEnd(false);
        yield return _process;
        setPresentationProcessStartEnd(true);
    }
    private IEnumerator PresentationProcess(IEnumerator _process)//这个函数是供外界调用的。
    {
        if (runningPresentationProcess != null)
        {
            while (!PresentationProcessEnded)
            {
                PresentationProcessTime += 0.01f;
                if (PresentationProcessTime > 5f)
                {
                    Debug.Log("进程超时.");
                    StopCoroutine(runningPresentationProcess);
                    break;
                }
                yield return null;
            };
        }
        PresentationProcessTime = 0;
        runningPresentationProcess = givePresentationProcessStartEndFlag(_process);
        yield return runningPresentationProcess;
    }
    
    private IEnumerator combineTwoIenumerator(IEnumerator a1, IEnumerator a2)
    {
        yield return a1;
        yield return a2;
        yield break;
    }
        
    void Start()
    {
        //_stagesManager.loadAndRefresh();
        Time.timeScale = 1;
        this.triggerMainProcess(StartUpProcess());
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
        Application.targetFrameRate = 60;
        myShowCharPositionDic = new Dictionary<PosNum, Transform>();
        myShowCharPositionDic.Add(new KeyValuePair<PosNum, Transform>(PosNum.back, Member0StandPoint));
        myShowCharPositionDic.Add(new KeyValuePair<PosNum, Transform>(PosNum.left, Member1StandPoint));
        myShowCharPositionDic.Add(new KeyValuePair<PosNum, Transform>(PosNum.front, Member2StandPoint));
        myShowCharPositionDic.Add(new KeyValuePair<PosNum, Transform>(PosNum.right, Member3StandPoint));
        _SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
    
        MainMenuCanvas.gameObject.SetActive(false);
        MainMenuCanvas.transform.SetSiblingIndex(0);
        SkillEditCanvas.gameObject.SetActive(false);
        SkillEditCanvas.transform.SetSiblingIndex(0);
        
        _LoadingCanvas.Loading_Canvas.gameObject.SetActive(true);
        _LoadingCanvas.turnOnProcessDescription(true);
        _LoadingCanvas.nowProcess("正在读取账户信息",0);

        //SceneProcessDictionary
        TeamEditMonsterDetail _TeamEditMonsterDetail = new TeamEditMonsterDetail(this);
        TeamEditFront teamEditFront = new TeamEditFront(this,FrontT);
        SkillStones skillStones = new SkillStones(this);
        SelfFightFront selfFightFront = new SelfFightFront(this,SelfFightUIT);
        SeasonsGamen seasonsGamen = new SeasonsGamen(this,_ProjectStagesManger,SeasonsT);
        ShowOneSeasonChapters _ShowOneSeasonChapters = new ShowOneSeasonChapters(this,_ProjectStagesManger,AllSeasonsGamensT);
        QuestInfo questInfo = new QuestInfo(this,QuestInfoT);
        MemberDetailProcess memberDetail = new MemberDetailProcess(this,MemberSelectT);
        MemberDetail_edit memberDetail_edit = new MemberDetail_edit(this);
        MemberDetail_skillshow memberDetail_Skillshow = new MemberDetail_skillshow(this,MemberT_show);
        frontPage frontPage = new frontPage(this,FrontT);
        FightModeChoose fightModeChoose = new FightModeChoose(this,FightModeChooseT);
        ChapterProcess chapterProcess = new ChapterProcess(this,ChapterInfoT);        
        SceneProcessDictionary = new Dictionary<MainSceneStep,MainSceneProcess>();
        SceneProcessDictionary.Add(MainSceneStep.TeamEditMonsterDetail,_TeamEditMonsterDetail);
        SceneProcessDictionary.Add(MainSceneStep.TeamEditFront,teamEditFront);
        SceneProcessDictionary.Add(MainSceneStep.SkillStones,skillStones);
        SceneProcessDictionary.Add(MainSceneStep.SelfFightFront,selfFightFront);
        SceneProcessDictionary.Add(MainSceneStep.ChaptersOfOneSeason,_ShowOneSeasonChapters);
        SceneProcessDictionary.Add(MainSceneStep.SeasonsGamen,seasonsGamen);
        SceneProcessDictionary.Add(MainSceneStep.QuestInfo,questInfo);
        SceneProcessDictionary.Add(MainSceneStep.MemberDetail,memberDetail);
        SceneProcessDictionary.Add(MainSceneStep.MemberDetail_edit,memberDetail_edit);
        SceneProcessDictionary.Add(MainSceneStep.MemberDetail_show,memberDetail_Skillshow);
        SceneProcessDictionary.Add(MainSceneStep.frontPage,frontPage);
        SceneProcessDictionary.Add(MainSceneStep.FightModeChoose,fightModeChoose);
        SceneProcessDictionary.Add(MainSceneStep.Chapter,chapterProcess);

        charIcon.iniFrames();
      
        _LoadingCanvas.nowProcess("正在启动技能石头背包", 0.6f);
        yield return (_SkillStonesBox.startUp());        
        _LoadingCanvas.nowProcess("正在加载技能编辑器", 0.7f);
        yield return (TheNineSlot.startUp());

        MainMenuCanvas.gameObject.SetActive(true);

        trySwitchToStep(MainSceneStep.frontPage, false);//就是一上来那个页面，选战斗模式的。
        _LoadingCanvas.turnOnProcessDescription(false);
        _LoadingCanvas.LightUp();
    }

    Vector3 tempV;
    public Vector3 caculateShowModelPosition(Vector3 screenP)//这个环节要说有什么问题的话，你那个主界面场景怎么确保总是能把射线找到地面呢。。。
    {
        tempV = CameraManager._camera.ViewportToWorldPoint(screenP);
        return tempV;
    }
    
    public void teamEditPress(int pressedMemberLocalID)
    {
        bool inTeamMemberChange = false;
        foreach (KeyValuePair<PosNum, prepareSceneCharShowSet> _Set in this.onSetCharShows)
        {
            if (_Set.Value._CharacterDataInfo != null)
            {
                if (_Set.Value._CharacterDataInfo.localID == pressedMemberLocalID) //如果当前选择的角色已经在队伍里存在...那其实有两种情况，一种是
                {
                    if (_Set.Value.positionNum != FloatOnHead.focusingPosNum)
                    {
                        inTeamMemberChange = true;
                        TeamSet.Instance._positionLocalCharKeySet4V4Mode.changePosition(FloatOnHead.focusingPosNum, _Set.Value.positionNum);
                    }
                    else
                    {
                        //那其实也就是点击了下原位置角色的头像
                    }
                }
            }
        }
        if (!inTeamMemberChange)
            TeamSet.Instance._positionLocalCharKeySet4V4Mode.changePositionLocalKey(FloatOnHead.focusingPosNum, pressedMemberLocalID);
    }

    private void arrangeShowModelOnTeam(int localID, PosNum PositionNum)//所以这是个可能把某个阵容位置里加入null的函数。
    {
        Transform t;
        myShowCharPositionDic.TryGetValue(PositionNum, out t);
        CharacterDataInfo oneChar = AccountCharsSet.getTheCharacterOfMine(localID);
        if (oneChar != null)
        {
            GameObject one = myModelPool.Instance.getMyModel(oneChar.localID);
            if (one)
            {
                one.SetActive(true);
                one.transform.SetParent(t);
                one.transform.localPosition = Vector3.zero;
                one.transform.localRotation = Quaternion.identity;
            }
        }

        if (onSetCharShows.ContainsKey(PositionNum))
            onSetCharShows[PositionNum] = new prepareSceneCharShowSet(PositionNum, oneChar);
        else
            onSetCharShows.Add(PositionNum, new prepareSceneCharShowSet(PositionNum, oneChar));
    }

    // 这个函数是读取现在账户情报的。如果之前的更改没保存那读取出来的信息是旧的
    // 那也就是说这里的refresh_from_database，false的话其实才是最新情报，true的话反而可能是旧情报
    public IEnumerator displayMy4V4Team(bool refresh_from_database, PosNum myFocusingTeamPosition)
    {
        if (refresh_from_database)
        {
            yield return (AccountCharsSet.Instance.loadMyOwnedCharsInfo());
            yield return (TeamSet.Instance.loadMyTeamSetInfoViaJsonFile("TeamSet.json"));
        }

        List<CharacterDataInfo> onsetLocals = new List<CharacterDataInfo>();

        positionLocalCharKeySet _positionLocalCharKeySet4V4Mode = TeamSet.Instance._positionLocalCharKeySet4V4Mode;
        TeamSet.Instance.refreshPositionLocalCharKeySet4V4Mode(AccountCharsSet.ownedChars);

        onSetCharShows = new Dictionary<PosNum, prepareSceneCharShowSet>();
        myModelPool.Instance.setAllMyCharactersModelActive(false);

        CharacterDataInfo _one;
        _one = AccountCharsSet.getTheCharacterOfMine(_positionLocalCharKeySet4V4Mode.getPositionLocalID(PosNum.back));
        if (_one != null)
            onsetLocals.Add(_one);
        _one = AccountCharsSet.getTheCharacterOfMine(_positionLocalCharKeySet4V4Mode.getPositionLocalID(PosNum.left));
        if (_one != null)
            onsetLocals.Add(_one);
        _one = AccountCharsSet.getTheCharacterOfMine(_positionLocalCharKeySet4V4Mode.getPositionLocalID(PosNum.front));
        if (_one != null)
            onsetLocals.Add(_one);
        _one = AccountCharsSet.getTheCharacterOfMine(_positionLocalCharKeySet4V4Mode.getPositionLocalID(PosNum.right));
        if (_one != null)
            onsetLocals.Add(_one);

        yield return (this._CharSetManager.buildTheseMyModels(onsetLocals.ToArray()));
        
        arrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.getPositionLocalID(PosNum.back), PosNum.back);
        arrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.getPositionLocalID(PosNum.left), PosNum.left);
        arrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.getPositionLocalID(PosNum.front), PosNum.front);
        arrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.getPositionLocalID(PosNum.right), PosNum.right);

        arrangeFloatInfoOfShowingChars(currentBattleEntryNum);
        if (myFocusingTeamPosition == PosNum.none)
            yield break;
        prepareSceneCharShowSet _prepareSceneCharShowSet;
        onSetCharShows.TryGetValue(myFocusingTeamPosition,out _prepareSceneCharShowSet);
        if (_prepareSceneCharShowSet != null && _prepareSceneCharShowSet._CharacterDataInfo != null)
            _SwapAllModelShader.arrangeAllModelShader(_prepareSceneCharShowSet._CharacterDataInfo.localID, myModelPool.Instance.ModelDicBasedOnPlayerLocalID);
    }

    int PoolObjectReparentStep = 0;
    void Update()
    {
		if (accountIntelliCoin)
		{
            if (AccountSet.Instance.localCustomerInfo != null)
                accountIntelliCoin.text = AccountSet.Instance.localCustomerInfo.IntelliCoin.ToString();
		}
        
        ProcessNagare();

        if (PoolObjectReparentStep == 10)
        {
            defaultPools.Instance.ReparentPooledObjects(false);
            PoolObjectReparentStep = 0;
        }
        PoolObjectReparentStep++;
    }

    public void gotcha()
    {
        CharacterDataInfo newChar = this._CharSetManager.gotcha();
    }

    //这个函数有这样的风险：如果你角色由这个函数正在调整位置的过程中step忽然间变了，那角色会停留在途中。而且风险可能不止这些。
    //说到底这个东西无非是为了确保四个角色在画面的上下左右四边，这不是必要的，只是我们所设计的一个外观小花样，而且这么正的排布这些角色其实只有在队伍编辑模式才有些意义。
    private Vector3 rotateTo;
    public void showModelPositionAdjusting()
    {
        Member0StandPoint.position = caculateShowModelPosition(new Vector3(0.5f, 0.7f, 10));//后
        rotateTo = _CameraManager.transform.position - Member0StandPoint.position;
        rotateTo.y = 0;
        Member0StandPoint.transform.rotation = Quaternion.LookRotation(rotateTo);
        foreach (Transform child in Member0StandPoint.transform)
        {
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
        }

        Member1StandPoint.position = caculateShowModelPosition(new Vector3(0.8f, 0.5f, 10));//左
        rotateTo = _CameraManager.transform.position - Member1StandPoint.position;
        rotateTo.y = 0;
        Member1StandPoint.transform.rotation = Quaternion.LookRotation(rotateTo);
        foreach (Transform child in Member1StandPoint.transform)
        {
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
        }

        Member2StandPoint.position = caculateShowModelPosition(new Vector3(0.5f, 0.4f, 10));//前
        rotateTo = _CameraManager.transform.position - Member2StandPoint.position;
        rotateTo.y = 0;
        Member2StandPoint.transform.rotation = Quaternion.LookRotation(rotateTo);
        foreach (Transform child in Member2StandPoint.transform)
        {
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
        }

        Member3StandPoint.position = caculateShowModelPosition(new Vector3(0.2f, 0.5f, 10));//右
        rotateTo = _CameraManager.transform.position - Member3StandPoint.position;
        rotateTo.y = 0;
        Member3StandPoint.transform.rotation = Quaternion.LookRotation(rotateTo);
        foreach (Transform child in Member3StandPoint.transform)
        {
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
        }
    }

    public void askIfLoadFight(SceneMode sceneMode,int sceneID)
    {
        _LoadingCanvas.arrangeValiationWindow(delegate{ LoadFight(sceneMode,sceneID);}, "开打？");
    }

    public void LoadFight(SceneMode sceneMode,int sceneID)//6.29 这个环节可能要进一步研究。进入战斗场景要做的事情安说很多，包括loadscene什么的，而这些都应该在这里进行。
    {
        List<int> enterRingLocalIDs = new List<int>();
        PosNumWithLocalKey _PosNumWithLocalKey;
        PosNum _PosNum = PosNum.none;
        for (int i = 0; i < GoingToLoadFight.Instance.nextBattle._LocalFight.EntryMemberNum; i++)
        {
            switch (i)
            {
                case 0:
                    _PosNum = PosNum.back;
                    break;
                case 1:
                    _PosNum = PosNum.left;
                    break;
                case 2:
                    _PosNum = PosNum.front;
                    break;
                case 3:
                    _PosNum = PosNum.right;
                    break;
            }

            _PosNumWithLocalKey = TeamSet.Instance._positionLocalCharKeySet4V4Mode.getPosMemInfo(_PosNum);
            if (_PosNumWithLocalKey != null)
            {
                enterRingLocalIDs.Add(_PosNumWithLocalKey.LocalID);
            }
            else
                Debug.Log("队伍位置槽和人员数方面产生问题");
        }

        _CharSetManager.preventTheseMyModelsFromDestroying(enterRingLocalIDs);
        FightSceneModeManager.Instance.setSceneMode(sceneMode);
        SceneManager.LoadScene(sceneID);
    }
    
    [EnumAction(typeof(MainSceneStep))]
    public void trySwitchToStep(int next_step)
    {
        this.trySwitchToStep((MainSceneStep)next_step, true);
    }
    
    [EnumAction(typeof(MainSceneStep))]
    public void trySwitchToStep(MainSceneStep next_step,bool foward)//这个是试图进入某个step。另一个是根据一些东西的选择情况来在某个step内对GUI进行刷新。两个都需要。
    {
        if (foward && currentProcess != null)
        {
            MainSceneStep returnToStep = currentProcess.step;
            UnityEngine.Events.UnityAction returnTOCurrent = () =>
            {
                trySwitchToStep(returnToStep, false);
            };
            _ReturnButtonManager.PUSH(returnTOCurrent);
        }

        changeProcess(next_step);
    }
    
    public MainSceneProcess accessCertainMainSceneProcessObject(MainSceneStep step)
    {
        return SceneProcessDictionary[step];
    }

    //看起来这个函数不应该在这个模块里，但其中的各种操作和整个mainmenu的乱七八糟东西相关性实在太多了，所以姑且放在这
    public IEnumerator monsterIconButton(int localId)
    {
        Debug.Log("于monsterbox点下了如下localid的头像：" + localId + ",Scenestep:" + currentProcess.step);
        switch (currentProcess.step)
        {
            case MainSceneStep.SelfFightFront:
                _SelfFightManager.monsterIConButton(localId);
                yield break;
            case MainSceneStep.MemberDetail:
                _MemberDetail.SetMemberDetailSystemFocusingCharacter(localId);//确立focusing角色
                yield return _MemberDetail.refreshMemberDetailGamenSystemBaseOnFocusingChar();
                yield break;
            case MainSceneStep.TeamEditMonsterDetail:
                _MemberDetail.SetMemberDetailSystemFocusingCharacter(localId);//确立focusing角色
                TeamEditMonsterDetail process = (TeamEditMonsterDetail)accessCertainMainSceneProcessObject(MainSceneStep.TeamEditMonsterDetail);
                yield return process.TeamEditMonsterDetailMonsterIconBehaviour();
            yield break;
            case MainSceneStep.JiNengRongLian_selectMaterialMonster:
                _RongLianGamen.RonglianVersionMonsterIcon(AccountCharsSet.getTheCharacterOfMine(localId));
                _RongLianGamen.GUIRefresh();
            yield break;
            case MainSceneStep.JiNengRongLian_selectBaseMonster:
                _RongLianGamen.RonglianVersionMonsterIcon(AccountCharsSet.getTheCharacterOfMine(localId));
                _RongLianGamen.GUIRefresh();
            yield break;
        }
        _MonsterBox.adjustAllIconsSize(localId);
        yield break;
    }
              	                     		
    // 为所有展示中的角色添加浮动信息，包括英雄。但不负责这个浮动信息的显示与隐藏。
	public void arrangeFloatInfoOfShowingChars(int entryNum)
	{
		foreach (KeyValuePair<PosNum, prepareSceneCharShowSet> _Set in onSetCharShows)
		{
            Transform T;
            myShowCharPositionDic.TryGetValue(_Set.Value.positionNum, out T);
            if (T.GetComponent<FloatOnHead>() == null)
            {
                T.gameObject.AddComponent<FloatOnHead>();
            }

            CharacterDataInfo _CharacterDataInfo = _Set.Value._CharacterDataInfo;//这些操作应该是由那几个角色自身来完成，面向对象
            if (_CharacterDataInfo != null)
            {
                _CharacterDataInfo = AccountCharsSet.getTheCharacterOfMine(_CharacterDataInfo.localID);//上面那个环节判定不是null外 ,还要进一步看我到底有没有这个角色
            }

            List<PosNum> shouldbeinfight = new List<PosNum>();
            for (int i = 0; i < entryNum; i++)// 这里就是体现出我们这个简化了的队伍编辑设计。我们不搞什么好几个人，就是那一个队伍四人团队。
            {
                switch(i)
                {
                    case 0:
                        shouldbeinfight.Add(PosNum.back);
                        break;
                    case 1:
                        shouldbeinfight.Add(PosNum.left);
                        break;
                    case 2:
                        shouldbeinfight.Add(PosNum.front);
                        break;
                    case 3:
                        shouldbeinfight.Add(PosNum.right);
                        break;
                }
            }

            if (shouldbeinfight.Contains(_Set.Value.positionNum))
                T.GetComponent<FloatOnHead>().applyParametersForTeamMemberEditMode(true,this, _CharacterDataInfo, _Set.Value.positionNum, CustomGUISkin,_CameraManager);
            else
                T.GetComponent<FloatOnHead>().applyParametersForTeamMemberEditMode(false, this, _CharacterDataInfo, _Set.Value.positionNum, CustomGUISkin,_CameraManager);
        }
	}
}
