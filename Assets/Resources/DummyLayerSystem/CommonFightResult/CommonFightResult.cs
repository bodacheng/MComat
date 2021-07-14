using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using UnityEngine.SceneManagement;
using Soul;
using Log;
using System;
using FightScene;

public class CommonFightResult : UILayer
{
    [SerializeField] private Button ReturnBtn;
    [SerializeField] private Button AgainBtn;
    [SerializeField] private RectTransform IconAndSKillShowUISetT;

    public RectTransform GetIconAndSKillShowUISetT()
    {
        return IconAndSKillShowUISetT;
    }
    
    public void Initialise(Action R, Action A)
    {
        ReturnBtn.onClick.AddListener(R.Invoke);
        AgainBtn.onClick.AddListener(A.Invoke);
    }
}
