using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public partial class Animation_Manger : MonoBehaviour
{
    // 大体的image就是能够直接通过动画文件名来随时决定animator当中各个层动画的播放。而不再需要复杂的animator。。
    // 然而这和unity的这个animator系统的初衷产生矛盾
    //整个系统建立在animator本身这样的前提下：
    //1. animator在状态迁移过程中，“当前状态”为迁移出发状态
    //2. 在迁移过程中，如果激发了迁移终点状态向另一状态（或返回至出发状态）迁移的条件，（起码我们知道trigger满足这点），则状态机会在按原节奏到达终点状态后，
	//再去向第三者状态迁移，之前的迁移过程并不会受干扰，后触发的迁移条件也并不会被遗忘，一切会按顺序进行
    //从而也就是说对最后要触发那个状态来说，从条件激活到开始进入会发生一点延迟。

    public Animator Animator;
    public string current_animation_name;

    private AnimationPlaying_Step Coroutine_Step = AnimationPlaying_Step.unstarted;
    private AnimatorOverrideController animatorOverride;
    private string nextStateName;
    private bool doNothingFlag;
    private string toRunAniName;
    private string fullbodylayer_return_trigger_name = null;
    private string to_be_override_animation_name = null;
    private string trigger_name = null;
    private string pre_overrided_anim_name = null;
    private bool isForShowMode = false;
    private AniProcessCoroutine _AniProcessCoroutine;
    private Coroutine animprocess;

    public void setAnimationPlayingStep(AnimationPlaying_Step _step)
    {
        this.Coroutine_Step = _step;
    }

    public IEnumerator overrideClipTimeCounter(animator_layer_index _animator_layer_index, string clip_name)
    {
        if (tryAnimationClip(clip_name) != null)
            yield return new WaitForSeconds(tryAnimationClip(clip_name).length);
        else
        {
            Debug.Log("错误，无法找到动画:" + clip_name);
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

    public class AniProcessCoroutine
    {
        Animation_Manger animation_Manger;
        animator_layer_index _animator_layer_index;
        string clip_name;//与下面的clip机能独立
        AnimationClip clip;//与上面的clip_name机能独立

        public AniProcessCoroutine(Animation_Manger animation_Manger, animator_layer_index animator_layer_index, string clip_name)
        {
            this.animation_Manger = animation_Manger;
            this._animator_layer_index = animator_layer_index;
            this.clip_name = clip_name;
        }

        public void customCoroutineTrigger()
        {
            if (this.animation_Manger.animprocess != null)
                this.animation_Manger.StopCoroutine(this.animation_Manger.animprocess);
            this.animation_Manger.PlayLayerAnim(_animator_layer_index, clip_name);
            this.animation_Manger.setAnimationPlayingStep(AnimationPlaying_Step.running);
            this.animation_Manger.animprocess = this.animation_Manger.StartCoroutine(this.animation_Manger.overrideClipTimeCounter(_animator_layer_index, clip_name));
        }
        
        public AniProcessCoroutine(Animation_Manger animation_Manger, animator_layer_index animator_layer_index,AnimationClip clip)
        {
            this.animation_Manger = animation_Manger;
            this._animator_layer_index = animator_layer_index;
            this.clip = clip;
        }
        
        public void customCoroutineTrigger_clip()
        {
            if (this.animation_Manger.animprocess != null)
                this.animation_Manger.StopCoroutine(this.animation_Manger.animprocess);
            this.animation_Manger.PlayLayerAnim_clip(_animator_layer_index, clip);
            this.animation_Manger.setAnimationPlayingStep(AnimationPlaying_Step.running);
            this.animation_Manger.animprocess = this.animation_Manger.StartCoroutine(this.animation_Manger.overrideClipTimeCounter(_animator_layer_index, clip));
        }
    }
    
    public void animationCustomCoroutineTrigger(animator_layer_index animator_layer_index, AnimationClip clip)
    {
        _AniProcessCoroutine = new AniProcessCoroutine(this, animator_layer_index, clip);
        _AniProcessCoroutine.customCoroutineTrigger_clip();
    }

    public void animationCustomCoroutineTrigger(animator_layer_index animator_layer_index, string clip_name)
    {
        _AniProcessCoroutine = new AniProcessCoroutine(this, animator_layer_index, clip_name);
        _AniProcessCoroutine.customCoroutineTrigger();
    }

    public AnimationPlaying_Step GetAnimationPlayingStep()
    {
        return Coroutine_Step;
    }

    public void setForShowMode(bool isForShowMode)
    {
        this.isForShowMode = isForShowMode;
    }

    void Awake()
    {
        toRunAniName = null;
        if (Animator == null)
            Animator = gameObject.GetComponent<Animator>();
        if (Animator == null)
            Debug.Log("animator not found");        
    }

    private IDictionary<string , AnimationClip> toLoadAnims;
	private AnimationClip _toUse;
	public AnimationClip tryAnimationClip(string clip_name)
	{
		if (clip_name != null) {
			toLoadAnims.TryGetValue (clip_name, out _toUse);
			if (_toUse != null) {
				return _toUse;
			} else {
                return null;
			}
		} else {
			return null;
		}
	}

    private AnimatorStateInfo _BaseAnimatorStateInfo;
    private AnimatorStateInfo AnimatorStateInfo;
    private AnimatorTransitionInfo _AnimatorTransitionInfo;
    public void PlayLayerAnim(animator_layer_index animator_layer_index, string clip_name)
    {
        //if ((PhotonNetwork.offlineMode && PhotonNetwork.connected) || !PhotonNetwork.connected)
        //{
			this.PlayLayerAnimRPC ((int)animator_layer_index, clip_name);
		//}
		//else
			//photonView.RPC("PlayLayerAnimRPC", PhotonTargets.All, (int)animator_layer_index, clip_name);
    }

    void PlayLayerAnimRPC(int animator_layer_index, string clip_name)
    {
        AnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(animator_layer_index);
        switch (animator_layer_index)
        {
            case 1:
                if (AnimatorStateInfo.IsName("Full Body.null"))
                {
                    if (Animator.GetAnimatorTransitionInfo(animator_layer_index).IsName("Full Body.null -> Full Body.full_body_state1"))
                    {
                        if (clip_name != "" && clip_name != null)
                        {
                            nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                            to_be_override_animation_name = "fullbody_empty2";
                            pre_overrided_anim_name = "fullbody_empty1";
                            trigger_name = "fullbody_trigger2";
                            fullbodylayer_return_trigger_name = "fullbody_return1";
                            break;
                        }
                        else if (clip_name == "" || clip_name == null)
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
                        if (clip_name != "" && clip_name != null)
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
                        if (clip_name != "" && clip_name != null)
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
                        if (clip_name != "" && clip_name != null)
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
                        if (clip_name != "" && clip_name != null)
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
                        if (clip_name != "" && clip_name != null)
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
                        if (clip_name != "" && clip_name != null)
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
                        if (clip_name != "" && clip_name != null)
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

        this.toRunAniName = clip_name;
        the_trigger(toRunAniName, this.doNothingFlag);
        doNothingFlag = false;
    }

    void the_trigger(string clip_name,bool doNothingFlag)
    {
        if (doNothingFlag)
        {
            return;
        }

        if (clip_name != null && clip_name != "")
        {
            if (trigger_name != null)
            {
                if (pre_overrided_anim_name != null)
                {
                    animatorOverride[pre_overrided_anim_name] = tryAnimationClip(current_animation_name); // 关于前动画起始点的问题有待商榷。可能没问题。
                }
                else
                {
                    // 0623重大修改！！！！！！把底下这行给comment out了
                    //myOverrideController[pre_overrided_anim_name] = null;//也就是说从null状态出发的时候
                }
                if (to_be_override_animation_name != null)
                {
                    animatorOverride[to_be_override_animation_name] = tryAnimationClip(clip_name);
                }
                //Animator.runtimeAnimatorController = animatorOverride;
                Animator.SetTrigger(trigger_name);                                    
                if (tryAnimationClip(clip_name) != null)
                {
                    current_animation_name = clip_name;
                }
                else
                {
                    current_animation_name = null;
                }

//                if (trigger_name == "fullbody_trigger1" && 
//                    (nextStateName == "Full Body.full_body_state1 -> Full Body.null" || nextStateName == "Full Body.full_body_state1 -> Full Body.full_body_state2" || nextStateName == "Full Body.full_body_state2 -> Full Body.null"))
//                {
//                    Debug.Log("!!!!!!!!!!!!!!");
//                }
//                if (trigger_name == "fullbody_trigger2" && 
//                    (nextStateName == "Full Body.full_body_state2 -> Full Body.null" || nextStateName == "Full Body.full_body_state2 -> Full Body.full_body_state1" || nextStateName == "Full Body.full_body_state2 -> Full Body.full_body_state1"))
//                {
//                    Debug.Log("!??!?!?!?!?!?!?!");
//                }
//                if (nextStateName == "Full Body.full_body_state2 -> Full Body.null" || nextStateName == "Full Body.full_body_state1 -> Full Body.null")
//                {
//                    Debug.Log("卧槽啊！！！！！");
//                }

                //if (myOverrideController[to_be_override_animation_name].name.GetHashCode() != clip_name.GetHashCode())
                //{
                    //Debug.Log(myOverrideController[to_be_override_animation_name].name);
                    //this.bugEmergerd = true;
                //}
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

public enum animator_layer_index
{
    Base_Layer = 0,
    Full_Body = 1
}

//  private static UnityEditor.Animations.AnimatorState[] GetStateNames(Animator animator)
//  {
//      controller = animator ? animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController : null;
//      return controller == null ? null : controller.layers.SelectMany(l => l.stateMachine.states).Select(s => s.state).ToArray();
//  }

//void Update()
//{
//if (isForShowMode)
//{
//if (this.personal_zhuangbi != null)
//{
//    if (idleCounter < zhuangbiJiange)
//    {
//        idleCounter += Time.deltaTime;
//    }
//    else
//    {
//        idleCounter = 0f;
//        zhuangbiJiange = UnityEngine.Random.Range(5, 15);
//        PlayLayerAnim( animator_layer_index.Full_Body,"zhuangbi");
//        StartCoroutine(wait(tryAnimationClip(current_animation_name).length));
//    }
//}
//return;
//}
//_AnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo((int)animator_layer_index.Full_Body);
//_AnimatorTransitionInfo = Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body);
//clean_null_state1_and_state2();

//724  以下环节去掉了。因为首先这个环节本身貌似存在逻辑问题导致任何时候动作播放时候都执行了，再一个我们也记不请这到底是为了应付哪个bug，而这个bug到底还是不是仍然存在。
//      bool ifTransition = false;
//      if (Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body).IsName("Full Body.null -> Full Body.full_body_state1")
//          ||Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body).IsName("Full Body.full_body_state1 -> Full Body.null")
//          || Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body).IsName("Full Body.full_body_state2 -> Full Body.null"))
//      {
//          ifTransition = true;
//      }
//      if (current_animation_name != null && current_animation_name != "")
//{
//        if (_AnimatorStateInfo.IsName("Full Body.null") && !ifTransition)
//        {
//            Debug.Log("runned:" + current_animation_name);
//this.PlayLayerAnimRPC((int)animator_layer_index.Full_Body, current_animation_name);
//    }
//}
//}

//新的动画资源体系下主要要修改的就是这个环节了。这个环节需要在所有文件夹下分别读取动画文件并且合并为一个大列表
//关于角色动作的个性化问题，我们首先想到的做法就是像个性化特效那种方式一样，在自己的文件夹下寻找信息，找到了就读取
//找不到就用default文件夹下面的。那么这样的话大概方向来说就是本函数搞成一个分阶段分步骤的过程而不是像现在这样一下子在文件夹下把所有片段读取。
//public void preloadAnims()//第一个参数是type，第二个参数是个人文件夹
//{
//       List<UnityEngine.Object> resources = new List<UnityEngine.Object>();
//       List<AnimationClip> to_use_anims = new List<AnimationClip>();
//       toLoadAnims = new Dictionary<string, AnimationClip>();

//       if (personal_path != null && personal_path != "")
//       {
//           List<UnityEngine.Object> personals = Resources.LoadAll(clip_path + "/PERSONALS/" + personal_path, typeof(AnimationClip)).ToList();
//           resources.AddRange(personals);
//           foreach (UnityEngine.Object one in resources)
//           {
//               to_use_anims.Add((AnimationClip)one);
//           }
//           foreach (AnimationClip _AnimationClip in to_use_anims)
//           {
//               if (!toLoadAnims.Keys.Contains(_AnimationClip.name))
//               {
//                   toLoadAnims.Add(new KeyValuePair<string, AnimationClip>(_AnimationClip.name, _AnimationClip));
//               }
//           }
//       }

//       resources = new List<UnityEngine.Object>();
//       List<UnityEngine.Object> G_Attack_State = Resources.LoadAll(clip_path + "/G_Attack_State", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> G_Attack_State_Stay = Resources.LoadAll(clip_path + "/G_Attack_State_Stay", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> G_Attack_State_B = Resources.LoadAll(clip_path + "/G_Attack_State_B", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> G_S = Resources.LoadAll(clip_path + "/G_S", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> GMStates = Resources.LoadAll(clip_path + "/GMStates", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> Gshoot = Resources.LoadAll(clip_path + "/Gshoot", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> Rush = Resources.LoadAll(clip_path + "/Rush", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> StandUp = Resources.LoadAll(clip_path + "/StandUp", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> Hurts = Resources.LoadAll(clip_path + "/Hurts", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> Death = Resources.LoadAll(clip_path + "/Death", typeof(AnimationClip)).ToList();
//       List<UnityEngine.Object> Basic = Resources.LoadAll(clip_path + "/Basic", typeof(AnimationClip)).ToList();

//       resources.AddRange(Basic);
//       resources.AddRange(Hurts);
//       resources.AddRange(Death);
//       resources.AddRange(StandUp);
//       resources.AddRange(Rush);
//       resources.AddRange(Gshoot);
//       resources.AddRange(GMStates);
//       resources.AddRange(G_S);
//       resources.AddRange(G_Attack_State_B);
//       resources.AddRange(G_Attack_State_Stay);
//       resources.AddRange(G_Attack_State);

//       hurtClipNames = new List<string>();
//       foreach(UnityEngine.Object o in Hurts)
//       {
//           hurtClipNames.Add(o.name);
//       }

//       deathClipNames = new List<string>();
//       foreach (UnityEngine.Object o in Death)
//       {
//           deathClipNames.Add(o.name);
//       }

//       to_use_anims = new List<AnimationClip>();
//       foreach (UnityEngine.Object one in resources)
//       {
//           to_use_anims.Add((AnimationClip)one);
//       }

//       foreach (AnimationClip _AnimationClip in to_use_anims)
//       {
//           if (!toLoadAnims.Keys.Contains(_AnimationClip.name))
//           {
//               toLoadAnims.Add(new KeyValuePair<string, AnimationClip>(_AnimationClip.name, _AnimationClip));
//           }
//       }
//}

//  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//  {
//if (PhotonNetwork.connected) 
//{
//  if (stream.isWriting)
//  {
//  }
//  else
//  {
//  }
//}
//}

//private void clean_null_state1_and_state2()
//{
//    if (_AnimatorStateInfo.IsName("Full Body.full_body_state1"))
//    {
//        if (!_AnimatorTransitionInfo.IsName("Full Body.full_body_state1 -> Full Body.full_body_state2")
//            &&
//            !_AnimatorTransitionInfo.IsName("Full Body.full_body_state1 -> Full Body.null"))
//        {
//            if (Animator.GetCurrentAnimatorClipInfo((int)animator_layer_index.Full_Body)[0].clip.name.GetHashCode() == "fullbody_empty1".GetHashCode())
//            {
//                Debug.Log(this.current_animation_name);
//                Animator.SetTrigger("fullbody_return1");
//            }
//        }
//    }
//    if (_AnimatorStateInfo.IsName("Full Body.full_body_state2"))
//    {
//        if (!_AnimatorTransitionInfo.IsName("Full Body.full_body_state2 -> Full Body.full_body_state1")
//            &&
//            !_AnimatorTransitionInfo.IsName("Full Body.full_body_state2 -> Full Body.null")
//           )
//        {
//            if (Animator.GetCurrentAnimatorClipInfo((int)animator_layer_index.Full_Body)[0].clip.name.GetHashCode() == "fullbody_empty2".GetHashCode())
//            {
//                Debug.Log(this.current_animation_name);
//                Animator.SetTrigger("fullbody_return2");
//            }
//        }
//    }
//}

//public bool ifChanceForAPhotonAniTransfer()
//{
//    bool a = Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body).IsName("Full Body.null -> Full Body.full_body_state1");
//    bool b = Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body).IsName("Full Body.full_body_state1 -> Full Body.full_body_state2");
//    bool c = Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body).IsName("Full Body.full_body_state1 -> Full Body.null");
//    bool d = Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body).IsName("Full Body.full_body_state2 -> Full Body.full_body_state1");
//    bool e = Animator.GetAnimatorTransitionInfo((int)animator_layer_index.Full_Body).IsName("Full Body.full_body_state2 -> Full Body.null");

//    if (a || b || c || d || e)
//    {
//        Debug.Log("在迁移");
//        return false;
//    }

//    if (Animator.GetCurrentAnimatorStateInfo((int)animator_layer_index.Full_Body).IsName("Full Body.null"))
//    {
//        Debug.Log("nullstate ，迁移条件应该激活了");
//        return true;
//    }

//    if (Animator.GetCurrentAnimatorStateInfo((int)animator_layer_index.Full_Body).normalizedTime < 0.8f)
//    {
//        Debug.Log("normalizedTime小于0.8 ");
//        return true;
//    }
//    Debug.Log(Animator.GetCurrentAnimatorStateInfo((int)animator_layer_index.Full_Body).normalizedTime);
//    return false;
//}