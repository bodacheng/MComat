using System;
using UnityEngine;
using UnityEngine.UI;

public class ArenaFightOver : UILayer
{
    [SerializeField] private Button ReturnBtn;

    public void Initialise(Action R)
    {
        ReturnBtn.onClick.AddListener(R.Invoke);
    }
}