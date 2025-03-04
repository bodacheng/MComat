using System;
using dataAccess;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DummyLayerSystem;
using FightScene;
using mainMenu;
using UniRx;

public partial class ArenaFightOver : UILayer
{
    #region common
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject winObject;
    [SerializeField] private GameObject loseObject;
    [SerializeField] private Image winImage;
    [SerializeField] private Image loseImage;
    [SerializeField] private RectTransform powerUpTip;
    [SerializeField] private Button returnBtn;
    [SerializeField] private RectTransform awardParent;
    [SerializeField] private RectTransform dmParent;
    [SerializeField] private Text currentDmCurrency;
    [SerializeField] private RectTransform vipSymbol;
    [SerializeField] private Text awardDmCurrency;
    [SerializeField] private RectTransform gdParent;
    [SerializeField] private Text currentGdCurrency;
    [SerializeField] private Text awardGdCurrency;
    [SerializeField] private float currencyTextChangeDuration = 3f;
    #endregion
    
    #region arena
    [SerializeField] private RectTransform arenaRankParent;
    [SerializeField] private ArenaRankIcon arenaRankIcon;
    [SerializeField] private Text arenaPoint;
    #endregion
    
    #region arcade
    [SerializeField] private NextOrAgainBtn againTab;
    [SerializeField] private NextOrAgainBtn nextTab;
    [SerializeField] private BOButton gotchaBtn;
    [SerializeField] private RectTransform adBtnParent;
    public NextOrAgainBtn AgainBtn => againTab;
    public NextOrAgainBtn NextBtn => nextTab;
    public RectTransform AdBtnParent => adBtnParent;
    #endregion
    
    private int arenaPointValue;
    private TweenerCore<int, int, NoOptions> _arenaPointTweenerCore;
    private TweenerCore<int, int, NoOptions> _dmAwardTweenerCore;
    private TweenerCore<int, int, NoOptions> _gdAwardTweenerCore;
    private readonly TweenTextScaleManager _tweenTextScaleManager = new TweenTextScaleManager();
    private float rewardTextChangeHalfDuration = 0.05f;
    
    private float resultAnimFactor = 0;
    
    private string diamond;
    private string DiamondText
    {
        set
        {
            if (diamond != value)
            {
                _tweenTextScaleManager.AddNew(currentDmCurrency.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
            }
            diamond = value;
            currentDmCurrency.text = diamond;
        }
        get => diamond;
    }
    
    private string gold;
    private string GoldText
    {
        set
        {
            if (gold != value)
            {
                _tweenTextScaleManager.AddNew(currentGdCurrency.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
            }
            gold = value;
            currentGdCurrency.text = gold;
        }
        get => gold;
    }
    
    private string arena;
    private string ArenaText
    {
        set
        {
            if (arena != value)
            {
                _tweenTextScaleManager.AddNew(arenaPoint.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
            }
            arena = value;
            arenaPoint.text = arena;
        }
        get => arena;
    }
    
    void Awake()
    {
        powerUpTip.gameObject.SetActive(false);
        arenaRankParent.gameObject.SetActive(false);
        dmParent.gameObject.SetActive(false);
        gdParent.gameObject.SetActive(false);
        awardParent.gameObject.SetActive(false);
    }
    
    void NextFight(FightInfo fight)
    {
        fight.Team1Auto = FightLoad.Fight.Team1Auto;
        fight.Team2Auto = true;
        fight.LoadMyTeam();
        FightLoad.Fight = fight;
        FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
        UILayerLoader.Remove<ArenaFightOver>();
    }
    
    public async void LoadNextArcadeStage()
    {
        Int32.TryParse(FightLoad.Fight.ID, out var nowStageNo);
        var nextStageNo = nowStageNo + 1;
        var nextFight = await ArcadeModeManager.Instance.LoadStage(nextStageNo);
        if (nextFight == null) return;
        
        string teamKey;
        if (nextFight.FightMode == FightMode.Group)
        {
            teamKey = "gangbang";
        }
        else
        {
            switch (FightLoad.Fight.FightMode)
            {
                case FightMode.Evolve:
                    teamKey = "arcade";
                    break;
                default:
                    teamKey = "origin";
                    break;
            }
        }
        
        nextFight.FightMembers.HeroSets = TeamSet.GetTargetSet(teamKey).LoadTeamDic();
        if (nextFight != null && QuestInfoPage.CanFightCheck(nextFight) && PlayerAccountInfo.Me.tutorialProgress == "Finished" && nowStageNo != 5)
        {
            nextTab.SetUp("Stage " + nextStageNo);
            nextTab.gameObject.SetActive(true);
            nextTab.SetUpAction(
                () =>
                {
                    NextFight(nextFight);
                },
                () =>
                {
                    NextFight(nextFight);
                },
                () =>
                {
                    NextFight(nextFight);
                }
            );
        }
    }
    
    public void Setup()
    {
        switch (FightLoad.Fight.EventType)
        {
            case FightEventType.Arena:
                break;
            case FightEventType.Quest:
                if (FightLoad.Fight.FightMode == FightMode.Group)
                {
                    againTab.SetUp("Stage " + FightLoad.Fight.ID);
                }
                else
                {
                    againTab.SetUp("Stage " + FightLoad.Fight.ID);
                }
                break;
            default:
                againTab.SetUp(null);
                break;
        }
        
        againTab.SetUpAction(
            () =>
            {
                NextFight(FightLoad.Fight);
            },
            ()=> NextFight(FightLoad.Fight),
            () =>
            {
                NextFight(FightLoad.Fight);
            }
        );
        
        returnBtn.onClick.AddListener(()=>
        {
            OnDestroy();
            FightScene.FightScene.target.ReturnToFront();
        });
        
        DiamondText = Currencies.DiamondCount.Value.ToString();
        Currencies.DiamondCount.Subscribe(
            x =>
            {
                int.TryParse(DiamondText, out int currentValue);
                int targetValue = currentValue;
                _dmAwardTweenerCore = DOTween.To(
                    () => targetValue,
                    setterValue => targetValue = setterValue,
                    x,
                    currencyTextChangeDuration
                ).OnUpdate(() =>
                {
                    DiamondText = targetValue.ToString();
                });
            }
        ).AddTo(this.gameObject);
        
        GoldText = Currencies.CoinCount.Value.ToString();
        Currencies.CoinCount.Subscribe(
            x =>
            {
                int.TryParse(GoldText, out int currentValue);
                int targetValue = currentValue;
                _gdAwardTweenerCore = DOTween.To(
                    () => targetValue,
                    setterValue => targetValue = setterValue,
                    x,
                    currencyTextChangeDuration
                ).OnUpdate(() =>
                {
                    GoldText = targetValue.ToString();
                });
            }
        ).AddTo(this.gameObject);
    }
    
    public void Step1Anim()
    {
        if (FightLogger.value.GetWinnerTeam() == Team.player1)
        {
            winObject.SetActive(true);
            DOTween.To(() => resultAnimFactor, (x) => resultAnimFactor = x, 2, 1).
                OnUpdate(
                    () =>
                    {
                        winImage.material.SetFloat("_Animation_Factor", resultAnimFactor);
                    }
            );
        }
        else
        {
            loseObject.SetActive(true);
            powerUpTip.gameObject.SetActive(true);
            DOTween.To(() => resultAnimFactor, (x) => resultAnimFactor = x, 2, 1).
                OnUpdate(
                    () =>
                    {
                        loseImage.material.SetFloat("_Animation_Factor", resultAnimFactor);
                    }
                );
        }
    }
    
    public void Step2Anim()
    {
        animator.SetTrigger("step2");
        GuideGocha();
    }

    void GuideGocha()
    {
        if (Currencies.DiamondCount.Value >= 90 && PlayerAccountInfo.Me.tutorialProgress == "Finished")
        {
            gotchaBtn.SetListener(() =>
            {
                ReturnLayer.ReturnMissionList.Clear();
                FightScene.FightScene.target.ReturnToFront(MainSceneStep.GotchaFront);
            });
            gotchaBtn.gameObject.SetActive(true);
        }
    }
    
    public void ShowAward(int awardDm, int awardGd)
    {
        awardParent.gameObject.SetActive(awardDm > 0 || awardGd > 0);
        if (awardDm > 0)
        {
            dmParent.gameObject.SetActive(true);
            Currencies.DiamondCount.Value += awardDm;
            GuideGocha();
            awardDmCurrency.text = "+" + awardDm;
            _tweenTextScaleManager.AddNew(awardDmCurrency.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
        }
        if (awardGd > 0)
        {
            gdParent.gameObject.SetActive(true);
            Currencies.CoinCount.Value += awardGd;
            awardGdCurrency.text = "+" + awardGd;
            _tweenTextScaleManager.AddNew(awardGdCurrency.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
        }
        vipSymbol.gameObject.SetActive(PlayerAccountInfo.Me.noAdsState);
    }
    
    public void ShowAward(int awardDm, int awardGd, int extraAdReward, int finishedStage = -1, bool oldStage = false)
    {
        awardParent.gameObject.SetActive(awardDm > 0 || awardGd > 0 || extraAdReward > 0);
        if (awardDm > 0 || extraAdReward > 0)
        {
            dmParent.gameObject.SetActive(true);
            Currencies.DiamondCount.Value += awardDm;
            GuideGocha();
            awardDmCurrency.text = "+" + awardDm;
            _tweenTextScaleManager.AddNew(awardDmCurrency.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
        }
        if (awardGd > 0)
        {
            gdParent.gameObject.SetActive(true);
            Currencies.CoinCount.Value += awardGd;
            awardGdCurrency.text = "+" + awardGd;
            _tweenTextScaleManager.AddNew(awardGdCurrency.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
        }
        
        if (PlayerAccountInfo.Me.tutorialProgress == "Finished" && 
            (!PlayerAccountInfo.Me.noAdsState || oldStage))
        {
#if UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
            FightScene.FightScene.target.ShowAds(
                extraAdReward, 
                adBtnParent, 
                () =>
                {
                    awardDmCurrency.text = "+" + (extraAdReward + awardDm);
                    _tweenTextScaleManager.AddNew(awardDmCurrency.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
                },
                finishedStage,
                finishedStage >= 3 || oldStage
            );
            if (oldStage)
            {
                adBtnParent.gameObject.SetActive((false));
            }
#endif
        }
        
        vipSymbol.gameObject.SetActive(PlayerAccountInfo.Me.noAdsState && !oldStage);
    }
    
    public void ShowArenaPoint(int oldPoint, int currentPoint)
    {
        awardParent.gameObject.SetActive(currentPoint > oldPoint);
        arenaRankParent.gameObject.SetActive(true);
        arenaRankIcon.Set(oldPoint);
        arenaPointValue = oldPoint;
        _arenaPointTweenerCore = DOTween.To(
            () => arenaPointValue,          // 何を対象にするのか
            num => arenaPointValue = num,   // 値の更新
            currentPoint,                  // 最終的な値
            2f                  // アニメーション時間
        ).OnUpdate(
            ()=>
            {
                if (PlayFabSetting.ArenaPointToRank(arenaPointValue) > PlayFabSetting.ArenaPointToRank(oldPoint))
                {
                    oldPoint = arenaPointValue;
                    arenaRankIcon.Set(arenaPointValue);
                    arenaRankIcon.RankUpAnim();
                }
                ArenaText = arenaPointValue.ToString();
            }
        );
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        _arenaPointTweenerCore?.Kill();
        _dmAwardTweenerCore?.Kill();
        _gdAwardTweenerCore?.Kill();
        storyBgColorChangeTween?.Kill();
        _tweenTextScaleManager.Clear();
    }
}