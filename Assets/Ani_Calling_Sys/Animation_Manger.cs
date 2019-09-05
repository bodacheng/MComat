using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// 2019 6.1:
// “动画的终结”现在我们改由动画片段末尾的“ThisIsEndOfAnimation”来判定。
// 但是产生了一个严重的问题，就是在都有ThisIsEndOfAnimation的两个动画片段迁移过程中，
// 迁移过程中前一个片段的ThisIsEndOfAnimation也会启动的问题（迁移区间内的前后片段包含动画事件都会触发，我们现在才知道）
// 为了解决这个事情我们原本是靠ThisIsEndOfAnimation事件的string参数来确定函数是不是对应正确的动画片段，而不是误终结了迁移目标的动画，
// 但我们发现这个方法没法解决前后动画一样的情况。尤其比如说闪避技能连续发动两次
// 我们发现了Unity animator的一个bug。拿闪避技能来说，依据游戏企划闪避无法靠状态取消来进行第二次闪避，
// 我们确定是前一个闪避动作结束后如果连按对应按钮的话才再次开始进行闪避，但因为仍然形成了连续，animator的全身层那边，full_body_state1仍然是直接向full_body_state2进行了迁移。
// 然后就是严重bug的所在：闪避动画末尾的ThisIsEndOfAnimation函数居然莫名其妙的触发了两次。
// 我想原因应该是Unity内部在处理迁移的时候有一定的逻辑问题，尤其我们让同一个动画迁移向同一个动画。。
// 为了解决这个问题，我们在ThisIsEndOfAnimation函数内加入了getOnAniTransitionFlag判断，来看每次ThisIsEndOfAnimation触发的时候，是不是在full_body_state1与full_body_state2之间迁移。
// 如果是的话，那么这次ThisIsEndOfAnimation不会把动画机状态判断为“终止”，因为实际情况是新的动画已经触发了。
// 注意看所有的状态因动画终止而回归idle，都是从full_body_state1或full_body_state2往null回归的。
// 只有在上面两种情况下ThisIsEndOfAnimation才应该起到应该有的作用。从而当下来看，理论上当前做法不会有什么问题。

public partial class Animation_Manger : MonoBehaviour
{
    // 大体的image就是能够直接通过动画文件名来随时决定animator当中各个层动画的播放。而不再需要复杂的animator。。
    // 然而这和unity的这个animator系统的初衷产生矛盾
    // 整个系统建立在animator本身这样的前提下：
    //1. animator在状态迁移过程中，“当前状态”为迁移出发状态
    //2. 在迁移过程中，如果激发了迁移终点状态向另一状态（或返回至出发状态）迁移的条件，（起码我们知道trigger满足这点），则状态机会在按原节奏到达终点状态后，
	//再去向第三者状态迁移，之前的迁移过程并不会受干扰，后触发的迁移条件也并不会被遗忘，一切会按顺序进行
    //从而也就是说对最后要触发那个状态来说，从条件激活到开始进入会发生一点延迟。

    public Animator Animator;
    public AnimationPlaying_Step Coroutine_Step = AnimationPlaying_Step.unstarted;
    private AnimatorOverrideController animatorOverride;
    private bool doNothingFlag;
    private string toRunAniName;
    private string fullbodylayer_return_trigger_name;
    private string to_be_override_animation_name;
    private string trigger_name;
    private string pre_overrided_anim_name;
    private IDictionary<string , AnimationClip> toLoadAnims;
    public AnimationClip _toUse;
    public float animationcounter = 0f;
    
    public bool getOnAniTransitionFlag()
    {
        return Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state1 -> Full Body.full_body_state2") ||
            Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state2 -> Full Body.full_body_state1");
    }

    void Update()
    {
        if (_toUse != null)
            animationcounter += Time.deltaTime;
    }

    public void setAnimationPlayingStep(AnimationPlaying_Step _step)
    {
        this.Coroutine_Step = _step;
    }

    public void animationTrigger(AnimationClip clip)
    {
        this.PlayLayerAnim_clip(clip);
        this.setAnimationPlayingStep(AnimationPlaying_Step.running);
    }

    public void animationTrigger(string clip_name)
    {
        this.PlayLayerAnim(clip_name);
        this.setAnimationPlayingStep(AnimationPlaying_Step.running);
    }

    public AnimationPlaying_Step GetAnimationPlayingStep()
    {
        return Coroutine_Step;
    }

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
    public void PlayLayerAnim(string clip_name)
    {
        animationcounter = 0;
        if (clip_name == null)
            _toUse = null;
        AnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(1);
        if (AnimatorStateInfo.IsName("Full Body.null"))
        {
            if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.null -> Full Body.full_body_state1"))
            {
                if (clip_name != "" && clip_name != null)
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                    to_be_override_animation_name = "fullbody_empty2";
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = "fullbody_trigger2";
                    fullbodylayer_return_trigger_name = "fullbody_return1";
                }
                else if (clip_name == "" || clip_name == null)
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = null;
                    fullbodylayer_return_trigger_name = "fullbody_return1";
                }
            }
            else
            {
                //nextStateName = "Full Body.null -> Full Body.full_body_state1";
                if (clip_name != "" && clip_name != null)
                {
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = null;
                    trigger_name = "fullbody_trigger1";
                    fullbodylayer_return_trigger_name = null;
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
                if (clip_name != "" && clip_name != null)
                {
                    //nextStateName = "Full Body.full_body_state2 -> Full Body.full_body_state1";
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = "fullbody_empty2";
                    trigger_name = "fullbody_trigger1";
                    fullbodylayer_return_trigger_name = "fullbody_return2";
                }
                else
                {
                    //nextStateName = "Full Body.full_body_state2 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty2";
                    trigger_name = null;
                    fullbodylayer_return_trigger_name = "fullbody_return2";
                }
            }
            else if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state1 -> Full Body.null"))
            {
                if (clip_name != "" && clip_name != null)
                {
                    //nextStateName = "Full Body.null -> Full Body.full_body_state1";
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = null;
                    trigger_name = "fullbody_trigger1";
                    fullbodylayer_return_trigger_name = null;
                }
                else
                {
                    doNothingFlag = true;
                }
            }
            else
            {
                if (clip_name != "" && clip_name != null)
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                    to_be_override_animation_name = "fullbody_empty2";
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = "fullbody_trigger2";
                    fullbodylayer_return_trigger_name = "fullbody_return1";
                }else{
                    // 0623重大修改！！！！！！
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = null;
                    fullbodylayer_return_trigger_name = "fullbody_return1";
                }
            }
        }
        if (AnimatorStateInfo.IsName("Full Body.full_body_state2"))
        {
            if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state2 -> Full Body.full_body_state1"))
            {
                if (clip_name != "" && clip_name != null)
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.full_body_state2";
                    to_be_override_animation_name = "fullbody_empty2";
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = "fullbody_trigger2";
                    fullbodylayer_return_trigger_name = "fullbody_return1";
                }
                else
                {
                    //nextStateName = "Full Body.full_body_state1 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty1";
                    trigger_name = null;
                    fullbodylayer_return_trigger_name = "fullbody_return1";
                }
            }
            else if (Animator.GetAnimatorTransitionInfo(1).IsName("Full Body.full_body_state2 -> Full Body.null"))
            {
                if (clip_name != "" && clip_name != null)
                {
                    //nextStateName = "Full Body.null -> Full Body.full_body_state1";
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = null;
                    trigger_name = "fullbody_trigger1";
                    fullbodylayer_return_trigger_name = null;
                }
                else
                {
                    doNothingFlag = true;
                }
            }
            else
            {
                if (clip_name != "" && clip_name != null)
                {
                    //nextStateName = "Full Body.full_body_state2 -> Full Body.full_body_state1";
                    to_be_override_animation_name = "fullbody_empty1";
                    pre_overrided_anim_name = "fullbody_empty2";
                    trigger_name = "fullbody_trigger1";
                    fullbodylayer_return_trigger_name = "fullbody_return2";
                }else{
                    // 0623重大修改！！！！！！
                    //nextStateName = "Full Body.full_body_state2 -> Full Body.null";
                    to_be_override_animation_name = null;
                    pre_overrided_anim_name = "fullbody_empty2";
                    trigger_name = null;
                    fullbodylayer_return_trigger_name = "fullbody_return2";
                }
            }
        }

        this.toRunAniName = clip_name;
        the_trigger(toRunAniName, this.doNothingFlag);
        doNothingFlag = false;
    }

    void the_trigger(string clip_name,bool _doNothingFlag)
    {
        if (_doNothingFlag)
            return;

        if (clip_name != null && clip_name != "")
        {
            if (trigger_name != null)
            {
                 // 2019.6.1重大修改。
                //if (pre_overrided_anim_name != null)
                    //animatorOverride[pre_overrided_anim_name] = currentAnimation;
                //else
                    // 0623重大修改！！！！！！把底下这行给comment out了
                    //myOverrideController[pre_overrided_anim_name] = null;//也就是说从null状态出发的时候
                
                if (to_be_override_animation_name != null)
                {
                    animatorOverride[to_be_override_animation_name] = tryAnimationClip(clip_name);
                }
                //Animator.runtimeAnimatorController = animatorOverride;
                Animator.SetTrigger(trigger_name);

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
        }
    }
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