using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoSwitch : MonoBehaviour
{
    [SerializeField] private Button Btn;
    [SerializeField] private Animator animator;
    
    private Action<bool> _action;
    private Func<bool> currentState;
    
    void Switch(bool on)
    {
        animator.SetBool("auto", on);
    }
    
    public void Initialize(Func<bool> state, Action<bool> action)
    {
        _action = action;
        currentState = state;
        Btn.onClick.AddListener(() =>
        {
            var changedState = !this.currentState();
            _action.Invoke(changedState);
            Switch(changedState);
        });
        
        Switch(this.currentState());
    }
}
