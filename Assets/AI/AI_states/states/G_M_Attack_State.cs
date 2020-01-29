using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

// This kind of state has to be triggerd on the ground, but doesnt need to be on ground wille running,
// During the state ,colliders of the character heriachy is disabled, same with the gravity 
public class G_M_Attack_State : Behavior {
    readonly string clip_name;
    readonly bool keepRotationAdjustment;
    readonly float RotationAdjustmentTime;
    readonly float rotate_speed;
    readonly int _skillEmergentLevel;

    public G_M_Attack_State(string clip_name)
	{
		this.clip_name = clip_name;
        behaviorEnterRanges = null;
    }

    public G_M_Attack_State(string clip_name, bool keepRotationAdjustment, float rotate_speed)
	{
		this.clip_name = clip_name;
        this.keepRotationAdjustment = keepRotationAdjustment;
        this.rotate_speed = rotate_speed;
        RotationAdjustmentTime = -1;
    }

    public G_M_Attack_State(string clip_name, float RotationAdjustmentTime, float rotate_speed)
    {
        this.clip_name = clip_name;
        keepRotationAdjustment = false;
        this.rotate_speed = rotate_speed;
        this.RotationAdjustmentTime = RotationAdjustmentTime;
    }

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
	}

    public bool Strategic_enter_condition()
    {
        return _AIStateRunner.GetNowState() != null &&
            (_AIStateRunner.GetNowState().StateType == BehaviorType.GI ||
            _AIStateRunner.GetNowState().StateType == BehaviorType.GR ||
            _AIStateRunner.GetNowState().StateType == BehaviorType.GM ||
            _AIStateRunner.GetNowState().StateType == BehaviorType.AC) && Sensor.EnemyAndTeammateBetweenMeAndEnemy() == null
            ? CheckToEnemyDisEnterCondition(RangePlusOne(behaviorEnterRanges))
            : Sensor.EnemyAndTeammateBetweenMeAndEnemy() == null && CheckToEnemyDisEnterCondition(behaviorEnterRanges);
    }

    Collider C;
    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        Animation_Manger.Animator.SetTrigger("face_reset");
        Animation_Manger.Animator.SetTrigger("confident");
        _Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
        lastFrameRotateAngle = 0;
        thisFrameRotateAngle = 0;
        personality_Events.CloseAllPersonalityEffects();
        _Rigidbody.velocity = Vector3.zero;
        C = Sensor.GetClosestColliderInSensorRange(true,true,true);
        if (C != null)
            RotateToTarget(C.transform.position, 1f, true);
		_Animator.applyRootMotion = true;
        Animation_Manger.AnimationTrigger(clip_name);
	}

	public override bool Capacity_Exit_Condition()
	{
        return !_Animator.GetCurrentAnimatorStateInfo(1).IsName("Full Body.null")
            && _Animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1f
            && _Animator.GetNextAnimatorClipInfoCount(1) == 0;
    }

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(false);
		_Animator.applyRootMotion = false;
	}

    Vector3 rotateTarget;
	public override void _State_FixedUpdate1() 
	{
        C = Sensor.GetClosestColliderInSensorRange(true,true,true);
        if (C!=null)
        {
            rotateTarget = C.transform.position;
            SingleDirectionRotateProcess(rotateTarget);                  
        }
        //_Rigidbody.velocity = new Vector3(0, 0, 0); //开启了的话相当于所向无敌
	}

    float lastFrameRotateAngle;
    float thisFrameRotateAngle;
    float ji;
    void SingleDirectionRotateProcess(Vector3 P)
    {
        //底下这个是说，攻击状态里角色在一个1f周期里有0.3f时长会调整方向，但是在这0.3f时间段里，如果产生了旋转不定向(比如已经转到目标)，那么转向就会提前结束。
        if (_SkillCancelFlag.hiddenMethods.GetRotationAdjustmentStartFlag() || keepRotationAdjustment)
        {
            thisFrameRotateAngle = this.RotateToTarget(P, 1f, true);
            ji = thisFrameRotateAngle * lastFrameRotateAngle;
            if (ji > 0)//同向
            {
                lastFrameRotateAngle = thisFrameRotateAngle;
            }
            else if (ji < 0)//反向
            {
                _SkillCancelFlag.TurnRotationAdjustmentStartFlag(0);
            }
            else
            {//刚开始计
                lastFrameRotateAngle = thisFrameRotateAngle;
            }
        }
    }
}
