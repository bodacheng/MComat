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
    
    [SerializeField] private Text awardCurrency;
    [SerializeField] private Text awardGDCurrency;
    #endregion
    
    #region arena
    [SerializeField] private ArenaRankIcon arenaRankIcon;
    [SerializeField] private Text arenaPoint;
    #endregion

    #region arcade
    [SerializeField] private Button againBtn;
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
        arenaRankIcon.Set(currentPoint);
        arenaRankIcon.gameObject.SetActive(true);
        arenaPointValue = oldPoint;
        _arenaPointTweenerCore = DOTween.To(
            () => arenaPointValue,          // 何を対象にするのか
            num => arenaPointValue = num,   // 値の更新
            currentPoint,                  // 最終的な値
            3.0f                  // アニメーション時間
        ).OnUpdate(()=> arenaPoint.text = arenaPointValue.ToString());
    }

    public async UniTask ShowAward(int awardCurrency, int awardGD)
    {
        async UniTask DM()
        {
            if (awardCurrency > 0)
            {
                this.awardCurrency.gameObject.SetActive(true);
                this.awardCurrency.color = Color.yellow;
                this.awardCurrency.text = awardCurrency.ToString();
                var currentDmValue = Currencies.DiamondCount.Value + awardCurrency;
                await UniTask.DelayFrame(40);
                this.awardCurrency.color = Color.green;
                _dmAwardTweenerCore = DOTween.To(
                    () => Currencies.DiamondCount.Value,          // 何を対象にするのか
                    num => Currencies.DiamondCount.Value = num,
                    currentDmValue,
                    3f
                ).OnUpdate(()=> this.awardCurrency.text = Currencies.DiamondCount.Value.ToString() + " (+" + awardCurrency + ")");
            }
        }

        async UniTask GD()
        {
            if (awardGD > 0)
            {
                this.awardGDCurrency.gameObject.SetActive(true);
                this.awardGDCurrency.color = Color.yellow;
                this.awardGDCurrency.text = awardGD.ToString();
                var currentGdValue = Currencies.CoinCount.Value + awardGD;
                await UniTask.DelayFrame(40);
                this.awardGDCurrency.color = Color.green;
                _gdAwardTweenerCore = DOTween.To(
                    () => Currencies.CoinCount.Value,          // 何を対象にするのか
                    num => Currencies.CoinCount.Value = num,
                    currentGdValue,
                    3f
                ).OnUpdate(()=> this.awardGDCurrency.text = Currencies.CoinCount.Value.ToString()+ " (+" + awardGD + ")");
            }
        }

        await UniTask.WhenAll(DM(), GD());
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        if (_arenaPointTweenerCore != null)
            _arenaPointTweenerCore.Kill();
        if (_dmAwardTweenerCore != null)
            _dmAwardTweenerCore.Kill();
        if (_gdAwardTweenerCore != null)
            _gdAwardTweenerCore.Kill();
    }
}