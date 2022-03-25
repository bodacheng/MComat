using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;

public class AutoSwitch : MonoBehaviour
{
    [SerializeField] private PressGesture Btn;
    [SerializeField] private GameObject onObject;
    [SerializeField] private GameObject offObject;
    
    private Action<bool> _action;
    private Func<bool> currentState;
    
    void Switch(bool on)
    {
        onObject.SetActive(on);
        offObject.SetActive(!on);
    }

    public void INI(Func<bool> currentState, Action<bool> action)
    {
        _action = action;
        this.currentState = currentState;
        Btn.Pressed += temp;
        
        void temp(object sender, System.EventArgs e)
        {
            var changedState = !this.currentState();
            _action.Invoke(changedState);
            Switch(changedState);
        }
        
        Switch(this.currentState());
    }
}
