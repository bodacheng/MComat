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
    [SerializeField] private RewardedAdsButton watchAdBtn;
    [SerializeField] private Text watchAdRewardText;
    [SerializeField] private Button againBtn;
    public Button AgainBtn => againBtn;
    #endregion
    
    private int arenaPointValue;
    private TweenerCore<int, int, NoOptions> _arenaPointTweenerCore;
    private TweenerCore<int, int, NoOptions> _dmAwardTweenerCore;
    private TweenerCore<int, int, NoOptions> _gdAwardTweenerCore;
    
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
                DOTween.To(
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
                DOTween.To(
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
        if (FightLogger.value.GetWinnerTeam() == Team.player1)
        {
            winObject.SetActive(true);
        }
        else
        {
            loseObject.SetActive(true);
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
        
        if (extraAdReward > 0)
        {
            string awardText = String.Empty;
            if (extraAdReward == 10)
            {
                awardText = "x2";
            }
            else if (extraAdReward == 20)
            {
                awardText = "x3";
            }
            watchAdRewardText.text = awardText;
            watchAdBtn.gameObject.SetActive(true);
            watchAdBtn.SetWatchedAdExtraProcess(
                () =>
                {
                    CloudScript.RequestAdReward(extraAdReward, () =>
                    {
                        awardDmCurrency.text = (extraAdReward + awardDm).ToString();
                        watchAdBtn.gameObject.SetActive(false);
                    });
                }
            );
            watchAdBtn.LoadAd();
        }
        else
        {
            watchAdBtn.gameObject.SetActive(false);
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
                if (PlayfabSetting.ArenaPointToRank(arenaPointValue) > PlayfabSetting.ArenaPointToRank(oldPoint))
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