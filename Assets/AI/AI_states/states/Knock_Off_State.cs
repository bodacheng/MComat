using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Knock_Off_State : AI_State
{
    readonly float knock_off_time;
    float time_counter;
    readonly bool if_r_rotation;
    Vector3 startPoint;
    Quaternion startquaternion;
    private Matrix4x4 m;
    DecompositionerPool superHitPool;
    bool Dropped;
    
    public Knock_Off_State(float knock_off_time)
    {
        this.knock_off_time = knock_off_time;
        if_r_rotation = false;
        StateType = stateType.KnockOff;
    }

    public Knock_Off_State(float knock_off_time,bool if_r_rotation)
    {
        this.knock_off_time = knock_off_time;
        this.if_r_rotation = if_r_rotation;
        StateType = stateType.KnockOff;
    }

    public override void Pre_process_before_enter()
    {
		base.Pre_process_before_enter ();
    }

    public override bool Enter_condition_priority2()
    {
        return false;
    }

    public override bool Force_enter_condition() => _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage).Count > 0;

    Decompositioner processingBlood;
    string KnockOffSparkPersonalEffectPath;    
    List<AnimationClip> knockoffAnimations;    
    public override void AI_State_enter()
    {
        base.AI_State_enter();
        Dropped = false;
        time_counter = 0;
        _DATA_CENTER.SetUsingGravity(false);
        _FightAttriCalReference.SetGettingDamageState(true);
        _Animator.SetFloat("speed", 0f);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _FightAttriCalReference.EnableAllHitBoxCollider(false);
        personality_Events.CloseAllPersonalityEffects();
        _FightAttriCalReference.ClearDamageLists();
        _FightAttriCalReference.plusCriticalGauge(2);
        _Rigidbody.velocity = Vector3.zero;
        //进入击飞状态后这个动画的播放应该是没有前提的。这一下和的机理比较绕，可以看一下BO_health那边eatdamage怎么写的。
        
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(this._AIStateRunner.characterType + "/basic_knockoffs", out knockoffAnimations);        
        int ranDom = (int)Random.Range(0,knockoffAnimations.Count);
        Animation_Manger.animationTrigger(knockoffAnimations[ranDom]);
        
		if (_FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage).Count > 0)
        {
            KnockOffSparkPersonalEffectPath = _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0].fromWeapon?.personalEffectPath;
            superHitPool = EffectAndHurtObjectLoading.Instance.IniEffectsPool("super_hit",KnockOffSparkPersonalEffectPath, 3);
            if (superHitPool != null)
            {
                processingBlood = superHitPool.Rent();
                processingBlood.transform.position = _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0].damageHappenPoint;
                processingBlood.transform.rotation = Quaternion.identity;
            }
            startPoint = gameObject.transform.position;
            startquaternion = Quaternion.Euler(_FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0].force_direction);
            m = Matrix4x4.TRS(startPoint, startquaternion, Vector3.one);
            if (if_r_rotation)
            {
                this.RotateToDirection(_FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0].force_direction, 20f, true);
            }
            _FightAttriCalReference.EatDamage(DamageType.knockOff_damage);
        }
        _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage).Clear();
    }

    public override bool Capacity_exit_condition()
    {
        return (this.time_counter > this.knock_off_time + 1f );
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _DATA_CENTER.SetUsingGravity(true);
        _FightAttriCalReference.SetGettingDamageState(false);
        _FightAttriCalReference.EnableAllHitBoxCollider(true);
        _Rigidbody.velocity = Vector3.zero;
    }

    public override void _State_FixedUpdate1()
    {
        time_counter += Time.fixedDeltaTime;
        //if (time_counter > 0.6f)
        //{
        //    _SkillCancelFlag.turn_on_flag();
        //}
        if (!Dropped)
        {
            RotateToVelocityNegative(3f, true);
            Debug.Log("y"+ FightGlobalSetting._knockOffyAnimationCurve.Evaluate(time_counter) * 1f);
            gameObject.transform.position = m.MultiplyPoint3x4(new Vector3(0, FightGlobalSetting._knockOffyAnimationCurve.Evaluate(time_counter) * 0.01f, 
                                                                                FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_counter) * 0.01f));
            //if (_DATA_CENTER.IsGrounded())
                //Dropped = true;
        }
        else{
            _Rigidbody.velocity = Vector3.zero;
        }
    }
}

// Knock_off_state should not be super_canceled to revive state,here is the reason:
// While in knock off state, the character may also be transited into knock off state again,
// during the transition of knock off animation to self,the turn on flag you put there is triggerd,
// The AIStateRunner will recognise it as a permision to enter the revive state,then what you will see
// is that revive state is somehow triggerd immmediately. 1107 -- haku

    //if (BS_Main_Health.returnEventDamageList() != null)
   //     {
            //if (BS_Main_Health.returnEventDamageList().Count > 0)
    //        {
                //if (BS_Main_Health.returnEventDamageList()[0].Position_set.Child == null)
    //            {
                //  BS_Main_Health.returnEventDamageList()[0].Position_set.Child = this.gameObject;
    //            }
                //if (BS_Main_Health.returnEventDamageList()[0].Position_set.Parent == null)
    //            {
                //  BS_Main_Health.returnEventDamageList()[0].Position_set.Parent = this.gameObject;
    //            }
                //BS_Main_Health.returnEventDamageList()[0].getAttackerHealthBody().eventAttackHitApprove(BS_Main_Health.returnEventDamageList()[0]);
                //BS_Main_Health.returnEventDamageList().Clear();
                //BS_Main_Health.returnDamageList(damageType.supper_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.heavy_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.light_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.knockOff_damage).Clear();
        //    }
        //}
        //if (BS_Main_Health.returnApprovedEventAttackAttempts() != null)
   //     {
            //BS_Main_Health.returnApprovedEventAttackAttempts().Clear();
        //}
