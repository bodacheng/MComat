using UnityEngine;
using UnityEngine.UI;

public class SideCharIcon : MonoBehaviour {

	public Data_Center _charDataCenter;
    
    [Header("浮动血条pretab")]
    [Space(6)]
    public Slider HpBar;
    [Header("浮动抵抗pretab")]
    [Space(6)]
    public Slider ResistBar;
    public Image ResistBarFillImage;

	public HeroIcon focusingCharIcon;

	float maxHp;
	float currentHp;

	public void IniHPShow(Data_Center watching)
	{
		_charDataCenter = watching;
		maxHp = 500;
		currentHp = maxHp;
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
