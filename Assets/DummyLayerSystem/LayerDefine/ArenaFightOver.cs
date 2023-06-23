using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DummyLayerSystem;
using FightScene;
using UniRx;

public class ArenaFightOver : UILayer
{
    #region common
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject winObject;
    [SerializeField] private GameObject loseObject;
    [SerializeField] private Image winImage;
    [SerializeField] private Image loseImage;
    [SerializeField] private Button returnBtn;
    [SerializeField] private RectTransform dmParent;
    [SerializeField] private Text currentDmCurrency;
    [SerializeField] private Text awardDmCurrency;
    [SerializeField] private RectTransform gdParent;
    [SerializeField] private Text currentGdCurrency;
    [SerializeField] private Text awardGdCurrency;
    [SerializeField] private float currencyTextChangeDuration = 2f;
    #endregion
    
    #region arena
    [SerializeField] private ArenaRankIcon arenaRankIcon;
    [SerializeField] private Text arenaPoint;
    #endregion
    
    #region arcade
    [SerializeField] private Text stageTitle;
    [SerializeField] private Text nextStageTitle;
    [SerializeField] private Button againBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private RectTransform adBtnParent;
    public Button AgainBtn => againBtn;
    #endregion
    
    private int arenaPointValue;
    private TweenerCore<int, int, NoOptions> _arenaPointTweenerCore;
    private TweenerCore<int, int, NoOptions> _dmAwardTweenerCore;
    private TweenerCore<int, int, NoOptions> _gdAwardTweenerCore;
    
    private float resultAnimFactor = 0;
    
    public async void LoadNextArcadeStage()
    {
        Int32.TryParse(FightScene.FightScene.Fight.ID, out var nowStageNo);
        var nextStageNo = nowStageNo + 1;
        var nextFight = await PlayerAccountInfo.Me.ArcadeModeManager.LoadStage(nextStageNo);
        if (nextFight != null && PlayerAccountInfo.Me.tutorialProgress == "Finished")
        {
            nextStageTitle.text = "Stage " + nextStageNo;
            nextBtn.gameObject.SetActive(true);
            nextBtn.onClick.AddListener(() =>
            {
                switch (nextFight.ArcadeFightMode)
                {
                    case 0:
                        nextFight.team1Mode =(TeamMode)PlayerPrefs.GetInt("preferAdventureMode", PlayerPrefs.GetInt("preferAdventureMode", 2));
                        break;
                    case 1:
                        nextFight.team1Mode = TeamMode.MultiRaid;
                        break;
                    case 2:
                        nextFight.team1Mode = TeamMode.Rotation;
                        break;
                }
                nextFight.team2Mode = nextFight.team1Mode;
                nextFight.EventType = FightEventType.Quest;
                nextFight.Team1Auto = FightScene.FightScene.Fight.Team1Auto;
                nextFight.Team2Auto = true;
                nextFight.LoadMyTeam();
                RTFightManager.Target.team2.Clear();
                FightScene.FightScene.Fight = nextFight;
                FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
                UILayerLoader.Remove<ArenaFightOver>();
            });
        }
    } 
    
    void Awake()
    {
        againBtn.onClick.AddListener(() =>
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
            UILayerLoader.Remove<ArenaFightOver>();
        });
        returnBtn.onClick.AddListener(FightScene.FightScene.target.ReturnToFront);
        currentDmCurrency.text = Currencies.DiamondCount.Value.ToString();
        Currencies.DiamondCount.Subscribe(
            x =>
            {
                int.TryParse(currentDmCurrency.text, out int currentValue);
                int targetValue = currentValue;
                _dmAwardTweenerCore = DOTween.To(
                    () => targetValue,
                    setterValue => targetValue = setterValue,
                    x,
                    currencyTextChangeDuration
                ).OnUpdate(() =>
                {
                    currentDmCurrency.text = targetValue.ToString();
                });
            }
        ).AddTo(this.gameObject);
        
        currentGdCurrency.text = Currencies.CoinCount.Value.ToString();
        Currencies.CoinCount.Subscribe(
            x =>
            {
                int.TryParse(currentGdCurrency.text, out int currentValue);
                int targetValue = currentValue;
                _gdAwardTweenerCore = DOTween.To(
                    () => targetValue,
                    setterValue => targetValue = setterValue,
                    x,
                    currencyTextChangeDuration
                ).OnUpdate(() =>
                {
                    currentGdCurrency.text = targetValue.ToString();
                });
            }
        ).AddTo(this.gameObject);
    }
    
    public void Step1Anim()
    {
        if (FightScene.FightScene.Fight.EventType == FightEventType.Quest)
        {
            stageTitle.text = "Stage " + FightScene.FightScene.Fight.ID;
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
    
    public void ShowAward(int awardDm, int awardGd, int extraAdReward)
    {
        if (awardDm > 0)
        {
            dmParent.gameObject.SetActive(true);
            Currencies.DiamondCount.Value += awardDm;
            awardDmCurrency.text = awardDm.ToString();
        }
        if (awardGd > 0)
        {
            gdParent.gameObject.SetActive(true);
            Currencies.CoinCount.Value += awardGd;
            awardGdCurrency.text = awardGd.ToString();
        }

        if (PlayerAccountInfo.Me.tutorialProgress == "Finished")
        {
            FightScene.FightScene.target.ShowAds(
                extraAdReward, adBtnParent, 
                () =>
                {
                    awardDmCurrency.text = (extraAdReward + awardDm).ToString();
                }
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
                arenaPoint.text = arenaPointValue.ToString();
            }
        );
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        _arenaPointTweenerCore?.Kill();
        _dmAwardTweenerCore?.Kill();
        _gdAwardTweenerCore?.Kill();
    }
}