using System;
using UnityEngine;
using UnityEngine.UI;

public class FightBeginBtn : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button btn;

    public void SetAction(Action action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(action.Invoke);
    }
    
    public void Enable(bool on)
    {
        animator.SetBool("On", on);
    }
}
