using System.Collections;
using System.Collections.Generic;
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

	public charIcon focusingCharIcon;

	float maxHp = 0;
	float currentHp = 0;

	public void iniHPShow(Data_Center watching)
	{
		_charDataCenter = watching;
		maxHp = 500;
		currentHp = maxHp;
	}
    
    public void recallBars()
    {
        HpBar.transform.SetParent(transform);
        HpBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,20,0);
        HpBar.transform.localScale = Vector3.one;
        ResistBar.transform.SetParent(transform);
        ResistBar.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,20,0);
        ResistBar.transform.localScale = Vector3.one;
    }
}
