using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Soul;

//本状态是最复杂的一个攻击种类状态，牵扯到攻击前冲刺
// 在3月21日我们对这个状态进行了进一步改进，现在针对冲刺阶段本状态有以下机制
// 1.基于位置计算位移速度，如果该速度低于0.2f则判断为被什么阻挡，停止冲刺
// 2.在冲刺阶段临时将自身质量调整为0.1从而避免冲刺到敌人身边还不停的情况下对敌人进行推挤
// 3.冲刺阶段将锁定目标敌人调整方向
// 以以上改动为基础，角色在设置方面增加了以下注意点：
// AttackRangeMarker组件最好是细长的capsulecollider，并且可以依据角色的体型尽可能的贴身（细），从而被攻击时敌人会冲刺到尽可能近的位置，不至于一些短手技能打不到

// 并且留下了一些问题：必须重新权衡此类攻击的AI进入范围，对整个系统的距离分段也要重新衡量，以及本状态的进入冲刺距离也都要重新仔细考虑。

public partial class G_Attack_State : AI_State {

	private string clip_name;
    private string dash_clip_name;
    private int _skillEmergentLevel;

    private bool isEventAttackLaunchState;
    private bool isEventAttackEndState;
    private float rushSpeed;
    private float approcahingSpeed;

    private float lastFrameRotateAngle;
    private float thisFrameRotateAngle;

    private float maxRushTime, rush_time_counter;
    private Transform rushingToTarget;
    private phase _phase;

    private UnityEngine.Events.UnityAction rushstart;
    private UnityEngine.Events.UnityAction rushend;
    private customCoroutine rushCoroutine;

    enum phase
    {
        noRushState = 0,
        farFromReach = 1,
        needToRush = 2,
        reached = 3,
        reachedFromThebeginning = 4
    }

    public G_Attack_State(string clip_name)
	{
		this.clip_name = clip_name;
        this.behaviorEnterRanges = null;
    }

    public G_Attack_State(string dash_clip_name, float rushSpeed, float maxRushTime,
                          float approachingSpeed,
                          string clip_name,
                          int skillEmergentLevel)
    {
        this.rushSpeed = rushSpeed;
        this.maxRushTime = maxRushTime;
        this.approcahingSpeed = approachingSpeed;
        this.clip_name = clip_name;
        this.dash_clip_name = dash_clip_name;
        this._skillEmergentLevel = skillEmergentLevel;
    }

    public G_Attack_State(string dash_clip_name,float rushSpeed, float maxRushTime, string clip_name, bool EventLauncher_Or_Ender)
	{
        this.maxRushTime = maxRushTime;
        this.dash_clip_name = dash_clip_name;
        this.rushSpeed = rushSpeed;
		this.clip_name = clip_name;
        this.isEventAttackLaunchState = EventLauncher_Or_Ender;
        this.isEventAttackEndState = !EventLauncher_Or_Ender;
    }

    public G_Attack_State(string clip_name, bool EventLauncher_Or_Ender)
    {
        this.clip_name = clip_name;
        this.isEventAttackLaunchState = EventLauncher_Or_Ender;
        this.isEventAttackEndState = !EventLauncher_Or_Ender;
    }

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
        rushstart = () =>
        {
            this._ResistanceManager.Resistance.Value +=1;
        };
        rushend = () =>
        {
            this._ResistanceManager.Resistance.Value -=1;
        };
        rushCoroutine = new customCoroutine(rushstart, 5f, rushend);
    }

    public override bool Enter_condition_priority1()
    {
        return _skillEmergentLevel == 1 ? strategic_enter_condition() : false;
    }

    public override bool Enter_condition_priority2()
	{
        return _skillEmergentLevel == 2 ? strategic_enter_condition() : false;
    }

    public override bool Enter_condition_priority3()
    {
        return _skillEmergentLevel == 3 ? strategic_enter_condition() : false;
    }

    public bool strategic_enter_condition()
    {
        if (this.Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            return false;

        if (this._AIStateRunner.GetNowState() != null)
        {
            if (this._AIStateRunner.GetNowState().nextAttackStateCanRushFirst == true)
                return this.CheckToEnemyDisEnterCondition(this.InnerAndMidAndFarRanges);

            //if (this._AIStateRunner.getNowState().StateType == stateType.GR ||
                //this._AIStateRunner.getNowState().StateType == stateType.GM ||
                //this._AIStateRunner.getNowState().StateType == stateType.GI)
                //return this.checkToEnemyDisEnterCondition(RangePlusOne(this.behaviorEnterRanges));
        }
        return this.CheckToEnemyDisEnterCondition(this.behaviorEnterRanges);
    }

    public override void AI_State_enter()
	{
        base.AI_State_enter();
        this.Animation_Manger.Animator.SetTrigger("face_reset");
        this.Animation_Manger.Animator.SetTrigger("confident");
        this._Animator.SetFloat("speed", 0f);
        this._DATA_CENTER.SetUsingGravity(true);
        _SkillCancelFlag.turn_off_flag();
        if (this.StateType == stateType.GR)
            _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
        if (this.StateType == stateType.GI)
            _SkillCancelFlag.TurnRotationAdjustmentStartFlagWithoutstepfoward(1);
            
        this._Rigidbody.velocity = Vector3.zero;
        lastFrameRotateAngle = 0;
        thisFrameRotateAngle = 0;

        this.rush_time_counter = 0f;
        _Animator.applyRootMotion = true;

        Sensor.getEnemiesByDistance(true);//这里算一下，下面的全是false。但要注意中途这个结果变为null
        if (Sensor.getEnemiesByDistance(false).Count == 0)
        {
            //一般来说下面这些情况不跑？
            _phase = phase.noRushState;
            Animation_Manger.AnimationTrigger(clip_name);
            return;
        }

        if (Sensor.getInnerEnemiesColliders().Count > 0)//内环检测结果
        {
            _phase = phase.reachedFromThebeginning;
            Animation_Manger.AnimationTrigger(clip_name);
            return;
        }
        
        if (Sensor.getClosestColliderInSensorRange(false,true,true) != null)
        {
            rushingToTarget = Sensor.getClosestColliderInSensorRange(false, true, true).transform;
        }
        if (rushingToTarget != null)
        {
            //也就是说能不能可不可能发生冲刺，完全取决于上一个状态了。如果我们想完全关闭这个功能，那确保所有状态nextAttackStateCanRushFirst是fale就行
            if (this._AIStateRunner.GetLastState() != null && this._AIStateRunner.GetLastState().nextAttackStateCanRushFirst && this.StateType == stateType.GR)
            {
                _phase = phase.needToRush;
                lastFrameRotateAngle = 0;
                thisFrameRotateAngle = 0;
                //this.AI_DATA_CENTER.switchToSmoothPhysicMaterial();
                if (Animation_Manger.TryAnimationClip(dash_clip_name) != null)
                    Animation_Manger.PlayLayerAnim(dash_clip_name);
                else
                {
                    Debug.Log("here:"+ clip_name);
                    Animation_Manger.PlayLayerAnim(null);
                }

                this._BuffsRunner.runSubCoroutineOfState(rushCoroutine);
                return;
            }
            else
            {
                _phase = phase.reachedFromThebeginning;//这个环节最绕脑子，大概指的是如果外环也有敌人，就当“已经到达”。但其实从出发点将，一般的普通近距离攻击在中距离下也不会触发才对
                Animation_Manger.AnimationTrigger(clip_name);
                return;
            }
        }

        if (Sensor.getClosestColliderInSensorRange(true,true,true) == null)//外环检测结果.走到这里就是说，如果内外环都没敌人
        {
            Animation_Manger.AnimationTrigger(clip_name);
            _phase = phase.farFromReach;
            return;
        }
    }

	public override bool Capacity_exit_condition()
	{
        if (Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over)
            return true;
        else
            return false;
	}

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        this._DATA_CENTER.SetUsingGravity(true);
        this.rushingToTarget = null;
        this._Weapon_Animation_Events.ClearMarkerManagers();
        _Animator.applyRootMotion = false;
        this.personality_Events.CloseAllPersonalityEffects();
        this._BuffsRunner.endSubCoroutineOfState(rushCoroutine);//冲刺阶段有可能没有正常结束就被强制离开当前技能状态
        this._BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts();
        if (isEventAttackLaunchState)
        {
			if (_FightAttriCalReference != null)
            {
				_FightAttriCalReference.ReturnApprovedEventAttackAttempts().Clear();
            }
        }
        if (isEventAttackEndState)
            this.EventAttackEnderProcess();
	}

    public override void _State_FixedUpdate1() 
	{
        switch (_phase)
        {
            case phase.noRushState://这里面可能还有一些远距离攻击什么的。哦。。。除非都没敌人了现在才可能会进入noRushState
                break;
            case phase.farFromReach:
                break;
            case phase.needToRush://也就是说冲刺中。在这个环节我们之所以没看到扭转方向的处理是因为在fixedUpdate里针对这个阶段使用this.RotateToVelocity(10f, true);
                if (rushingToTarget == null)
                {
                    this._Rigidbody.velocity = Vector3.zero;
                    _phase = phase.reached;
                }
                if (Sensor.getInnerEnemiesColliders().Count > 0 || rush_time_counter > maxRushTime)
                {
                    _phase = phase.reached;
                }
                if (_phase == phase.reached)
                {
                    Animation_Manger.AnimationTrigger(clip_name);
                    _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
                    lastFrameRotateAngle = 0;
                    thisFrameRotateAngle = 0;
                    this._Rigidbody.velocity = Vector3.zero;
                    this.Sensor.OneRoundDetectionStart(5);
                    this._BuffsRunner.endSubCoroutineOfState(rushCoroutine);
                }
                break;
            case phase.reached:
                break;
            case phase.reachedFromThebeginning://reachedFromThebeginning现在其实是两种情况：1. 冲刺状态一开始内环就有敌人 2.非冲刺状态一开始外环有敌人
                break;
            default:
                break;
        }
        //if (isEventAttackLaunchState)
        //{
        //    this.DetectApprovedEventAttack();
        //}
    }
}
