using UnityEngine;
using UnityEngine.UI;
using System;

public class ArcadeFightResult : UILayer
{
    [SerializeField] private Button ReturnBtn;
    [SerializeField] private Button AgainBtn;
    
    public void Initialise(Action R, Action A)
    {
        ReturnBtn.onClick.AddListener(R.Invoke);
        AgainBtn.onClick.AddListener(A.Invoke);
    }
}
