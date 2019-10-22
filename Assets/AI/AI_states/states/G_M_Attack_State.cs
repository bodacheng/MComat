using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

// This kind of state has to be triggerd on the ground, but doesnt need to be on ground wille running,
// During the state ,colliders of the character heriachy is disabled, same with the gravity 
public class G_M_Attack_State : AI_State {
	private string clip_name;
    private bool keepRotationAdjustment;
    private float RotationAdjustmentTime;
    private float rotate_speed;

    private int _skillEmergentLevel;

    public G_M_Attack_State(string clip_name)
	{
		this.clip_name = clip_name;
        this.behaviorEnterRanges = null;
    }

    public G_M_Attack_State(string clip_name, bool keepRotationAdjustment, float rotate_speed)
	{
		this.clip_name = clip_name;
        this.keepRotationAdjustment = keepRotationAdjustment;
        this.rotate_speed = rotate_speed;
        this.RotationAdjustmentTime = -1;
    }

    public G_M_Attack_State(string clip_name, float RotationAdjustmentTime, float rotate_speed, int skillEmergentLevel)
    {
        this.clip_name = clip_name;
        this.keepRotationAdjustment = false;
        this.rotate_speed = rotate_speed;
        this.RotationAdjustmentTime = RotationAdjustmentTime;
        this._skillEmergentLevel = skillEmergentLevel;
    }

    public override void pre_process_before_enter()
	{
		base.pre_process_before_enter ();
	}

    public override bool enter_condition_priority1()
    {
        if (_skillEmergentLevel == 1)
        {
            return strategic_enter_condition();
        }
        return false;
    }

    public override bool enter_condition_priority2()
    {
        if (_skillEmergentLevel == 2)
        {
            return strategic_enter_condition();
        }
        return false;
    }

    public override bool enter_condition_priority3()
    {
        if (_skillEmergentLevel == 3)
        {
            return strategic_enter_condition();
        }
        return false;
    }

    public bool strategic_enter_condition()
    {
        if (this._AIStateRunner.getNowState() != null &&
            (this._AIStateRunner.getNowState().StateType == stateType.GI || 
            this._AIStateRunner.getNowState().StateType == stateType.GR || 
            this._AIStateRunner.getNowState().StateType == stateType.GM || 
            this._AIStateRunner.getNowState().StateType == stateType.AC) && this.Sensor.EnemyAndTeammateBetweenMeAndEnemy() == null)
            return this.checkToEnemyDisEnterCondition(RangePlusOne(this.behaviorEnterRanges));
        if (this.Sensor.EnemyAndTeammateBetweenMeAndEnemy() == null)
            return (this.checkToEnemyDisEnterCondition(this.behaviorEnterRanges));
        return false;
    }

    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        this.Animation_Manger.Animator.SetTrigger("face_reset");
        this.Animation_Manger.Animator.SetTrigger("confident");
        
        this._DATA_CENTER.setGravitySwitch(true);
        this._Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turn_off_flag();
        _SkillCancelFlag.turnRotationAdjustmentStartFlag(1);
        lastFrameRotateAngle = 0;
        thisFrameRotateAngle = 0;
        this.personality_Events.CloseAllPersonalityEffects();
        this._Rigidbody.velocity = Vector3.zero;
        
        Collider C = Sensor.getClosestColliderInSensorRange(true,true,true);
        if (C != null)
            this.RotateToTarget(C.transform.position, 10000f, true);
 
        _FightAttriCalReference.ReturnDamageList(DamageType.stagger).Clear();
		_Animator.applyRootMotion = true;
        //this.AI_DATA_CENTER.switchToSmoothPhysicMaterial();
        Animation_Manger.animationTrigger(clip_name);
	}

	public override bool capacity_exit_condition()
	{
        if (Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over)
			return true;
		else
			return false;
	}

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        this._DATA_CENTER.setGravitySwitch(true);
        this._BO_Ani_E.CloseEffectsOnBodyParts();
		_Animator.applyRootMotion = false;
	}

    private Vector3 rotateTarget;
	public override void _State_FixedUpdate1() 
	{
        if (_FightAttriCalReference.ReturnDamageList(DamageType.stagger).Count > 0)
        {
            //this._Animator.applyRootMotion = false;
            //this._Rigidbody.velocity = BS_Main_Health.returnDamageList(damageType.stagger)[0].force_direction.normalized * 4f;

            //this.gameObject.transform.position += BS_Main_Health.returnDamageList(damageType.stagger)[0].force_direction.normalized * 1f;
            _FightAttriCalReference.ReturnDamageList(DamageType.stagger).Clear();
        }

        Collider C = Sensor.getClosestColliderInSensorRange(true,true,true);
        if (C!=null)
        {
            rotateTarget = C.transform.position;
            singleDirectionRotateProcess(rotateTarget);                  
        }
        //_Rigidbody.velocity = new Vector3(0, 0, 0); //开启了的话相当于所向无敌
	}

    private float lastFrameRotateAngle = 0;
    private float thisFrameRotateAngle = 0;
    private float ji = 0f;
    void singleDirectionRotateProcess(Vector3 P)
    {
        //底下这个是说，攻击状态里角色在一个1f周期里有0.3f时长会调整方向，但是在这0.3f时间段里，如果产生了旋转不定向(比如已经转到目标)，那么转向就会提前结束。
        if (_SkillCancelFlag.getRotationAdjustmentStartFlag() || keepRotationAdjustment)
        {
            thisFrameRotateAngle = this.RotateToTarget(P, 1f, true);
            ji = thisFrameRotateAngle * lastFrameRotateAngle;
            if (ji > 0)//同向
            {
                lastFrameRotateAngle = thisFrameRotateAngle;
            }
            else if (ji < 0)//反向
            {
                _SkillCancelFlag.turnRotationAdjustmentStartFlag(0);
            }
            else
            {//刚开始计
                lastFrameRotateAngle = thisFrameRotateAngle;
            }
        }
    }
}
