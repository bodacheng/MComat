using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SideUnitIcon : MonoBehaviour {
    
    [SerializeField] Slider hpBar;
    [SerializeField] Text hpText;
    [SerializeField] Slider resistBar;
    [SerializeField] GameObject[] charges;
    [SerializeField] GameObject dreamComboFlg;
    [SerializeField] HeroIcon focusingCharIcon;
    [SerializeField] Text teamIndicator;
    public HeroIcon Icon => focusingCharIcon;
    public Text TeamIndicator => teamIndicator;
    public GameObject DreamComboFlg => dreamComboFlg;
    private RectTransform hpBarRect;
    private RectTransform resistBarRect;
    private int displayedHp = int.MinValue;

    void Awake()
    {
        hpBarRect = hpBar != null ? hpBar.transform as RectTransform : null;
        resistBarRect = resistBar != null ? resistBar.transform as RectTransform : null;
    }

    private Tweener resistBarTweener;
    public void RefreshResistanceBar(float resistance)
    {
        var targetValue = resistance / 10f;
        if (Mathf.Approximately(resistBar.value, targetValue))
        {
            return;
        }

        resistBarTweener?.Kill();
        resistBarTweener = DOTween.To(() => resistBar.value, (x) => resistBar.value = x, targetValue, 0.2f);
    }
    
    private Tweener hpBarTweener;
    public void RefreshHpBar(float currentHp, float wholeHp)
    {
        var hpValue = Mathf.CeilToInt(currentHp);
        if (displayedHp != hpValue)
        {
            displayedHp = hpValue;
            hpText.text = hpValue.ToString();
        }

        var targetValue = wholeHp > 0 ? currentHp / wholeHp : 0f;
        if (Mathf.Approximately(hpBar.value, targetValue))
        {
            return;
        }

        hpBarTweener?.Kill();
        hpBarTweener = DOTween.To(() => hpBar.value, (x) => hpBar.value = x, targetValue, 0.2f);
    }

    void OnDestroy()
    {
        resistBarTweener?.Kill();
        hpBarTweener?.Kill();
    }

    public void RefreshExBar(int currentEx)
    {
        SetActiveIfNeeded(charges[2], currentEx >= 90);
        SetActiveIfNeeded(charges[1], currentEx >= 60);
        SetActiveIfNeeded(charges[0], currentEx >= 30);
    }
    
    public void RecallBars()
    {
        hpBar.transform.SetParent(transform);
        if (hpBarRect != null)
        {
            hpBarRect.anchoredPosition = new Vector2(0, 12);
        }
        hpBar.transform.localScale = Vector3.one;
        resistBar.transform.SetParent(transform);
        if (resistBarRect != null)
        {
            resistBarRect.anchoredPosition = new Vector2(0, 12);
        }
        resistBar.transform.localScale = Vector3.one;
    }

    public void GreyOut()
    {
        SetActiveIfNeeded(hpBar.gameObject, false);
        SetActiveIfNeeded(hpText.gameObject, false);
        SetActiveIfNeeded(resistBar.gameObject, false);
        SetActiveIfNeeded(teamIndicator.gameObject, false);
        if (dreamComboFlg != null)
        {
            SetActiveIfNeeded(dreamComboFlg, false);
        }
        focusingCharIcon.CooldownCurtainUpdate(1);
    }

    static void SetActiveIfNeeded(GameObject target, bool active)
    {
        if (target != null && target.activeSelf != active)
        {
            target.SetActive(active);
        }
    }
}
