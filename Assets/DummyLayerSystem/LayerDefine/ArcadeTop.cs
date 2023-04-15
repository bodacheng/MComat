using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System.Linq;
using Cysharp.Threading.Tasks;
using ModelView;

public class ArcadeTop : UILayer
{
    [SerializeField] DedicatedCameraConnector connector;
    [SerializeField] VerticalLayoutGroup container;
    [SerializeField] Button jumpToNewStage;
    [SerializeField] StageButton normalStagePrefab;
    [SerializeField] StageButton bossStagePrefab;
    [SerializeField] NineForShow nineForShow;
    [SerializeField] Button nextChapter;
    [SerializeField] Button lastChapter;
    
    List<int> _currentStages;
    readonly List<StageButton> _stageButtons = new List<StageButton>();
    private StageModeTable _stageModeTable;
    private Func<int, UniTask<FightInfo>> _loadStageAsset;
    int _maxStageNum; 
    
    public void Setup(StageModeTable stageModeTable, Func<int, UniTask<FightInfo>> loadStageAsset, int maxStageNum)
    {
        this._stageModeTable = stageModeTable;
        this._loadStageAsset = loadStageAsset;
        nextChapter.onClick.AddListener(ShowNextStages);
        lastChapter.onClick.AddListener(ShowLastStages);
        jumpToNewStage.onClick.AddListener(ToNew);
        this._maxStageNum = maxStageNum;
        
        // Unit View Size Calulate
        var unitViewSize = (PosCal.canvasWidth - (1100 + 100));
        if (unitViewSize > PosCal.canvasHeight)
            unitViewSize = PosCal.canvasHeight;
        connector.GetComponent<RectTransform>().sizeDelta = new Vector2(unitViewSize,unitViewSize);
    }
    
    async UniTask IconButtonFeature(HeroIcon heroIcon)
    {
        ProgressLayer.Loading(string.Empty);
        BackGroundPS.target.ChangeBGByElement(heroIcon.unitConfig.element);
        // 显示模型
        await connector.ShowModel(heroIcon.unitConfig.RECORD_ID);
        // 显示技能组
        await nineForShow.SkillSetInfoOfUnitOnArcadePage(heroIcon.unitInfo);
        
        nineForShow.AddOnClickToSlots(
            (RECORD_ID) =>
            {
                var skillConfig = SkillConfigTable.GetSkillConfig(RECORD_ID);
                connector.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
            }
        );
        ProgressLayer.Close();
    }

    void ToNew()
    {
        var stages = NewStages(PlayerAccountInfo.Me.arcadeProcess);
        ShowStages(stages).Forget();
    }

    void ShowNextStages()
    {
        ShowStages(NewStages( _currentStages.Count > 0 ? _currentStages.Max() + 1:0)).Forget();
    }
    
    void ShowLastStages()
    {
        ShowStages(NewStages(_currentStages.Count > 0 ?_currentStages.Min() - 2:0)).Forget();
    }
    
    public async UniTask ShowStages(List<int> stages)
    {
        ProgressLayer.Loading("Loading stages");
        container.transform.gameObject.SetActive(false);
        foreach (var child in _stageButtons) {
            Destroy(child.gameObject);
        }
        _stageButtons.Clear();
        _currentStages = stages;
        var tasks = new List<UniTask>();
        for (var index = 0; index < _currentStages.Count; index++)
        {
            tasks.Add(LoadStage(_currentStages[index]));
        }
        await UniTask.WhenAll(tasks);
        Refresh();
        if (container != null)
            container.transform.gameObject.SetActive(true);
        ProgressLayer.Close();
    }
    
    async UniTask LoadStage(int stageNo)
    {
        var one = await _loadStageAsset(stageNo);
        if (one == null)
        {
            return;
        }
        
        var stageBtn = Instantiate(stageNo % 5 == 0 ? bossStagePrefab : normalStagePrefab);
        _stageButtons.Add(stageBtn);
        void LoadThisStage()
        {
            if (PlayerAccountInfo.Me.arcadeProcess + 1 >=  stageNo)
            {
                one.EventType = FightEventType.Quest;
                one.ArcadeFightMode = _stageModeTable.GetModeById(one.ID);
                PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, one, true);
            }
        }
        stageBtn.Button.onClick.AddListener(LoadThisStage);
        stageBtn.name = "Stage" + stageNo;
        stageBtn.StageNo = stageNo;
        stageBtn.CriticalGaugeMode = one.team2CGMode;
        if (one.FightMembers != null)
        {
            stageBtn.LoadUnitIcons(one.FightMembers.EnemySets.GetValues(), IconButtonFeature, stageNo == _currentStages.Max());
        }
    }

    void Refresh()
    {
        if (container.IsDestroyed())
            return;
        
        _stageButtons.Sort((a, b) => b.StageNo.CompareTo(a.StageNo));
        for (var i = 0; i < _stageButtons.Count; i++)
        {
            var stageBtn = _stageButtons[i];
            var btnAnimator = stageBtn.GetComponent<Animator>();
            if (btnAnimator != null)
                btnAnimator.enabled = PlayerAccountInfo.Me.arcadeProcess + 1 == stageBtn.StageNo;
            
            var rewardDic = PlayFabReadClient.StageAwards;
            var reward = rewardDic[stageBtn.StageNo.ToString()];
            stageBtn.ShowRewards(reward.d,reward.g);
            stageBtn.ChangeColorOfIcons(PlayerAccountInfo.Me.arcadeProcess + 1 >= stageBtn.StageNo);
            stageBtn.AwardRender(PlayerAccountInfo.Me.arcadeProcess + 1> stageBtn.StageNo);
            stageBtn.transform.SetParent(container.transform);
            stageBtn.transform.localPosition = Vector3.zero;
            stageBtn.transform.localRotation = Quaternion.identity;
            stageBtn.transform.localScale = Vector3.one;
        }
        
        int _currentStagesMax = _currentStages.Count > 0 ? _currentStages.Max() : PlayerAccountInfo.Me.arcadeProcess;
        nextChapter.gameObject.SetActive((PlayerAccountInfo.Me.arcadeProcess + 1 > _currentStagesMax) && (_maxStageNum > _currentStagesMax));
        lastChapter.gameObject.SetActive(_currentStages.Count == 0 || _currentStages.Min() > 5);

        var progressChapter = PlayerAccountInfo.Me.arcadeProcess == _maxStageNum
            ? (PlayerAccountInfo.Me.arcadeProcess - 1) / 5
            : PlayerAccountInfo.Me.arcadeProcess / 5;
        var currentChapter = _currentStages.Count != 0 ? _currentStages.Min() / 5 : _maxStageNum / 5;
        
        jumpToNewStage.gameObject.SetActive(progressChapter != currentChapter);
        
        container.CalculateLayoutInputHorizontal();
        container.CalculateLayoutInputVertical();
        container.SetLayoutHorizontal();
        container.SetLayoutVertical();
    }
    
    public List<int> NewStages(int progress)
    {
        if (progress > _maxStageNum)
        {
            progress = _maxStageNum;
        }
        else if (progress == _maxStageNum)
        {
            progress -= 1;
        }

        var currentChapter = progress / 5;
        var returnValue = new List<int>();
        for (int stageNoPlus = 1; stageNoPlus <= 5; stageNoPlus++)
        {
            int targetNo = stageNoPlus + currentChapter * 5;
            if (targetNo <= _maxStageNum)
            {
                returnValue.Add(targetNo);
            }
        }
        return returnValue;
    }
}