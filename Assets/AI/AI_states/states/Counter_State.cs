using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

public class Counter_State : AI_State {
	private readonly string clip_name;
    private readonly bool keepRotationAdjustment;
    private readonly float RotationAdjustmentTime;
    private readonly float rotate_speed;
    private readonly int _skillEmergentLevel;    
    private readonly List<Collider> damagingweaponList;

    private UnityEngine.Events.UnityAction burststart;
    private UnityEngine.Events.UnityAction burstend;
    private customCoroutine burstCoroutine;

    private readonly string burstEventKey;
    private readonly int burstTriggerDamageAmount;
    private int lastframeResistent;
    private int gotdamageamont;
    
    public Counter_State(string clip_name, float RotationAdjustmentTime, float rotate_speed, int burstTriggerDamageAmount,int skillEmergentLevel)
    {
        this.clip_name = clip_name;
        this.keepRotationAdjustment = false;
        this.rotate_speed = rotate_speed;
        this.RotationAdjustmentTime = RotationAdjustmentTime;
        this.burstTriggerDamageAmount = burstTriggerDamageAmount;
        this._skillEmergentLevel = skillEmergentLevel;
    }
    
    void BurstCoroutineConfig(string key)
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
                    EffectAndHurtObjectLoading.Instance.GenerateEffect("break_free", null,
                    this._DATA_CENTER.geometryCenter.position, Quaternion.identity, this._DATA_CENTER.geometryCenter);
                    this._ResistanceManager.Resistance.Value +=10;
                };
                burstend = () =>
                {
                    this._ResistanceManager.Resistance.Value -=10;
                };
                burstCoroutine = new customCoroutine(burststart, 1f, burstend);
                break;
            case "magic_release":
                burststart = () =>
                {
                    this._SkillCancelFlag.turn_on_flag();
                    this._BO_Ani_E.ReleasePreparedMagicToAir(null);
                    this._ResistanceManager.Resistance.Value +=10;
                };
                burstend = () =>
                {
                    this._ResistanceManager.Resistance.Value -=10;
                };
                burstCoroutine = new customCoroutine(burststart, 0.2f, burstend);
            break;
            default:
                break;                
        }
    }

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
        burststart = () =>
        {
            this._SkillCancelFlag.turn_on_flag();
            EffectAndHurtObjectLoading.Instance.GenerateEffect("break_free", null,
            this._DATA_CENTER.geometryCenter.position, Quaternion.identity, this._DATA_CENTER.geometryCenter);
            this._ResistanceManager.Resistance.Value +=10;
        };
        burstend = () =>
        {
            this._ResistanceManager.Resistance.Value -=10;
        };
        burstCoroutine = new customCoroutine(burststart, 1f, burstend);
        this.nextAttackStateCanRushFirst = true;
	}

    public override bool Enter_condition_priority1()
    {
        return Sensor.getNearbyDamagingWeaponColliders().Count > 0 && this.CheckToEnemyDisEnterCondition(this.behaviorEnterRanges);
    }

    public override bool Enter_condition_priority2()
    {
        return Sensor.getInnerEnemiesColliders().Count > 0 && this.CheckToEnemyDisEnterCondition(this.behaviorEnterRanges);
    }

    public override bool Enter_condition_priority3()
    {
        return Sensor.getInnerEnemiesColliders().Count > 0 && this.CheckToEnemyDisEnterCondition(this.behaviorEnterRanges);
    }

    public override void AI_State_enter()
	{
		base.AI_State_enter ();
        this._SkillCancelFlag.turn_off_flag();     
        this._Animator.SetFloat("speed", 0f);
        _SkillCancelFlag.TurnRotationAdjustmentStartFlagWithoutstepfoward(1);
        lastFrameRotateAngle = 0;
        thisFrameRotateAngle = 0;
        this.personality_Events.CloseAllPersonalityEffects();
        Animation_Manger.AnimationTrigger(clip_name);
        
        gotdamageamont = 0;
        lastframeResistent = this._ResistanceManager.Resistance.Value;
        
        Collider C = Sensor.getClosestColliderInSensorRange(true,false,false);
        if (C != null)
            rotateTarget = C.transform.position;
	}

	public override bool Capacity_exit_condition()
	{
        return Animation_Manger.GetAnimationPlayingStep() == AnimationPlaying_Step.over ? true : false;
    }

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        this._ResistanceManager.hiddenMethods.SetNextCounterEventName(null);
	}
    
	public override void _State_FixedUpdate1() 
	{
        this._Rigidbody.velocity = Vector3.zero;
        if (this._ResistanceManager.hiddenMethods.GetNextCounterEventName() != null)//这个代表吸收伤害区间已经开始了
        {
            if (lastframeResistent > this._ResistanceManager.Resistance.Value)
                gotdamageamont++;
            if (gotdamageamont >= this._ResistanceManager.hiddenMethods.GetNextCounterEventDamageTriggerAmount() && this._ResistanceManager.Resistance.Value != 0)
            {
                this.BurstCoroutineConfig(this._ResistanceManager.hiddenMethods.GetNextCounterEventName());
                this._BuffsRunner.runSubCoroutineOfState(burstCoroutine);
                gotdamageamont = -100;//也就是说不再让角色有可能在本状态内再次爆发
            }
        }
        lastframeResistent = this._ResistanceManager.Resistance.Value;
        SingleDirectionRotateProcess(rotateTarget);
	}
    
    private Vector3 rotateTarget = Vector3.zero;
    private float lastFrameRotateAngle;
    private float thisFrameRotateAngle;
    private float ji;    
    void SingleDirectionRotateProcess(Vector3 P)
    {
        //底下这个是说，攻击状态里角色在一个1f周期里有0.3f时长会调整方向，但是在这0.3f时间段里，如果产生了旋转不定向(比如已经转到目标)，那么转向就会提前结束。
        if (_SkillCancelFlag.hiddenMethods.GetRotationAdjustmentStartFlag() || keepRotationAdjustment)
        {
            thisFrameRotateAngle = RotateToTarget(P, 10f, true);
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
