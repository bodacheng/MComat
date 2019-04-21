using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideCharIcon : MonoBehaviour {

	public Data_Center _charDataCenter;
	public Slider theHpBar;

	public charIcon focusingCharIcon;

	BO_Health myHealth;
	int maxHp = 0;
	int currentHp = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CurrentHpBarShow();
	}

	public void iniHPShow(Data_Center watching)
	{
		_charDataCenter = watching;
		if (_charDataCenter)
		{
			myHealth = _charDataCenter.getBOHealth();
		}
		maxHp = _charDataCenter._playerBattleInfo.MaxHP;
		currentHp = maxHp;
	}
    
	public void CurrentHpBarShow()
	{
		if (myHealth == null)
			return;
        currentHp = myHealth._health;
		theHpBar.value = (float)currentHp / (float)maxHp;
	}
}
