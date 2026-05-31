using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Cysharp.Threading.Tasks;
using ModelView;
using mainMenu;

public class ArcadeTop : UILayer
{
    [SerializeField] DedicatedCameraConnector connector;
    [SerializeField] float cameraConnectorRightSpace = 1200;
    [SerializeField] float cameraConnectorVerticalSpace = 150;
    
    [SerializeField] VerticalLayoutGroup container;
    [SerializeField] Button jumpToNewStage;
    [SerializeField] StageButton normalStagePrefab;
    [SerializeField] StageButton bossStagePrefab;
    [SerializeField] NineForShow nineForShow;
    [SerializeField] Button nextChapter;
    [SerializeField] Button lastChapter;

    private MainSceneStep step;
    List<int> _currentStages;
    readonly List<StageButton> _stageButtons = new List<StageButton>();
    int _showStagesVersion;
    
    LoadStageDelegate LoadStageMethod;
    Action<int, bool> directToStage;
    int _maxStageNum;
    int _stageCountPerPage = 3;

    int GetCurrentProgress()
    {
        if (step == MainSceneStep.ArcadeFront)
        {
            return ArcadeModeManager.ClampQuestProgress(PlayerAccountInfo.Me.arcadeProcess);
        }
        return PlayerAccountInfo.Me.gangbangProcess;
    }
    
    void SetupCommon()
    {
        nextChapter.onClick.RemoveListener(ShowNextStages);
        lastChapter.onClick.RemoveListener(ShowLastStages);
        jumpToNewStage.onClick.RemoveListener(ToNew);
        nextChapter.onClick.AddListener(ShowNextStages);
        lastChapter.onClick.AddListener(ShowLastStages);
        jumpToNewStage.onClick.AddListener(ToNew);
        
        ResizeCameraConnectorRefLeft(connector.GetComponent<RectTransform>(), cameraConnectorRightSpace, cameraConnectorVerticalSpace);
    }
    
    public void SetupArcade(int maxStageNum, LoadStageDelegate loadFightInfo, Action<int, bool> directToStage)
    {
        step = MainSceneStep.ArcadeFront;
        this.LoadStageMethod = loadFightInfo;
        this.directToStage = directToStage;
        this._maxStageNum = maxStageNum;
        SetupCommon();
    }
    
    bool IsActiveShowStages(int showStagesVersion)
    {
        return showStagesVersion == _showStagesVersion && this != null && container != null && !container.IsDestroyed();
    }
    
    void DestroyStageButton(StageButton stageButton)
    {
        if (stageButton == null)
            return;
        
        stageButton.gameObject.SetActive(false);
        Destroy(stageButton.gameObject);
    }
    
    void DestroyStageButtons(IEnumerable<StageButton> stageButtons)
    {
        foreach (var stageButton in stageButtons)
        {
            DestroyStageButton(stageButton);
        }
    }
    
    void ClearStageButtons()
    {
        DestroyStageButtons(_stageButtons);
        _stageButtons.Clear();
        
        if (container == null)
            return;
        
        foreach (Transform child in container.transform)
        {
            var stageButton = child.GetComponent<StageButton>();
            if (stageButton != null)
            {
                DestroyStageButton(stageButton);
            }
        }
    }
    
    public override void OnDestroy()
    {
        _showStagesVersion++;
        ClearStageButtons();
        base.OnDestroy();
    }
    
    async UniTask IconButtonFeature(UnitInfo unitInfo, int showStagesVersion)
    {
        if (!IsActiveShowStages(showStagesVersion) || unitInfo == null)
            return;
        
        UnitConfig unitConfig = Units.GetUnitConfig(unitInfo.r_id);
        
        ProgressLayer.Loading(string.Empty);
        BackGroundPS.target.ChangeBGByElement(unitConfig.element);
        
        await UniTask.WhenAll(
            connector.ShowModel(unitConfig.RECORD_ID), 
            nineForShow.SkillSetInfoOfUnitOnArcadePage(unitInfo.set)
        );
        
        if (!IsActiveShowStages(showStagesVersion))
        {
            ProgressLayer.Close();
            return;
        }
        
        nineForShow.AddOnClickToSlots(
            (RECORD_ID) =>
            {
                if (!IsActiveShowStages(showStagesVersion))
                    return;
                
                var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(RECORD_ID);
                connector.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
            }
        );
        ProgressLayer.Close();
    }

    void ToNew()
    {
        var stages = NewStages(GetCurrentProgress());
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
        if (container == null || container.IsDestroyed())
            return;
        
        var showStagesVersion = ++_showStagesVersion;
        ProgressLayer.Loading("Loading stages");
        container.transform.gameObject.SetActive(false);
        ClearStageButtons();
        _currentStages = stages;
        var progress = GetCurrentProgress();
        var tasks = new List<UniTask<StageButton>>();
        var currentStagesMax = _currentStages.Count > 0 ? _currentStages.Max() : 0;
        for (var index = 0; index < _currentStages.Count; index++)
        {
            tasks.Add(LoadStage(_currentStages[index], 
                _currentStages[index] == progress + 1,
                _currentStages[index] == currentStagesMax,
                showStagesVersion));
        }
        
        var loadedStageButtons = await UniTask.WhenAll(tasks);
        if (!IsActiveShowStages(showStagesVersion))
        {
            DestroyStageButtons(loadedStageButtons);
            return;
        }
        
        _stageButtons.AddRange(loadedStageButtons.Where(stageButton => stageButton != null));
        Refresh(progress,  
            step == MainSceneStep.ArcadeFront ? PlayFabReadClient.StageAwards : PlayFabReadClient.GangbangAwards);
        if (container != null)
            container.transform.gameObject.SetActive(true);
        if (IsActiveShowStages(showStagesVersion))
            ProgressLayer.Close();
    }
    
    async UniTask<StageButton> LoadStage(int stageNo, bool isNewStage, bool clickBoss, int showStagesVersion)
    {
        var one = await LoadStageMethod(stageNo);
        if (!IsActiveShowStages(showStagesVersion) || one == null)
        {
            return null;
        }
        
        var stageBtn = Instantiate(stageNo % _stageCountPerPage == 0 ? bossStagePrefab : normalStagePrefab);
        stageBtn.gameObject.SetActive(false);
        stageBtn.Button.onClick.AddListener(
            ()=>
            {
                if (IsActiveShowStages(showStagesVersion))
                    directToStage(stageNo, true);
            }
        );
        stageBtn.name = "Stage" + stageNo;
        stageBtn.StageNo = stageNo;
        stageBtn.CriticalGaugeMode = one.FightMode == FightMode.Evolve ? CriticalGaugeMode.Normal : one.team2CGMode;
        stageBtn.NewFlg.SetActive(isNewStage);
        if (one.FightMembers != null)
        {
            if (one.FightMode is FightMode.Group)
            {
                stageBtn.LoadUnitIconsGangbang(
                    one.FightMembers.EnemySets.GetValues(), 
                    (x)=> one.GetTeam2GroupSet(x).Count,
                    (unitInfo)=> IconButtonFeature(unitInfo, showStagesVersion), 
                    clickBoss,
                    ()=> IsActiveShowStages(showStagesVersion));
            }
            else
            {
                stageBtn.LoadUnitIcons(
                    one.FightMembers.EnemySets.GetValues(),
                    (unitInfo)=> IconButtonFeature(unitInfo, showStagesVersion),
                    clickBoss,
                    ()=> IsActiveShowStages(showStagesVersion));
            }
        }
        stageBtn.SetFightModeFlg(one.FightMode);
        var getUnitRId = UnitGetChart(stageNo);
        var unitCheck = Units.GetUnitConfig(getUnitRId);
        if (unitCheck != null)
        {
            stageBtn.ShowUnitGetInfo(getUnitRId);
        }
        
        if (!IsActiveShowStages(showStagesVersion))
        {
            DestroyStageButton(stageBtn);
            return null;
        }
        
        return stageBtn;
        
        string UnitGetChart(int stage)
        {
            string unitAward = null;
            switch (stage) {
                case 1:
                    unitAward = "1";
                    break;
                case 5:
                    unitAward = "2";
                    break;
                case 10:
                    unitAward = "4";
                    break;
                case 15:
                    unitAward = "7";
                    break;
                case 20:
                    unitAward = "6";
                    break;
                case 50:
                    unitAward = "5";
                    break;
                default:
                    break;
            }
            return unitAward;
        }
    }

    void Refresh(int progress, IDictionary<string, Award> stageAwards)
    {
        if (container.IsDestroyed())
            return;
        
        _stageButtons.Sort((a, b) => b.StageNo.CompareTo(a.StageNo));
        for (var i = 0; i < _stageButtons.Count; i++)
        {
            var stageBtn = _stageButtons[i];
            var btnAnimator = stageBtn.GetComponent<Animator>();
            if (btnAnimator != null)
                btnAnimator.enabled = progress + 1 == stageBtn.StageNo;
            
            var rewardDic = stageAwards;
            var reward = rewardDic[stageBtn.StageNo.ToString()];
            stageBtn.RewardUI.ShowRewards(reward.d,reward.g);
            stageBtn.RewardUI.AwardRender(progress + 1 > stageBtn.StageNo);
            stageBtn.ChangeColorOfIcons(progress + 1 >= stageBtn.StageNo);
            stageBtn.transform.SetParent(container.transform);
            stageBtn.transform.localPosition = Vector3.zero;
            stageBtn.transform.localRotation = Quaternion.identity;
            stageBtn.transform.localScale = Vector3.one;
            stageBtn.gameObject.SetActive(true);
        }
        
        int currentStagesMax = _currentStages.Count > 0 ? _currentStages.Max() : progress;
        nextChapter.gameObject.SetActive((progress + 1 > currentStagesMax) && (_maxStageNum > currentStagesMax));
        lastChapter.gameObject.SetActive(_currentStages.Count == 0 || _currentStages.Min() > _stageCountPerPage);

        var progressChapter = progress == _maxStageNum
            ? (progress - 1) / _stageCountPerPage
            : progress / _stageCountPerPage;
        var currentChapter = _currentStages.Count != 0 ? _currentStages.Min() / _stageCountPerPage : _maxStageNum / _stageCountPerPage;
        
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
            progress = _maxStageNum - 1;
        }
        else if (progress == _maxStageNum)
        {
            progress -= 1;
        }
        var currentChapter = progress / _stageCountPerPage;
        var returnValue = new List<int>();
        for (int stageNoPlus = 1; stageNoPlus <= _stageCountPerPage; stageNoPlus++)
        {
            int targetNo = stageNoPlus + currentChapter * _stageCountPerPage;
            if (targetNo <= _maxStageNum)
            {
                returnValue.Add(targetNo);
            }
        }
        return returnValue;
    }
}
