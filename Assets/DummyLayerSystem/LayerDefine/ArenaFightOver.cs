using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DummyLayerSystem;
using FightScene;

public class ArenaFightOver : UILayer
{
    #region commen
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject winObject;
    [SerializeField] private GameObject loseObject;
    [SerializeField] private Button returnBtn;
    [SerializeField] private Text awardDmCurrency;
    [SerializeField] private Text awardGDCurrency;
    #endregion
    
    #region arena
    [SerializeField] private ArenaRankIcon arenaRankIcon;
    [SerializeField] private Text arenaPoint;
    #endregion
    
    #region arcade
    [SerializeField] private Button againBtn;
    public Button AgainBtn => againBtn;
    #endregion
    
    private int arenaPointValue;
    private TweenerCore<int, int, NoOptions> _arenaPointTweenerCore;
    private TweenerCore<int, int, NoOptions> _dmAwardTweenerCore;
    private TweenerCore<int, int, NoOptions> _gdAwardTweenerCore;
    
    private void Awake()
    {
        againBtn.onClick.AddListener(() =>
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
            UILayerLoader.Remove<ArenaFightOver>();
        });
        returnBtn.onClick.AddListener(FightScene.FightScene.target.ReturnToFront);
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

    public async UniTask ShowAward(int awardDm, int awardGD)
    {
        async UniTask DM()
        {
            if (awardDm > 0)
            {
                this.awardDmCurrency.gameObject.SetActive(true);
                this.awardDmCurrency.text = Currencies.DiamondCount.Value.ToString();
                var currentDmValue = Currencies.DiamondCount.Value + awardDm;
                await UniTask.Delay( TimeSpan.FromSeconds(1) );
                _dmAwardTweenerCore = DOTween.To(
                    () => Currencies.DiamondCount.Value,          // 何を対象にするのか
                    num => Currencies.DiamondCount.Value = num,
                    currentDmValue,
                    3f
                ).OnUpdate(()=> this.awardDmCurrency.text = Currencies.DiamondCount.Value+ " (+" + awardDm + ")");
            }
        }

        async UniTask GD()
        {
            if (awardGD > 0)
            {
                awardGDCurrency.gameObject.SetActive(true);
                awardGDCurrency.text = Currencies.CoinCount.Value.ToString();
                var currentGdValue = Currencies.CoinCount.Value + awardGD;
                await UniTask.Delay( TimeSpan.FromSeconds(2) );
                _gdAwardTweenerCore = DOTween.To(
                    () => Currencies.CoinCount.Value,          // 何を対象にするのか
                    num => Currencies.CoinCount.Value = num,
                    currentGdValue,
                    3f
                ).OnUpdate(()=> awardGDCurrency.text = Currencies.CoinCount.Value+ " (+" + awardGD + ")");
            }
        }
        
        await UniTask.WhenAll(DM(), GD());
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        _arenaPointTweenerCore?.Kill();
        _dmAwardTweenerCore?.Kill();
        _gdAwardTweenerCore?.Kill();
    }
}