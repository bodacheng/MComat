using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenLayer : UILayer
{
    [SerializeField] private Button TouchScreen;
    
    public void Initialise(Action R)
    {
        TouchScreen.onClick.AddListener(R.Invoke);
    }
}
