using UnityEngine;

public partial class Animation_Manger : MonoBehaviour
{
    void PlayLayerAnim_clip(AnimationClip clip, bool in_transition , float Duration)
    {
        AnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(1);
        if (Animator.GetBool("in_transition"))
        {
            Animator.SetBool("in_transition", in_transition);
            Animator.Play("null", 1);//Animator.GetNextAnimatorStateInfo(1).fullPathHash
            Animator.Update(0);
            if (clip != null)
            {
                to_be_override_animation_name = "fullbody_empty1";
                animatorOverride[to_be_override_animation_name] = clip;
                Animator.CrossFade("full_body_state1", Duration);
            }else{

            }
        }
        else
        {
            Animator.SetBool("in_transition", in_transition);
            if (AnimatorStateInfo.IsName("Full Body.null"))
            {
                if (clip != null)
                {
                    to_be_override_animation_name = "fullbody_empty1";
                }
                else
                {
                    Animator.SetBool("in_transition", false);
                    return;
                }
            }
            if (AnimatorStateInfo.IsName("Full Body.full_body_state1"))
            {
                to_be_override_animation_name = clip != null ? "fullbody_empty2" : null;
            }
            if (AnimatorStateInfo.IsName("Full Body.full_body_state2"))
            {
                to_be_override_animation_name = clip != null ? "fullbody_empty1" : null;
            }
            
            if (clip != null)
            {
                animatorOverride[to_be_override_animation_name] = clip;                    
                if (to_be_override_animation_name == "fullbody_empty2")
                {
                    Animator.CrossFade("full_body_state2", Duration);
                }
                if (to_be_override_animation_name == "fullbody_empty1")
                {
                    Animator.CrossFade("full_body_state1", Duration);
                }
            }else{
                Animator.CrossFade("null", Duration);
            }       
        }
    }
}