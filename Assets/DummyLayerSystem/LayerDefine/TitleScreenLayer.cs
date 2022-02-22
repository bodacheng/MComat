using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenLayer : UILayer
{
    [SerializeField] private Button TouchScreen;
    [SerializeField] private Button loginBtn;
    
    public void Initialise(Action R, Action login)
    {
        TouchScreen.onClick.AddListener(R.Invoke);
        loginBtn.onClick.AddListener(login.Invoke);
    }
}
