using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using UnityEngine;
using FightScene;
using UnityEngine.UI;
using UniRx;

public class FightingStepLayer : UILayer
{
    [Header("Pause Button")]
    [SerializeField] BOButton pauseButton;
    
    [Header("MobileInputsManager")]
    [SerializeField] MobileInputsManager inputsManager;
    
    [Header("TeamUIManager")]
    [SerializeField] TeamUIManager team1UI;
    [SerializeField] TeamUIManager team2UI;
    
    [Header("Tutorial")]
    [SerializeField] ClickNextTutorial clickNextTutorial;
    
    [Header("DreamCombo Tutorial")]
    [SerializeField] GameObject clickTriggerDreamCombo;

    [Header("教程强制点自动环节黑幕")] 
    [SerializeField] GameObject forceClickAutoBtnBlackMask;

    [Header("fps")] 
    [SerializeField] private Text fps;

    [SerializeField] private Animator animator;
    
    public TeamUIManager Team1UI => team1UI;
    public TeamUIManager Team2UI => team2UI;
    
    private int _wholeLiveUnitCountNum = -1;
    public void SetSensor()
    {
        int count = Team1UI.LiveUnitCountNum + Team2UI.LiveUnitCountNum;
        if (count != _wholeLiveUnitCountNum)
        {
            FightScene.FightScene.target.SensorUnity.SensorSetting(BoundaryControlByGod._BattleRingRadius, count);
        }
        _wholeLiveUnitCountNum = count;
    }
    
    public MobileInputsManager InputsManager => inputsManager;
    
    public static FightingStepLayer Open()
    {
        var fightingLayer = CommonSetting.PcMode ? 
            UILayerLoader.Load<FightingStepLayer>(true, "FightingStepLayer_st", true):
            UILayerLoader.Load<FightingStepLayer>();
        fightingLayer.InputsManager.FXCamera = FightScene.FightScene.target.fxCamera;
        return fightingLayer;
    }
    private readonly int _indicatorFlash = Animator.StringToHash("WinIndicatorFlash");
    public void WinIndicatorFlash()
    {
        if (animator != null)
            animator.SetTrigger(_indicatorFlash);
    }
    
    public void PreparingMode(bool preparingMode)
    {
        team1UI.Refresh(RTFightManager.Target.team1.RMode_Unit?.Value);
        team2UI.Refresh(RTFightManager.Target.team2.RMode_Unit?.Value);
        inputsManager.PreparingMode(preparingMode);
        pauseButton.gameObject.SetActive(!preparingMode);
    }
    
    public async UniTask Setup(bool active = true, Action<float> onProgress = null)
    {
        gameObject.SetActive(active && FightLoad.Fight.EventType != FightEventType.Screensaver);
        onProgress?.Invoke(0f);
        await StartUp(
            (x) =>
            {
                PlayerPrefs.SetInt("auto", x ? 1:0);
                RTFightManager.Target.team1.Auto = x;
            },
            (x) =>
            {
                RTFightManager.Target.team2.Auto = x;
            },
            ()=>
            {
                var pauseLayer = !CommonSetting.PcMode ? UILayerLoader.Load<FightScenePauseSupport>(true, null, true):
                    UILayerLoader.Load<FightScenePauseSupport>(true, "FightScenePauseSupport_st", true);
                
                pauseLayer.Setup(
                    ()=> { Time.timeScale = 0; },
                    ()=>FightScene.FightScene.target.ReturnToFront(),
                    () =>
                    {
                        Time.timeScale = 1;
                        UILayerLoader.Remove<FightScenePauseSupport>();
                        AppSetting.Save();
                        
                        if (InputsManager.KeyBindingUpdater != null)
                            InputsManager.KeyBindingUpdater.INI();
                        
                        WinIndicatorFlash();
                    }
                );
            },
            onProgress);
    }
    
    public static void Close()
    {
        var layer = UILayerLoader.Get<FightingStepLayer>();
        if (layer == null)
            return;
        layer.inputsManager.FocusUnit(null);
        layer.inputsManager.Clear();
        layer.team1UI.Clear();
        layer.team2UI.Clear();
        UILayerLoader.Remove<FightingStepLayer>();
    }

    public void OpenTutorial()
    {
        pauseButton.gameObject.SetActive(false);
        clickNextTutorial.Open();
    }
    
    public bool Initialized { get; set; } = false;

    private float deltaTime = 0.0f;
    private void ShowFps()
    {
        // 计算帧率
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        // 在 Text 上显示帧率
        fps.text = $"FPS: {Mathf.Ceil(1.0f / deltaTime)}";
    }

    async UniTask StartUp(Action<bool> switchTeam1Auto, Action<bool> switchTeam2Auto, Action pauseAction,
        Action<float> onProgress = null)
    {
        Initialized = false;
        onProgress?.Invoke(0.08f);
        
        RTFightManager.Target.team1.InputsManager = inputsManager;
        RTFightManager.Target.team2.InputsManager = inputsManager;
        
        pauseButton.SetListener(pauseAction.Invoke);
        
        team1UI.FightMode = FightLoad.Fight.FightMode;
        team2UI.FightMode = FightLoad.Fight.FightMode;
        team1UI.TeamConfig = RTFightManager.Target.heroTeamConfig;
        team2UI.TeamConfig = RTFightManager.Target.EnemyTeamConfig;
        team1UI.TeamConfig.playID = FightLoad.Fight.Team1ID;
        team2UI.TeamConfig.playID = FightLoad.Fight.Team2ID;
        team1UI.TeamMembers = RTFightManager.Target.team1.teamMembers;
        team2UI.TeamMembers = RTFightManager.Target.team2.teamMembers;
        onProgress?.Invoke(0.22f);
        
        // 角色第二次初始化在这之前已经结束
        team1UI.InsTeamUI(RTFightManager.Target.team1.ReadyForNextMember, (() => RTFightManager.Target.team1.Auto),switchTeam1Auto, RTFightManager.Target.team1.RMode_Unit);
        team2UI.InsTeamUI(RTFightManager.Target.team2.ReadyForNextMember, (() => RTFightManager.Target.team2.Auto),switchTeam2Auto, RTFightManager.Target.team2.RMode_Unit);
        onProgress?.Invoke(0.4f);

        team1UI.LiveUnitCount.gameObject.SetActive(FightLoad.Fight.IsGroupBattle);
        team2UI.LiveUnitCount.gameObject.SetActive(FightLoad.Fight.IsGroupBattle);

        if (FightLoad.Fight.IsGroupBattle)
        {
            fps.gameObject.SetActive(true);
            Observable.EveryUpdate().Subscribe((_) =>
            {
                ShowFps();
            }).AddTo(fps.gameObject);
        }
        
        var members = RTFightManager.Target.team1.teamMembers.GetValues();
        var inputEffectsLoading = new List<UniTask>();
        var totalMembers = members.Count;
        var completedMembers = 0;
        foreach (var d in members)
        {
            inputEffectsLoading.Add(LoadInputEffects(d));
        }

        async UniTask LoadInputEffects(Data_Center unit)
        {
            await inputsManager.ElementRegister(unit.element, RTFightManager.Target.UnitInfoRef[unit]);
            completedMembers++;
            var phaseProgress = totalMembers <= 0 ? 1f : (float)completedMembers / totalMembers;
            onProgress?.Invoke(Mathf.Lerp(0.4f, 0.92f, phaseProgress));
        }

        await UniTask.WhenAll(inputEffectsLoading);
        inputsManager.GroupSkillIcons();
        if (FightLoad.Fight.AllowsManualUnitControl && inputsManager.CurrentFocus.Value != null)
        {
            inputsManager.FocusUnit(inputsManager.CurrentFocus.Value, true);
        }
        onProgress?.Invoke(1f);
        
        // foreach (var d in RTFightManager.Target.team2.teamMembers.GetValues())
        // {
        //     await inputsManager.ElementRegister(d.element, RTFightManager.Target.UnitInfoRef[d]);
        // }
        Initialized = true;
    }

    public void ForceClickAutoBtn()
    {
        forceClickAutoBtnBlackMask.SetActive(true);
        clickNextTutorial.Button.onClick.AddListener(
            ()=>
            {
                team1UI.AutoSwitch.ChangeAutoState(true);
                forceClickAutoBtnBlackMask.SetActive(false);
            });
    }

    bool preTeam1AIState;
    
    public void TutorialModeForceOnClickDreamCombo()
    {
        clickTriggerDreamCombo.SetActive(false);
        inputsManager.DreamComboDown();
        Team1UI.AutoSwitch.ChangeAutoState(preTeam1AIState);
        Team2UI.AutoSwitch.ChangeAutoState(true);
    }
    
    public void ForceClickDreamComboBtn()
    {
        preTeam1AIState = Team1UI.AutoSwitch.CurrentState();
        clickTriggerDreamCombo.SetActive(true);
    }

    private void OnDisable()
    {
        var c = RTFightManager.Target._CameraManager.GetMode(C_Mode.CertainYAntiVibration);
        var mode = ((ChatGptFix)c);
        mode.CanSetH = false;
    }

    public override void OnDestroy()
    {
        var c = RTFightManager.Target._CameraManager.GetMode(C_Mode.CertainYAntiVibration);
        var mode = ((ChatGptFix)c);
        mode.CanSetH = false;
        _wholeLiveUnitCountNum = -1;
        base.OnDestroy();
    }
}
