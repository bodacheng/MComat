using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SideUnitIcon : MonoBehaviour {
    
    [SerializeField] Slider HpBar;
    [SerializeField] Text HpText;
    [SerializeField] Slider ResistBar;
    [SerializeField] GameObject[] charges;
    [SerializeField] HeroIcon focusingCharIcon;

    public HeroIcon Icon => focusingCharIcon;
    
    public void RefreshResistanceBar(float resistance)
    {
        DOTween.To(() => ResistBar.value, (x) => ResistBar.value = x, resistance / 10f, 0.2f);
    }
    
    public void RefreshHpBar(float currentHp, float wholeHp)
    {
        HpText.text = Mathf.Ceil(currentHp).ToString();
        DOTween.To(() => HpBar.value, (x) => HpBar.value = x, currentHp / wholeHp, 0.2f);
    }
    
    public void RefreshExBar(int currentEx)
    {
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

    public void GreyOut()
    {
        HpBar.gameObject.SetActive(false);
        HpText.gameObject.SetActive(false);
        ResistBar.gameObject.SetActive(false);
        focusingCharIcon.CooldownCurtainUpdate(1);
    }
}
