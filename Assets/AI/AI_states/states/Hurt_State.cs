using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Hurt_State : AI_State {
    private readonly float light_damage_force;
    private readonly float heavy_damage_force;
    private readonly float light_dizzy_time;
    private readonly float heavy_dizzy_time;
    private float used_dizzy_time;
    private Vector3 force_direction;
    private float time_counter;
	private readonly Coroutine shaderChangeProcess;
    private List<AnimationClip> hurtclips;

    public Hurt_State(float light_damage_force, float heavy_damage_force, float light_dizzy_time,float heavy_dizzy_time)
	{
        this.light_damage_force = light_damage_force;
        this.heavy_damage_force = heavy_damage_force;
        this.light_dizzy_time = light_dizzy_time;
        this.heavy_dizzy_time = heavy_dizzy_time;
    }

	public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
    }

	public override bool Force_enter_condition()
	{
        return _FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage).Count > 0
            || _FightAttriCalReference.ReturnDamageList(DamageType.light_damage).Count > 0
            || _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage).Count > 0
            || _FightAttriCalReference.ReturnEventDamageList().Count > 0
            ? true
            : false;
    }

	public override void _State_FixedUpdate1()
	{
        this.time_counter += Time.fixedDeltaTime;
        if (this.time_counter > 0.05f)
        {
            _Rigidbody.velocity = Vector3.zero;
        }else{
            //this.gameObject.transform.Translate((this.gameObject.transform.position + force_direction)* Time.fixedDeltaTime);
        }
    }

    public override void AI_State_enter()
	{
		base.AI_State_enter();
        //_SkillCancelFlag.turn_on_flag();//可以挣脱
        this._Animator.SetFloat("speed", 0f);
        this._DATA_CENTER.SetUsingGravity(true);//在之后的eatDamage环节可能被再次解放重力
        this._FightAttriCalReference.SetGettingDamageState(true);
        this._Weapon_Animation_Events.ClearMarkerManagers();
        this._BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts();
        this.hurtclips = AnimationResourceLoader.SeriesAnimationClipsDic[_AIStateRunner.characterType + "/basic_hurts"];

        int ranDom = (int)Random.Range(0,hurtclips.Count);
		if (_FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage).Count > 0)
		{
            used_dizzy_time = heavy_dizzy_time;
            force_direction = _FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage)[0].force_direction;
            force_direction.y = 0;
            this._Rigidbody.velocity = force_direction.normalized * heavy_damage_force;
            Animation_Manger.animationTrigger(hurtclips[ranDom]);
            _FightAttriCalReference.GetKnockOffCount().plusGauge(3f);
			_FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f);            
            _FightAttriCalReference.EatDamage(DamageType.heavy_damage);
            _FightAttriCalReference.plusCriticalGauge(1);
		}

		if (_FightAttriCalReference.ReturnDamageList(DamageType.light_damage).Count > 0) 
        {
            used_dizzy_time = light_dizzy_time;
            //force_direction = BS_Main_Health.returnDamageList(damageType.light_damage)[0].testHurtGetFixPos - gameObject.transform.position;
            force_direction = _FightAttriCalReference.ReturnDamageList(DamageType.light_damage)[0].force_direction;
            force_direction.y = 0;
            _Rigidbody.velocity = force_direction.normalized * light_damage_force;
            Animation_Manger.animationTrigger(hurtclips[ranDom]);
            _FightAttriCalReference.plusCriticalGauge(1);
			_FightAttriCalReference.GetKnockOffCount().plusGauge(1f);
            _FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f);
            _FightAttriCalReference.EatDamage(DamageType.light_damage);    
			//为了让轻攻击带来的连击能保证持续，不在轻攻击处进行击飞积累
        }

		if (_FightAttriCalReference.ReturnDamageList(DamageType.supper_damage).Count > 0)
        {
            used_dizzy_time = heavy_dizzy_time;
            //force_direction = BS_Main_Health.returnDamageList(damageType.supper_damage)[0].testHurtGetFixPos - gameObject.transform.position;
            force_direction = _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage)[0].force_direction;
            force_direction.y = 0;
            _Rigidbody.velocity = force_direction.normalized * heavy_damage_force;
            Animation_Manger.animationTrigger(hurtclips[ranDom]);
            _FightAttriCalReference.plusCriticalGauge(1);
			_FightAttriCalReference.GetKnockOffCount().plusGauge(4f);
            _FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f);
            if (_FightAttriCalReference.GetKnockOffCount().getGauge() >= 5f)
            {
                _FightAttriCalReference.ApplyDamage(new V_Damage(DamageType.knockOff_damage, force_direction,
                                                        _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage)[0].damageHappenPoint, 
                                                        _FightAttriCalReference,
                                                        _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage)[0].fromWeapon));
                _FightAttriCalReference.GetKnockOffCount().setGauge(0f);
            }
            _FightAttriCalReference.EatDamage(DamageType.supper_damage);
        }
        this.RotateToDirection(-force_direction,0.5f,true);
        this.time_counter = 0f;
        this.personality_Events.CloseAllPersonalityEffects();
        this.Animation_Manger.Animator.SetTrigger("face_reset");
        this.Animation_Manger.Animator.SetTrigger("hurt");
    }

	public override bool Capacity_exit_condition()
	{
        return this.time_counter > used_dizzy_time ? true : false;
    }

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        this._FightAttriCalReference.SetGettingDamageState(false);
        //_SkillCancelFlag.turn_off_flag();//可以挣脱
        this.time_counter = 0f;
        this._Rigidbody.velocity = Vector3.zero;
        if (this._AIStateRunner.getTryState().StateType == stateType.AC || this._AIStateRunner.getTryState().StateType == stateType.GI ||
            this._AIStateRunner.getTryState().StateType == stateType.GM || this._AIStateRunner.getTryState().StateType == stateType.GR)
        {
            this._FightAttriCalReference.BeHitCountInterrupt();
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
