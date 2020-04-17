using UnityEngine;
using Soul;
using Skill;

//本状态是最复杂的一个攻击种类状态，牵扯到攻击前冲刺
// 在3月21日我们对这个状态进行了进一步改进，现在针对冲刺阶段本状态有以下机制
// 1.基于位置计算位移速度，如果该速度低于0.2f则判断为被什么阻挡，停止冲刺
// 2.在冲刺阶段临时将自身质量调整为0.1从而避免冲刺到敌人身边还不停的情况下对敌人进行推挤
// 3.冲刺阶段将锁定目标敌人调整方向
// 以以上改动为基础，角色在设置方面增加了以下注意点：
// AttackRangeMarker组件最好是细长的capsulecollider，并且可以依据角色的体型尽可能的贴身（细），从而被攻击时敌人会冲刺到尽可能近的位置，不至于一些短手技能打不到
// 并且留下了一些问题：必须重新权衡此类攻击的AI进入范围，对整个系统的距离分段也要重新衡量，以及本状态的进入冲刺距离也都要重新仔细考虑。

public class G_Attack_State : Behavior {
    readonly string clip_name;
    readonly string dash_clip_name;
    readonly int _skillEmergentLevel;
    readonly bool isEventAttackLaunchState;
    readonly bool isEventAttackEndState;
    readonly float rushSpeed;
    readonly float approcahingSpeed;
    readonly float maxRushTime;
    float rush_time_counter;
    Phase _phase;
    UnityEngine.Events.UnityAction rushstart;
    UnityEngine.Events.UnityAction rushend;
    CustomCoroutine rushCoroutine;

    enum Phase
    {
        noRushState = 0,
        farFromReach = 1,
        needToRush = 2,
        reached = 3,
        reachedFromThebeginning = 4
    }

    #region Constructor
    public G_Attack_State(string clip_name)
	{
		this.clip_name = clip_name;
    }

    public G_Attack_State(string dash_clip_name, float rushSpeed, float maxRushTime, float approachingSpeed, string clip_name)
    {
        this.rushSpeed = rushSpeed;
        this.maxRushTime = maxRushTime;
        approcahingSpeed = approachingSpeed;
        this.clip_name = clip_name;
        this.dash_clip_name = dash_clip_name;
    }

    public G_Attack_State(string dash_clip_name,float rushSpeed, float maxRushTime, string clip_name, bool EventLauncher_Or_Ender)
	{
        this.maxRushTime = maxRushTime;
        this.dash_clip_name = dash_clip_name;
        this.rushSpeed = rushSpeed;
		this.clip_name = clip_name;
        isEventAttackLaunchState = EventLauncher_Or_Ender;
        isEventAttackEndState = !EventLauncher_Or_Ender;
    }

    public G_Attack_State(string clip_name, bool EventLauncher_Or_Ender)
    {
        this.clip_name = clip_name;
        isEventAttackLaunchState = EventLauncher_Or_Ender;
        isEventAttackEndState = !EventLauncher_Or_Ender;
    }
    #endregion
    
    #region Capacity Enter Exit
    public override bool Capacity_Exit_Condition()
    {
        return AnimationCasualFinishedFlag() && this.Animation_Manger._toUse.name == clip_name;
    }
    
    //public override bool Capacity_enter_condition()
    //{
    //    return base.Capacity_enter_condition() && !_Animator.GetBool("in_transition");
    //}
    #endregion
    
    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
        rushstart = () =>
        {
            _ResistanceManager.Resistance.Value +=1;
        };
        rushend = () =>
        {
            _ResistanceManager.Resistance.Value -=1;
        };
        rushCoroutine = new CustomCoroutine(rushstart, 5f, rushend);
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _Weapon_Animation_Events.ClearMarkerManagers();
        _Animator.applyRootMotion = false;
        personality_Events.CloseAllPersonalityEffects();
        _BuffsRunner.EndSubCoroutineOfState(rushCoroutine);//冲刺阶段有可能没有正常结束就被强制离开当前技能状态
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
        if (isEventAttackLaunchState)
        {
            if (_FightAttriCalReference != null)
            {
                _FightAttriCalReference.ReturnApprovedEventAttackAttempts().Clear();
            }
        }
        if (isEventAttackEndState)
            EventAttackEnderProcess();
    }

    Collider collider;
    public override void AI_State_enter()
	{
        base.AI_State_enter();
        collider = null;
        //Animation_Manger.Animator.SetTrigger("face_reset");
        //Animation_Manger.Animator.SetTrigger("confident");
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        if (StateType == BehaviorType.GR)
            _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
        if (StateType == BehaviorType.GI)
            _SkillCancelFlag.TurnRotationAdjustmentStartFlagWithoutstepfoward(1);
        _Rigidbody.velocity = Vector3.zero;
        rush_time_counter = 0f;
        _Animator.applyRootMotion = true;
        Sensor.ContinuousDetectionStart(2);
        Sensor.GetEnemiesByDistance(true);
        if (Sensor.GetEnemiesByDistance(false).Count == 0)
        {
            //一般来说下面这些情况不跑？
            _phase = Phase.noRushState;
            Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
            return;
        }

        collider = Sensor.GetClosestEnemyColliderInSensorRange();
        if (collider == null)
        {
            Animation_Manger.AnimationTrigger(clip_name, true, 0.05f);
            _phase = Phase.farFromReach;
            return;
        }
        float distance = Vector3.Distance(gameObject.transform.position, collider.transform.position);
        if (distance < 5f)//内环检测结果
        {
            _phase = Phase.reachedFromThebeginning;
            Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
            if (Sensor.GetEnemiesByDistance(false).Count > 0)
            {
                if (Sensor.GetEnemiesByDistance(false)[0] != null)
                {
                    RotateToTarget_Tween(Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f,true);
                }
            }
            return;
        }
        
        if (distance < 10f)
        {
            if (Sensor.GetEnemiesByDistance(false).Count > 0)
            {
                if (Sensor.GetEnemiesByDistance(false)[0] != null)
                {
                    RotateToTarget_Tween(Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f, true);
                }
            }
            //也就是说能不能可不可能发生冲刺，完全取决于上一个状态了。如果我们想完全关闭这个功能，那确保所有状态nextAttackStateCanRushFirst是fale就行
            if (_AIStateRunner.GetLastState() != null && _AIStateRunner.GetLastState().nextAttackStateCanRushFirst && StateType == BehaviorType.GR)
            {
                _phase = Phase.needToRush;
                if (Animation_Manger.TryAnimationClip(dash_clip_name) != null)
                    Animation_Manger.AnimationTrigger(dash_clip_name,true,0.05f);
                else
                {
                    Debug.Log("here:"+ clip_name);
                    Animation_Manger.PlayLayerAnim(null,true,0f);
                }
                _BuffsRunner.RunSubCoroutineOfState(rushCoroutine);
            }
            else
            {
                _phase = Phase.reachedFromThebeginning;//这个环节最绕脑子，大概指的是如果外环也有敌人，就当“已经到达”。但其实从出发点将，一般的普通近距离攻击在中距离下也不会触发才对
                Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
                return;
            }
        }

        Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
        _phase = Phase.farFromReach;
        return;
    }
       
    public override void _State_FixedUpdate1() 
	{
        switch (_phase)
        {
            case Phase.noRushState://这里面可能还有一些远距离攻击什么的。哦。。。除非都没敌人了现在才可能会进入noRushState
                break;
            case Phase.farFromReach:
                break;
            case Phase.needToRush://也就是说冲刺中。
                if (collider == null)
                {
                    _Rigidbody.velocity = Vector3.zero;
                    _phase = Phase.reached;
                }
                else
                {
                    Move(collider.transform.position - gameObject.transform.position, rushSpeed, true);
                    if (Vector3.Distance(gameObject.transform.position, collider.transform.position) < 2f)
                    {
                        _phase = Phase.reached;
                    }
                    if (_phase == Phase.reached)
                    {
                        Animation_Manger.AnimationTrigger(clip_name, true, 0.05f);
                        _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
                        _Rigidbody.velocity = Vector3.zero;
                        Sensor.GetEnemiesByDistance(true);
                        _BuffsRunner.EndSubCoroutineOfState(rushCoroutine);
                        if (Sensor.GetEnemiesByDistance(false).Count > 0)
                        {
                            if (Sensor.GetEnemiesByDistance(false)[0] != null)
                            {
                                RotateToTarget_Tween(Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f, true);
                            }
                        }
                    }
                }
                if (rush_time_counter > maxRushTime)
                {
                    _phase = Phase.reached;
                }
                if (_phase == Phase.reached)
                {
                    Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
                    _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
                    _Rigidbody.velocity = Vector3.zero;
                    Sensor.OneRoundDetectionStart(5);
                    _BuffsRunner.EndSubCoroutineOfState(rushCoroutine);
                }
                break;
            case Phase.reached:
                if (Sensor.GetEnemiesByDistance(false).Count > 0)
                {
                    if (Sensor.GetEnemiesByDistance(false)[0] != null)
                        AttackApprocach(Sensor.GetEnemiesByDistance(false)[0].transform.position, approcahingSpeed);
                }
                break;
            case Phase.reachedFromThebeginning://reachedFromThebeginning现在其实是两种情况：1. 冲刺状态一开始内环就有敌人 2.非冲刺状态一开始外环有敌人
                if (Sensor.GetEnemiesByDistance(false).Count > 0)
                {
                    if (Sensor.GetEnemiesByDistance(false)[0] != null)
                    {
                        AttackApprocach(Sensor.GetEnemiesByDistance(false)[0].transform.position, approcahingSpeed);
                    }
                }
                break;
        }
        //if (isEventAttackLaunchState)
        //{
        //    this.DetectApprovedEventAttack();
        //}
    }
}
