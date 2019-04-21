using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_State : AI_State {
	private string clip_name;
    private bool keepRotationAdjustment;
    private float RotationAdjustmentTime;
    private float rotate_speed;
    private skillEmergentLevel _skillEmergentLevel;    
    private List<Collider> damagingweaponList;

    private UnityEngine.Events.UnityAction burststart;
    private UnityEngine.Events.UnityAction burstend;
    private customCoroutine burstCoroutine;

    private string burstEventKey;

    private int burstTriggerDamageAmount;
    private int lastframeResistent;
    private int gotdamageamont = 0;
    
    public Counter_State(string clip_name, float RotationAdjustmentTime, float rotate_speed, int burstTriggerDamageAmount,skillEmergentLevel skillEmergentLevel)
    {
        this.clip_name = clip_name;
        this.keepRotationAdjustment = false;
        this.rotate_speed = rotate_speed;
        this.RotationAdjustmentTime = RotationAdjustmentTime;
        this.burstTriggerDamageAmount = burstTriggerDamageAmount;
        this._skillEmergentLevel = skillEmergentLevel;
    }
    
    void burstCoroutineConfig(string key)
    {
        if (key == null)
        {
            burstCoroutine = null;
            return;
        }
        switch(key)
        {
            case "resistup":
                burststart = () =>
                {
                    this._SkillCancelFlag.turn_on_flag();
                    defaultPools.Instance.GenerateEffect("break_free", null,
                    this.AI_DATA_CENTER.geometryCenter.position, Quaternion.identity, this.AI_DATA_CENTER.geometryCenter);
                    this.BS_Main_Health.Resistance +=10;
                };
                burstend = () =>
                {
                    this.BS_Main_Health.Resistance -=10;
                };
                burstCoroutine = new customCoroutine(burststart, 1f, burstend);
                break;
            case "magic_release":
                burststart = () =>
                {
                    this._SkillCancelFlag.turn_on_flag();
                    this._BO_Ani_E.releasePreparedMagicToAir(null);
                    this.BS_Main_Health.Resistance +=10;
                };
                burstend = () =>
                {
                    this.BS_Main_Health.Resistance -=10;
                };
                burstCoroutine = new customCoroutine(burststart, 0.2f, burstend);
            break;
            default:
                break;                
        }
    }

    public override void pre_process_before_enter()
	{
		base.pre_process_before_enter ();
        burststart = () =>
        {
            this._SkillCancelFlag.turn_on_flag();
            defaultPools.Instance.GenerateEffect("break_free", null,
            this.AI_DATA_CENTER.geometryCenter.position, Quaternion.identity, this.AI_DATA_CENTER.geometryCenter);
            this.BS_Main_Health.Resistance +=10;
        };
        burstend = () =>
        {
            this.BS_Main_Health.Resistance -=10;
        };
        burstCoroutine = new customCoroutine(burststart, 1f, burstend);
        this.nextAttackStateCanRushFirst = true;
	}

    public override bool enter_condition_priority1()
    {
        if (Sensor.getNearbyDamagingWeaponColliders().Count > 0)
            return this.checkToEnemyDisEnterCondition(this.behaviorEnterRanges);
        return false;
    }

    public override bool enter_condition_priority2()
    {
        if (Sensor.getInnerEnemiesColliders().Count > 0)
            return this.checkToEnemyDisEnterCondition(this.behaviorEnterRanges);
        return false;
    }

    public override bool enter_condition_priority3()
    {
        if (Sensor.getInnerEnemiesColliders().Count > 0)
            return this.checkToEnemyDisEnterCondition(this.behaviorEnterRanges);
        return false;
    }

    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        this._SkillCancelFlag.turn_off_flag();     
        this._Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.turnRotationAdjustmentStartFlagWithoutstepfoward(1);
        lastFrameRotateAngle = 0;
        thisFrameRotateAngle = 0;
        AI_DATA_CENTER.deActiveObjects();
        Animation_Manger.animationCustomCoroutineTrigger(animator_layer_index.Full_Body, clip_name);
        
        gotdamageamont = 0;
        lastframeResistent = this.BS_Main_Health.Resistance;
        
        Collider C = Sensor.getClosestColliderInSensorRange(true,false,false);
        if (C != null)
            rotateTarget = C.transform.position;
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
        this.BS_Main_Health.setNextCounterEventName(null);
	}
    
	public override void _f_State_Update() 
	{
        this._Rigidbody.velocity = Vector3.zero;
        if (this.BS_Main_Health.getNextCounterEventName() != null)//这个代表吸收伤害区间已经开始了
        {
            if (lastframeResistent > this.BS_Main_Health.Resistance)
                gotdamageamont++;
            if (gotdamageamont >= this.BS_Main_Health.getNextCounterEventDamageTriggerAmount() && this.BS_Main_Health.Resistance != 0)
            {
                this.burstCoroutineConfig(this.BS_Main_Health.getNextCounterEventName());
                this._AIStateRunner.runSubCoroutineOfState(burstCoroutine);
                gotdamageamont = -100;//也就是说不再让角色有可能在本状态内再次爆发
            }
        }
        lastframeResistent = this.BS_Main_Health.Resistance;
        singleDirectionRotateProcess(rotateTarget);
	}
    
    private Vector3 rotateTarget = Vector3.zero;
    private float lastFrameRotateAngle = 0;
    private float thisFrameRotateAngle = 0;
    private float ji = 0f;    
    void singleDirectionRotateProcess(Vector3 P)
    {
        //底下这个是说，攻击状态里角色在一个1f周期里有0.3f时长会调整方向，但是在这0.3f时间段里，如果产生了旋转不定向(比如已经转到目标)，那么转向就会提前结束。
        if (_SkillCancelFlag.getRotationAdjustmentStartFlag() || keepRotationAdjustment)
        {
            thisFrameRotateAngle = this.RotateToTarget(P, 0.5f, true);
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
