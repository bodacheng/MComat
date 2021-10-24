using System.Collections.Generic;
using UnityEngine;

public partial class Animation_Manger
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
                to_be_override_anim_name = "fullbody_empty1";
                animatorOverride[to_be_override_anim_name] = clip;
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
                    to_be_override_anim_name = "fullbody_empty1";
                }
                else
                {
                    Animator.SetBool("in_transition", false);
                    return;
                }
            }
            if (AnimatorStateInfo.IsName("Full Body.full_body_state1"))
            {
                to_be_override_anim_name = clip != null ? "fullbody_empty2" : null;
            }
            if (AnimatorStateInfo.IsName("Full Body.full_body_state2"))
            {
                to_be_override_anim_name = clip != null ? "fullbody_empty1" : null;
            }
            
            if (clip != null)
            {
                animatorOverride[to_be_override_anim_name] = clip;                    
                if (to_be_override_anim_name == "fullbody_empty2")
                {
                    Animator.CrossFade("full_body_state2", Duration);
                }
                if (to_be_override_anim_name == "fullbody_empty1")
                {
                    Animator.CrossFade("full_body_state1", Duration);
                }
            }else{
                Animator.CrossFade("null", Duration);
            }       
        }
    }
    
    List<AnimationClip> hurtclips_back, hurtclips_low, hurtclips_high, hurtclips_press, hurtclips_lay;
    List<AnimationClip> knockoffAnimations;
    public void PrepareHurtAndKnockOffAnimations(string type)
    {
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_knockoffs", out knockoffAnimations);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/back", out hurtclips_back);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/low", out hurtclips_low);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/high", out hurtclips_high);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/press", out hurtclips_press);
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(type + "/basic_hurts/lay", out hurtclips_lay);
    }
    
    public AnimationClip GetRandomHurtAnim(string hurtPos)
    {
        List<AnimationClip> target;
        switch (hurtPos)
        {
            case "back":
            target = hurtclips_back;
            break;
            case "lay":
            target = hurtclips_lay;
            break;
            case "high":
            target = hurtclips_high;
            break;
            case "low":
            target = hurtclips_low;
            break;
            case "press":
            target = hurtclips_press;
            break;
            default:
            target = hurtclips_high;
            break;
        }
        int ranDom = Random.Range(0, target.Count);
        return target[ranDom];
    }
    
    public AnimationClip GetRandomKnockOffAnim()
    {
        int ranDom = Random.Range(0, knockoffAnimations.Count);
        return knockoffAnimations[ranDom];
    }
}