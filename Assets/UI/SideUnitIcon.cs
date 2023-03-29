using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SideUnitIcon : MonoBehaviour {
    
    public Slider HpBar;
    [SerializeField] Text HpText;
    
    [Header("浮动抵抗")]
    [SerializeField] Slider ResistBar;
    
    [Header("浮动Ex条")]
    public Slider ExBar;
    
    [Header("必杀技点")]
    [SerializeField] GameObject[] charges;
    
    public HeroIcon focusingCharIcon;
    
    public void RefreshResistanceBar(float resistance)
    {
        DOTween.To(() => ResistBar.value, (x) => ResistBar.value = x, resistance / 10f, 0.2f);
    }
    
    public void RefreshHpBar(float currentHp, float wholeHp)
    {
        HpText.text = Mathf.Ceil(currentHp).ToString();
        DOTween.To(() => HpBar.value, (x) => HpBar.value = x, currentHp / wholeHp, 0.2f);
    }
    
    public void RefreshExBar(int currentEx, int wholeEx)
    {
        if (currentEx > 0 && !ExBar.fillRect.gameObject.activeSelf)
        {
            ExBar.fillRect.gameObject.SetActive(true);
        }
        DOTween.To(() => ExBar.value, (x) => ExBar.value = x, (float)currentEx / wholeEx, 0.1f).OnComplete(() => { if (System.Math.Abs(ExBar.value) < 0.1) ExBar.fillRect.gameObject.SetActive(false); });
        if (currentEx >= 90)
        {
            charges[2].SetActive(true);
        }else{
            charges[2].SetActive(false);
        }
        
        if (currentEx >= 60)
        {
            charges[1].SetActive(true);
        }else{
            charges[1].SetActive(false);
        }
        
        if (currentEx >= 30)
        {
            charges[0].SetActive(true);
        }else{
            charges[0].SetActive(false);
        }
    }
    
    public void RecallBars()
    {
        HpBar.transform.SetParent(transform);
        HpBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,15,0);
        HpBar.transform.localScale = Vector3.one;
        ResistBar.transform.SetParent(transform);
        ResistBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,15,0);
        ResistBar.transform.localScale = Vector3.one;
    }
}
