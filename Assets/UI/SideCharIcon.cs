using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideCharIcon : MonoBehaviour {

	public Data_Center _charDataCenter;
    
    [Header("浮动血条pretab")]
    [Space(6)]
    public Slider hpBarPrefab;
    [Header("浮动抵抗pretab")]
    [Space(6)]
    public Slider resistBarPrefab;

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
        hpBarPrefab.transform.SetParent(transform);
        hpBarPrefab.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,20,0);
        hpBarPrefab.transform.localScale = Vector3.one;
        resistBarPrefab.transform.SetParent(transform);
        resistBarPrefab.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,20,0);
        resistBarPrefab.transform.localScale = Vector3.one;
    }
}
