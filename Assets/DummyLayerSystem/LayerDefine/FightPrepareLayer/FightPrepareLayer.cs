using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using dataAccess;
using mainMenu;
using ModelView;
using Singleton;

public partial class FightPrepareLayer : UILayer
{
    [SerializeField] HeroIcon fighterIcon;
    [SerializeField] RectTransform myTeamShowT;
    [SerializeField] RectTransform enemyTeamShowT;
    [SerializeField] float unitIconSize = 200;
    [SerializeField] BOButton editTeamButton; // 根据进入战斗模式决定是否显示
    [SerializeField] GameObject teamEditIndicator;
    [SerializeField] Text teamEditIndicatorText;
    [SerializeField] GameObject modeFlgR;
    [SerializeField] GameObject modeFlgM;
    [SerializeField] GameObject modeFlgE;
    [SerializeField] GameObject modeFlgG;
    [SerializeField] FightModeSwitch fightModeSwitch;
    [SerializeField] GameObject enemyDoubleExModeFlg;
    [SerializeField] GameObject enemyInfiniteExModeFlg;
    [SerializeField] FightBeginBtn beginFight;
    [SerializeField] Text team1Name;
    [SerializeField] Text team2Name;
    [SerializeField] Text team1OneWord;
    [SerializeField] Text team2OneWord;
    [SerializeField] Text arcadeStageNoText;
    [SerializeField] RewardUI rewardUI;
    [SerializeField] BOButton toArcadeFrontBtn;
    [SerializeField] Image view2D;
    [SerializeField] Animator layerAnimator;
    [SerializeField] Animator unitOutAnimator;
    [SerializeField] NineForShow nineForShow;
    [SerializeField] NineForShow nineForShowE;
    [SerializeField] DedicatedCameraConnector connector;
    [SerializeField] DedicatedCameraConnector connectorE;
    [Header("战场选择")]
    [SerializeField] BattleGroundSwitch battleGroundSwitch;

    public BattleGroundSwitch BattleGroundSwitch => battleGroundSwitch;

    readonly HashSet<string> _preparedHeroModelIds = new HashSet<string>(StringComparer.Ordinal);
    readonly HashSet<string> _preparedEnemyModelIds = new HashSet<string>(StringComparer.Ordinal);
    readonly HashSet<string> _warmedIconRecordIds = new HashSet<string>(StringComparer.Ordinal);

    public void SetLayerAnimatorTrigger(string code)
    {
        layerAnimator.SetTrigger(code);
    }
    
    public void SetFightMode(FightMode fightMode)
    {
        fightModeSwitch.Setup(fightMode);
    }
    
    public FightMode GetSettingFightMode()
    {
        return fightModeSwitch.FightMode;
    }
    
    public void SetFightBeginFeature(Action fightBegin)
    {
        beginFight.SetAction(fightBegin);
    }
    
    public void SetFightBeginEnableRender(bool canFight)
    {
        beginFight.Enable(canFight);
    }
    
    
    public void SetTeamEditFeature(Action teamEdit)
    {
        editTeamButton.onClick.RemoveAllListeners();
        editTeamButton.onClick.AddListener(()=> teamEdit());
    }

    public void SetFightModeFlg(FightMode fightMode)
    {
        switch (fightMode)
        {
            case FightMode.Evolve:
                modeFlgE.SetActive(true);
                break;
            case FightMode.Group:
                modeFlgG.SetActive(true);
                break;
            case FightMode.Multi:
                modeFlgM.SetActive(true);
                break;
            case FightMode.Rotate:
                modeFlgR.SetActive(true);
                break;
        }
    }

    public void SetArcadeFeature(Action toArcadeFront, string arcadeStageNo)
    {
        arcadeStageNoText.gameObject.SetActive(true);
        arcadeStageNoText.text = "Stage " + arcadeStageNo;
        toArcadeFrontBtn.gameObject.SetActive(PlayerAccountInfo.Me.tutorialProgress == "Finished");
        toArcadeFrontBtn.SetListener(toArcadeFront);
        
        fightModeSwitch.gameObject.SetActive(false);
        
        var rewardDic = PlayFabReadClient.StageAwards;
        var reward = rewardDic[arcadeStageNo];
        rewardUI.ShowRewards(reward.d,reward.g);
        int.TryParse(arcadeStageNo, out var arcadeStageNoInt);
        rewardUI.AwardRender(PlayerAccountInfo.Me.arcadeProcess + 1 > arcadeStageNoInt);
        rewardUI.gameObject.SetActive(true);
        nineForShow.AddOnClickToSlots(
            (RECORD_ID) =>
            {
                var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(RECORD_ID);
                connector.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
            }
        );
        nineForShowE.AddOnClickToSlots(
            (RECORD_ID) =>
            {
                var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(RECORD_ID);
                connectorE.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
            }
        );
    }
    
    public void SetEventFeature(string arcadeStageNo)
    {
        arcadeStageNoText.gameObject.SetActive(true);
        var hardTxt = string.Empty;
        if (arcadeStageNo.Contains("easy"))
        {
            hardTxt = "easy";
        }
        if (arcadeStageNo.Contains("normal"))
        {
            hardTxt = "normal";
        }
        if (arcadeStageNo.Contains("hard"))
        {
            hardTxt = "hard";
        }
        arcadeStageNoText.text = Translate.Get("EventFightTitle")+ " " + hardTxt;
        toArcadeFrontBtn.gameObject.SetActive(false);
        rewardUI.gameObject.SetActive(false);
        // rewardUI.ShowRewards(award.d,award.g);
        // rewardUI.AwardRender(cleared);
        nineForShow.AddOnClickToSlots(
            (RECORD_ID) =>
            {
                var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(RECORD_ID);
                connector.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
            }
        );
    }
    
    public async UniTask StageMembersInfoShow(FightInfo stage, CancellationToken token)
    {
        // ----------------- 准备数据缓存 -----------------
        var heroUnits = stage.FightMembers.HeroSets.GetValues();
        var enemyUnits = stage.FightMembers.EnemySets.GetValues();
        var heroLookup = BuildUnitLookup(heroUnits);
        var enemyLookup = BuildUnitLookup(enemyUnits);

        WarmupUnitIcons(heroUnits);
        WarmupUnitIcons(enemyUnits);

        // ----------------- 本地帮助函数 -----------------
        UniTask FocusHero(string id) =>
            FocusTeamUnit(id,
                          heroLookup,
                          connector,
                          nineForShow);

        UniTask FocusEnemy(string id) =>
            FocusTeamUnit(id,
                          enemyLookup,
                          connectorE,
                          nineForShowE);

        // 用于收集所有要等待的异步任务
        var focusTasks = new List<UniTask>();

        // ----------------- 我方成员 -----------------
        var icons1 = MemberInfosShow(
            heroUnits,
            id => FocusHero(id).Forget(),
            myTeamShowT,
            true,
            PlayerAccountInfo.Me.tutorialProgress == "Finished");

        var defaultHeroId = icons1.FirstOrDefault()?.InstanceID;
        if (!string.IsNullOrEmpty(defaultHeroId))
            focusTasks.Add(FocusHero(defaultHeroId));
        
        WarmupUnitModels(heroUnits, defaultHeroId, connector, _preparedHeroModelIds, token);

        // ----------------- 队伍空位提示 -----------------
        string teamKey = stage.EventType switch
        {
            FightEventType.Arena => "arena",
            FightEventType.Quest => FightLoad.Fight.FightMode switch
            {
                FightMode.Group   => "gangbang",
                FightMode.Evolve  => "arcade",
                _                 => "origin"
            },
            _ => "origin"
        };

        bool hasExtraSeat = !TeamSet.Legal(teamKey);
        teamEditIndicatorText.text = hasExtraSeat ? Translate.Get("HasExtraSeat") : string.Empty;
        teamEditIndicator.SetActive(hasExtraSeat);

        // ----------------- 敌方成员 -----------------
        var icons2 = MemberInfosShow(
            enemyUnits,
            id => FocusEnemy(id).Forget(),
            enemyTeamShowT,
            false);

        var defaultEnemyId = icons2.FirstOrDefault()?.InstanceID;
        if (!string.IsNullOrEmpty(defaultEnemyId))
            focusTasks.Add(FocusEnemy(defaultEnemyId));

        WarmupUnitModels(enemyUnits, defaultEnemyId, connectorE, _preparedEnemyModelIds, token);

        // ----------------- 并行等待所有 Focus 操作 -----------------
        await UniTask.WhenAll(focusTasks);

        if (token.IsCancellationRequested)
        {
            return;
        }
        
        // ----------------- UI 收尾 -----------------
        if (stage.EventType == FightEventType.Arena)
        {
            team1Name.text   = stage.Team1LeaderboardEntry?.DisplayName;
            team2Name.text   = stage.Team2LeaderboardEntry?.DisplayName;
            team1OneWord.text = stage.Team1OneWord;
            team2OneWord.text = stage.Team2OneWord;
        }
        else
        {
            team1Name.text = "YOU";
        }

        enemyDoubleExModeFlg.SetActive(stage.team2CGMode == CriticalGaugeMode.DoubleGain);
        enemyInfiniteExModeFlg.SetActive(stage.team2CGMode == CriticalGaugeMode.Unlimited);
    }
    
    async UniTask FocusTeamUnit(string instanceId, IReadOnlyDictionary<string, UnitInfo> teamUnits, DedicatedCameraConnector _connector, NineForShow _nineForShow)
    {
        if (string.IsNullOrEmpty(instanceId) || teamUnits == null || !teamUnits.TryGetValue(instanceId, out var info))
        {
            return;
        }

        ProgressLayer.Loading(String.Empty);
        try
        {
            await UniTask.WhenAll(
                _nineForShow.SkillSetInfoOfUnitOnArcadePage(info.set),
                //Set2DView(info.r_id, view2D, unitOutAnimator, 0, 0.6f, 0, DedicatedCameraConnector.Unit2DViewYoKoSpaceWhenAtRight(info.r_id)),
                _connector.ShowModel(info.r_id)
            );
        }
        finally
        {
            ProgressLayer.Close();
        }
    }
    
    List<HeroIcon> MemberInfosShow(List<UnitInfo> heroSets, Action<string> iconBehaviour, RectTransform _showT, bool withSkillCheck, bool btnInteractive = true)
    {
        foreach (Transform t in _showT)
        {
            Destroy(t.gameObject);
        }
        var icons = new List<HeroIcon>();
        foreach(var oneMember in heroSets)
        {
            var v = HeroIcon.ArrangeHeroIconToParent(fighterIcon, oneMember, iconBehaviour, _showT, unitIconSize, withSkillCheck, true);
            v.iconButton.interactable = btnInteractive;
            icons.Add(v);
        }
        return icons;
    }
    
    static Dictionary<string, UnitInfo> BuildUnitLookup(List<UnitInfo> units)
    {
        var lookup = new Dictionary<string, UnitInfo>(units?.Count ?? 0, StringComparer.Ordinal);
        if (units == null)
        {
            return lookup;
        }

        foreach (var unit in units)
        {
            if (!string.IsNullOrEmpty(unit?.id))
            {
                lookup[unit.id] = unit;
            }
        }

        return lookup;
    }

    void WarmupUnitIcons(IEnumerable<UnitInfo> units)
    {
        if (units == null)
        {
            return;
        }

        foreach (var unit in units)
        {
            var recordId = unit?.r_id;
            if (string.IsNullOrEmpty(recordId) || !_warmedIconRecordIds.Add(recordId))
            {
                continue;
            }

            UniTask.Void(async () =>
            {
                try
                {
                    await UnitIconDic.Load(recordId);
                }
                catch (OperationCanceledException)
                {
                    // ignore cancellation during warmup
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[FightPrepareLayer] Icon warmup failed for {recordId}: {ex.Message}");
                }
            });
        }
    }

    void WarmupUnitModels(List<UnitInfo> units, string skipInstanceId, DedicatedCameraConnector targetConnector, HashSet<string> preparedIds, CancellationToken token)
    {
        if (targetConnector == null || units == null || preparedIds == null)
        {
            return;
        }

        var preloadTasks = new List<UniTask>();
        foreach (var unit in units)
        {
            if (token.IsCancellationRequested)
            {
                break;
            }
            if (unit == null || unit.id == skipInstanceId)
            {
                continue;
            }

            var recordId = unit.r_id;
            if (string.IsNullOrEmpty(recordId) || !preparedIds.Add(recordId))
            {
                continue;
            }

            preloadTasks.Add(targetConnector.PrepareModel(recordId));
        }

        if (preloadTasks.Count == 0)
        {
            return;
        }

        UniTask.Void(async () =>
        {
            try
            {
                await UniTask.WhenAll(preloadTasks).AttachExternalCancellation(token);
            }
            catch (OperationCanceledException)
            {
                // cancellation is expected when layer is closed early
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[FightPrepareLayer] Model warmup failed: {ex.Message}");
            }
        });
    }
    
    public void TutorialForceFightBegin()
    {
        editTeamButton.gameObject.SetActive(false);
    }
}
