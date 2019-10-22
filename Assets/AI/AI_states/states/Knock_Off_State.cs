using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Knock_Off_State : AI_State
{
    readonly float knock_off_time;
    float time_counter;
    readonly float Upforce;
    readonly float horizentalForce;
    readonly bool if_r_rotation;
    DecompositionerPool superHitPool;

    public Knock_Off_State(float knock_off_time, float Upforce,float horizentalForce)
    {
        this.knock_off_time = knock_off_time;
        this.Upforce = Upforce;
        this.horizentalForce = horizentalForce;
        if_r_rotation = false;
        StateType = stateType.KnockOff;
    }

    public Knock_Off_State(float knock_off_time, float Upforce, float horizentalForce,bool if_r_rotation)
    {
        this.knock_off_time = knock_off_time;
        this.Upforce = Upforce;
        this.horizentalForce = horizentalForce;
        this.if_r_rotation = if_r_rotation;
        StateType = stateType.KnockOff;
    }

    public override void pre_process_before_enter()
    {
		base.pre_process_before_enter ();
    }

    public override bool enter_condition_priority2()
    {
        return false;
    }

    public override bool force_enter_condition() => _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage).Count > 0;

    Vector3 used_velcoity;
    Decompositioner processingBlood;
    Vector3 force_direction;
    string KnockOffSparkPersonalEffectPath;    
    List<AnimationClip> knockoffAnimations;    
    public override void AI_State_enter()
    {
        base.AI_State_enter();
        this._DATA_CENTER.setGravitySwitch(false);
        this.time_counter = 0;
        this._FightAttriCalReference.SetGettingDamageState(true);
        this._Animator.SetFloat("speed", 0f);
        this._Weapon_Animation_Events.clearMarkerManagers();
        this._FightAttriCalReference.EnableAllHitBoxCollider(false);
        this.personality_Events.CloseAllPersonalityEffects();

		if (_FightAttriCalReference.ReturnDamageList(DamageType.supper_damage).Count > 0)
        {
			_FightAttriCalReference.ReturnDamageList(DamageType.supper_damage).Clear();
        }
		if (_FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage).Count > 0)
        {
			_FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage).Clear();
        }
		if (_FightAttriCalReference.ReturnDamageList(DamageType.light_damage).Count > 0)
        {
			_FightAttriCalReference.ReturnDamageList(DamageType.light_damage).Clear();
        }

        this._FightAttriCalReference.plusCriticalGauge(2);

        _Rigidbody.velocity = Vector3.zero;
        //进入击飞状态后这个动画的播放应该是没有前提的。这一下和的机理比较绕，可以看一下BO_health那边eatdamage怎么写的。
        
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(this._AIStateRunner.characterType + "/basic_knockoffs", out knockoffAnimations);
        
        int ranDom = (int)Random.Range(0,knockoffAnimations.Count);
                
        Animation_Manger.animationTrigger(knockoffAnimations[ranDom]);
        
        if (_FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage) != null)
        {
			if (_FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage).Count > 0)
            {
                //if (BS_Main_Health.returnDamageList(damageType.supper_damage)[0].ifExplosion)
				force_direction = _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0].force_direction;
                KnockOffSparkPersonalEffectPath = _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0].fromWeapon != null
                    ? _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0].fromWeapon.personalEffectPath
                    : null;

                superHitPool = EffectAndHurtObjectLoading.Instance.IniEffectsPool("super_hit",KnockOffSparkPersonalEffectPath, 3);
                if (superHitPool != null)
                {
                    processingBlood = superHitPool.Rent();
                    processingBlood.transform.position = _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0].damageHappenPoint;
                    processingBlood.transform.rotation = Quaternion.identity;
                }
                
                //else
                //{
                //    force_direction = BS_Main_Health.returnDamageList(damageType.supper_damage)[0].testHurtGetFixPos - gameObject.transform.position;
                //}

                force_direction.y = 0;
                if (if_r_rotation)
                {
                    this.RotateToDirection(force_direction, 20f, true);
                }

                used_velcoity = force_direction.normalized * horizentalForce + Vector3.up * Upforce;
			    _Rigidbody.velocity = used_velcoity;
                _FightAttriCalReference.EatDamage(DamageType.knockOff_damage);
            }
            _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage).Clear();
        }
    }

    public override bool capacity_exit_condition()
    {
        return this.time_counter > this.knock_off_time + 1f;
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _FightAttriCalReference.SetGettingDamageState(false);
        _FightAttriCalReference.EnableAllHitBoxCollider(true);
        _Rigidbody.velocity = Vector3.zero;
    }

    private void ClearOtherDamage()
	{
        if (_FightAttriCalReference.ReturnDamageList(DamageType.supper_damage) != null && _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage).Count > 0)
        {
            //force_direction = BS_Main_Health.returnDamageList(damageType.supper_damage)[0].testHurtGetFixPos - gameObject.transform.position;
            force_direction = _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage)[0].force_direction;
            force_direction.y = 20f;

            this._FightAttriCalReference.plusCriticalGauge(2);

            _FightAttriCalReference.ApplyDamage(new V_Damage(DamageType.knockOff_damage, force_direction,
                                    _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage)[0].damageHappenPoint, _FightAttriCalReference,
                                                    _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage)[0].fromWeapon));

            _FightAttriCalReference.EatDamage(DamageType.supper_damage);
        }

        if (_FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage) != null && _FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage).Count > 0)
        {
            //force_direction = BS_Main_Health.returnDamageList(damageType.heavy_damage)[0].testHurtGetFixPos - gameObject.transform.position;
            force_direction = _FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage)[0].force_direction;
            force_direction.y = 10f;

            this._FightAttriCalReference.plusCriticalGauge(2);

            _FightAttriCalReference.ApplyDamage(new V_Damage(DamageType.knockOff_damage, force_direction,
                    _FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage)[0].damageHappenPoint, _FightAttriCalReference,
                                                    _FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage)[0].fromWeapon));
            _FightAttriCalReference.EatDamage(DamageType.heavy_damage);
        }
        
        if (_FightAttriCalReference.ReturnDamageList(DamageType.light_damage) != null)
        {
            if (_FightAttriCalReference.ReturnDamageList(DamageType.light_damage).Count > 0)
            {
                //force_direction = BS_Main_Health.returnDamageList(damageType.light_damage)[0].testHurtGetFixPos - gameObject.transform.position;
                force_direction = _FightAttriCalReference.ReturnDamageList(DamageType.light_damage)[0].force_direction;
                force_direction.y = 5f;

                this._FightAttriCalReference.plusCriticalGauge(2);

                _FightAttriCalReference.ApplyDamage(new V_Damage(DamageType.knockOff_damage, force_direction,
                                                        _FightAttriCalReference.ReturnDamageList(DamageType.light_damage)[0].damageHappenPoint, _FightAttriCalReference,
                                                        _FightAttriCalReference.ReturnDamageList(DamageType.light_damage)[0].fromWeapon));

                _FightAttriCalReference.EatDamage(DamageType.light_damage);
            }
        }
	}

    public override void _State_FixedUpdate1()
    {
		//clearOtherDamage();
        time_counter += Time.fixedDeltaTime;
        if (time_counter > 0.6f)
        {
            _SkillCancelFlag.turn_on_flag();
        }

        if (!this._DATA_CENTER.getGravitySwitch())
        {
            _Rigidbody.velocity = used_velcoity;//一定让它飞起来
            if (time_counter > 0.1)
            {
                this._DATA_CENTER.setGravitySwitch(true);
            }
        }else{
            this._Rigidbody.velocity = Vector3.zero;
        }

        if (!_DATA_CENTER.IsGrounded())
            this.RotateToVelocityNegative(3f, true);
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
