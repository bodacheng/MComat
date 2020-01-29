using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public partial class Animation_Manger : MonoBehaviour
{
    void PlayLayerAnim_clip(AnimationClip clip)
    {
        AnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(1);
        if (AnimatorStateInfo.IsName("Full Body.null"))
        {
            if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.null -> Full Body.full_body_state1"))
            {
                if (clip != null)
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                    to_be_override_animation_name = "fullbody_empty2";
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = "fullbody_trigger2";
                }
                else if (clip == null)
                {                            
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = null;
                }
            }
            else
            {
                //nextStateName = "Full Body.null -> Full Body.full_body_state1";
                if (clip != null)
                {
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = null;
                    trigger_name = "fullbody_trigger1";
                }
                else
                {
                    doNothingFlag = true;
                }
            }
        }
        if (AnimatorStateInfo.IsName("Full Body.full_body_state1"))
        {
            if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state1 -> Full Body.full_body_state2"))
            {
                if (clip != null)
                {
                    //nextStateName = "Full Body.full_body_state2 -> Full Body.full_body_state1";
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = "fullbody_empty2";
                    trigger_name = "fullbody_trigger1";
                }
                else
                {
                    //nextStateName = "Full Body.full_body_state2 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty2";
                    trigger_name = null;
                }
            }
            else if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state1 -> Full Body.null"))
            {
                if (clip != null)
                {
                    //nextStateName = "Full Body.null -> Full Body.full_body_state1";
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = null;
                    trigger_name = "fullbody_trigger1";
                }
                else
                {
                    doNothingFlag = true;
                }
            }
            else
            {
                if (clip != null)
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                    to_be_override_animation_name = "fullbody_empty2";
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = "fullbody_trigger2";
                }else{
                    // 0623重大修改！！！！！！
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = null;
                }
            }
        }
        if (AnimatorStateInfo.IsName("Full Body.full_body_state2"))
        {
            if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state2 -> Full Body.full_body_state1"))
            {
                if (clip != null)
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                    to_be_override_animation_name = "fullbody_empty2";
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = "fullbody_trigger2";
                }
                else
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = null;
                }
            }
            else if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state2 -> Full Body.null"))
            {
                if (clip != null)
                {
                    //nextStateName = "Full Body.null -> Full Body.full_body_state1";
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = null;
                    trigger_name = "fullbody_trigger1";
                }
                else
                {
                    doNothingFlag = true;
                }
            }
            else
            {
                if (clip != null)
                {
                    //nextStateName = "Full Body.full_body_state2 -> Full Body.full_body_state1";
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = "fullbody_empty2";
                    trigger_name = "fullbody_trigger1";
                }else{
                    // 0623重大修改！！！！！！
                    //nextStateName = "Full Body.full_body_state2 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty2";
                    trigger_name = null;
                }
            }
        }

        the_trigger(clip, this.doNothingFlag);
        doNothingFlag = false;
    }


    void the_trigger(AnimationClip clip,bool _doNothingFlag)
    {
        if (_doNothingFlag)
            return;

        if (clip != null)
        {
            if (trigger_name != null)
            {
                // 2019.6.1重大修改。
                //if (pre_overrided_anim_name != null)
                    //animatorOverride[pre_overrided_anim_name] = clip; 
                //else
                    // 0623重大修改！！！！！！把底下这行给comment out了
                    //myOverrideController[pre_overrided_anim_name] = null;//也就是说从null状态出发的时候
                if (to_be_override_animation_name != null)
                {
                    animatorOverride[to_be_override_animation_name] = clip;
                }
                //Animator.runtimeAnimatorController = animatorOverride;
                if (to_be_override_animation_name == "fullbody_empty2")
                    Animator.CrossFade("full_body_state2", 0.1f);
                if (to_be_override_animation_name == "fullbody_empty1")
                    Animator.CrossFade("full_body_state1", 0.1f);
            }
        }
        else
        {
            Animator.CrossFade("null", 0.15f);
        }
    }
}