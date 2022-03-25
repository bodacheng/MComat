using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSwitch : MonoBehaviour
{
    [SerializeField] private Button Btn;
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
        
        Btn.onClick.AddListener(() =>
        {
            var changedState = !this.currentState();
            _action.Invoke(changedState);
            Switch(changedState);
        });
        
        Switch(this.currentState());
    }
}
