using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class Knock_Off_State : AI_State
{
    private float knock_off_time;
    private float time_counter;
    private float Upforce;
    private float horizentalForce;
    bool if_r_rotation;

    EZObjectPool superHitPool;

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

    public override bool force_enter_condition()
    {
		if (BS_Main_Health.returnDamageList(damageType.knockOff_damage).Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector3 used_velcoity;
    GameObject processingBlood;
    Vector3 force_direction;
    string KnockOffSparkPersonalEffectPath;    
    List<AnimationClip> knockoffAnimations;    
    public override void AI_State_enter()
    {
        base.AI_State_enter();
        this.time_counter = 0;
        this.BS_Main_Health.SetGettingDamageState(true);
        this._Animator.SetFloat("speed", 0f);
        this._Weapon_Animation_Events.clearMarkerManagers();
        this.BS_Main_Health.enableAllHitBoxCollider(false);
        this.AI_DATA_CENTER.deActiveObjects();
        landedCal = 1;

		if (BS_Main_Health.returnDamageList(damageType.supper_damage).Count > 0)
        {
			BS_Main_Health.returnDamageList(damageType.supper_damage).Clear();
        }
		if (BS_Main_Health.returnDamageList(damageType.heavy_damage).Count > 0)
        {
			BS_Main_Health.returnDamageList(damageType.heavy_damage).Clear();
        }
		if (BS_Main_Health.returnDamageList(damageType.light_damage).Count > 0)
        {
			BS_Main_Health.returnDamageList(damageType.light_damage).Clear();
        }

        this.BS_Main_Health.plusCriticalGauge(5);

        _Rigidbody.velocity = Vector3.zero;
        //进入击飞状态后这个动画的播放应该是没有前提的。这一下和的机理比较绕，可以看一下BO_health那边eatdamage怎么写的。
        
        defaultPools.SeriesAnimationClipsDic.TryGetValue(this._AIStateRunner.characterType + "/basic_knockoffs", out knockoffAnimations);
        
        int ranDom = (int)Random.Range(0,knockoffAnimations.Count);
        Animation_Manger.animationCustomCoroutineTrigger(animator_layer_index.Full_Body, knockoffAnimations[ranDom]);
        
        if (BS_Main_Health.returnDamageList(damageType.knockOff_damage) != null)
        {
			if (BS_Main_Health.returnDamageList(damageType.knockOff_damage).Count > 0)
            {
                //if (BS_Main_Health.returnDamageList(damageType.supper_damage)[0].ifExplosion)
				force_direction = BS_Main_Health.returnDamageList(damageType.knockOff_damage)[0].force_direction;
                if (BS_Main_Health.returnDamageList(damageType.knockOff_damage)[0].fromWeapon != null)
                    KnockOffSparkPersonalEffectPath = BS_Main_Health.returnDamageList(damageType.knockOff_damage)[0].fromWeapon.personalEffectPath;
                else
                    KnockOffSparkPersonalEffectPath = null;

                superHitPool = defaultPools.Instance.iniEffectsPool("super_hit",KnockOffSparkPersonalEffectPath, 2);
                if (superHitPool != null)
                {
                    processingBlood = superHitPool.TryGetNextObject(BS_Main_Health.returnDamageList(damageType.knockOff_damage)[0].damageHappenPoint, Quaternion.identity);
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
                BS_Main_Health.eatDamage(damageType.knockOff_damage);
            }
            BS_Main_Health.returnDamageList(damageType.knockOff_damage).Clear();
        }
		//if (BS_Main_Health.returnEventDamageList() != null)
   //     {
			//if (BS_Main_Health.returnEventDamageList().Count > 0)
    //        {
				//if (BS_Main_Health.returnEventDamageList()[0].Position_set.Child == null)
    //            {
				//	BS_Main_Health.returnEventDamageList()[0].Position_set.Child = this.gameObject;
    //            }
				//if (BS_Main_Health.returnEventDamageList()[0].Position_set.Parent == null)
    //            {
				//	BS_Main_Health.returnEventDamageList()[0].Position_set.Parent = this.gameObject;
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
    }

    public override bool capacity_exit_condition()
    {
        if (this.time_counter > this.knock_off_time + 1f)
            return true;
        else
            return false;
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        this.BS_Main_Health.SetGettingDamageState(false);
        this.BS_Main_Health.enableAllHitBoxCollider(true);
        landedCal = 1;
        this._Rigidbody.velocity = Vector3.zero;
    }

	void clearOtherDamage()
	{
		if (BS_Main_Health.returnDamageList(damageType.supper_damage) != null)
        {
            if (BS_Main_Health.returnDamageList(damageType.supper_damage).Count > 0)
            {
                //force_direction = BS_Main_Health.returnDamageList(damageType.supper_damage)[0].testHurtGetFixPos - gameObject.transform.position;
                force_direction = BS_Main_Health.returnDamageList(damageType.supper_damage)[0].force_direction;
                force_direction.y = 20f;

                this.BS_Main_Health.plusCriticalGauge(5);

                BS_Main_Health.ApplyDamage(new v_Damage(0, damageType.knockOff_damage, force_direction,
                                        BS_Main_Health.returnDamageList(damageType.supper_damage)[0].damageHappenPoint, BS_Main_Health,
                                                        BS_Main_Health.returnDamageList(damageType.supper_damage)[0].fromWeapon
                                                       ));

                BS_Main_Health.eatDamage(damageType.supper_damage);
            }
        }

        if (BS_Main_Health.returnDamageList(damageType.heavy_damage) != null)
        {
            if (BS_Main_Health.returnDamageList(damageType.heavy_damage).Count > 0)
            {
                //force_direction = BS_Main_Health.returnDamageList(damageType.heavy_damage)[0].testHurtGetFixPos - gameObject.transform.position;
                force_direction = BS_Main_Health.returnDamageList(damageType.heavy_damage)[0].force_direction;
                force_direction.y = 10f;

                this.BS_Main_Health.plusCriticalGauge(5);

                BS_Main_Health.ApplyDamage(new v_Damage(0, damageType.knockOff_damage, force_direction,
                        BS_Main_Health.returnDamageList(damageType.heavy_damage)[0].damageHappenPoint, BS_Main_Health,
                                                        BS_Main_Health.returnDamageList(damageType.heavy_damage)[0].fromWeapon
                                                       ));

                BS_Main_Health.eatDamage(damageType.heavy_damage);
            }
        }

        if (BS_Main_Health.returnDamageList(damageType.light_damage) != null)
        {
            if (BS_Main_Health.returnDamageList(damageType.light_damage).Count > 0)
            {
                //force_direction = BS_Main_Health.returnDamageList(damageType.light_damage)[0].testHurtGetFixPos - gameObject.transform.position;
                force_direction = BS_Main_Health.returnDamageList(damageType.light_damage)[0].force_direction;
                force_direction.y = 5f;

                this.BS_Main_Health.plusCriticalGauge(5);

                BS_Main_Health.ApplyDamage(new v_Damage(0, damageType.knockOff_damage, force_direction,
                                                        BS_Main_Health.returnDamageList(damageType.light_damage)[0].damageHappenPoint, BS_Main_Health,
                                                        BS_Main_Health.returnDamageList(damageType.light_damage)[0].fromWeapon
                                                       ));

                BS_Main_Health.eatDamage(damageType.light_damage);
            }
        }
	}

    public override void _f_State_Update()
    {
		clearOtherDamage();

        time_counter += Time.deltaTime;
        if (time_counter > 0.6f)
        {
            _SkillCancelFlag.turn_on_flag();
        }

        if (!_Rigidbody.useGravity)
        {
			_Rigidbody.useGravity = true;
        }

        if (landedCal == 1)
        {
            if (!AI_DATA_CENTER.IsGrounded())
            {
                landedCal = 2;
            }else{
                _Rigidbody.velocity = used_velcoity;//一定让它飞起来
            }
        }
        if (landedCal == 2)
        {
            if (AI_DATA_CENTER.IsGrounded())
            {
                landedCal = 3;
            }
        }

        if (landedCal == 3)
        {
            this._Rigidbody.velocity = Vector3.zero;
        }

        if (!AI_DATA_CENTER.IsGrounded())
            this.RotateToVelocityNegative(3f, true);
    }

    int landedCal;//1 还在地上 2 已经被打飞起来 3 落地

    public override void _State_FixedUpdate()
    {
        
    }
}

// Knock_off_state should not be super_canceled to revive state,here is the reason:
// While in knock off state, the character may also be transited into knock off state again,
// during the transition of knock off animation to self,the turn on flag you put there is triggerd,
// The AIStateRunner will recognise it as a permision to enter the revive state,then what you will see
// is that revive state is somehow triggerd immmediately. 1107 -- haku
