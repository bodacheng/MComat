using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoSwitch : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Animator animator;
    
    private Action<bool> _action;
    private Func<bool> _currentState;

    private bool startState;

    void OnEnable()
    {
        Switch(startState);
    }
    
    void Switch(bool on)
    {
        animator.SetBool("auto", on);
    }
    
    public void Initialize(Func<bool> state, Action<bool> action)
    {
        _action = action;
        _currentState = state;
        btn.onClick.AddListener(() =>
        {
            var changedState = !this._currentState();
            _action.Invoke(changedState);
            Switch(changedState);
        });

        startState = this._currentState();
        Switch(this._currentState());
    }
}
