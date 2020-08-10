using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SideCharIcon : MonoBehaviour {

    public Data_Center _charDataCenter;
    
    [Header("浮动血条pretab")]
    [Space(6)]
    public Slider HpBar;
    
    [Header("浮动血条pretab")]
    [Space(6)]
    public Text HpText;
    
    [Header("浮动抵抗pretab")]
    [Space(6)]
    public Slider ResistBar;
    public Image ResistBarFillImage;
    
    [Header("浮动Ex条")]
    [Space(6)]
    public Slider ExBar;
    
    [Header("必杀技点")]
    [Space(6)]
    public GameObject[] charges;
    
    public HeroIcon focusingCharIcon;
    
    float maxHp;
    
    public void RefreshResistanceBar()
    {
        DOTween.To(() => ResistBar.value, (x) => ResistBar.value = x, _charDataCenter._ResistanceManager.Resistance.Value / 10f, 0.2f);
        ResistBarFillImage.color = _charDataCenter._ResistanceManager.Resistance.Value > 0 ? Color.yellow : Color.clear;
    }
    
    public void RefreshHpBar(float current_hp, float whole_hp)
    {
        HpText.text = Mathf.Ceil(current_hp).ToString();
        DOTween.To(() => HpBar.value, (x) => HpBar.value = x, current_hp / whole_hp, 0.2f);
    }
    
    public void RefreshExBar(int current_ex, int wholeex)
    {
        if (current_ex > 0 && !ExBar.fillRect.gameObject.activeSelf)
        {
            ExBar.fillRect.gameObject.SetActive(true);
        }
        DOTween.To(() => ExBar.value, (x) => ExBar.value = x, (float)current_ex / wholeex, 0.1f).OnComplete(() => { if (System.Math.Abs(ExBar.value) < 0.1) ExBar.fillRect.gameObject.SetActive(false); });
        if (current_ex >= 90)
        {
            charges[2].SetActive(true);
        }else{
            charges[2].SetActive(false);
        }
        
        if (current_ex >= 60)
        {
            charges[1].SetActive(true);
        }else{
            charges[1].SetActive(false);
        }
        
        if (current_ex >= 30)
        {
            charges[0].SetActive(true);
        }else{
            charges[0].SetActive(false);
        }
    }
    
    public void INIHPShow(Data_Center watching, float MaxHp)
    {
    	_charDataCenter = watching;
        maxHp = MaxHp;
        HpBar.value = 1;
    }
    
    public void RecallBars()
    {
        HpBar.transform.SetParent(transform);
        HpBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,20,0);
        HpBar.transform.localScale = Vector3.one;
        ResistBar.transform.SetParent(transform);
        ResistBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,20,0);
        ResistBar.transform.localScale = Vector3.one;
    }
}
