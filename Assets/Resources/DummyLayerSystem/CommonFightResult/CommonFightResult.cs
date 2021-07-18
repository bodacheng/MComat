using UnityEngine;
using UnityEngine.UI;
using System;

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
