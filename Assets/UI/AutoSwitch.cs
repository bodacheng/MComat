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

    private bool isOn;
    private Action<bool> _action;
    
    void Switch(bool on)
    {
        isOn = on;
        _action.Invoke(isOn);
        onObject.SetActive(on);
        offObject.SetActive(!on);
    }
    
    private void Awake()
    {
        Btn.onClick.AddListener(() =>
        {
            Switch(!isOn);
        });
    }

    public void INI(bool on, Action<bool> action)
    {
        _action = action;
        Switch(on);
    }
}
