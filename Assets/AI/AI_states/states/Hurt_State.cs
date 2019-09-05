using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Hurt_State : AI_State {

    private float light_damage_force, heavy_damage_force;
    private float light_dizzy_time,heavy_dizzy_time,used_dizzy_time;
    private Vector3 force_direction;
    private float time_counter;
	private Coroutine shaderChangeProcess;
    private List<AnimationClip> hurtclips;

    public Hurt_State(float light_damage_force, float heavy_damage_force, float light_dizzy_time,float heavy_dizzy_time)
	{
        this.light_damage_force = light_damage_force;
        this.heavy_damage_force = heavy_damage_force;
        this.light_dizzy_time = light_dizzy_time;
        this.heavy_dizzy_time = heavy_dizzy_time;
    }

	public override void pre_process_before_enter()
	{
		base.pre_process_before_enter ();
		BS_Main_Health.enabled = true;
    }

	public override bool force_enter_condition()
	{
		if (BS_Main_Health.returnDamageList(damageType.heavy_damage).Count > 0 
			|| BS_Main_Health.returnDamageList(damageType.light_damage).Count > 0
		    || BS_Main_Health.returnDamageList(damageType.supper_damage).Count > 0
			|| BS_Main_Health.returnEventDamageList().Count > 0)
        {
			return true;
		} else {
			return false;
		}
	}

	public override void _State_FixedUpdate1()
	{
        this.time_counter += Time.fixedDeltaTime;
        if (this.time_counter > 0.05f)
        {
            _Rigidbody.velocity = Vector3.zero;
        }else
        {
            //this.gameObject.transform.Translate((this.gameObject.transform.position + force_direction)* Time.fixedDeltaTime);
        }
    }

    public override void AI_State_enter()
	{
		base.AI_State_enter();
        //_SkillCancelFlag.turn_on_flag();//可以挣脱
        this._Animator.SetFloat("speed", 0f);
        this._DATA_CENTER.setGravitySwitch(true);//在之后的eatDamage环节可能被再次解放重力
        this.BS_Main_Health.SetGettingDamageState(true);
        _Weapon_Animation_Events.clearMarkerManagers();
        this._BO_Ani_E.closeEffectsOnBodyParts();
        hurtclips = AnimationResourceLoader.SeriesAnimationClipsDic[_AIStateRunner.characterType + "/basic_hurts"];

        int ranDom = (int)Random.Range(0,hurtclips.Count);

		if (BS_Main_Health.returnDamageList(damageType.heavy_damage).Count > 0)
		{
            used_dizzy_time = heavy_dizzy_time;
            force_direction = BS_Main_Health.returnDamageList(damageType.heavy_damage)[0].force_direction;
            force_direction.y = 0;
            this._Rigidbody.velocity = force_direction.normalized * heavy_damage_force;
            Animation_Manger.animationTrigger(hurtclips[ranDom]);
            BS_Main_Health.GetKnockOffCount().plusGauge(3f);
			BS_Main_Health.GetKnockOffCount().plusTimeCounter(0.2f);            
            BS_Main_Health.eatDamage(damageType.heavy_damage);
            this.BS_Main_Health.plusCriticalGauge(1);
		}

		if (BS_Main_Health.returnDamageList(damageType.light_damage).Count > 0) 
        {
            used_dizzy_time = light_dizzy_time;
            //force_direction = BS_Main_Health.returnDamageList(damageType.light_damage)[0].testHurtGetFixPos - gameObject.transform.position;
            force_direction = BS_Main_Health.returnDamageList(damageType.light_damage)[0].force_direction;
            force_direction.y = 0;
            this._Rigidbody.velocity = force_direction.normalized * light_damage_force;
            Animation_Manger.animationTrigger(hurtclips[ranDom]);
            this.BS_Main_Health.plusCriticalGauge(1);
			BS_Main_Health.GetKnockOffCount().plusGauge(1f);
            BS_Main_Health.GetKnockOffCount().plusTimeCounter(0.2f);
            BS_Main_Health.eatDamage(damageType.light_damage);    
			//为了让轻攻击带来的连击能保证持续，不在轻攻击处进行击飞积累
        }

		if (BS_Main_Health.returnDamageList(damageType.supper_damage).Count > 0)
        {
            used_dizzy_time = heavy_dizzy_time;
            //force_direction = BS_Main_Health.returnDamageList(damageType.supper_damage)[0].testHurtGetFixPos - gameObject.transform.position;
            force_direction = BS_Main_Health.returnDamageList(damageType.supper_damage)[0].force_direction;
            force_direction.y = 0;
            this._Rigidbody.velocity = force_direction.normalized * heavy_damage_force;
            Animation_Manger.animationTrigger(hurtclips[ranDom]);
            this.BS_Main_Health.plusCriticalGauge(1);
			BS_Main_Health.GetKnockOffCount().plusGauge(4f);
            BS_Main_Health.GetKnockOffCount().plusTimeCounter(0.2f);
            if (BS_Main_Health.GetKnockOffCount().getGauge() >= 10f)
            {
                BS_Main_Health.ApplyDamage(new v_Damage(damageType.knockOff_damage, force_direction,
                                                        BS_Main_Health.returnDamageList(damageType.supper_damage)[0].damageHappenPoint, 
                                                        BS_Main_Health,
                                                        BS_Main_Health.returnDamageList(damageType.supper_damage)[0].fromWeapon));
                BS_Main_Health.GetKnockOffCount().setGauge(0f);
            }
            BS_Main_Health.eatDamage(damageType.supper_damage);
        }
        this.RotateToDirection(-force_direction,0.5f,true);
        this.time_counter = 0f;
        _DATA_CENTER.deActiveObjects();
    }

	public override bool capacity_exit_condition()
	{
        if (this.time_counter > used_dizzy_time)
        {
			return true;
        }else{
            return false;
        }
	}

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        this.BS_Main_Health.SetGettingDamageState(false);
        //_SkillCancelFlag.turn_off_flag();//可以挣脱
        this.time_counter = 0f;
        this._Rigidbody.velocity = Vector3.zero;
        if (this._AIStateRunner.getTryState().StateType == stateType.AC || this._AIStateRunner.getTryState().StateType == stateType.GI ||
            this._AIStateRunner.getTryState().StateType == stateType.GM || this._AIStateRunner.getTryState().StateType == stateType.GR)
        {
            this.BS_Main_Health.BeHitCountInterrupt();
        }
    }
}

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
//BO_Health attackerHealth = BS_Main_Health.returnEventDamageList()[0].getAttackerHealthBody();
//            if (attackerHealth != null)
//            {
//  attackerHealth.eventAttackHitApprove(BS_Main_Health.returnEventDamageList()[0]);
//            }

//BS_Main_Health.returnDamageList(damageType.heavy_damage).Clear();
//BS_Main_Health.returnDamageList(damageType.light_damage).Clear();
//BS_Main_Health.returnDamageList(damageType.supper_damage).Clear();
//BS_Main_Health.returnDamageList(damageType.knockOff_damage).Clear();
//            this.time_counter = 0f;
//BS_Main_Health.returnEventDamageList().Clear();
//    }
//}
