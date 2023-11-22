using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DummyLayerSystem;
using FightScene;
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
    [SerializeField] private RectTransform dmParent;
    [SerializeField] private Text currentDmCurrency;
    [SerializeField] private Text awardDmCurrency;
    [SerializeField] private RectTransform gdParent;
    [SerializeField] private Text currentGdCurrency;
    [SerializeField] private Text awardGdCurrency;
    [SerializeField] private float currencyTextChangeDuration = 3f;
    #endregion
    
    #region arena
    [SerializeField] private ArenaRankIcon arenaRankIcon;
    [SerializeField] private Text arenaPoint;
    #endregion
    
    #region arcade
    [SerializeField] private Text stageTitle;
    [SerializeField] private Text nextStageTitle;
    [SerializeField] private BOButton againFor1v1Btn;
    [SerializeField] private BOButton againForMultiBtn;
    [SerializeField] private BOButton againBtn;
    [SerializeField] private BOButton nextBtn;
    [SerializeField] private BOButton nextFor1v1Btn;
    [SerializeField] private BOButton nextForMultiBtn;
    [SerializeField] private RectTransform adBtnParent;
    public BOButton AgainBtn => againBtn;
    public BOButton NextBtn => nextBtn;
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
    
    void NextFight(TeamMode mode, FightInfo fight)
    {
        switch (mode)
        {
            case TeamMode.Keep:
                fight.team1Mode =(TeamMode)PlayerPrefs.GetInt("preferAdventureMode", PlayerPrefs.GetInt("preferAdventureMode", 2));
                break;
            case TeamMode.MultiRaid:
                fight.team1Mode = TeamMode.MultiRaid;
                break;
            case TeamMode.Rotation:
                fight.team1Mode = TeamMode.Rotation;
                break;
        }
        fight.team2Mode = fight.team1Mode;
        fight.Team1Auto = FightLoad.Fight.Team1Auto;
        fight.Team2Auto = true;
        if (fight.EventType != FightEventType.Gangbang) // 因为gangbang模式只用NextFight这个函数重开当前战斗，开启下一场战斗是有单独的函数。
            fight.LoadMyTeam();
        FightLoad.Fight = fight;
        FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
        UILayerLoader.Remove<ArenaFightOver>();
    }
    
    public async void LoadNextArcadeStage()
    {
        Int32.TryParse(FightLoad.Fight.ID, out var nowStageNo);
        var nextStageNo = nowStageNo + 1;
        var nextFight = await PlayerAccountInfo.Me.ArcadeModeManager.LoadStage(nextStageNo);
        if (nextFight != null && PlayerAccountInfo.Me.tutorialProgress == "Finished" && nowStageNo != 5)
        {
            nextStageTitle.text = "Stage " + nextStageNo;
            nextBtn.gameObject.SetActive(true);
            nextFor1v1Btn.gameObject.SetActive(nextFight.ArcadeFightMode is 0 or 2);
            nextForMultiBtn.gameObject.SetActive(nextFight.ArcadeFightMode is 0 or 1);
            nextBtn.SetListener(() =>
            {
                NextFight((TeamMode)nextFight.ArcadeFightMode, nextFight);
            });
            nextFor1v1Btn.SetListener(() =>
            {
                NextFight(TeamMode.Rotation, nextFight);
            });
            nextForMultiBtn.SetListener(() =>
            {
                NextFight(TeamMode.MultiRaid, nextFight);
            });
        }
    }
    
    public void Setup(Action onClickStoryMask = null)
    {
        if (onClickStoryMask != null)
        {
            storyMaskBtn.SetListener(()=>
            {
                storyBgImage.gameObject.SetActive(false);
                onClickStoryMask();
            });
        }

        switch (FightLoad.Fight.EventType)
        {
            case FightEventType.Arena:
                break;
            case FightEventType.Quest:
                againFor1v1Btn.gameObject.SetActive(FightLoad.Fight.ArcadeFightMode is 0 or 2);
                againForMultiBtn.gameObject.SetActive(FightLoad.Fight.ArcadeFightMode is 0 or 1);
                break;
            case FightEventType.Gangbang:
                againFor1v1Btn.gameObject.SetActive(false);
                againForMultiBtn.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        
        againBtn.SetListener(
            () =>
            {
                NextFight(FightLoad.Fight.team1Mode, FightLoad.Fight);
            }
        );
        againFor1v1Btn.SetListener(() =>
        {
            NextFight(TeamMode.Rotation, FightLoad.Fight);
        });
        againForMultiBtn.SetListener(() =>
        {
            NextFight(TeamMode.MultiRaid, FightLoad.Fight);
        });
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
        if (FightLoad.Fight.EventType == FightEventType.Quest)
        {
            stageTitle.text = "Stage " + FightLoad.Fight.ID;
        }
        
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
    }
    
    public void ShowAward(int awardDm, int awardGd, int extraAdReward, int finishedStage = -1)
    {
        if (awardDm > 0)
        {
            dmParent.gameObject.SetActive(true);
            Currencies.DiamondCount.Value += awardDm;
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

        if (PlayerAccountInfo.Me.tutorialProgress == "Finished"
            &&
            !PlayerAccountInfo.Me.noAdsState)
        {
            FightScene.FightScene.target.ShowAds(
                extraAdReward, 
                adBtnParent, 
                () =>
                {
                    awardDmCurrency.text = "+" + (extraAdReward + awardDm);
                    _tweenTextScaleManager.AddNew(awardDmCurrency.transform, Vector3.one * 1.2f, Vector3.one, rewardTextChangeHalfDuration);
                },
                finishedStage
            );
        }
    }
    
    public void ShowArenaPoint(int oldPoint, int currentPoint)
    {
        arenaPoint.gameObject.SetActive(true);
        arenaRankIcon.Set(oldPoint);
        arenaRankIcon.gameObject.SetActive(true);
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