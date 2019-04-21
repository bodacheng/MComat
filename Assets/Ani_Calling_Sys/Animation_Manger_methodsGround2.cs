using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public partial class Animation_Manger : MonoBehaviour
{
    public IEnumerator overrideClipTimeCounter(animator_layer_index _animator_layer_index, AnimationClip clip)
    {
        if (clip != null)
            yield return new WaitForSeconds(clip.length);
        else
        {
            Debug.Log("不允许带入空动画片段");
            yield return new WaitForSeconds(0.1f);
        }
        setAnimationPlayingStep(AnimationPlaying_Step.over);
        //this.PlayLayerAnim(_animator_layer_index, null);
        //上面这一行之所以不要了，归根结底是为了防止一些情况下应该正常播放的动作被中止
        //一个动画进程结束后就把动作复位，这看起来好像没有问题，但并不是所有情况下我们激活动画都是靠animationCustomCoroutineTrigger函数。
        // animationCustomCoroutineTrigger函数会让上一个动画进程结束后再开始新进程，
        // 对于那些不是靠animationCustomCoroutineTrigger激活动画的情况，它们就没有终止上一个动画进程，导致上一个进程仍然在进行，进行到末尾就会运行PlayLayerAnim(_animator_layer_index, null)
        // 所以我们现在只在移动待机状态的进入点运行PlayLayerAnim(_animator_layer_index, null)
    }

    public void PlayLayerAnim_clip(animator_layer_index animator_layer_index, AnimationClip clip)
    {
        //if ((PhotonNetwork.offlineMode && PhotonNetwork.connected) || !PhotonNetwork.connected)
        //{
            this.PlayLayerAnimRPC ((int)animator_layer_index, clip);
        //}
        //else
            //photonView.RPC("PlayLayerAnimRPC", PhotonTargets.All, (int)animator_layer_index, clip_name);
    }

    void PlayLayerAnimRPC(int animator_layer_index, AnimationClip clip)
    {
        AnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(animator_layer_index);
        switch (animator_layer_index)
        {
            case 1:
                if (AnimatorStateInfo.IsName("Full Body.null"))
                {
                    if (Animator.GetAnimatorTransitionInfo(animator_layer_index).IsName("Full Body.null -> Full Body.full_body_state1"))
                    {
                        if (clip != null)
                        {
                            nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                            to_be_override_animation_name = "fullbody_empty2";
                            pre_overrided_anim_name = "fullbody_empty1";
                            trigger_name = "fullbody_trigger2";
                            fullbodylayer_return_trigger_name = "fullbody_return1";
                            break;
                        }
                        else if (clip == null)
                        {                            
                            nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                            to_be_override_animation_name = null;
                            pre_overrided_anim_name = "fullbody_empty1";
                            trigger_name = null;
                            fullbodylayer_return_trigger_name = "fullbody_return1";
                            break;
                        }
                    }
                    else
                    {
                        nextStateName = "Full Body.null -> Full Body.full_body_state1";
                        if (clip != null)
                        {
                            to_be_override_animation_name = "fullbody_empty1";
                            pre_overrided_anim_name = null;
                            trigger_name = "fullbody_trigger1";
                            fullbodylayer_return_trigger_name = null;
                            break;
                        }
                        else
                        {
                            doNothingFlag = true;
                            break;
                        }
                    }
                }
                if (AnimatorStateInfo.IsName("Full Body.full_body_state1"))
                {
                    if (Animator.GetAnimatorTransitionInfo(animator_layer_index).IsName("Full Body.full_body_state1 -> Full Body.full_body_state2"))
                    {
                        if (clip != null)
                        {
                            nextStateName = "Full Body.full_body_state2 -> Full Body.full_body_state1";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = "fullbody_empty1";
                            pre_overrided_anim_name = "fullbody_empty2";
                            trigger_name = "fullbody_trigger1";
                            fullbodylayer_return_trigger_name = "fullbody_return2";
                            break;
                        }
                        else
                        {
                            nextStateName = "Full Body.full_body_state2 -> Full Body.null";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = null;
                            pre_overrided_anim_name = "fullbody_empty2";
                            trigger_name = null;
                            fullbodylayer_return_trigger_name = "fullbody_return2";
                            break;
                        }
                    }
                    else if (Animator.GetAnimatorTransitionInfo(animator_layer_index).IsName("Full Body.full_body_state1 -> Full Body.null"))
                    {
                        if (clip != null)
                        {
                            nextStateName = "Full Body.null -> Full Body.full_body_state1";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = "fullbody_empty1";
                            pre_overrided_anim_name = null;
                            trigger_name = "fullbody_trigger1";
                            fullbodylayer_return_trigger_name = null;
                            break;
                        }
                        else
                        {
                            doNothingFlag = true;
                            break;
                        }
                    }
                    else
                    {
                        if (clip != null)
                        {
                            nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = "fullbody_empty2";
                            pre_overrided_anim_name = "fullbody_empty1";
                            trigger_name = "fullbody_trigger2";
                            fullbodylayer_return_trigger_name = "fullbody_return1";
                        }else{
                            // 0623重大修改！！！！！！
                            nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = null;
                            pre_overrided_anim_name = "fullbody_empty1";
                            trigger_name = null;
                            fullbodylayer_return_trigger_name = "fullbody_return1";
                        }
                        break;
                    }
                }
                if (AnimatorStateInfo.IsName("Full Body.full_body_state2"))
                {
                    if (Animator.GetAnimatorTransitionInfo(animator_layer_index).IsName("Full Body.full_body_state2 -> Full Body.full_body_state1"))
                    {
                        if (clip != null)
                        {
                            nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = "fullbody_empty2";
                            pre_overrided_anim_name = "fullbody_empty1";
                            trigger_name = "fullbody_trigger2";
                            fullbodylayer_return_trigger_name = "fullbody_return1";
                            break;
                        }
                        else
                        {
                            nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = null;
                            pre_overrided_anim_name = "fullbody_empty1";
                            trigger_name = null;
                            fullbodylayer_return_trigger_name = "fullbody_return1";
                            break;
                        }
                    }
                    else if (Animator.GetAnimatorTransitionInfo(animator_layer_index).IsName("Full Body.full_body_state2 -> Full Body.null"))
                    {
                        if (clip != null)
                        {
                            nextStateName = "Full Body.null -> Full Body.full_body_state1";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = "fullbody_empty1";
                            pre_overrided_anim_name = null;
                            trigger_name = "fullbody_trigger1";
                            fullbodylayer_return_trigger_name = null;
                            break;
                        }
                        else
                        {
                            doNothingFlag = true;
                            break;
                        }
                    }
                    else
                    {
                        if (clip != null)
                        {
                            nextStateName = "Full Body.full_body_state2 -> Full Body.full_body_state1";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = "fullbody_empty1";
                            pre_overrided_anim_name = "fullbody_empty2";
                            trigger_name = "fullbody_trigger1";
                            fullbodylayer_return_trigger_name = "fullbody_return2";
                        }else{
                            // 0623重大修改！！！！！！
                            nextStateName = "Full Body.full_body_state2 -> Full Body.null";
                            current_animation_name = Animator.GetCurrentAnimatorClipInfo(animator_layer_index)[0].clip.name;
                            to_be_override_animation_name = null;
                            pre_overrided_anim_name = "fullbody_empty2";
                            trigger_name = null;
                            fullbodylayer_return_trigger_name = "fullbody_return2";
                        }
                        break;
                    }
                }
                break;
            default:
                break;
        }

        the_trigger(clip, this.doNothingFlag);
        doNothingFlag = false;
    }


    void the_trigger(AnimationClip clip,bool doNothingFlag)
    {
        if (doNothingFlag)
            return;

        if (clip != null)
        {
            if (trigger_name != null)
            {
                if (pre_overrided_anim_name != null)
                {
                    animatorOverride[pre_overrided_anim_name] = clip; // 关于前动画起始点的问题有待商榷。可能没问题。
                }
                else
                {
                    // 0623重大修改！！！！！！把底下这行给comment out了
                    //myOverrideController[pre_overrided_anim_name] = null;//也就是说从null状态出发的时候
                }
                if (to_be_override_animation_name != null)
                {
                    animatorOverride[to_be_override_animation_name] = clip;
                }
                //Animator.runtimeAnimatorController = animatorOverride;
                Animator.SetTrigger(trigger_name);                                    
                current_animation_name = clip.name;
            }
        }
        else
        {
            if (fullbodylayer_return_trigger_name != null)
            {
                Animator.SetTrigger(fullbodylayer_return_trigger_name);
            }
            current_animation_name = null;
        }
    }
}