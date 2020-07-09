using UnityEngine;

public class FaciaManager : MonoBehaviour
{
    public Animator animator;
    public string HurtTrigger;
    public string WinTrigger;
    public string AggressiveTrigger;
    
    public void TriggerExpression(Facial facial)
    {
        if (animator == null)
            return;
        switch (facial)
        {
            case Facial.hurt:
                if (!string.IsNullOrEmpty(HurtTrigger))
                animator.SetTrigger(HurtTrigger);
            break;
            case Facial.win:
                if (!string.IsNullOrEmpty(WinTrigger))
                animator.SetTrigger(WinTrigger);
            break;
            case Facial.aggressive:
                if (!string.IsNullOrEmpty(AggressiveTrigger))
                animator.SetTrigger(AggressiveTrigger);
            break;
        }
    }
    
    public void Reset()
    {
        if (animator == null)
            return;
        animator.SetTrigger("face_reset");
    }
}

public enum Facial
{
    hurt,
    win,
    aggressive
}