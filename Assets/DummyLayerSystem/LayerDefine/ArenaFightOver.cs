using TMPro;
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
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject winObject;
    [SerializeField] private GameObject loseObject;
    [SerializeField] private Button returnBtn;
    
    [SerializeField] private TextMeshProUGUI awardCurrency;
    [SerializeField] private TextMeshProUGUI awardGDCurrency;
    #endregion
    
    #region arena
    [SerializeField] private ArenaRankIcon arenaRankIcon;
    [SerializeField] private TextMeshProUGUI arenaPoint;
    #endregion

    #region arcade
    [SerializeField] private Button AgainBtn;
    [SerializeField] private Button NextBtn;
    #endregion
    
    private int arenaPointValue;
    private TweenerCore<int, int, NoOptions> _tweenerCore;
    
    private void Awake()
    {
        AgainBtn.onClick.AddListener(() =>
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
            UILayerLoader.Remove("ArcadeFightResult");
        });
        returnBtn.onClick.AddListener(NetFightScene.target.ReturnToFront);
    }
    
    static ArenaFightOver Get()
    {
        var l = UILayerLoader.Get("ArenaFightOver");
        ArenaFightOver returnValue = null;
        if (l != null)
        {
            returnValue = l as ArenaFightOver;
        }
        return returnValue;
    }
    
    public static ArenaFightOver Open()
    {
        var returnValue = Get();
        if (returnValue != null)
        {
            return returnValue;
        }
        returnValue = UILayerLoader.Load(NetFightScene.target.T.gameObject, "ArenaFightOver") as ArenaFightOver;
        return returnValue;
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
        _animator.SetTrigger("step2");
    }
    
    public void ShowArenaPoint(int oldPoint, int currentPoint)
    {
        arenaPoint.gameObject.SetActive(true);
        arenaRankIcon.Set(currentPoint);
        arenaRankIcon.gameObject.SetActive(true);
        arenaPointValue = oldPoint;
        _tweenerCore = DOTween.To(
            () => arenaPointValue,          // 何を対象にするのか
            num => arenaPointValue = num,   // 値の更新
            currentPoint,                  // 最終的な値
            3.0f                  // アニメーション時間
        ).OnUpdate(()=> arenaPoint.text = arenaPointValue.ToString());
    }

    public void ShowAward(int awardCurrency, int awardGD)
    {
        this.awardCurrency.gameObject.SetActive(awardCurrency > 0);
        this.awardCurrency.text = awardCurrency.ToString();
        
        this.awardGDCurrency.gameObject.SetActive(awardGD > 0);
        this.awardGDCurrency.text = awardGD.ToString();
    }
    
    public override void OnDestroy()
    {
        base.OnDestroy();
        if (_tweenerCore != null)
            _tweenerCore.Kill();
    }
}